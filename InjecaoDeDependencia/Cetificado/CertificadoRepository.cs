namespace InjecaoDeDependencia
{
    public class CertificadoRepository : ICertificadoRepository
    {
        public void AtualizarSerialNumber(int idCertificado, string serialNumber)
        {

        }

        public CertificadoModel GetById(int idCertificado)
        {
            var certificadoModel = new CertificadoModel
            {
                SerialNumber = idCertificado == 1
                    ? "3EFC8D8A157180AD418722EB68E3355A"
                    : "",
                CaminhoArquivo = @"c:\TMP\Certificado\CampoBelo.pfx",
                Senha = "06091966"
            };

            return certificadoModel;
        }
    }
}
