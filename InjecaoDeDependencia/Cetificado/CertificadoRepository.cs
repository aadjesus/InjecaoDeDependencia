namespace InjecaoDeDependencia
{
    public class CertificadoRepository : ICertificadoRepository
    {
        public void AtualizarSerialNumber(int id, string serialNumber)
        {

        }

        public CertificadoModel GetById(int id)
        {
            var certificadoModel = new CertificadoModel
            {
                SerialNumber = id == 1
                    ? "7B594C7E03F68AA18A69E7CA577EB3C663A0DFBC"
                    : "",
                CaminhoArquivo = @"c:\TMP\Certificado\Transtassi.pfx",
                Senha = "12345678"
            };

            return certificadoModel;
        }
    }
}
