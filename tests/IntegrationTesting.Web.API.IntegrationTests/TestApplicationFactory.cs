using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTesting.Web.API.IntegrationTests
{
    public class TestApplicationFactory : WebApplicationFactory<IApplicationMarker>, IAsyncLifetime
    {
        public TestApplicationFactory()
        {

        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public new Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }
    }
}