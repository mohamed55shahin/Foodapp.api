using FoodApp.Api.ViewModle;

namespace FoodApp.Service
{
    public interface IAuthenticationsService
    {
         Task<ResponsiveView<AuthModel>> RegisterAsync(RegistrationViewModel registrationView); 
         Task<ResponsiveView<AuthModel>> loginAsync( LoginView login );
    }
}
