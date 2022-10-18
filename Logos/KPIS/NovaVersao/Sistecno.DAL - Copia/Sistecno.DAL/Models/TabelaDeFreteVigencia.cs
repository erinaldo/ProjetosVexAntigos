using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TabelaDeFreteVigencia
    {
        public TabelaDeFreteVigencia()
        {
            this.TabelaDeFreteRotaModalValors = new List<TabelaDeFreteRotaModalValor>();
        }

        public int IDTabelaDeFreteVigencia { get; set; }
        public int IDTabelaDeFrete { get; set; }
        public System.DateTime Data { get; set; }
        public virtual TabelaDeFrete TabelaDeFrete { get; set; }
        public virtual ICollection<TabelaDeFreteRotaModalValor> TabelaDeFreteRotaModalValors { get; set; }
    }
}
