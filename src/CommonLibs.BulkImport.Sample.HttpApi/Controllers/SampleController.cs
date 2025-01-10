using CommonLibs.BulkImport.Sample.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CommonLibs.BulkImport.Sample.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SampleController : AbpControllerBase
{
    protected SampleController()
    {
        LocalizationResource = typeof(SampleResource);
    }
}
