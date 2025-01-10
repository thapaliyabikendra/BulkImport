using Volo.Abp.Modularity;

namespace CommonLibs.BulkImport;

public abstract class BulkImportApplicationTestBase<TStartupModule> : BulkImportTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
