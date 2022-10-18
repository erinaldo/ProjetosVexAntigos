using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Conferencia
    {
        public Conferencia()
        {
            this.ConferenciaItemNaoCadastradoes = new List<ConferenciaItemNaoCadastrado>();
            this.ConferenciaPallets = new List<ConferenciaPallet>();
        }

        public int IdConferencia { get; set; }
        public int IdRomaneio { get; set; }
        public int IdUsuario { get; set; }
        public Nullable<System.DateTime> Inicio { get; set; }
        public Nullable<System.DateTime> Final { get; set; }
        public string Situacao { get; set; }
        public string Chave { get; set; }
        public virtual Romaneio Romaneio { get; set; }
        public virtual ICollection<ConferenciaItemNaoCadastrado> ConferenciaItemNaoCadastradoes { get; set; }
        public virtual ICollection<ConferenciaPallet> ConferenciaPallets { get; set; }
    }
}
