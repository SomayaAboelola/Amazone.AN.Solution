using Amazone.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Amazone.Apis.Extentions
{
    public static class UserManagerExtention
    {
        public static async Task<ApplicationUser> FindUserwithaddressAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)

        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Include(a => a.Addresses).FirstOrDefaultAsync(e => e.NormalizedEmail == email.ToUpper());
            return user;
        }
    }
}