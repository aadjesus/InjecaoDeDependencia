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
            _store.Close();            
            _store.Dispose();

            _certificate2?.Reset();
            _certificate2?.Dispose();

            GC.SuppressFinalize(this);
        }

        private X509Certificate2 this[string serialNumber]
        {
            get
            {
                if (_certificate2 == null)
                    _certificate2 = _store.Certificates.Find(X509FindType.FindBySerialNumber, serialNumber, false)[0];

                return _certificate2;
            }
        }

        public X509Certificate2 Obter(int idCertificado)
        {
            var certificadoModel = _certificadoRepository.GetById(idCertificado);

            if (string.IsNullOrEmpty(certificadoModel.SerialNumber) ||
                this[certificadoModel.SerialNumber] == null)
            {
                _certificate2 = new X509Certificate2(
                    certificadoModel.CaminhoArquivo.Trim(),
                    certificadoModel.Senha,
                    X509KeyStorageFlags.EphemeralKeySet); // ñ cria o arquivo fisicamente

                _certificadoRepository.AtualizarSerialNumber(
                    certificadoModel.Id,
                    _certificate2.SerialNumber);

                _store.Add(_certificate2);
            }

            var retorno = this[certificadoModel.SerialNumber];

            if (retorno.NotAfter < DateTime.UtcNow.ToLocalTime())
                throw new CertificadoCteException(string.Concat(eMensagens.CertificadoExpirado, retorno.NotAfter));

            if (retorno.NotBefore > DateTime.UtcNow.ToLocalTime())
                throw new CertificadoCteException(string.Concat(eMensagens.CertificadoDataInvalida, retorno.NotBefore));

            return retorno;
        }
    }
}
