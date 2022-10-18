using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class FilialCidadeSetor
    {
        public int IdFilialCidadeSetor { get; set; }
        public int IdFilial { get; set; }
        public Nullable<int> IdCidade { get; set; }
        public Nullable<int> IdSetor { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Setor Setor { get; set; }
    }
}
