using FoodApp.Api.FoodApp.Core.Enums;
using FoodApp.Api.FoodApp.Service.RoleService;
using FoodApp.Core.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Security.Cryptography.Xml;

namespace FoodApp.Api.Helper
{
    public class CoustemAurhorizeFilter : ActionFilterAttribute
    {
        IRoleService roleService;

        features features; 

         public CoustemAurhorizeFilter( IRoleService roleService, features features)
         {
            this.roleService = roleService;
            this.features = features;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var calims = context.HttpContext.User;

            var RoleID = calims.FindFirst("roletype");

            if (RoleID == null || string.IsNullOrEmpty(RoleID.Value)) {

                throw new UnauthorizedAccessException(); 
            }
        
           var role  = (Role) int.Parse(RoleID.Value);

            var check = roleService.HasAccess(role , features );

            if (check == null) throw new UnauthorizedAccessException();


            base.OnActionExecuted(context);
        }

     

    }
}
