using Amazone.Apis.Extentions;
using Amazone.Apis.Helper;
using Amazone.Apis.Middelware;
using Amazone.Repository._IdentityRepo;
using Amazone.Repository.Context;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Amazone.Apis
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddDbContext<StoreDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddSingleton<IConnectionMultiplexer>(Service =>
           {
               var connect = builder.Configuration.GetConnectionString("Redis");
               return ConnectionMultiplexer.Connect(connect);
           });

            builder.Services.AddApplicationService();
            builder.Services.AddIdentityService();

            builder.Services.AddAuthenticationService(builder.Configuration);

            builder.Services.AddCors(async options =>
            {
                options.AddPolicy("MyPolicy", policyOptions =>
                {

                    policyOptions.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontBaseUrl"]);
                });
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerService();

            var app = builder.Build();

            await ApplySeeding.SeedAsync(app);


            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddelware>();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddelware();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("MyPolicy");

            app.MapControllers();

            app.UseAuthentication();

            app.UseAuthorization();

            app.Run();

        }
    }
}