using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Roteirizacao
    {
        public Roteirizacao()
        {
            this.Roteirizacao1 = new List<Roteirizacao>();
            this.Roteirizacao11 = new List<Roteirizacao>();
        }

        public int IDRoteirizacao { get; set; }
        public int IDFilial { get; set; }
        public int IDRoteirizacaoTipo { get; set; }
        public int IDParent { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual ICollection<Roteirizacao> Roteirizacao1 { get; set; }
        public virtual Roteirizacao Roteirizacao2 { get; set; }
        public virtual ICollection<Roteirizacao> Roteirizacao11 { get; set; }
        public virtual Roteirizacao Roteirizacao3 { get; set; }
    }
}
