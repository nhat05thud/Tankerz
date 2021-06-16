using Volo.Abp.Settings;

namespace Tankerz.Settings
{
    public class TankerzSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(TankerzSettings.MySetting1));
        }
    }
}
