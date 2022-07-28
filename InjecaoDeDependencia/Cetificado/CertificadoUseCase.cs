using System;
using System.Security.Cryptography.X509Certificates;

namespace InjecaoDeDependencia
{
    public class CertificadoUseCase : ICertificadoUseCase, IDisposable
    {
        private X509Certificate2 _certificate2;
        private readonly X509Store _store;
        private readonly ICertificadoRepository _certificadoRepository;

        public CertificadoUseCase(ICertificadoRepository certificadoRepository)
        {
            _certificadoRepository = certificadoRepository;

            _store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            _store.Open(OpenFlags.ReadWrite);
        }

        public void Dispose()
        {
            _certificate2?.Dispose();
            _store.Dispose();

            GC.SuppressFinalize(this);
        }

        public X509Certificate2 this[int id] =>
            Obter(id);

        private X509Certificate2 this[string serialNumber] =>
            string.IsNullOrEmpty(serialNumber) ||
            _store.Certificates.Find(X509FindType.FindBySerialNumber, serialNumber, false) is var certificado && certificado.Count == 0
                ? null
                : certificado[0];

        public X509Certificate2 Obter(int id)
        {
            var certificadoModel = _certificadoRepository.GetById(id);

            if (this[certificadoModel.SerialNumber] == null)
            {
                _certificate2 = new X509Certificate2(
                    certificadoModel.CaminhoArquivo.Trim(),
                    certificadoModel.Senha,
                    X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

                _certificadoRepository.AtualizarSerialNumber(
                    certificadoModel.Id,
                    _certificate2.SerialNumber);

                _store.Add(_certificate2);
            }

            var retorno = this[certificadoModel.SerialNumber];

            if (retorno.NotAfter < DateTime.UtcNow.ToLocalTime())
                throw new CertificadoCteException(string.Concat(eMensagens.CertificadoExpirado, " - ", retorno.NotAfter));

            if (retorno.NotBefore > DateTime.UtcNow.ToLocalTime())
                throw new CertificadoCteException(string.Concat(eMensagens.CertificadoDataInvalida, " - ", retorno.NotBefore));

            return retorno;
        }
    }
}
