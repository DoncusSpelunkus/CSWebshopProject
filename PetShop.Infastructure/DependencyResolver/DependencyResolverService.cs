using Factory.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Factory.Infastructure.DependencyResolver;

public static class DependencyResolverService
{
    public static void RegisterInfrastructureLayer(IServiceCollection services)
    {
        services.AddScoped<IShopRepo, ShopRepo>();
    }
}