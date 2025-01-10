using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CommonLibs.BulkImport.Data;
using Volo.Abp.DependencyInjection;

namespace CommonLibs.BulkImport.EntityFrameworkCore;

public class EntityFrameworkCoreBulkImportDbSchemaMigrator
    : IBulkImportDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBulkImportDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the BulkImportDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BulkImportDbContext>()
            .Database
            .MigrateAsync();
    }
}
