
using CarsManager.Orleans.Application.Cqrs.Queries;

namespace CarsManager.Orleans.Application.Services;
public static class RegisterCqrs
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllAvailableCarsDetailsQuery).Assembly));
        return services;
    }
}