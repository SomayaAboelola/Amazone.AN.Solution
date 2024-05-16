using Amazone.Core.Entities.Identity;
using Amazone.Repository._IdentityRepo;
using Microsoft.AspNetCore.Identity;

namespace Amazone.Apis.Extentions
{
    public static class IdentityServiceExtention
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddApplicationService();


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            return services;
        }
    }
}
