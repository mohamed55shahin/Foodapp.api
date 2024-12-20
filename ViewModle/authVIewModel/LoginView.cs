using System.ComponentModel.DataAnnotations;

namespace FoodApp.Api.ViewModle
{
    public class LoginView
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
