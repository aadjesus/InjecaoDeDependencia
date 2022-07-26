using System;
using System.Security.Cryptography.X509Certificates;

namespace InjecaoDeDependencia
{
    public class CertificadoCteException : Exception
    {
        public CertificadoCteException(Exception innerException, X509Certificate2 certificate) : base("Erro ao tentar carregar o certificado.", innerException)
        {
        }

        public CertificadoCteException(Exception innerException) : base("Erro ao tentar carregar o certificado.", innerException)
        {
        }

        public CertificadoCteException(string mensagem) : base(mensagem)
        {
        }
    }
}
