using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CommonLibs.BulkImport.Data;

/* This is used if database provider does't define
 * IBulkImportDbSchemaMigrator implementation.
 */
public class NullBulkImportDbSchemaMigrator : IBulkImportDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
