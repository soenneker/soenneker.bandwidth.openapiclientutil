using Soenneker.Bandwidth.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Bandwidth.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class BandwidthOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IBandwidthOpenApiClientUtil _openapiclientutil;

    public BandwidthOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IBandwidthOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
