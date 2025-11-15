using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pit2Api.Model.Interfaces
{
    public interface IWeatherForecastService
    {
        public Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();
    }
}
