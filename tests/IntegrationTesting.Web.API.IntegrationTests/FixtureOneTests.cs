using FluentAssertions;

namespace IntegrationTesting.Web.API.IntegrationTests
{
    public class FixtureOneTests : IClassFixture<TestApplicationFactory>, IAsyncLifetime
    {
        private readonly TestApplicationFactory _factory;
        private readonly HttpClient _client;

        public FixtureOneTests(TestApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        public Task InitializeAsync()
        {
            return _factory.InitializeAsync();
        }

        public Task DisposeAsync()
        {
            return _factory.DisposeAsync();
        }

        [Fact]
        public async Task GetWeatherForecast_TestOne()
        {
            var response = await _client.GetAsync("WeatherForecast/GetWeatherForecast");

            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}