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
        public async Task GetNote_TestOne()
        {
            var response = await Client.GetAsync("Notes/0");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetNote_TestTwo()
        {
            var note = new Notes.Note()
            {
                Content = "This is a test"
            };
            // EF will auto generate the id property
            await SetupDbContextAsync(context =>
            {
                context.Notes.Add(note);
            });

            var response = await Client.GetAsync($"Notes/{note.Id}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}