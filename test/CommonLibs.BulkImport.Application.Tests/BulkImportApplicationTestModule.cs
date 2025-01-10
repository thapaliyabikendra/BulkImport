using Volo.Abp.Modularity;

namespace CommonLibs.BulkImport;

[DependsOn(
    typeof(BulkImportApplicationModule),
    typeof(BulkImportDomainTestModule)
)]
public class BulkImportApplicationTestModule : AbpModule
{

}
