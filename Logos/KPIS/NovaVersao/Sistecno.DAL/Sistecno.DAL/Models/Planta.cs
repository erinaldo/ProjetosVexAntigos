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
    
    public partial class Planta
    {
        public int IDPlanta { get; set; }
        public int IDFilial { get; set; }
        public string Descricao { get; set; }
        public string Endereco { get; set; }
        public Nullable<decimal> AreaTotal { get; set; }
        public Nullable<decimal> AreaUtil { get; set; }
        public Nullable<decimal> Largura { get; set; }
        public Nullable<decimal> Profundidade { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public Nullable<decimal> LocalLarguraPadrao { get; set; }
        public Nullable<decimal> LocalProfundidadePadrao { get; set; }
        public Nullable<decimal> LocalAlturaPadrao { get; set; }
        public Nullable<decimal> LocalCapacidadePadrao { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Ativo { get; set; }
        public Nullable<int> IDPLANTALEIAUTE { get; set; }
    
        public virtual Filial Filial { get; set; }
    }
}