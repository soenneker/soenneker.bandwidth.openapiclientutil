using Soenneker.Bandwidth.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Bandwidth.OpenApiClientUtil.Abstract;

/// <summary>
/// Creates and caches an authenticated <see cref="BandwidthOpenApiClient"/>.
/// </summary>
public interface IBandwidthOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached client, creating it on first use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel initial client creation.</param>
    /// <returns>The cached generated client.</returns>
    ValueTask<BandwidthOpenApiClient> Get(CancellationToken cancellationToken = default);
}
