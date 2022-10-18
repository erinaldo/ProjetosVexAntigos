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
    
    public partial class Produto
    {
        public Produto()
        {
            this.ConferenciaPalletDocVolItem = new HashSet<ConferenciaPalletDocVolItem>();
            this.ConferenciaPalletEntrada = new HashSet<ConferenciaPalletEntrada>();
            this.ConferenciaPalletEntradaLote = new HashSet<ConferenciaPalletEntradaLote>();
            this.PalletDocumentoItem = new HashSet<PalletDocumentoItem>();
            this.ProdutoEmbalagem = new HashSet<ProdutoEmbalagem>();
            this.ProdutoFoto = new HashSet<ProdutoFoto>();
            this.RomaneioConferenciaItem = new HashSet<RomaneioConferenciaItem>();
            this.RomaneioConferenciaItem1 = new HashSet<RomaneioConferenciaItem>();
        }
    
        public int IDProduto { get; set; }
        public string CodigoDeBarras { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public Nullable<decimal> Largura { get; set; }
        public Nullable<decimal> Comprimento { get; set; }
        public Nullable<decimal> PesoLiquido { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public string Especie { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
    
        public virtual ICollection<ConferenciaPalletDocVolItem> ConferenciaPalletDocVolItem { get; set; }
        public virtual ICollection<ConferenciaPalletEntrada> ConferenciaPalletEntrada { get; set; }
        public virtual ICollection<ConferenciaPalletEntradaLote> ConferenciaPalletEntradaLote { get; set; }
        public virtual ICollection<PalletDocumentoItem> PalletDocumentoItem { get; set; }
        public virtual ICollection<ProdutoEmbalagem> ProdutoEmbalagem { get; set; }
        public virtual ICollection<ProdutoFoto> ProdutoFoto { get; set; }
        public virtual ICollection<RomaneioConferenciaItem> RomaneioConferenciaItem { get; set; }
        public virtual ICollection<RomaneioConferenciaItem> RomaneioConferenciaItem1 { get; set; }
    }
}
