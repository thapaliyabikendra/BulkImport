using System.Threading.Tasks;

namespace CommonLibs.BulkImport.Sample.Data;

public interface ISampleDbSchemaMigrator
{
    Task MigrateAsync();
}
