using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ReposicaoRogeCB
    {
        public int IdReposicaoRogeCB { get; set; }
        public Nullable<int> IdReposicaoRogeItem { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Embalagem { get; set; }
        public Nullable<int> Quantidade { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public virtual ReposicaoRogeItem ReposicaoRogeItem { get; set; }
    }
}
