using System.Threading.Tasks;

namespace CommonLibs.BulkImport.Data;

public interface IBulkImportDbSchemaMigrator
{
    Task MigrateAsync();
}
