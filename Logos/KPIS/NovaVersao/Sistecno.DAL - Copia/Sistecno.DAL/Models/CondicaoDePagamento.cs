using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CondicaoDePagamento
    {
        public CondicaoDePagamento()
        {
            this.Tituloes = new List<Titulo>();
        }

        public int IDCondicaoDePagamento { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string Condicao { get; set; }
        public virtual ICollection<Titulo> Tituloes { get; set; }
    }
}
