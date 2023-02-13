using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using IntegrationTesting.Web.API.Notes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Server;

namespace IntegrationTesting.Web.API.IntegrationTests
{
    public class TestApplicationFactory : WebApplicationFactory<IApplicationMarker>, IAsyncLifetime
    {
        private readonly TestcontainerDatabase _dbContainer;

        public TestApplicationFactory()
        {
            _dbContainer = new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithDatabase(new MsSqlTestcontainerConfiguration
                {
                    Database = "Notes",
                    Password = "SpeakFriend&Enter",
                })
                .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithName(Guid.NewGuid().ToString("D"))
                .WithCleanUp(true)
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
            await dbContext.Database.MigrateAsync();
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var wireMockServer = WireMockServer.Start();

            builder.UseSetting("WeatherApi:BaseUrl", wireMockServer.Url);
            builder.UseSetting("ConnectionStrings:SqlServer", _dbContainer.ConnectionString);
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(wireMockServer);
            });
            builder.UseEnvironment("Development");
            base.ConfigureWebHost(builder);
        }
    }
}