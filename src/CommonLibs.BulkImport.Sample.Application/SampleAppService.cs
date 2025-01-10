using System;
using System.Collections.Generic;
using System.Text;
using CommonLibs.BulkImport.Sample.Localization;
using Volo.Abp.Application.Services;

namespace CommonLibs.BulkImport.Sample;

/* Inherit your application services from this class.
 */
public abstract class SampleAppService : ApplicationService
{
    protected SampleAppService()
    {
        LocalizationResource = typeof(SampleResource);
    }
}
