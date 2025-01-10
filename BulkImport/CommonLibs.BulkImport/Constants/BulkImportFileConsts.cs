using System.ComponentModel;

namespace CommonLibs.BulkImport.Application.Constants;
public static class BulkImportFileConsts
{
    public static class TemplateFileNames
    {
        public const string Student = "bulk-import-student-template.xlsx";
    }
    public static readonly string[] ExtensionAllowed = {
        ".csv",
        ".xlsx",
    };

    [Description("File Size in Bits")]
    public static readonly int FileSizeLimit = 10 * 1024 * 1024;
}
