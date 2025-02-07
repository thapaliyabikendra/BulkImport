namespace CommonLibs.BulkImport.Application.Constants;
public class BulkImportOptions
{
    public long FileSizeLimit { get; set; }
    public string[] AllowedExtensions { get; set; }
    public string BlobContainerName { get; set; }
}
