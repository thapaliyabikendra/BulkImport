using Microsoft.Extensions.Localization;
using CommonLibs.BulkImport.Sample.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace CommonLibs.BulkImport.Sample;

[Dependency(ReplaceServices = true)]
public class SampleBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<SampleResource> _localizer;

    public SampleBrandingProvider(IStringLocalizer<SampleResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
