
using Phone_Market.Code;
using Phone_Market.DTO;
using Phone_Market.Enums;
using Phone_Market.Services;
using Phone_Market.Services.EmailImplementation;
using System.Security.Claims;

namespace Phone_Market
{

    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ControllerDependencies>();
            services.AddScoped<ServiceDependencies>();
            services.AddScoped<EmailService>();
            services.AddScoped<Account_Service>();
            services.AddScoped<Brand_Service>();
            services.AddScoped<Category_Service>();
            services.AddScoped<Color_Service>();
            services.AddScoped<Product_Service>();
            services.AddScoped<Rating_Service>();
           
            return services;

        }

        public static IServiceCollection AddCurrentUser (this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor?.HttpContext;
                if (httpContext == null || !httpContext!.User!.Identity!.IsAuthenticated)
                    return new CurrentUserDTO { IsLoggedIn = false };
                var isIdValid = Guid.TryParse(httpContext.User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value, out Guid id);
                if (!isIdValid)
                {
                    throw new Exception("Id-ul nu e int");
                }
                var isRoleIdValid = int.TryParse(httpContext.User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.Role)?.Value, out int roleId);
                if (!isRoleIdValid)
                {
                    throw new Exception("IdRole-ul nu e int");
                }
                return new CurrentUserDTO
                {
                    Id = id,
                    FullName = httpContext.User?.Claims?.FirstOrDefault(s => s.Type == ClaimTypes.Name)?.Value ?? "",
                    RoleId = roleId,
                    IsLoggedIn = true,
                };
            });
            return services;
        }

        public static IServiceCollection AddPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireRole(((int)RoleType.Admin).ToString()));
                options.AddPolicy("User", policy =>
                    policy.RequireRole(((int)RoleType.User).ToString()));
            });
            return services;
        }

        //public static IServiceCollection AddAutoMappers(this IServiceCollection services)
        //{
        //    services.AddScoped<ToOrganizerRequestShow>();
        //    return services;
        //}
    }
}
