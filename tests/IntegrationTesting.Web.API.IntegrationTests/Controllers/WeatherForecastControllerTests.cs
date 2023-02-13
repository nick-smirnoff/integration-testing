using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace IntegrationTesting.Web.API.IntegrationTests.Controllers
{
    public class WeatherForecastTests : ControllerFixtureBase
    {
        public WeatherForecastTests(TestApplicationFactory factory)
            : base(factory)
        { }

        [Fact]
        public async Task GetWeatherForecast_TestOne()
        {
            WireMockServer
                .Given(Request.Create().WithPath("/weatherapi/locationforecast/2.0/compact"))
                .RespondWith(Response.Create()
                    .WithBodyFromFile("./Mocks/response-weatherapi-locationforecast.json")
                    .WithStatusCode(System.Net.HttpStatusCode.OK));

            var response = await Client.GetAsync("WeatherForecast");

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task GetWeatherForecast_TestTwo()
        {
            WireMockServer
                .Given(Request.Create().WithPath("/weatherapi/locationforecast/2.0/compact"))
                .RespondWith(Response.Create()
                    .WithBody("Something went wrong")
                    .WithStatusCode(System.Net.HttpStatusCode.Forbidden));

            var response = await Client.GetAsync("WeatherForecast");

            response.IsSuccessStatusCode.Should().BeFalse();
        }
    }
}