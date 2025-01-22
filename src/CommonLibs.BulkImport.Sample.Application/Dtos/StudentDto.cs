using CommonLibs.BulkImport.Application.Attributes;
using CsvHelper.Configuration.Attributes;
using Ganss.Excel;

namespace CommonLibs.BulkImport.Sample.Dtos;
public class StudentDto
{
    [Column("Name")]
    [Name("Name")]
    [UniqueIdentifier]
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Grade { get; set; }
}
