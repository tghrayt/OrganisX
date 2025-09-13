using AuthService.Application.Services;
using AuthService.Application.SPIs;
using AuthService.Domain.APIs;
using AuthService.Infrastructure.CommandQueryProvisers;
using AuthService.Infrastructure.Repositories;

namespace AuthService.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            //MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
            });

            // SPIs
            services.AddScoped<IAuthSpi, AuthRepository>();

            // Services
            services.AddScoped<IAuthService, AuthServiceImpl>();

            return services;
        }
    }
}
