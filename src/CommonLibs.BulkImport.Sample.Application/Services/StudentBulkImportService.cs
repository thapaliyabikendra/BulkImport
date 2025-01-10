using CommonLibs.BulkImport.Application.Services;
using CommonLibs.BulkImport.Sample.Dtos;
using CommonLibs.BulkImport.Sample.Entities;
using System;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace CommonLibs.BulkImport.Sample.Services;
public class StudentBulkImportService : BulkImportService<Student, Guid, StudentDto>
{
    public StudentBulkImportService(IRepository<Student, Guid> repository, IObjectMapper objectMapper) : base(repository, objectMapper)
    {
    }
}
