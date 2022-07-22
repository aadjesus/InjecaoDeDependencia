using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace InjecaoDeDependencia
{
    public interface IWeatherForecast
    {
        Guid Id { get; set; }

        string Descricao { get; set; }
    }

    public class WeatherForecast : IWeatherForecast
    {
        public WeatherForecast() 
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Descricao { get; set; }
    }


    public interface ICertificadoHelperBase 
    {
        X509Certificate2 Obter(int idCertificado);
    }

    public class CertificadoHelperBase : ICertificadoHelperBase , IDisposable        
    {
        private readonly X509Store _x509Store;

        public CertificadoHelperBase()
        {
            _x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            _x509Store.Open(OpenFlags.ReadWrite);
        }

        public void Dispose()
        {
            _x509Store?.Close();
        }

        public X509Certificate2 Obter(int idCertificado)
        {
            var certificadoModel = new CertificadoModel 
            {
                 SerialNumber = idCertificado == 1 
                    ? "3EFC8D8A157180AD418722EB68E3355A"
                    : ""
            };

            if (string.IsNullOrEmpty(certificadoModel.SerialNumber) ||
                _x509Store
                    .Certificates
                    .OfType<X509Certificate2>()
                    .FirstOrDefault(f => f.SerialNumber == certificadoModel.SerialNumber) is var certificado && certificado == null)
            {
                certificado = new X509Certificate2(
                    certificadoModel.CaminhoArquivo.Trim(),
                    certificadoModel.Senha,
                    X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

                _x509Store.Add(certificado);                
            }

            return certificado;
        }

        private X509Certificate2 Validar(X509Certificate2 certificado)
        {
            if (certificado.NotAfter < DateTime.UtcNow.ToLocalTime())
                throw new Exception();

            if (certificado.NotBefore > DateTime.UtcNow.ToLocalTime())
                throw new Exception();

            return certificado;
        }
    }

    public class CertificadoModel 
    {
        public virtual string CaminhoArquivo { get; set; }
        public virtual string Senha { get; set; }
        public virtual string SerialNumber { get; set; }
    }
}
