using AeroDesk.Application.Common.Interfaces;
using AeroDesk.Infrastructure.BackgroundServices;
using AeroDesk.Infrastructure.Email;
using AeroDesk.Infrastructure.FileStorage;
using AeroDesk.Infrastructure.Persistence;
using AeroDesk.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AeroDesk.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<IEmailService, SmtpEmailService>();
            services.AddHostedService<FlightNotificationBackgroundService>();

            // NEW: Current User Service for Authorization
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}