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
    
    public partial class RomaneioPrevisaoRegiao
    {
        public int IdRomaneioPrevisaoRegiao { get; set; }
        public int IdRomaneioPrevisao { get; set; }
        public int IdRegiao { get; set; }
        public Nullable<int> OrdemDeCarregamento { get; set; }
        public Nullable<System.DateTime> data { get; set; }
    
        public virtual Regiao Regiao { get; set; }
        public virtual RomaneioPrevisao RomaneioPrevisao { get; set; }
    }
}