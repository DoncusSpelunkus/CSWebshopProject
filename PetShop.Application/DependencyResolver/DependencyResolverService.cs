using Factory.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Factory.Application.DependencyResolver;

public static class DependencyResolverService
{
    public static void RegisterApplicationLayer(IServiceCollection services)
    {
        services.AddScoped<IShopService, ShopService>();
    }
}