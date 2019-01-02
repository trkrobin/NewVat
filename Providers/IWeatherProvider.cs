using System.Collections.Generic;
using newvat.Models;

namespace newvat.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
