using Microsoft.Extensions.Localization;
using CommonLibs.BulkImport.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace CommonLibs.BulkImport;

[Dependency(ReplaceServices = true)]
public class BulkImportBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<BulkImportResource> _localizer;

    public BulkImportBrandingProvider(IStringLocalizer<BulkImportResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
