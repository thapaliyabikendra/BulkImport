using System;
using System.Collections.Generic;
using System.Text;
using CommonLibs.BulkImport.Localization;
using Volo.Abp.Application.Services;

namespace CommonLibs.BulkImport;

/* Inherit your application services from this class.
 */
public abstract class BulkImportAppService : ApplicationService
{
    protected BulkImportAppService()
    {
        LocalizationResource = typeof(BulkImportResource);
    }
}
