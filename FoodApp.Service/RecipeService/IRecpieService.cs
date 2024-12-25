using FoodApp.Api.ViewModle.authVIewModel;
using FoodApp.Api.ViewModle.RecpieViewModle;
using FoodApp.Core.Entities;

namespace FoodApp.Api.FoodApp.Service.RecipeService
{
    public interface IRecpieService  
    {

        ResponsiveView<bool> createRecipes(RecpieCreateViewModel recpieCreate);
        ResponsiveView<bool> DeletRecipe(int id); 
        ResponsiveView<RecpieDetailsViewModel> RecpieDetiles(int id);
        ResponsiveView<IEnumerable<RecipeViewModel>> GetAll();
        ResponsiveView<IEnumerable<RecpieDetailsViewModel>> getAllAdmin(); 


        ResponsiveView<Recipe> update(UpdateRecipeViewMode updateRecipe ); 







    }
}
