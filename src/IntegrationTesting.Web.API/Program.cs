using IntegrationTesting.Web.API.Notes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NotesDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("SqlServer");
    if (string.IsNullOrEmpty(connection))
    {
        options.UseInMemoryDatabase(databaseName: "Notes");
    }
    else
    {
        options.UseSqlServer(connection);
    }
});
builder.Services.AddHttpClient("weatherclient", options =>
{
    options.BaseAddress = new Uri(builder.Configuration["WeatherApi:BaseUrl"]);
    options.DefaultRequestHeaders.UserAgent.ParseAdd("integration-testing-prototype");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
