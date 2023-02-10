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
    }
}