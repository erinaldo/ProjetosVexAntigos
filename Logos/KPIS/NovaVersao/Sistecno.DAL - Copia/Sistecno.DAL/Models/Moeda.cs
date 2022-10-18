using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Moeda
    {
        public Moeda()
        {
            this.MoedaCotacaos = new List<MoedaCotacao>();
        }

        public int IDMoeda { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Simbolo { get; set; }
        public string SimboloMonetario { get; set; }
        public Nullable<int> IDPais { get; set; }
        public string UtilizadaNasCotacoes { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public virtual Pai Pai { get; set; }
        public virtual ICollection<MoedaCotacao> MoedaCotacaos { get; set; }
    }
}
