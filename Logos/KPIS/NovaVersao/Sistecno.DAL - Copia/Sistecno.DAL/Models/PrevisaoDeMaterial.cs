using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class PrevisaoDeMaterial
    {
        public int IDPrevisaoDeMaterial { get; set; }
        public int IDClienteDivisao { get; set; }
        public string Pedido { get; set; }
        public string Fornecedor { get; set; }
        public string Descricao { get; set; }
        public Nullable<int> Quantidade { get; set; }
        public Nullable<System.DateTime> PrevisaoDeEntrega { get; set; }
        public Nullable<System.DateTime> DataDeRecebimentoDoMaterial { get; set; }
        public Nullable<int> QuantidadeRecebidaNoFisico { get; set; }
        public string NotaFiscal { get; set; }
        public Nullable<System.DateTime> DataDeDisponibilidadeDoMaterialNoSite { get; set; }
        public int IDCliente { get; set; }
        public string TipoDeMaterial { get; set; }
        public Nullable<int> QuantidadeRecebidaNoFiscal { get; set; }
        public string VolumesCaixas { get; set; }
        public string QualidadeDoProduto { get; set; }
        public int IDUsuario { get; set; }
        public string TipoDeVeiculo { get; set; }
        public Nullable<int> QtdVeiculo { get; set; }
        public string OpcaoCarga { get; set; }
        public Nullable<int> QtdAjudante { get; set; }
        public Nullable<int> NCM { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public string FornecedorCnpj { get; set; }
        public string Estado { get; set; }
        public string Transportadora { get; set; }
        public string TransportadoraContato { get; set; }
        public Nullable<int> QtdPallets { get; set; }
    }
}
