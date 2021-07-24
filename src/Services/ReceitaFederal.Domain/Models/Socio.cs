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
            Numero = numero;
            TipoSocio = tipoSocio;
            IdEmpresa = idEmpresa;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Numero { get; private set; }
        public TipoSocio TipoSocio { get; private set; }
        public int IdEmpresa { get; private set; }
        public virtual Empresa Empresa { get; private set; }

    }
}