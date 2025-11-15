using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pit2Api.Infra.Repositories
{
    public static class Scripts
    {
        public const string GetAllWeatherForecasts = @"
            SELECT Date, TemperatureC, TemperatureF, Summary
            FROM WeatherForecast
        ";
    }
}
