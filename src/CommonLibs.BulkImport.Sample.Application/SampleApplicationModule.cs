using CommonLibs.BulkImport.Application;
using CommonLibs.BulkImport.Application.Contracts;
using CommonLibs.BulkImport.Sample.Dtos;
using CommonLibs.BulkImport.Sample.Mappings;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace CommonLibs.BulkImport.Sample;

[DependsOn(
    typeof(SampleDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(SampleApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule),
    typeof(BulkImportApplicationModule)
    )]
public class SampleApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SampleApplicationModule>();
        });
        // Register Mapping Providers
        context.Services.AddTransient<IMappingProvider<StudentDto>, StudentMappingProvider>();

    }
}
