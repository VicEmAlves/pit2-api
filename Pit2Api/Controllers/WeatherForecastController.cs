using Microsoft.AspNetCore.Mvc;
using Pit2Api.Model.Interfaces;
using Pit2Api.Model.Models;

namespace Pit2Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherForecastService _weatherService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherService)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        [HttpGet("GetWeather")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await _weatherService.GetWeatherForecasts();
        }

        [HttpGet("GetWeatherDataBase")]
        public async Task<IEnumerable<WeatherForecast>> GetDataBase()
        {
            return await _weatherService.GetAllWeatherForecasts();
        }
    }
}
