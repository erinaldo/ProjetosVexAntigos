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
    
    public partial class DepositoPlantaLocalizacaoRegra
    {
        public int IdDepositoPlantaLocalizacaoRegra { get; set; }
        public int IdDepositoPlantaLocalizacao { get; set; }
        public Nullable<int> IdProdutoCliente { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> idGrupoDeProduto { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual DepositoPlantaLocalizacao DepositoPlantaLocalizacao { get; set; }
        public virtual DepositoPlantaLocalizacaoRegra DepositoPlantaLocalizacaoRegra1 { get; set; }
        public virtual DepositoPlantaLocalizacaoRegra DepositoPlantaLocalizacaoRegra2 { get; set; }
        public virtual GrupoDeProduto GrupoDeProduto { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
    }
}