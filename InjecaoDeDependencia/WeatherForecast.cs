using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace InjecaoDeDependencia
{
    public interface IWeatherForecast
    {
        Guid Id { get; set; }

        string Descricao { get; set; }
    }

    public class WeatherForecast : IWeatherForecast
    {
        public WeatherForecast()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Descricao { get; set; }
    } 
}
