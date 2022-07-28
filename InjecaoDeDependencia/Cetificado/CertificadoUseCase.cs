using System;
using System.Security.Cryptography.X509Certificates;

namespace InjecaoDeDependencia
{
    public class CertificadoUseCase : ICertificadoUseCase, IDisposable
    {
        private X509Certificate2 _certificate2;
        private readonly ICertificadoRepository _certificadoRepository;

        public CertificadoUseCase(ICertificadoRepository certificadoRepository)
        {
            _certificadoRepository = certificadoRepository;
        }

        public void Dispose()
        {
            _certificate2?.Dispose();

            GC.SuppressFinalize(this);
        }

        public X509Certificate2 Obter(int id)
        {
            var certificadoModel = _certificadoRepository.GetById(id);

            _certificate2 = new X509Certificate2(
                certificadoModel.CaminhoArquivo.Trim(),
                certificadoModel.Senha,
                X509KeyStorageFlags.EphemeralKeySet); // ñ cria o arquivo fisicamente

            if (string.IsNullOrEmpty(certificadoModel.SerialNumber))
                _certificadoRepository.AtualizarSerialNumber(
                    certificadoModel.Id,
                    _certificate2.SerialNumber);

            if (_certificate2.NotAfter < DateTime.UtcNow.ToLocalTime())
                throw new CertificadoCteException(string.Concat(eMensagens.CertificadoExpirado, " - ", _certificate2.NotAfter));

            if (_certificate2.NotBefore > DateTime.UtcNow.ToLocalTime())
                throw new CertificadoCteException(string.Concat(eMensagens.CertificadoDataInvalida, " - ", _certificate2.NotBefore));

            return _certificate2;
        }
    }
}
