using FluentAssertions;
using IntegrationTesting.Web.API.Notes;
using System.Net.Http.Json;

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
            var note = new Note()
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

        [Fact]
        public async Task CreateNote_TestOne()
        {
            var payload = new CreateNote() { Content = "this is my new message" };
            var response = await Client.PostAsJsonAsync("Notes", payload);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            var result = await response.Content.ReadFromJsonAsync<Note>();
            result.Should().NotBeNull();
            result?.Id.Should().BeGreaterThan(0);
            result?.Content.Should().BeEquivalentTo(payload.Content);
        }

        [Fact]
        public async Task CreateNote_TestTwo()
        {
            var payload = new CreateNote() { Content = "this is my message for future reference" };
            var createResponse = await Client.PostAsJsonAsync("Notes", payload);
            createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            var fetchResponse = await Client.GetAsync(createResponse.Headers.Location);
            fetchResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            var result = await fetchResponse.Content.ReadFromJsonAsync<Note>();
            result.Should().NotBeNull();
            result?.Id.Should().BeGreaterThan(0);
            result?.Content.Should().BeEquivalentTo(payload.Content);
        }

        [Fact]
        public async Task UpdateNote_TestOne()
        {
            var payload = new UpdateNote() { Content = "this note does not exist" };
            var response = await Client.PutAsJsonAsync("Notes/0", payload);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateNote_TestTwo()
        {
            var existingNote = new Note()
            {
                Content = "this is an outdated note"
            };
            await SetupDbContextAsync(context =>
            {
                context.Notes.Add(existingNote);
            });

            var payload = new UpdateNote() { Content = "this is now an updated note" };
            var response = await Client.PutAsJsonAsync($"Notes/{existingNote.Id}", payload);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<Note>();
            result.Should().NotBeNull();
            result?.Id.Should().Be(existingNote.Id);
            result?.Content.Should().BeEquivalentTo(payload.Content);
        }

        [Fact]
        public async Task DeleteNote_TestOne()
        {
            var response = await Client.DeleteAsync("Notes/0");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteNote_TestTwo()
        {
            var existingNote = new Note()
            {
                Content = "this is an old note that can go in the bin"
            };
            await SetupDbContextAsync(context =>
            {
                context.Notes.Add(existingNote);
            });

            var response = await Client.DeleteAsync($"Notes/{existingNote.Id}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteNote_TestThree()
        {
            var existingNote = new Note()
            {
                Content = "this is an old note that can go in the bin and will no longer exist"
            };
            await SetupDbContextAsync(context =>
            {
                context.Notes.Add(existingNote);
            });

            var deleteResponse = await Client.DeleteAsync($"Notes/{existingNote.Id}");
            deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

            var fetchResponse = await Client.GetAsync($"Notes/{existingNote.Id}");
            fetchResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}