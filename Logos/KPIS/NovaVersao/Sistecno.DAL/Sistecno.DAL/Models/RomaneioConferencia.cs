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
    
    public partial class RomaneioConferencia
    {
        public RomaneioConferencia()
        {
            this.RomaneioConferenciaItem = new HashSet<RomaneioConferenciaItem>();
        }
    
        public int IdRomaneioConferencia { get; set; }
        public int IdRomaneio { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> Abertura { get; set; }
        public Nullable<System.DateTime> Inicio { get; set; }
        public Nullable<System.DateTime> Final { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }
    
        public virtual Romaneio Romaneio { get; set; }
        public virtual ICollection<RomaneioConferenciaItem> RomaneioConferenciaItem { get; set; }
    }
}
