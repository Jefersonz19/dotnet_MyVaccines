using Microsoft.EntityFrameworkCore;
using MyVaccine.WebaApi.Literals;
using MyVaccine.WebaApi.Models;
using System.Runtime.CompilerServices;

namespace MyVaccine.WebaApi.Configurations;

public static class DbConfigurations
{
    public static IServiceCollection SetDatabaseConfiguration(this IServiceCollection Services)
    {
        //var connectionString = Environment.GetEnvironmentVariable(MyVaccineLiterals.CONNECTION_STRING);
        var connectionString = "Server=localhost, 14330; Database=MyVaccineAppDb; User Id=sa; Password=Abc.123456; TrustServerCertificate=True;" ;
        Services.AddDbContext<MyVaccineAppDbContext>(options =>
            options.UseSqlServer(connectionString));

        return Services;
    }
}
