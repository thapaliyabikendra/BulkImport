using CommonLibs.BulkImport.Application.Constants;
using CommonLibs.BulkImport.Application.Dtos;
using CommonLibs.BulkImport.Application.Interfaces;
using CsvHelper;
using FluentValidation;
using Ganss.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;
using Volo.Abp.Validation;

namespace CommonLibs.BulkImport.Application.Services;
public class BulkImportService<TEntity, TKey, TDto, TValidator> : ApplicationService where TEntity : class, IEntity<TKey>,  new() where TDto : IBulkImportDto where TValidator : AbstractValidator<TDto>, new()
{
    private readonly string _validationTitle = "Bulk Import Validations";

    private readonly IRepository<TEntity, TKey> _repository;
    private readonly IObjectMapper _objectMapper;

    protected BulkImportService(IRepository<TEntity, TKey> repository, IObjectMapper objectMapper)
    {
        _repository = repository;
        _objectMapper = objectMapper;
    }

    /// <summary>
    /// Handles the bulk import from an uploaded a file.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task BulkImportAsync([FromForm] [Required] BulkImportFileDto input)
    {
        try
        {
            var extension = new FileInfo(input.File.FileName).Extension;
            if (!BulkImportFileConsts.ExtensionAllowed.Any(x => x.Equals(extension, StringComparison.OrdinalIgnoreCase)))
            {
                var msg = "Invalid File Extension. Allowed extensions are: " + string.Join(", ", BulkImportFileConsts.ExtensionAllowed);
                throw new UserFriendlyException(msg, "400");
            }

            if (input.File.Length > BulkImportFileConsts.FileSizeLimit)
            {
                var msg = "File Size Exceeded. Maximum allowed size is " + (BulkImportFileConsts.FileSizeLimit / (1024 * 1024)) + " MB.";
                throw new UserFriendlyException(msg, "400");
            }

            using var stream = new MemoryStream();
            await input.File.CopyToAsync(stream);
            stream.Position = 0;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var data = await GetDataAsync(stream, extension);

            if (data.Count == 0)
            {
                var msg = "The uploaded file does not contain any data.";
                throw new UserFriendlyException(msg, "404");
            }

            await BulkImportAsync(data);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task BulkImportAsync(List<TDto> input)
    {

        try
        {
            if (input.Count == 0)
            {
                var msg = "Please enter at least one data.";
                throw new AbpValidationException(_validationTitle,
                                                [
                                                    new(msg, ["itemCount"])
                                                ]);
            }

            //var newItems = new List<TEntity>();

            var allValidationErrors = new List<string>();
            foreach (var item in input)
            {
                var validator = new TValidator();
                var validations = await validator.ValidateAsync(item);

                if (!validations.IsValid)
                {
                    allValidationErrors.AddRange(validations.Errors.Select(e => e.ErrorMessage));
                }
            }

            //if (allValidationErrors.Any())
            //{
            //    throw new AbpValidationException(_validationTitle, allValidationErrors);
            //}

            var newItems = _objectMapper.Map<List<TDto>, List<TEntity>>(input);

            if (newItems.Count != 0)
            {
                await _repository.InsertManyAsync(newItems);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private static async Task<List<TDto>> GetDataAsync(MemoryStream stream, string extension)
    {
        try
        {
            stream.Position = 0;
            if (extension.Equals(FileTypeConsts.CsvType, StringComparison.OrdinalIgnoreCase))
            {
                using var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
                using var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null,
                });
                var records = csv.GetRecords<TDto>()
                .Where(emp => !string.IsNullOrWhiteSpace(emp.DataIdentifier))
                .ToList();

                return records;

            }
            else if (extension.Equals(FileTypeConsts.XlsxType, StringComparison.OrdinalIgnoreCase))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var importer = new ExcelMapper(stream)
                {
                    SkipBlankCells = true,
                    HeaderRow = true,
                };

                var records = importer.Fetch<TDto>()
                .Where(emp => !string.IsNullOrWhiteSpace(emp.DataIdentifier))
                .ToList();

                return records;
            }
            else
            {
                throw new UserFriendlyException("Unsupported file format.", "400");
            }
        }
        catch (UserFriendlyException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException("Internal Server Error.", "500");
        }
    }

    /// <summary>
    /// Retrieves a list of allowed file extensions for bulk import operations from the configuration.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task<string[]> GetBulkImportAllowedTypesAsync()
    {
        try
        {

            if (BulkImportFileConsts.ExtensionAllowed == null || BulkImportFileConsts.ExtensionAllowed.Length == 0)
            {
                throw new UserFriendlyException("No allowed file extensions are configured for bulk import.", "400");
            }
            var allowedTypes = BulkImportFileConsts.ExtensionAllowed;


            return await Task.FromResult(allowedTypes);
        }
        catch (UserFriendlyException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException("An error occurred while fetching the allowed types for bulk import.", "500");
        }
    }
}
