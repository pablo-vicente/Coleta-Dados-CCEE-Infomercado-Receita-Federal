using System;
using System.Globalization;
using System.Net.NetworkInformation;
using ReceitaFederal.Domain.Enums;

namespace ReceitaFederal.Domain.Models
{
    public class Socio
    {
        public Socio(string nome, string numero, TipoSocio tipoSocio, int idEmpresa)
        {
            Nome = nome;
            Numero = ValidarNumeroSocio(numero, tipoSocio);
            TipoSocio = tipoSocio;
            IdEmpresa = idEmpresa;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Numero { get; private set; }
        public TipoSocio TipoSocio { get; private set; }
        public int IdEmpresa { get; private set; }
        public virtual Empresa Empresa { get; private set; }

        private string ValidarNumeroSocio(string numeroSocio, TipoSocio tipoSocio)
        {
            switch (tipoSocio)
            {
                case TipoSocio.PessoalJuridica:
                    return Convert.ToUInt64(numeroSocio, CultureInfo.CurrentCulture)
                        .ToString(@"00\.000\.000\/0000\-00", CultureInfo.CurrentCulture);
                case TipoSocio.PessoaFisica:
                case TipoSocio.Estrangeiro:
                    return numeroSocio;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}