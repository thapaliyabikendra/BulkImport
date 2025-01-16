using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibs.BulkImport.Application.Constants;
public class BulkImportOptions
{
    public long FileSizeLimit { get; set; }
    public string[] AllowedExtensions { get; set; }
}
