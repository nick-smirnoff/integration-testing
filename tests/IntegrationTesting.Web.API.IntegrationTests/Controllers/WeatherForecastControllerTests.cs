using FluentAssertions;

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
            var response = await Client.GetAsync("WeatherForecast");

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task GetWeatherForecast_TestTwo()
        {
            var response = await Client.GetAsync("WeatherForecast");

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task GetWeatherForecast_TestThree()
        {
            var response = await Client.GetAsync("WeatherForecast");

            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}