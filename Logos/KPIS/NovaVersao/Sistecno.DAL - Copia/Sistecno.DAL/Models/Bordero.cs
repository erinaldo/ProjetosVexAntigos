using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Bordero
    {
        public Bordero()
        {
            this.BorderoTituloDuplicatas = new List<BorderoTituloDuplicata>();
        }

        public int IDBordero { get; set; }
        public int IDFilial { get; set; }
        public string Tipo { get; set; }
        public int Numero { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> IdBancoConta { get; set; }
        public Nullable<decimal> ValorTotal { get; set; }
        public string Descricao { get; set; }
        public string Modalidade { get; set; }
        public string TipoDeAgendamento { get; set; }
        public string TipoDeMovimento { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual ICollection<BorderoTituloDuplicata> BorderoTituloDuplicatas { get; set; }
    }
}
