using Pit2Api.Infra.Repositories.Abstraction;
using Pit2Api.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Repositories.Implementation
{
    public class WeatherRepository(ISqlDatabase _database) : IWeatherRepository
    {

        public async Task<IEnumerable<WeatherForecast>> GetAllWeatherForecasts()
        {
            return await _database.QueryManyAsync<WeatherForecast>(Scripts.GetAllWeatherForecasts);
        }
    }
}
