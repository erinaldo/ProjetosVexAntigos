using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EstoqueOperacao
    {
        public EstoqueOperacao()
        {
            this.EstoqueDivisaoMovs = new List<EstoqueDivisaoMov>();
        }

        public int IDEstoqueOperacao { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string Sistema { get; set; }
        public virtual ICollection<EstoqueDivisaoMov> EstoqueDivisaoMovs { get; set; }
    }
}
