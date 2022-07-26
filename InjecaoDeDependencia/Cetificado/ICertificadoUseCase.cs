using System.Security.Cryptography.X509Certificates;

namespace InjecaoDeDependencia
{
    public interface ICertificadoUseCase
    {
        X509Certificate2 Obter(int idCertificado);
    }
}
