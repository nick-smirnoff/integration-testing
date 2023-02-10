using FluentAssertions;

namespace IntegrationTesting.Web.API.IntegrationTests
{
    public class NotesTests : IClassFixture<TestApplicationFactory>, IAsyncLifetime
    {
        private readonly TestApplicationFactory _factory;
        private readonly HttpClient _client;

        public NotesTests(TestApplicationFactory factory)
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
        public async Task GetNotes_TestOne()
        {
            var response = await _client.GetAsync("Notes");

            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}