using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Frota
    {
        public Frota()
        {
            this.FrotaGrupoItemControleItems = new List<FrotaGrupoItemControleItem>();
        }

        public int IDFrota { get; set; }
        public decimal NumeroDaFrota { get; set; }
        public virtual ICollection<FrotaGrupoItemControleItem> FrotaGrupoItemControleItems { get; set; }
        public virtual Veiculo Veiculo { get; set; }
    }
}
