using AutoMapper;
using FoodApp.Api.ViewModle.RecpieViewModle;
using FoodApp.Core.Entities;

namespace FoodApp.Api.ViewModle.Profiles
{
    public class RecipesProfile : Profile
    {

        public RecipesProfile() {

            CreateMap<RecpieCreateViewModel, Recipe>().
                ForPath((dst=> dst.Item.EnName),(opt=>opt.MapFrom(so=> so.Name)))
                .ForMember(d=>d.Tags, opt=> opt.
                MapFrom(s => s.TagId.
                Select(c => new RecipeTag {TagId=c})));


            CreateMap<Recipe, RecpieDetailsViewModel>().
               ForMember(dst => dst.Title, opt => opt.MapFrom(so => so.Item.EnName))
                .ForMember(dst => dst.CategoryName, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(dst => dst.tagname, opt => opt.
                 MapFrom(s => s.Tags.Select(nam =>nam.Tags.Name).ToList()));

            CreateMap<UpdateRecipeViewMode, Recipe>().
                ForPath(dst => dst.Item.EnName, opt => opt.MapFrom(sou => sou.Name));


            CreateMap<Recipe, RecipeViewModel>()
               .ForMember(dst => dst.categoryname, opt => opt.MapFrom(s => s.Category.Name));
              
        }

    }
}
