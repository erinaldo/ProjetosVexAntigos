using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RelControleDePedido
    {
        public int IDPedido { get; set; }
        public Nullable<int> IDNotaFiscal { get; set; }
        public Nullable<int> NumPedido { get; set; }
        public Nullable<int> NumNF { get; set; }
        public string Serie { get; set; }
        public string Solicitante { get; set; }
        public string Origem { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string CNPJ_Destinatario { get; set; }
        public string Nome_Fantasia { get; set; }
        public string Razao_Social { get; set; }
        public Nullable<decimal> Vl_Total_NF { get; set; }
        public Nullable<decimal> Qtde_Volumes_NF { get; set; }
        public Nullable<decimal> Peso_Real_Total_NF { get; set; }
        public Nullable<decimal> Peso_Cub_Total_NF { get; set; }
        public string Data_Pedido { get; set; }
        public string Hora_Pedido { get; set; }
        public string Data_Autorizacao { get; set; }
        public string Hora_Autorizacao { get; set; }
        public string Data_Liberacao { get; set; }
        public string Hora_Liberacao { get; set; }
        public string Data_Ini_Separacao { get; set; }
        public string Hora_Ini_Separacao { get; set; }
        public string Data_Fim_Separacao { get; set; }
        public string Hora_Fim_Separacao { get; set; }
        public string Data_Emissao_NF { get; set; }
        public string Hora_Emissao_NF { get; set; }
        public string Previsao_de_Entrega { get; set; }
        public string Data_de_Entrega { get; set; }
        public string Modal_Executado { get; set; }
        public string Documento_1EntradaSaida { get; set; }
        public string Documento_1TipoDeDocumento { get; set; }
        public string DocumentoTipoDeDocumento { get; set; }
        public Nullable<int> DocumentoIDCliente { get; set; }
    }
}
