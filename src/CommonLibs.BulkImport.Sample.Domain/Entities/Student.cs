using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CommonLibs.BulkImport.Sample.Entities;
public class Student: FullAuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Grade { get; set; }
}
