using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CommonLibs.BulkImport.Application.Dtos;
public record BulkImportFileDto
{
    [Required]
    public IFormFile File { get; set; }
}
