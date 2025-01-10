using CommonLibs.BulkImport.Application.Interfaces;
using Ganss.Excel;

namespace CommonLibs.BulkImport.Sample.Dtos;
public class StudentDto: IBulkImportDto
{
    [Column("Name")]
    public string DataIdentifier { get; set; }
    public string Surname { get; set; }
    public string Grade { get; set; }
}
