using Microsoft.Extensions.DependencyInjection;

namespace Tap0309;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTap0309(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<Tap0309Reader>();
        return serviceCollection;
    }
}
