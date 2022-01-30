using API.Services.Providers;
using API.Services.Services;
using API.Services.Users;
using Domain.Interfaces;
using Domain.Users;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services
            , IConfiguration configuration)
        {
            return services.AddDbContext<EFContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("DDDConnectionString")));
        }

        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            return services
                .AddScoped<UserService>();
        }

        public static IServiceCollection AddProviderServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ProviderService>()
                .AddScoped<ServiceService>();
        }
    }
}