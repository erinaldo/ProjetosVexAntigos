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
    
    public partial class DTHistorico
    {
        public int IdDTHistorico { get; set; }
        public int IdDT { get; set; }
        public string Historico { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<System.DateTime> DataDaOcorrencia { get; set; }
    
        public virtual DT DT { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
