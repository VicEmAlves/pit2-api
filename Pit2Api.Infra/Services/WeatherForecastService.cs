using Pit2Api.Infra.Repositories.Abstraction;
using Pit2Api.Model.Interfaces;
using Pit2Api.Model.Models;

namespace Pit2Api.Infra.Services
{
    public class WeatherForecastService(IWeatherRepository _repository): IWeatherForecastService
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
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllWeatherForecasts()
        {
            return await _repository.GetAllWeatherForecasts();
        }
    }
}
