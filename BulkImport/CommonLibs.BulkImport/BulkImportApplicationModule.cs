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
   
    }
}
