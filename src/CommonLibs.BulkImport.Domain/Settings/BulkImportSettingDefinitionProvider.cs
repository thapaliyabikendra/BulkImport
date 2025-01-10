using Volo.Abp.Settings;

namespace CommonLibs.BulkImport.Settings;

public class BulkImportSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BulkImportSettings.MySetting1));
    }
}
