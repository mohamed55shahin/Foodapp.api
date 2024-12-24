using FoodApp.Api.FoodApp.Service.RecipeService;
using FoodApp.Api.ViewModle.authVIewModel;
using FoodApp.Api.ViewModle.RecpieViewModle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeService _recipeService;

        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost("create")]
        public IActionResult CreateRecipe([FromBody] RecpieCreateViewModel recipeCreate)
        {
            if (!ModelState.IsValid)  return BadRequest(); 
            var result = _recipeService.createRecipes(recipeCreate);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            var result = _recipeService.DeletRecipe(id);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("all")]

        public IActionResult GetAllRecipes()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);  
            var result = _recipeService.GetAll();
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("all-admin")]
        public IActionResult GetAllRecipesForAdmin()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _recipeService.getAllAdmin();
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("details/{id}")]
        public IActionResult GetRecipeDetails(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _recipeService.RecpieDetiles(id);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("update")]
        public IActionResult UpdateRecipe([FromBody] UpdateRecipeViewMode updateRecipe)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _recipeService.update(updateRecipe);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
