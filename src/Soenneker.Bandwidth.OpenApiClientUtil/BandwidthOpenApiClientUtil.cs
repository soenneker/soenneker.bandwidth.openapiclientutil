using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Bandwidth.HttpClients.Abstract;
using Soenneker.Bandwidth.OpenApiClientUtil.Abstract;
using Soenneker.Bandwidth.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Bandwidth.OpenApiClientUtil;

///<inheritdoc cref="IBandwidthOpenApiClientUtil"/>
public sealed class BandwidthOpenApiClientUtil : IBandwidthOpenApiClientUtil
{
    private readonly AsyncSingleton<BandwidthOpenApiClient> _client;

    public BandwidthOpenApiClientUtil(IBandwidthOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<BandwidthOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Bandwidth:ApiKey");
            string authHeaderValueTemplate = configuration["Bandwidth:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new BandwidthOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<BandwidthOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
