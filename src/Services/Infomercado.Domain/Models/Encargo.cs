using System;

namespace Infomercado.Domain.Models
{
    public class Encargo
    {
        public Encargo(DateTime mes, double restricaoConstrainedOff, double restricaoConstrainedOn,
            double compensacaoSincrona, double outrosServicosAncilares, double despachoSegurancaEnergetica,
            double deslocamentoUsinaHidreletrica, double ressarcimentoDeslocamento,
            double ressarcimentoDespachoReservaPotenciaOperativa, double? encargoImportacaoEnergia, 
            int idPerfilAgente,
            int idParcelaUsina)
        {
            Mes = mes;
            RestricaoConstrainedOff = restricaoConstrainedOff;
            RestricaoConstrainedOn = restricaoConstrainedOn;
            CompensacaoSincrona = compensacaoSincrona;
            OutrosServicosAncilares = outrosServicosAncilares;
            DespachoSegurancaEnergetica = despachoSegurancaEnergetica;
            DeslocamentoUsinaHidreletrica = deslocamentoUsinaHidreletrica;
            RessarcimentoDeslocamento = ressarcimentoDeslocamento;
            RessarcimentoDespachoReservaPotenciaOperativa = ressarcimentoDespachoReservaPotenciaOperativa;
            EncargoImportacaoEnergia = encargoImportacaoEnergia;
            IdPerfilAgente = idPerfilAgente;
            IdParcelaUsina = idParcelaUsina;
        }

        public int Id { get; private set; }
        
        public DateTime Mes { get; private set; }

        public double RestricaoConstrainedOff { get; private set; }
        
        public double RestricaoConstrainedOn { get; private set; }
        
        public double CompensacaoSincrona { get; private set; }
        
        public double OutrosServicosAncilares { get; private set; }
        
        public double DespachoSegurancaEnergetica { get; private set; }
        
        public double DeslocamentoUsinaHidreletrica { get; private set; }
        
        public double RessarcimentoDeslocamento { get; private set; }
        
        public double RessarcimentoDespachoReservaPotenciaOperativa { get; private set; }

        public double? EncargoImportacaoEnergia { get; private set; }
        
        public int IdPerfilAgente { get; private set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }
        
        public int IdParcelaUsina { get; private set; }
        
        public virtual ParcelaUsina ParcelaUsina { get; private set; }


        public void AtualizarRestricaoConstrainedOff(double restricaoConstrainedOff) => RestricaoConstrainedOff = restricaoConstrainedOff;

        public void AtualizarRestricaoConstrainedOn(double restricaoConstrainedOn) => RestricaoConstrainedOn = restricaoConstrainedOn;
        public void AtualizarCompensacaoSincrona(double compensacaoSincrona) => CompensacaoSincrona = compensacaoSincrona;
        public void AtualizarOutrosServicosAncilares(double outrosServicosAncilares) => OutrosServicosAncilares = outrosServicosAncilares;

        public void AtualizarDespachoSegurancaEnergetica(double despachoSegurancaEnergetica) => DespachoSegurancaEnergetica = despachoSegurancaEnergetica;

        public void AtualizarDeslocamentoUsinaHidreletrica(double deslocamentoUsinaHidreletrica) => DeslocamentoUsinaHidreletrica = deslocamentoUsinaHidreletrica;

        public void AtualizarRessarcimentoDeslocamento(double ressarcimentoDeslocamento) => RessarcimentoDeslocamento = ressarcimentoDeslocamento;

        public void AtualizarRessarcimentoDespachoReservaPotenciaOperativa(double ressarcimentoDespachoReservaPotenciaOperativa) => RessarcimentoDespachoReservaPotenciaOperativa = ressarcimentoDespachoReservaPotenciaOperativa;

        public void AtualizarEncargoImportacaoEnergia(double encargoImportacaoEnergia) => EncargoImportacaoEnergia = encargoImportacaoEnergia;
    }          
}