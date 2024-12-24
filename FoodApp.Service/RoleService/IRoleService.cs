using FoodApp.Api.FoodApp.Core.Enums;
using FoodApp.Api.ViewModle.authVIewModel;
using FoodApp.Core.Enums;

namespace FoodApp.Api.FoodApp.Service.RoleService
{
    public interface IRoleService
    {
        ResponsiveView<bool> addRole(RoleFeatureViewModel roleFeature); 
        
        //ResponsiveView<bool> assignFeaturesToRole(RoleFeaturesModel rolefeaturs);
        
        ResponsiveView<IEnumerable<features>> GetFeature(Role role);

        ResponsiveView<bool> HasAccess(Role role , features features);

    }
}
