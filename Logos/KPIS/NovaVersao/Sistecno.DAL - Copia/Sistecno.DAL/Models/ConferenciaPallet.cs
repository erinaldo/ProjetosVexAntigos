using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ConferenciaPallet
    {
        public ConferenciaPallet()
        {
            this.ConferenciaPalletDocs = new List<ConferenciaPalletDoc>();
            this.ConferenciaPalletEntradas = new List<ConferenciaPalletEntrada>();
            this.ConferenciaPalletEntradaLotes = new List<ConferenciaPalletEntradaLote>();
            this.ConferenciaPalletProdutoXXXXXes = new List<ConferenciaPalletProdutoXXXXX>();
        }

        public int IdConferenciaPallet { get; set; }
        public int IdConferencia { get; set; }
        public int IdPallet { get; set; }
        public int IdDepositoPlantaLocalizacao { get; set; }
        public virtual Conferencia Conferencia { get; set; }
        public virtual DepositoPlantaLocalizacao DepositoPlantaLocalizacao { get; set; }
        public virtual UnidadeDeArmazenagem UnidadeDeArmazenagem { get; set; }
        public virtual ICollection<ConferenciaPalletDoc> ConferenciaPalletDocs { get; set; }
        public virtual ICollection<ConferenciaPalletEntrada> ConferenciaPalletEntradas { get; set; }
        public virtual ICollection<ConferenciaPalletEntradaLote> ConferenciaPalletEntradaLotes { get; set; }
        public virtual ICollection<ConferenciaPalletProdutoXXXXX> ConferenciaPalletProdutoXXXXXes { get; set; }
    }
}
