using AutoMapper;
using FoodApp.Api.ViewModle.RecpieViewModle;
using FoodApp.Core.Entities;

namespace FoodApp.Api.ViewModle.Profiles
{
    public class RecipesProfile : Profile
    {

        public RecipesProfile() {

            CreateMap<Recipe, RecpieCreateViewModel>().
                ForMember(dst=> dst.Name ,opt=>opt.MapFrom(so=> so.Item.EnName))
                .ForMember(d=>d.TagId, opt=> opt.
                MapFrom(s => s.Tags.
                Select(c => new { }))); 
        
        } 

    }
}
