using Volo.Abp.Modularity;

namespace CommonLibs.BulkImport.Sample;

/* Inherit from this class for your domain layer tests. */
public abstract class SampleDomainTestBase<TStartupModule> : SampleTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
