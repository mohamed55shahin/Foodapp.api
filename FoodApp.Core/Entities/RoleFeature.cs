using FoodApp.Api.FoodApp.Core.Enums;
using FoodApp.Core.Entities;
using FoodApp.Core.Enums;

namespace FoodApp.Api.FoodApp.Core.Entities
{
    public class RoleFeature: BaseEntity 
    {
        public Role role { get; set;  } 

        public features feature { get; set;  }  
    }
}
