using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjecaoDeDependencia.Servico
{
    public class Servico1
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Tipo { get; set; } = "Singleton";
    }
}
