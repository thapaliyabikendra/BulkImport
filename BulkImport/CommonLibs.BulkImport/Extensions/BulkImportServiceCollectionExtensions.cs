using CommonLibs.BulkImport.Application.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibs.BulkImport.Application.Extensions;
public static class BulkImportServiceCollectionExtensions
{
    public static IServiceCollection AddBulkImport(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BulkImportOptions>(options =>
        {
            var bulkImportSection = configuration.GetSection("BulkImport");
            if (bulkImportSection.Exists())
            {
                options.AllowedExtensions = Array.Empty<string>();
                bulkImportSection.Bind(options);
            }
            else
            {
                // Use default values if appsettings.json does not specify BulkImport
                options.FileSizeLimit = 10485760;
                options.AllowedExtensions = new string[] { ".csv", ".xlsx" };
            }
        });

        return services;
    }
}
