using Volo.Abp.Modularity;

namespace CommonLibs.BulkImport.Sample;

[DependsOn(
    typeof(SampleApplicationModule),
    typeof(SampleDomainTestModule)
)]
public class SampleApplicationTestModule : AbpModule
{

}
