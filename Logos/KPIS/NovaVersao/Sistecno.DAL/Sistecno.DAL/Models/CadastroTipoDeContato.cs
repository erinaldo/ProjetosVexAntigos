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
    
    public partial class CadastroTipoDeContato
    {
        public CadastroTipoDeContato()
        {
            this.CadastroContatoEndereco = new HashSet<CadastroContatoEndereco>();
        }
    
        public int IDCadastroTipoDeContato { get; set; }
        public string Nome { get; set; }
    
        public virtual ICollection<CadastroContatoEndereco> CadastroContatoEndereco { get; set; }
    }
}
