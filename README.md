[![](https://img.shields.io/nuget/v/soenneker.bandwidth.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.bandwidth.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.bandwidth.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.bandwidth.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.bandwidth.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.bandwidth.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.bandwidth.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.bandwidth.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Bandwidth.OpenApiClientUtil

Creates and caches an authenticated `BandwidthOpenApiClient` for dependency-injected applications.

## Installation

```bash
dotnet add package Soenneker.Bandwidth.OpenApiClientUtil
```

## Configuration

```json
{
  "Bandwidth": {
    "ApiKey": "your-token",
    "ClientBaseUrl": "https://api.bandwidth.com/api/v2/",
    "AuthHeaderName": "Authorization",
    "AuthHeaderValueTemplate": "Bearer {token}"
  }
}
```

`Bandwidth:ApiKey` is required; the other values shown are defaults. Set the header template to the authentication scheme required by the Bandwidth service you call. For an already Base64-encoded Basic credential, use `Basic {token}`.

This utility configures one base URL and one authentication header. Bandwidth endpoints requiring another host, a different credential set, or multiple authentication headers should use a separately configured generated client.

## Registration

```csharp
using Soenneker.Bandwidth.OpenApiClientUtil.Registrars;

services.AddBandwidthOpenApiClientUtilAsScoped();
```

The scoped utility uses a singleton HTTP-client provider. Ending a scope disposes that utility's generated client state but leaves the singleton provider and its `HttpClient` alive. Use `AddBandwidthOpenApiClientUtilAsSingleton()` when the generated client should also be shared application-wide.

## Usage

```csharp
using Soenneker.Bandwidth.OpenApiClient;
using Soenneker.Bandwidth.OpenApiClient.Models;
using Soenneker.Bandwidth.OpenApiClientUtil.Abstract;

public sealed class BandwidthMessageService
{
    private readonly IBandwidthOpenApiClientUtil _clientUtil;

    public BandwidthMessageService(IBandwidthOpenApiClientUtil clientUtil)
    {
        _clientUtil = clientUtil;
    }

    public async Task<MessagesList?> GetMessages(string accountId, CancellationToken cancellationToken = default)
    {
        BandwidthOpenApiClient client = await _clientUtil.Get(cancellationToken);
        return await client.Messaging.Users[accountId].Messages.GetAsync(cancellationToken: cancellationToken);
    }
}
```

`Get()` lazily creates one generated client per utility instance and returns it afterward. Authentication and base-address configuration are captured during initial creation. Credentials are added only to HTTPS requests and are pinned to the first request host.
