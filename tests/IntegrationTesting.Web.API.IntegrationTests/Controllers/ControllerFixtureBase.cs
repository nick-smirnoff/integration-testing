using IntegrationTesting.Web.API.Notes;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Server;

namespace IntegrationTesting.Web.API.IntegrationTests.Controllers
{
    public abstract class ControllerFixtureBase : IClassFixture<TestApplicationFactory>, IAsyncLifetime
    {
        protected TestApplicationFactory Factory { get; }
        protected HttpClient Client { get; }
        protected WireMockServer WireMockServer { get; }

        protected ControllerFixtureBase(TestApplicationFactory factory)
        {
            Factory = factory;
            Client = factory.CreateClient();
            WireMockServer = factory.Services.GetRequiredService<WireMockServer>();
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            WireMockServer.Reset();
            return Task.CompletedTask;
        }

        protected async Task SetupDbContextAsync(Action<NotesDbContext> action)
        {
            using var scope = Factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
            action(context);
            await context.SaveChangesAsync();
        }
    }
}