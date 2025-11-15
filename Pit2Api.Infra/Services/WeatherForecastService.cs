using Microsoft.Extensions.Options;
using Pit2Api.Model;
using Pit2Api.Model.Interfaces;
using Pit2Api.Model.Models;
using System.Runtime.InteropServices.Marshalling;

namespace Pit2Api.Infra.Services
{
    public class WeatherForecastService(IOptions<Config> _config): IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
        {
            await Task.Delay(100); // Simulate async operation
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _config.Value.Test// Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}
