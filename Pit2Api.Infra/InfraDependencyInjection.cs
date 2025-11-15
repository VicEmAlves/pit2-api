using Microsoft.Extensions.DependencyInjection;
using Pit2Api.Infra.Services;
using Pit2Api.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pit2Api.Infra
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddInfraDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }
    }
}
