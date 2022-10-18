using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Feriado
    {
        public int IdFeriado { get; set; }
        public Nullable<int> IdCidade { get; set; }
        public Nullable<int> IdEstado { get; set; }
        public System.DateTime Data { get; set; }
        public string Descricao { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
