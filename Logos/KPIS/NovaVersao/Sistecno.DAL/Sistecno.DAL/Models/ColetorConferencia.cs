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
    
    public partial class ColetorConferencia
    {
        public ColetorConferencia()
        {
            this.ColetorConferenciaItem = new HashSet<ColetorConferenciaItem>();
            this.ColetorConferenciaVolume = new HashSet<ColetorConferenciaVolume>();
        }
    
        public int IdColetorConferencia { get; set; }
        public int IdFilial { get; set; }
        public int IdUsuario { get; set; }
        public int IdDocumento { get; set; }
        public System.DateTime Data { get; set; }
        public string Status { get; set; }
        public Nullable<int> VolumesFaltantes { get; set; }
        public string CodigoRetorno { get; set; }
        public string DescricaoRetorno { get; set; }
    
        public virtual Documento Documento { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<ColetorConferenciaItem> ColetorConferenciaItem { get; set; }
        public virtual ICollection<ColetorConferenciaVolume> ColetorConferenciaVolume { get; set; }
    }
}