using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ResultadoNFENF
    {
        public int IDEntrada { get; set; }
        public Nullable<int> NFEntrada { get; set; }
        public Nullable<System.DateTime> NFEDataDeEmissao { get; set; }
        public Nullable<decimal> NFEValorDoICMS { get; set; }
        public Nullable<int> NFSaida { get; set; }
        public int IDSaida { get; set; }
        public Nullable<System.DateTime> NFSDataDeEmissao { get; set; }
        public string remetente { get; set; }
        public string destinatario { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Estado { get; set; }
        public int NFEIDCliente { get; set; }
        public Nullable<int> Pedido { get; set; }
        public string ProdutoCodigo { get; set; }
        public string ProdutoDescricao { get; set; }
        public Nullable<System.DateTime> NFSDataDoMovimento { get; set; }
        public Nullable<decimal> Aliquota { get; set; }
        public string UFSaida { get; set; }
        public Nullable<decimal> NFSValorDoICMS { get; set; }
        public Nullable<decimal> NFSValorDaNota { get; set; }
        public Nullable<decimal> NFSPesoBruto { get; set; }
        public Nullable<decimal> NFSMetragemCubica { get; set; }
        public Nullable<decimal> NFSVolumes { get; set; }
        public int NFEIDFilial { get; set; }
        public string NFSEntradaSaida { get; set; }
        public string NFSAtivo { get; set; }
        public string NFEEntradaSaida { get; set; }
        public string NFEAtivo { get; set; }
    }
}
