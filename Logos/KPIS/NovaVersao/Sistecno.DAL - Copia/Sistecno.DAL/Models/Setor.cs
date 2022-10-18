using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Setor
    {
        public Setor()
        {
            this.ClienteSetorFilials = new List<ClienteSetorFilial>();
            this.FilialCidadeSetors = new List<FilialCidadeSetor>();
            this.RegiaoItems = new List<RegiaoItem>();
            this.SetorCeps = new List<SetorCep>();
        }

        public int IDSetor { get; set; }
        public Nullable<int> Codigo { get; set; }
        public string Nome { get; set; }
        public Nullable<int> IdBairro { get; set; }
        public virtual ICollection<ClienteSetorFilial> ClienteSetorFilials { get; set; }
        public virtual ICollection<FilialCidadeSetor> FilialCidadeSetors { get; set; }
        public virtual ICollection<RegiaoItem> RegiaoItems { get; set; }
        public virtual ICollection<SetorCep> SetorCeps { get; set; }
    }
}
