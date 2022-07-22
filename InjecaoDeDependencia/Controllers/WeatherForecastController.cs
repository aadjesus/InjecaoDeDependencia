using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjecaoDeDependencia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecast _weatherForecast;
        private readonly ICertificadoHelperBase _certificadoHelperBase;

        public WeatherForecastController(
            IWeatherForecast weatherForecast,
            ICertificadoHelperBase certificadoHelperBase)
        {
            _weatherForecast = weatherForecast;
            _certificadoHelperBase = certificadoHelperBase;
        }

        [HttpGet(nameof(GetTeste))]
        public IActionResult GetTeste()
        {
            return Ok(_weatherForecast);
        }

        [HttpGet(nameof(GetTeste1))]
        public IActionResult GetTeste1(int id)
        {
            return Ok(_certificadoHelperBase.Obter(id));
        }
    }
}
