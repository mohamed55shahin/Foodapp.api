using FoodApp.Api.Data.Repository;
using FoodApp.Api.FoodApp.Core.Enums;
using FoodApp.Api.ViewModle.authVIewModel;
using FoodApp.Api.ViewModle.RecpieViewModle;
using FoodApp.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace FoodApp.Api.FoodApp.Service.RecipeService
{
    public class RecipeService : IRecpieService
    {
        IRepository<Recipe> repository;

        public RecipeService(IRepository<Recipe> repository)
        {
            this.repository = repository;
        }

        public ResponsiveView<bool> createRecipes(RecpieCreateViewModel recpieCreate)
        {  
            if(recpieCreate == null) return new FailerResView<bool>(Errorcode.emptyData , "no data have been fatched");

            var recpieExist = repository.Any(c=> c.Item.EnName == recpieCreate.Name);

            if(recpieExist) return new FailerResView<bool>(Errorcode.recipeExist, " this recpie is already exist in the system");

            var creatRec = recpieCreate.MapTo<Recipe>();    

             repository.Add(creatRec);  
             repository.SaveChanges();

            return new SuccessResView<bool>(true, "recipe created sucessfuly "); 
        }

        public ResponsiveView<bool> DeletRecipe(int id)
        {
            var entityExist = repository.Get(c=> c.ID==id && !c.Deleted).FirstOrDefault();

            if(entityExist is null) return new FailerResView<bool>(Errorcode.alreadyDeleted , "this recipe not found ar already deleted ");

            repository.Delete(entityExist);

            repository.SaveChanges();

            return new SuccessResView<bool>(true, "deleted sucssfuly"); 
        }

        public ResponsiveView<IEnumerable<RecipeViewModel>> GetAll()
        {
            var recipes = repository.GetAll();
            if (!recipes.Any()) return new FailerResView<IEnumerable<RecipeViewModel>>(Errorcode.unfoundData, "there is no recipes to display ");
        
            var recipesview = recipes.ProjectTo<RecipeViewModel>();

            return new SuccessResView<IEnumerable<RecipeViewModel>>(recipesview);

        }

        public ResponsiveView<IEnumerable<RecpieDetailsViewModel>> getAllAdmin()
        {
            var recipes = repository.GetAll();
            if (!recipes.Any()) return new FailerResView<IEnumerable<RecpieDetailsViewModel>>(Errorcode.unfoundData, "there is no recipe to display ");


            var recipesview = recipes.ProjectTo<RecpieDetailsViewModel>();

            return new SuccessResView<IEnumerable<RecpieDetailsViewModel>>(recipesview);
        }

        public ResponsiveView<RecpieDetailsViewModel> RecpieDetiles(int id)
        {
            var exists = repository.Get(c => c.ID == id);

            if (exists is null) return new FailerResView <RecpieDetailsViewModel>(Errorcode.unfoundData," thereis no recipr by this id");

            var quary  = exists.MapToFristOrDeafult<RecpieDetailsViewModel>();

            return new SuccessResView<RecpieDetailsViewModel>(quary, "this the data about this recpie");

        }

        public ResponsiveView<Recipe> update(UpdateRecipeViewMode updateRecipe)
        {
            if (updateRecipe is null)
                return new FailerResView<Recipe>(Errorcode.emptyData, "faild to match data");



            var recipe = updateRecipe.MapTo<Recipe>(); 
            if(recipe is null) return new FailerResView<Recipe>(Errorcode.unfoundData, "not fouund ");

            repository.SaveInclude(recipe , nameof(recipe.Description) , nameof(recipe.Item.EnName));

            return new SuccessResView<Recipe>(recipe, "updated successfuly"); 
         }
    }
}
