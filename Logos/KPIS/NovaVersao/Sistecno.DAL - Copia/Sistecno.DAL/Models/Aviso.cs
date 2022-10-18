using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Aviso
    {
        public int IdAviso { get; set; }
        public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
        public Nullable<int> IdClienteDivisao { get; set; }
        public string Operacao { get; set; }
        public Nullable<int> IdCanalDeVenda { get; set; }
        public Nullable<int> Sequencia { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ClienteDivisao ClienteDivisao { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
