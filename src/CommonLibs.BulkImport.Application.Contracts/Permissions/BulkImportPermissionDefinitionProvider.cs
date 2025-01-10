using CommonLibs.BulkImport.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CommonLibs.BulkImport.Permissions;

public class BulkImportPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BulkImportPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BulkImportPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BulkImportResource>(name);
    }
}
