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
    
    public partial class ArquivoItem
    {
        public int IdArquivoItem { get; set; }
        public int IdArquivo { get; set; }
        public string NomeDoArquivo { get; set; }
        public byte[] ConteudoArquivo { get; set; }
    
        public virtual Arquivo Arquivo { get; set; }
    }
}
