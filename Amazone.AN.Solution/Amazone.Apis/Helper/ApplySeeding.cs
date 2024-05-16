using Amazone.Core.Entities.Identity;
using Amazone.Repository._IdentityRepo;
using Amazone.Repository.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Amazone.Apis.Helper
{
    public class ApplySeeding
    {
        public static async Task SeedAsync(WebApplication app)
        {
            using var Scope = app.Services.CreateScope();

            var Service = Scope.ServiceProvider;

            var _context = Service.GetRequiredService<StoreDbContext>();

            var _contextIdentity = Service.GetRequiredService<AppIdentityDbContext>();

            var _contextSeed = Service.GetRequiredService<UserManager<ApplicationUser>>();

            var loggerfactory = Service.GetRequiredService<ILoggerFactory>();

            try
            {
                await _context.Database.MigrateAsync();

                await StoreContextSeed.ApplySeedingAsync(_context);

                await _contextIdentity.Database.MigrateAsync();

                await AppIdentityContextSeed.SeedUserAsync(_contextSeed);

            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();

                logger.LogError(ex.Message);

            }
        }
    }
}
