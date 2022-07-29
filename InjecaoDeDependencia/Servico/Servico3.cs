using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjecaoDeDependencia.Servico
{
    public class Servico3
    {
        private readonly Servico1 _servico1;
        private readonly Servico2 _servico2;
        private readonly Servico2 _servico2Novo;

        public Servico3(
            Servico1 servico1,
            Servico2 servico2,
            Servico2 servico2Novo)
        {
            _servico1 = servico1;
            _servico2 = servico2;
            _servico2Novo = servico2Novo;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid Servico1Id => _servico1.Id;
        public Guid Servico2Id => _servico2.Id;
        public Guid Servico2IdNovo => _servico2Novo.Id;

        public string Tipo { get; set; } = "Transient";
    }
}
