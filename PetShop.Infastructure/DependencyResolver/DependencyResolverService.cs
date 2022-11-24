using PetShop.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace PetShop.Infastructure.DependencyResolver;

public static class DependencyResolverService
{
    public static void RegisterInfrastructureLayer(IServiceCollection services)
    {
        services.AddScoped<IShopRepo, ShopRepo>();
    }
}