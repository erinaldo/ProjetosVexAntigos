using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class FrotaGrupoItemControleItem
    {
        public int IDFrotaGrupoItemControleItem { get; set; }
        public int IDFrotaGrupoItemControle { get; set; }
        public int IDFrota { get; set; }
        public virtual Frota Frota { get; set; }
        public virtual FrotaGrupoItemControle FrotaGrupoItemControle { get; set; }
    }
}
