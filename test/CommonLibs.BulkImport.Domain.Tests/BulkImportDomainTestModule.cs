using Volo.Abp.Modularity;

namespace CommonLibs.BulkImport;

[DependsOn(
    typeof(BulkImportDomainModule),
    typeof(BulkImportTestBaseModule)
)]
public class BulkImportDomainTestModule : AbpModule
{

}
