using Volo.Abp.Modularity;

namespace CommonLibs.BulkImport.Sample;

public abstract class SampleApplicationTestBase<TStartupModule> : SampleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
