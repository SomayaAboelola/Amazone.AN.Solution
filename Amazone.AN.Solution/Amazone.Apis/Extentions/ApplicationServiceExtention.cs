using Amazone.Apis.Errors;
using Amazone.Apis.Helper;
using Amazone.Core;
using Amazone.Core.Repositories.Contract;
using Amazone.Core.Services.Contract;
using Amazone.Repository;
using Amazone.Repository.BasketRepository;
using Amazone.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Amazone.Apis.Extentions
{
    public static class ApplicationServiceExtention
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPaymentServices), typeof(PaymentServices));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddScoped(typeof(IAuthService), typeof(AuthServices));

            services.AddScoped(typeof(IOrderServiece), typeof(OrderService));

            services.AddScoped(typeof(IProductServices), typeof(ProductServices));

            services.AddAutoMapper(typeof(MappingProfile));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(p => p.ErrorMessage)
                                                         .ToList();
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }

        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configu)
        {
            services.AddAuthentication/*(JwtBearerDefaults.AuthenticationScheme)*/
             (option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configu["jwt:AuthAudiance"],

                    ValidateIssuer = true,
                    ValidIssuer = configu["jwt:AuthIssue"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configu["jwt:AuhKey"]))


                };
            });
            return services;

        }
    }
}
