namespace InjecaoDeDependencia
{
    public interface ICertificadoRepository
    {
        CertificadoModel GetById(int idCertificado);
        void AtualizarSerialNumber(int idCertificado, string serialNumber);
    }
}
