using CommonLibs.BulkImport.Application.Attributes;
using CommonLibs.BulkImport.Application.Constants;
using CommonLibs.BulkImport.Application.Contracts;
using CommonLibs.BulkImport.Application.Dtos;
using CommonLibs.BulkImport.Application.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using CsvHelper;
using FluentValidation;
using Ganss.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Validation;
using System.Drawing;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;


namespace CommonLibs.BulkImport.Application.Services;
public class BulkImportService<TEntity, TKey, TDto, TValidator> : ApplicationService where TEntity : class, IEntity<TKey>, 
    new() where TValidator : AbstractValidator<TDto>, new()
{
    private readonly string _validationTitle = "Bulk Import Validations";

    private readonly IRepository<TEntity, TKey> _repository;
    private readonly IObjectMapper _objectMapper;
    private readonly BulkImportOptions _options;
    private readonly IMappingProvider<TDto> _mappingProvider;
    private readonly IBlobContainer _blobContainer;

    protected BulkImportService(IRepository<TEntity, TKey> repository, 
        IObjectMapper objectMapper, 
        IOptions<BulkImportOptions> options,
        IMappingProvider<TDto> mappingProvider,
        IBlobContainerFactory blobContainerFactory)
    {
        _repository = repository;
        _objectMapper = objectMapper;
        _options = options.Value;
        _mappingProvider = mappingProvider;
        _blobContainer = blobContainerFactory.Create("test-blob");
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
            if (!_options.AllowedExtensions.Any(x => x.Equals(extension, StringComparison.OrdinalIgnoreCase)))
            {
                var msg = "Invalid File Extension. Allowed extensions are: " + string.Join(", ", _options.AllowedExtensions);
                throw new UserFriendlyException(msg, "400");
            }

            if (input.File.Length > _options.FileSizeLimit)
            {
                var msg = "File Size Exceeded. Maximum allowed size is " + (_options.FileSizeLimit / (1024 * 1024)) + " MB.";
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

            var allValidationErrors = new List<ValidationResult>();
            foreach (var item in input.Index())
            {
                var index = item.Index + 1;
                var validator = new TValidator();
                var validations = await validator.ValidateAsync(item.Item);

                if (!validations.IsValid)
                {
                    var prefixMessage = $"Row {index}: ";
                    var errors = validations.Errors.Select(e => new ValidationResult(prefixMessage + e.ErrorMessage));
                    allValidationErrors.AddRange(errors);
                }
            }

            if (allValidationErrors.Any())
            {
                throw new AbpValidationException(_validationTitle, allValidationErrors);
            }

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

    private async Task<List<TDto>> GetDataAsync(MemoryStream stream, string extension)
    {
        try
        {
            // Check property-level attribute
            var type = typeof(TDto);
            var property = type.GetProperties()
                                          .FirstOrDefault(p => p.GetCustomAttributes(typeof(UniqueIdentifierAttribute), false).Any());

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
                var records = csv.GetRecords<TDto>();
                records = FilterByUniqueIdentifier(records, property.Name);
                return records.ToList();
            }
            else if (extension.Equals(FileTypeConsts.XlsxType, StringComparison.OrdinalIgnoreCase))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var importer = new ExcelMapper(stream)
                {
                    SkipBlankCells = true,
                    HeaderRow = true,
                };

                var records = importer.Fetch<TDto>();
                //.FilterByUniqueIdentifier(property.Name)
                //.ToList();
                if (records != null)
                {
                    records = FilterByUniqueIdentifier(records, property.Name);
                }
                else
                {
                    throw new UserFriendlyException("Failed to fetch Excel data.");
                }

                return records.ToList();
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

            if (_options.AllowedExtensions == null || _options.AllowedExtensions.Length == 0)
            {
                throw new UserFriendlyException("No allowed file extensions are configured for bulk import.", "400");
            }
            var allowedTypes = _options.AllowedExtensions;


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

    public IEnumerable<TDto> FilterByUniqueIdentifier<TDto>(IEnumerable<TDto> source, string fieldName)
    {
        // Define the parameter for the expression (e.g., s => s.DataIdentifier)
        ParameterExpression parameter = Expression.Parameter(typeof(TDto), "s");

        // Get the property to be filtered using reflection
        PropertyInfo property = typeof(TDto).GetProperty(fieldName);

        // Ensure the property is of type string before proceeding
        if (property == null || property.PropertyType != typeof(string))
        {
            throw new ArgumentException($"The field '{fieldName}' is not a string property.");
        }

        // Build the expression to check if the property is not null or whitespace
        Expression propertyAccess = Expression.Property(parameter, property);

        // Reference the static method IsNullOrWhiteSpace from the string class
        MethodInfo isNullOrWhiteSpaceMethod = typeof(string).GetMethod("IsNullOrWhiteSpace", new[] { typeof(string) });
        Expression callIsNullOrWhiteSpace = Expression.Call(null, isNullOrWhiteSpaceMethod, propertyAccess);

        // Create a lambda expression that returns `!IsNullOrWhiteSpace(property)`
        Expression negation = Expression.Not(callIsNullOrWhiteSpace);
        Expression<Func<TDto, bool>> lambda = Expression.Lambda<Func<TDto, bool>>(negation, parameter);

        // Apply the filter dynamically using LINQ's Where method
        return source.Where(lambda.Compile());
    }
    public async Task<String> GetTemplateAsync()
    {
        try
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            package.Workbook.Properties.Author = "Bulk Import System";

            var ws = package.Workbook.Worksheets.Add("Template");
            ws.Name = "Template";
            ws.Cells.Style.Font.Size = 11;
            ws.Cells.Style.Font.Name = "Calibri";

            var mappings = _mappingProvider.GetMappings();
            if (mappings == null || mappings.Count == 0)
            {
                throw new InvalidOperationException("No mapping configuration found.");
            }

            var headers = mappings.Keys.ToList();
            int colIndex = 1;
            int rowIndex = 1;

            // Write Headers
            foreach (var header in headers)
            {
                var cell = ws.Cells[rowIndex, colIndex];

                var fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);

                var border = cell.Style.Border;
                border.Bottom.Style =
                    border.Top.Style =
                        border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;

                cell.Value = header;
                colIndex++;
            }

            // Apply AutoFilter and Column Width
            using (ExcelRange autoFilterCells = ws.Cells[1, 1, 1, headers.Count])
            {
                autoFilterCells.AutoFilter = true;
                autoFilterCells.AutoFitColumns();
            }

            // Generate the Excel template as a byte array
            byte[] excelBytes = await package.GetAsByteArrayAsync();

            // Define a unique file name
            string fileName = $"Template_{Guid.NewGuid()}.xlsx";

            // Save the file to blob storage
            await _blobContainer.SaveAsync(fileName, excelBytes, true);

            return fileName; // Return file name for reference
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException($"Error generating template: {ex.Message}");
        }
    }
}
