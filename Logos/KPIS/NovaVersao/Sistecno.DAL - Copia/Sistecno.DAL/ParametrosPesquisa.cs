using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.DAL
{

    public class ParametrosPesquisa
    {
        public string Campo { get; set; }
        public string Valor { get; set; }
        public string Tipo { get; set; }


        public ParametrosPesquisa()
        {

        }
        public ParametrosPesquisa(string campo, string valor, string tipo)
        {
            Campo = campo;
            Valor = valor;
            Tipo = tipo;
        }
    }
}