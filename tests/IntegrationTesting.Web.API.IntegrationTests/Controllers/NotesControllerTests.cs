using FluentAssertions;

namespace IntegrationTesting.Web.API.IntegrationTests.Controllers
{
    public class NotesControllerTests : ControllerFixtureBase
    {
        public NotesControllerTests(TestApplicationFactory factory)
            : base(factory)
        { }

        [Fact]
        public async Task GetNotes_TestOne()
        {
            var response = await Client.GetAsync("Notes");

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task GetNotes_TestTwo()
        {
            var response = await Client.GetAsync("Notes");

            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}