using InjecaoDeDependencia.Servico;
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
    public class TesteController : ControllerBase
    {
        private readonly ICertificadoUseCase _certificadoUseCase;
        private readonly Servico1 _servico1;
        private readonly Servico2 _servico2;
        private readonly Servico3 _servico3;

        public TesteController(
            ICertificadoUseCase certificadoHelperBase,
            Servico1 servico1,
            Servico2 servico2,
            Servico3 servico3)
        {
            _servico1 = servico1;
            _servico2 = servico2;
            _servico3 = servico3;
            _certificadoUseCase = certificadoHelperBase;
        }


        [HttpGet(nameof(GetCertificado))]
        public IActionResult GetCertificado(int id)
        {
            return Ok(_certificadoUseCase.Obter(id));
        }

        [HttpGet(nameof(InjecaoDeDependência))]
        public IActionResult InjecaoDeDependência(int id)
        {           
            return Ok(new 
            {
                a = _servico1,
                b = _servico2,
                c = _servico3
            });
        }
    }
}
