namespace InjecaoDeDependencia
{
    public class CertificadoModel
    {
        public virtual int Id { get; set; }
        public virtual string CaminhoArquivo { get; set; }
        public virtual string Senha { get; set; }
        public virtual string SerialNumber { get; set; }
    }
}
