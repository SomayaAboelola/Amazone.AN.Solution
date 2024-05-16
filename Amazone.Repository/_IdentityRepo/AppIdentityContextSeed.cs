using Amazone.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Repository._IdentityRepo
{
    public static class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync (UserManager<ApplicationUser> userManager )
        {
           
                if (!userManager.Users.Any())
                {
                    var user = new ApplicationUser()
                    {
                        DisplayName = "SomayaMagdy",
                        Email = "SomayaMagdy@gmail.com",
                        UserName = "SomayaMagdy",
                        PhoneNumber = "010656414",
                        Addresses = new Address
                        {
                            FirstName = "Salama",
                            LastName = "Hamada",
                            Street = "StreetEltearan",
                            City = "Cairo",
                            Country = "Egypt"
                        }

                    };
                    await userManager.CreateAsync(user, "Pa$sw0rd");
                }
            }
       
        }
    }

