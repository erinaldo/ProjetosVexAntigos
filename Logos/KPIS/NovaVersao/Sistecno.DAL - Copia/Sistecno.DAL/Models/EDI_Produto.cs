using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_Produto
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public int IDProduto { get; set; }
        public string CodigoDeBarras { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public Nullable<decimal> Largura { get; set; }
        public Nullable<decimal> Comprimento { get; set; }
        public Nullable<decimal> PesoLiquido { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public string Especie { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
    }
}
