using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Repositories.Abstraction
{
    public interface IWeatherRepository
    {
        public Task<IEnumerable<WeatherForecast>> GetAllWeatherForecasts();
    }
}
