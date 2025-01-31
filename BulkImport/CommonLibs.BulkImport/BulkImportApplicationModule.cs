using CommonLibs.BulkImport.Application.Constants;
using CommonLibs.BulkImport.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using NPOI.POIFS.Crypt;
using Volo.Abp.Application;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using Volo.Abp.Modularity;

namespace CommonLibs.BulkImport.Application;

[DependsOn(new Type[]
{
    typeof(AbpDddApplicationModule),
    typeof(AbpBlobStoringFileSystemModule)
})]
public class BulkImportApplicationModule : AbpModule
{
    public BulkImportApplicationModule()
    {
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        /*var configuration = context.Services.GetConfiguration();
        
        Configure<BulkImportOptions>(configuration.GetSection("BulkImport"));*/


        //Configure Blob Storing
        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.ConfigureDefault(container =>
            {
                container.UseFileSystem(fileSystem =>
                {
                    fileSystem.BasePath = "C:\\BlobStorage";
                });
            });
        });
        //context.Services.AddScoped<IBlobContainerFactory, BlobContainerFactory>();
    }
}
