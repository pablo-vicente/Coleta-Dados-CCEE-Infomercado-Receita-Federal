using System;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class ConsumoParcelaCarga
    {
        public ConsumoParcelaCarga(DateTime mes, int codigoCarga, string carga, string cidade, string estado, string ramoAtividade, Submercado submercado, DateTime? dataMigracao, int? codigoPerfilDistribuidora, string siglaPerfilDistribuidora, double capacidadeCargaMw, double consumoAmbienteLivreMWh, double consumoAjustadoParcelaCativaCargaLivreMWh, double consumoAjustadoParcelaCargaMWh, int idPerfilAgente)
        {
            Mes = mes;
            CodigoCarga = codigoCarga;
            Carga = carga;
            Cidade = cidade;
            Estado = estado;
            RamoAtividade = ramoAtividade;
            Submercado = submercado;
            DataMigracao = dataMigracao;
            CodigoPerfilDistribuidora = codigoPerfilDistribuidora;
            SiglaPerfilDistribuidora = siglaPerfilDistribuidora;
            CapacidadeCargaMw = capacidadeCargaMw;
            ConsumoAmbienteLivreMWh = consumoAmbienteLivreMWh;
            ConsumoAjustadoParcelaCativaCargaLivreMWh = consumoAjustadoParcelaCativaCargaLivreMWh;
            ConsumoAjustadoParcelaCargaMWh = consumoAjustadoParcelaCargaMWh;
            IdPerfilAgente = idPerfilAgente;
        }

        public int Id { get; private set; }

        public DateTime Mes { get; private set; }

        public int CodigoCarga { get; private set; }

        public string Carga { get; private set; }

        public string Cidade { get; private set; }
        
        public string Estado { get; private set; }
        
        public string RamoAtividade { get; private set; }
        
        public Submercado Submercado { get; private set; }

        public DateTime? DataMigracao { get; private set; }

        public int? CodigoPerfilDistribuidora { get; private set; }

        public string SiglaPerfilDistribuidora { get; private set; }
        
        public double CapacidadeCargaMw { get; private set; }

        public double ConsumoAmbienteLivreMWh { get; private set; }
        
        public double ConsumoAjustadoParcelaCativaCargaLivreMWh { get; private set; }
        
        public double ConsumoAjustadoParcelaCargaMWh { get; private set; }
        
        public int IdPerfilAgente { get; private set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }

        public void AtualizarCodigoCarga(int codigoCarga) => CodigoCarga = codigoCarga;

        public void AtualizarCarga(string carga)
        {
            if (!string.IsNullOrEmpty(carga))
                Carga = carga;
            
        }
        
        public void AtualizarCidade(string cidade)
        {
            if (!string.IsNullOrEmpty(cidade))
                Cidade = cidade;
            
        }
        
        public void AtualizarEstado(string estado)
        {
            if (!string.IsNullOrEmpty(estado))
                Estado = estado;
            
        }
        
        public void AtualizarRamoAtividade(string ramoAtividade)
        {
            if (!string.IsNullOrEmpty(ramoAtividade))
                RamoAtividade = ramoAtividade;
            
        }

        public void AtualizarSubmercado(Submercado submercado) => Submercado = submercado;

        public void AtualizarDataMigracao(DateTime? dataMigracao)
        {
            if (dataMigracao is not null)
                DataMigracao = dataMigracao;
            
        }

        public void AtualizarCodigoPerfilDistribuidora(int? codigoPerfilDistribuidora)
        {
            if (codigoPerfilDistribuidora is not null)
                CodigoPerfilDistribuidora = codigoPerfilDistribuidora;
        }
        
        public void AtualizarSiglaPerfilDistribuidora(string siglaPerfilDistribuidora)
        {
            if (!string.IsNullOrEmpty(siglaPerfilDistribuidora))
                SiglaPerfilDistribuidora = siglaPerfilDistribuidora;
            
        }

        public void AtualizarCapacidadeCargaMW(double capacidadeCargaMW) => CapacidadeCargaMw = capacidadeCargaMW;

        public void AtualizarConsumoAmbienteLivreMWh(double consumoAmbienteLivreMWh) => ConsumoAmbienteLivreMWh = consumoAmbienteLivreMWh;

        public void AtualizarConsumoAjustadoParcelaCativaCargaLivreMWh(double consumoAjustadoParcelaCativaCargaLivreMWh) => ConsumoAjustadoParcelaCativaCargaLivreMWh = consumoAjustadoParcelaCativaCargaLivreMWh;

        public void AtualizarConsumoAjustadoParcelaCargaMWh(double consumoAjustadoParcelaCargaMWh) => ConsumoAjustadoParcelaCargaMWh = consumoAjustadoParcelaCargaMWh;
    }
}