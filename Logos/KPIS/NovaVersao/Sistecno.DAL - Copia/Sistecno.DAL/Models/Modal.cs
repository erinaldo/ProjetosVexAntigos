using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Modal
    {
        public Modal()
        {
            this.DTs = new List<DT>();
            this.TabelaDeFreteRotaModals = new List<TabelaDeFreteRotaModal>();
        }

        public int IDModal { get; set; }
        public Nullable<int> IDEmpresa { get; set; }
        public string Nome { get; set; }
        public string Cte { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<TabelaDeFreteRotaModal> TabelaDeFreteRotaModals { get; set; }
    }
}
