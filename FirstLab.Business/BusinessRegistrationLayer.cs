using FirstLab.Business.Infrastructure;
using FirstLab.Business.Interfaces;
using FirstLab.Business.Services;
using FirstLab.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FirstLab.Business;

public static class BusinessLayerRegistration
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataLayer(configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IUserService,UserService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<JwtHandler>();

        return services;
    }
}