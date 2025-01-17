using CommonLibs.BulkImport.Application.Constants;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace CommonLibs.BulkImport.Application;

[DependsOn(new Type[]
{
    typeof(AbpDddApplicationModule),
})]
public class BulkImportApplicationModule : AbpModule
{
    public BulkImportApplicationModule()
    {
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        
        Configure<BulkImportOptions>(configuration.GetSection("BulkImport"));
    }
}
