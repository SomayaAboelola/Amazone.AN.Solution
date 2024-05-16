using Amazone.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Core.Services.Contract
{
    public interface IAuthService
    {
        Task<string> CreatTokenAsync(ApplicationUser user ,UserManager<ApplicationUser> userManager);
    }
}
