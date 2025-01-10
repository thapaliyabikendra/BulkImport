using CommonLibs.BulkImport.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CommonLibs.BulkImport.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BulkImportController : AbpControllerBase
{
    protected BulkImportController()
    {
        LocalizationResource = typeof(BulkImportResource);
    }
}
