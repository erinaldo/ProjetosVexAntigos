using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_ProdutoCliente
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public Nullable<int> IDProdutoCliente { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public Nullable<int> IDGrupoDeProduto { get; set; }
        public Nullable<int> IDDepositoPlantaLocalizacao { get; set; }
        public Nullable<int> IDClienteTipoDeMaterial { get; set; }
        public Nullable<int> IDUnidadeDeMedida { get; set; }
        public Nullable<int> IDContaContabilCredito { get; set; }
        public Nullable<int> IDContaContabilDebito { get; set; }
        public Nullable<int> IDCentroDeCustoCredito { get; set; }
        public Nullable<int> IDCentroDeCustoDebito { get; set; }
        public Nullable<int> IDCfop { get; set; }
        public string Codigo { get; set; }
        public string CodigoDoCliente { get; set; }
        public string Descricao { get; set; }
        public string DesmembraNaNF { get; set; }
        public string MetodoDeMovimentacao { get; set; }
        public string SolicitarDataDeValidade { get; set; }
        public Nullable<decimal> SaldoMinimo { get; set; }
        public Nullable<decimal> ConsumoMensal { get; set; }
        public Nullable<decimal> Ressuprimento { get; set; }
        public Nullable<decimal> UnidadeDoFornecedor { get; set; }
        public Nullable<System.DateTime> DataLimiteDeUso { get; set; }
        public string IsentoDeICMS { get; set; }
        public Nullable<decimal> ReducaoDeICMS { get; set; }
        public string DecretoDoICMS { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<decimal> Lastro { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public string Ativo { get; set; }
        public string GrupoDeProdutoCliente { get; set; }
    }
}