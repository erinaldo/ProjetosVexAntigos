using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UnidadeDeArmazenagem
    {
        public UnidadeDeArmazenagem()
        {
            this.ConferenciaPallets = new List<ConferenciaPallet>();
            this.InventarioContagemProdutoes = new List<InventarioContagemProduto>();
            this.MovimentacaoRomaneioItems = new List<MovimentacaoRomaneioItem>();
            this.PalletDocumentoes = new List<PalletDocumento>();
            this.UnidadeDeArmazenagemAgrups = new List<UnidadeDeArmazenagemAgrup>();
            this.UnidadeDeArmazenagemLotes = new List<UnidadeDeArmazenagemLote>();
        }

        public int IDUnidadeDeArmazenagem { get; set; }
        public int IDFilial { get; set; }
        public int IDDepositoPlantaLocalizacao { get; set; }
        public Nullable<System.DateTime> Impressao { get; set; }
        public virtual ICollection<ConferenciaPallet> ConferenciaPallets { get; set; }
        public virtual DepositoPlantaLocalizacao DepositoPlantaLocalizacao { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual ICollection<InventarioContagemProduto> InventarioContagemProdutoes { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItems { get; set; }
        public virtual Pallet Pallet { get; set; }
        public virtual ICollection<PalletDocumento> PalletDocumentoes { get; set; }
        public virtual ICollection<UnidadeDeArmazenagemAgrup> UnidadeDeArmazenagemAgrups { get; set; }
        public virtual ICollection<UnidadeDeArmazenagemLote> UnidadeDeArmazenagemLotes { get; set; }
    }
}
