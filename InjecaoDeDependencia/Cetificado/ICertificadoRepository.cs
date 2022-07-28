namespace InjecaoDeDependencia
{
    public interface ICertificadoRepository
    {
        CertificadoModel GetById(int id);
        void AtualizarSerialNumber(int id, string serialNumber);
    }
}
