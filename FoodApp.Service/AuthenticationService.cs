using FoodApp.Api.FoodApp.Core.Enums;
using FoodApp.Api.ViewModle.authVIewModel;
using FoodApp.Core.Entities;
using FoodApp.Core.Enums;
using FoodApp.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodApp.Api.FoodApp.Service
{
    public class AuthenticationService : IAuthenticationsService
    {
        private UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthenticationService(UserManager<ApplicationUser> user , RoleManager<IdentityRole> role  , IOptions<JWT> jWT)
        {
             userManager = user;
             _roleManager = role;
             _jwt = jWT.Value;

        }

        public async Task<ResponsiveView<AuthModel>> RegisterAsync(RegistrationViewModel registrationView)
        {
            var cheak = await userManager.FindByEmailAsync(registrationView.Email);
            if (cheak != null)
                return new FailerResView<AuthModel>(Errorcode.userexist);

            if (await userManager.FindByNameAsync(registrationView.Username) is not null)
                return new FailerResView<AuthModel>(Errorcode.userexist);

            // default  role is user 
            var rolee = Role.User; 

            if(!string.IsNullOrEmpty(registrationView.SecretKeyForAdmin) && registrationView.SecretKeyForAdmin == "117004")
            {
                rolee = Role.Admin; 
            }
          
           var user = registrationView.MapTo<ApplicationUser>();  
            user.roles = rolee;
            var result = await userManager.CreateAsync(user, registrationView.Password);

            if (!result.Succeeded)
            {
                var error = new StringBuilder();
                foreach (var item in result.Errors)
                {

                    error.Append($"{item.Description}, ");

                }
                return new FailerResView<AuthModel>(Errorcode.registerfaild, error.ToString());
            }

            var userRole = user.roles; 


            var jwtSecurityToken = await CreateJwtToken(user , userRole );

            return new SuccessResView<AuthModel>(new AuthModel
                  {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
                },"registration sucess"); 
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user, Role role )
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roleClaims = new List<Claim>();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(ClaimTypes.Role ,((int)role).ToString()) ,
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }
            .Union(userClaims);
             

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken (
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<ResponsiveView<AuthModel>> loginAsync(LoginView model)
        {
            var authModel = new AuthModel();

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                return new FailerResView<AuthModel>(Errorcode.loginfaild, "password or eamil in correct");  
            }

            var jwtSecurityToken = await CreateJwtToken(user , user.roles);
         

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            return new SuccessResView<AuthModel>(authModel); 
        }

        public async Task<ResponsiveView<IEnumerable<UserViewModel>>> GetAllUser()
        {
          IQueryable<ApplicationUser> users =(IQueryable<ApplicationUser>) await userManager.Users.ToListAsync();

          if(users.Count()==0) return new FailerResView<IEnumerable<UserViewModel>>
                    (Errorcode.usersnotExits , "no user in the system");

            var usersview = users.ProjectTo<UserViewModel>();

            return new SuccessResView<IEnumerable<UserViewModel>>(usersview, "list of all users in the systenm ");
        }
    }
}
