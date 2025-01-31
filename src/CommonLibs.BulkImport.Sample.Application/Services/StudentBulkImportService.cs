using CommonLibs.BulkImport.Application.Constants;
using CommonLibs.BulkImport.Application.Contracts;
using CommonLibs.BulkImport.Application.Services;
using CommonLibs.BulkImport.Sample.Dtos;
using CommonLibs.BulkImport.Sample.Entities;
using CommonLibs.BulkImport.Sample.Validators;
using Microsoft.Extensions.Options;
using System;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace CommonLibs.BulkImport.Sample.Services;
public class StudentBulkImportService : BulkImportService<Student, Guid, StudentDto, StudentDtoValidator>
{
    public StudentBulkImportService(IRepository<Student, Guid> repository,
        IObjectMapper objectMapper, IOptions<BulkImportOptions> options,
        IMappingProvider<StudentDto> mappingProvider,
        IBlobContainerFactory blobContainerFactory) : base(repository, objectMapper, options, mappingProvider, blobContainerFactory)
    {
    }
}
