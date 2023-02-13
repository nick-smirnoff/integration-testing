using IntegrationTesting.Web.API.Weather;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace IntegrationTesting.Web.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly HttpClient _client;

        public WeatherForecastController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("weatherclient");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecast()
        {
            try
            {
                var response = await _client.GetAsync("weatherapi/locationforecast/2.0/compact?lat=51.5&lon=0");
                var content = await response.Content.ReadAsStringAsync();
                var json = JsonObject.Parse(content);
                var timeseries = json["properties"]["timeseries"].AsArray();
                var result = timeseries.Select(node =>
                {
                    return new WeatherForecast()
                    {
                        Date = node["time"].ToString(),
                        Temperature = node["data"]["instant"]["details"]["air_temperature"].ToString(),
                    };
                }).ToList();

                return Ok(result);
            }
            catch
            {
                return StatusCode(418);
            }
        }
    }
}