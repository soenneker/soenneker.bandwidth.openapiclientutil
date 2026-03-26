using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Bandwidth.HttpClients.Registrars;
using Soenneker.Bandwidth.OpenApiClientUtil.Abstract;

namespace Soenneker.Bandwidth.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class BandwidthOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="BandwidthOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddBandwidthOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddBandwidthOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IBandwidthOpenApiClientUtil, BandwidthOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="BandwidthOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddBandwidthOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddBandwidthOpenApiHttpClientAsSingleton()
                .TryAddScoped<IBandwidthOpenApiClientUtil, BandwidthOpenApiClientUtil>();

        return services;
    }
}
