using Volo.Abp.Modularity;

namespace CommonLibs.BulkImport;

/* Inherit from this class for your domain layer tests. */
public abstract class BulkImportDomainTestBase<TStartupModule> : BulkImportTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
