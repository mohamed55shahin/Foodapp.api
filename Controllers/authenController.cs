using FoodApp.Api.ViewModle;
using FoodApp.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authenController : ControllerBase
    {
        private readonly IAuthenticationsService authenticationService; 

        public authenController(IAuthenticationsService authenticationService)
        {
             this.authenticationService = authenticationService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authenticationService.RegisterAsync(model);

            if (!result.IsSuccess)
                return BadRequest(result.Massage);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginView model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authenticationService.loginAsync(model);

            if (!result.IsSuccess)
                return BadRequest(result.Massage);

            return Ok(result);
        }



    }
}
