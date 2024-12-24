using AutoMapper;
using FoodApp.Api.FoodApp.Core.Entities;
using FoodApp.Api.ViewModle.authVIewModel;

namespace FoodApp.Api.ViewModle.Profiles
{
    public class RoleFeatureProfile : Profile 
    {
         public RoleFeatureProfile() { 
         
          CreateMap<RoleFeatureViewModel , RoleFeature> (); 


        
        }

    }
}
