//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistecno.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MovimentacaoRomaneio
    {
        public MovimentacaoRomaneio()
        {
            this.MovimentacaoRomaneioItem = new HashSet<MovimentacaoRomaneioItem>();
        }
    
        public int IDMovimentacaoRomaneio { get; set; }
        public Nullable<int> IDMovimentacao { get; set; }
        public Nullable<int> IDRomaneio { get; set; }
    
        public virtual Movimentacao Movimentacao { get; set; }
        public virtual Romaneio Romaneio { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItem { get; set; }
    }
}