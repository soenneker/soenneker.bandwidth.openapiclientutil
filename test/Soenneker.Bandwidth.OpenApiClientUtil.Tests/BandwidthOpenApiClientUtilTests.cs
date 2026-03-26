using Soenneker.Bandwidth.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Bandwidth.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class BandwidthOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IBandwidthOpenApiClientUtil _openapiclientutil;

    public BandwidthOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IBandwidthOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
