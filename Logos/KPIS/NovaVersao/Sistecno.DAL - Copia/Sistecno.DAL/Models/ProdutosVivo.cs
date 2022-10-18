using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProdutosVivo
    {
        public Nullable<long> linha { get; set; }
        public byte[] FOTO { get; set; }
        public string CODIGODOCLIENTE { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string PERECIVEL { get; set; }
        public string VALIDADE { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public int SALDODIVISAODISPONIVEL { get; set; }
        public string Conteudo { get; set; }
        public string UnidadeDeMedida { get; set; }
        public Nullable<decimal> Altura { get; set; }
        public Nullable<decimal> Largura { get; set; }
        public Nullable<decimal> Comprimento { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public Nullable<decimal> PesoLiquido { get; set; }
        public string RODOVIARIO { get; set; }
        public string AERIO { get; set; }
        public Nullable<System.DateTime> DataLimiteDeUso { get; set; }
    }
}
