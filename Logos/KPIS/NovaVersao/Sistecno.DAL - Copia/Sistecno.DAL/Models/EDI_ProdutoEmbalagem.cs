using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_ProdutoEmbalagem
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public int IDProdutoEmbalagem { get; set; }
        public int IDProdutoCliente { get; set; }
        public int IDProduto { get; set; }
        public Nullable<int> IDProdutoInterno { get; set; }
        public string Conteudo { get; set; }
        public decimal UnidadeDoCliente { get; set; }
        public Nullable<decimal> UnidadeLogistica { get; set; }
        public string UnidadeDeMedida { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
    }
}
