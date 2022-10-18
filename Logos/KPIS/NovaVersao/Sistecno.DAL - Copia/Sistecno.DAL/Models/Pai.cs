using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Pai
    {
        public Pai()
        {
            this.Estadoes = new List<Estado>();
            this.Moedas = new List<Moeda>();
            this.RegiaoItems = new List<RegiaoItem>();
        }

        public int IDPais { get; set; }
        public string Sigla { get; set; }
        public string Sigla1 { get; set; }
        public string Nome { get; set; }
        public string CodigoDoBacen { get; set; }
        public virtual ICollection<Estado> Estadoes { get; set; }
        public virtual ICollection<Moeda> Moedas { get; set; }
        public virtual ICollection<RegiaoItem> RegiaoItems { get; set; }
    }
}
