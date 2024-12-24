using FoodApp.Api.ViewModle.authVIewModel;
using Microsoft.Identity.Client;

namespace FoodApp.Service
{
    public interface IAuthenticationsService
    {
         Task<ResponsiveView<AuthModel>> RegisterAsync(RegistrationViewModel registrationView); 
         Task<ResponsiveView<AuthModel>> loginAsync( LoginView login );
        Task<ResponsiveView<IEnumerable<UserViewModel>>> GetAllUser(); 
     
     }
}
