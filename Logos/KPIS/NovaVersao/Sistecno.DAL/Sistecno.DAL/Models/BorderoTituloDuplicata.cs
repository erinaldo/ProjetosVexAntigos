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
    
    public partial class BorderoTituloDuplicata
    {
        public int IDBorderoTituloDuplicata { get; set; }
        public int IDBordero { get; set; }
        public int IDTituloDuplicata { get; set; }
    
        public virtual Bordero Bordero { get; set; }
        public virtual TituloDuplicata TituloDuplicata { get; set; }
    }
}
