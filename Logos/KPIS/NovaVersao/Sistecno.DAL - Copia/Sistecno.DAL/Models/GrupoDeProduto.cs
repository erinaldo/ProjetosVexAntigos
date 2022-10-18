using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class GrupoDeProduto
    {
        public GrupoDeProduto()
        {
            this.DepositoPlantaLocalizacaoRegras = new List<DepositoPlantaLocalizacaoRegra>();
            this.GrupoDeProduto1 = new List<GrupoDeProduto>();
            this.ProdutoClientes = new List<ProdutoCliente>();
        }

        public int IDGrupoDeProduto { get; set; }
        public int IDCliente { get; set; }
        public string Codigo { get; set; }
        public string CodigoNoCliente { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> ProximoNumero { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public Nullable<int> IDParente { get; set; }
        public string Ativo { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DepositoPlantaLocalizacaoRegra> DepositoPlantaLocalizacaoRegras { get; set; }
        public virtual ICollection<GrupoDeProduto> GrupoDeProduto1 { get; set; }
        public virtual GrupoDeProduto GrupoDeProduto2 { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoClientes { get; set; }
    }
}
