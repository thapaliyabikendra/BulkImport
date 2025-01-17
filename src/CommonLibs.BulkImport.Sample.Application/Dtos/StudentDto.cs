using CommonLibs.BulkImport.Application.Interfaces;
using CsvHelper.Configuration.Attributes;
using Ganss.Excel;

namespace CommonLibs.BulkImport.Sample.Dtos;
public class StudentDto: IBulkImportDto
{
    [Column("Name")]
    [Name("Name")]
    public string DataIdentifier { get; set; }
    public string Surname { get; set; }
    public string Grade { get; set; }
}
