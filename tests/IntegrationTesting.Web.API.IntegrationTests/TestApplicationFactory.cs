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
            throw new NotImplementedException();
        }

        public new Task DisposeAsync()
        {
            throw new NotImplementedException();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }
    }
}