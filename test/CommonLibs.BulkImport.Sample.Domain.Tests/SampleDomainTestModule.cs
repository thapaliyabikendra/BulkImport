using Volo.Abp.Modularity;

namespace CommonLibs.BulkImport.Sample;

[DependsOn(
    typeof(SampleDomainModule),
    typeof(SampleTestBaseModule)
)]
public class SampleDomainTestModule : AbpModule
{

}
