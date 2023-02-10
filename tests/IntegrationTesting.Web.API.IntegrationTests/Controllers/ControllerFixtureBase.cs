using IntegrationTesting.Web.API.Notes;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTesting.Web.API.IntegrationTests.Controllers
{
    public abstract class ControllerFixtureBase : IClassFixture<TestApplicationFactory>, IAsyncLifetime
    {
        protected TestApplicationFactory Factory { get; }
        protected HttpClient Client { get; }

        protected ControllerFixtureBase(TestApplicationFactory factory)
        {
            Factory = factory;
            Client = factory.CreateClient();
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            Client.Dispose();
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