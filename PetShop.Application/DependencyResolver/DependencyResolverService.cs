using Microsoft.Extensions.DependencyInjection;
using PetShop.Application.Interfaces;

namespace PetShop.Application.DependencyResolver;

public static class DependencyResolverService
{
    public static void RegisterApplicationLayer(IServiceCollection services)
    {
        services.AddScoped<IShopService, ShopService>();
    }
}