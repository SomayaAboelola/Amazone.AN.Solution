using Amazone.Core.Entities.Identity;
using Amazone.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthServices(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> CreatTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            // Private Claims (User_Defined)

            var authClaim = new List<Claim>()
            {
                new Claim (ClaimTypes.Name ,user.DisplayName) ,
                new Claim(ClaimTypes.Email ,user.Email)
            };

            var userRole = await userManager.GetRolesAsync(user);
            foreach (var Role in userRole) 
            {
                authClaim.Add(new Claim (ClaimTypes.Role ,Role));   
            }

            // Security Key

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:AuhKey"]));

            // Register Claim 

            var token = new JwtSecurityToken(

               audience: _config["jwt:AuthAudiance"] ,
                issuer: _config["jwt:AuthIssue"],
                expires: DateTime.Now.AddDays(double.Parse( _config["jwt:AuthExpire"] ?? "0")) ,
                claims: authClaim ,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.Aes128CbcHmacSha256)

                    );

            // value

            return  new JwtSecurityTokenHandler().WriteToken(token);    
        }
  
    }
}
