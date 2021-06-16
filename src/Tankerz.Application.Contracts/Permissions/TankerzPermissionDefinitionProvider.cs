using Tankerz.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tankerz.Permissions
{
    public class TankerzPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(TankerzPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(TankerzPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<TankerzResource>(name);
        }
    }
}
