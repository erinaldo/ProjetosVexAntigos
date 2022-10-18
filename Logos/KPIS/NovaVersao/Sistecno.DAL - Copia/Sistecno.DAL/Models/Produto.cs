using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Produto
    {
        public Produto()
        {
            this.ConferenciaPalletDocVolItems = new List<ConferenciaPalletDocVolItem>();
            this.ConferenciaPalletEntradas = new List<ConferenciaPalletEntrada>();
            this.ConferenciaPalletEntradaLotes = new List<ConferenciaPalletEntradaLote>();
            this.PalletDocumentoItems = new List<PalletDocumentoItem>();
            this.ProdutoEmbalagems = new List<ProdutoEmbalagem>();
            this.ProdutoFotoes = new List<ProdutoFoto>();
            this.RomaneioConferenciaItems = new List<RomaneioConferenciaItem>();
            this.RomaneioConferenciaItems1 = new List<RomaneioConferenciaItem>();
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
        public virtual ICollection<ConferenciaPalletDocVolItem> ConferenciaPalletDocVolItems { get; set; }
        public virtual ICollection<ConferenciaPalletEntrada> ConferenciaPalletEntradas { get; set; }
        public virtual ICollection<ConferenciaPalletEntradaLote> ConferenciaPalletEntradaLotes { get; set; }
        public virtual ICollection<PalletDocumentoItem> PalletDocumentoItems { get; set; }
        public virtual ICollection<ProdutoEmbalagem> ProdutoEmbalagems { get; set; }
        public virtual ICollection<ProdutoFoto> ProdutoFotoes { get; set; }
        public virtual ICollection<RomaneioConferenciaItem> RomaneioConferenciaItems { get; set; }
        public virtual ICollection<RomaneioConferenciaItem> RomaneioConferenciaItems1 { get; set; }
    }
}
