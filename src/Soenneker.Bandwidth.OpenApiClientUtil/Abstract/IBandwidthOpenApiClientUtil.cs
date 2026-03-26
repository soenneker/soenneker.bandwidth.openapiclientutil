using Soenneker.Bandwidth.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Bandwidth.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IBandwidthOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<BandwidthOpenApiClient> Get(CancellationToken cancellationToken = default);
}
