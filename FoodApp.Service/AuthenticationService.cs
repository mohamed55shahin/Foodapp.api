using FoodApp.Api.FoodApp.Core.Enums;
using FoodApp.Api.ViewModle;
using FoodApp.Core.Entities;
using FoodApp.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
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

            var user = new ApplicationUser
            {
                 UserName = registrationView.Username,
                Email = registrationView.Email,
                Fname = registrationView.FirstName,
                Lname = registrationView.LastName,
            };
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
            await userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new SuccessResView<AuthModel>(new AuthModel
                  {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
                }); 
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
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

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();
            return new SuccessResView<AuthModel>(authModel); 
        }
    }
}
