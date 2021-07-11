using System;
using System.Collections.Generic;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class ParcelaUsina
    {
        public ParcelaUsina(int codigo, string sigla, CaracteristicaParcelaUsina caracteristicaParcelaUsina, DateTime? dataInícioOperacaoComercialCcee)
        {
            Codigo = codigo;
            Sigla = sigla;
            CaracteristicaParcelaUsina = caracteristicaParcelaUsina;
            DataInícioOperacaoComercialCcee = dataInícioOperacaoComercialCcee;
            DadosGeracaoUsina = new List<DadosGeracaoUsina>();
        }

        public int Id { get; private set; }
        
        public int Codigo { get; private set; }
        
        public string Sigla { get; private set; }
        
        public CaracteristicaParcelaUsina CaracteristicaParcelaUsina { get; private set; }
        
        public DateTime? DataInícioOperacaoComercialCcee { get; private set; }

        public ICollection<DadosGeracaoUsina> DadosGeracaoUsina { get; private set; }
        
        public int IdUsina { get; private set; }
        
        public virtual Usina Usina {get; private set; }


        public void AtualizarSigla(string sigla) => Sigla = sigla;
        public void AtualizarcaracteristicaParcelaUsina(CaracteristicaParcelaUsina caracteristicaParcelaUsina) => CaracteristicaParcelaUsina = caracteristicaParcelaUsina;

        public void AtualizarcaracteristicaDataInícioOperacaoComercialCcee(DateTime? dataInícioOperacaoComercialCcee)
        {
            if (dataInícioOperacaoComercialCcee is not  null)
                DataInícioOperacaoComercialCcee = dataInícioOperacaoComercialCcee;
        } 
    }
}