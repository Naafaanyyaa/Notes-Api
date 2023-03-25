using FirstLab.Data.Interfaces;
using FirstLab.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavePets.Data.Interfaces;

namespace FirstLab.Data;

public static class DataLayerRegistration
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoDbFactory>(new MongoDbFactory(configuration.GetValue<string>("ConnectionStrings:MongoDBConnection")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<INoteRepository, NoteRepository>();

        return services;
    }
}