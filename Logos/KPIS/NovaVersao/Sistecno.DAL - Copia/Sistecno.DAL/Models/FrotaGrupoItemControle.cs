using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class FrotaGrupoItemControle
    {
        public FrotaGrupoItemControle()
        {
            this.FrotaGrupoItemControleItems = new List<FrotaGrupoItemControleItem>();
        }

        public int IDFrotaGrupoItemControle { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<FrotaGrupoItemControleItem> FrotaGrupoItemControleItems { get; set; }
    }
}
