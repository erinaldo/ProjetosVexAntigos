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
    
    public partial class RomaneioPrevisao
    {
        public RomaneioPrevisao()
        {
            this.RomaneioPrevisaoRegiao = new HashSet<RomaneioPrevisaoRegiao>();
        }
    
        public int IdRomaneioPrevisao { get; set; }
        public System.DateTime Data { get; set; }
        public string Observacao { get; set; }
        public string Status { get; set; }
        public Nullable<int> IdRegiao { get; set; }
    
        public virtual ICollection<RomaneioPrevisaoRegiao> RomaneioPrevisaoRegiao { get; set; }
    }
}
