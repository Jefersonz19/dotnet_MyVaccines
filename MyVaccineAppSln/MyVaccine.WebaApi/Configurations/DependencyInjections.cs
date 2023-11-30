using MyVaccine.WebaApi.Repositories.Contracts;
using MyVaccine.WebaApi.Repositories.Implementations;
using MyVaccine.WebaApi.Services.Contracts;
using MyVaccine.WebaApi.Services.Implementations;

namespace MyVaccine.WebaApi.Configurations;

public static class DependencyInjections
{
    public static IServiceCollection SetDependencyInjection(this IServiceCollection services)
    {
        #region Repositories Injection
        services.AddScoped<IUserRepository, UserRepository>();
        #endregion

        #region Services Injection
        services.AddScoped<IUserService, UserService>();
        #endregion
        return services;
    }
}
