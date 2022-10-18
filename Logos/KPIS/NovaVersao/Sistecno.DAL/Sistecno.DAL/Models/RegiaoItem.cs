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
    
    public partial class RegiaoItem
    {
        public RegiaoItem()
        {
            this.CadastroRegiao = new HashSet<CadastroRegiao>();
        }
    
        public int IDRegiaoItem { get; set; }
        public int IDRegiao { get; set; }
        public Nullable<int> IDCadastro { get; set; }
        public Nullable<int> IDSetor { get; set; }
        public Nullable<int> IDCidade { get; set; }
        public Nullable<int> IDEstado { get; set; }
        public Nullable<int> IDPais { get; set; }
        public Nullable<int> IDRoteirizacaoTipo { get; set; }
        public Nullable<int> Ordem { get; set; }
        public Nullable<decimal> Distancia { get; set; }
        public Nullable<int> Ordenar { get; set; }
    
        public virtual Cadastro Cadastro { get; set; }
        public virtual ICollection<CadastroRegiao> CadastroRegiao { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual Regiao Regiao { get; set; }
        public virtual RoteirizacaoTipo RoteirizacaoTipo { get; set; }
        public virtual Setor Setor { get; set; }
    }
}
