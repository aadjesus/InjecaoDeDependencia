using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjecaoDeDependencia.Servico
{
    public class Servico2
    {
        private readonly Servico1 _servico1;

        public Servico2(Servico1 servico1)
        {
            _servico1 = servico1;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid Servico1Id => _servico1.Id;
        
        public string Tipo { get; set; } = "Scoped";
    }
}
