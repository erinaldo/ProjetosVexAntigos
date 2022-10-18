using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoAte
    {
        public int IDDocumentoAte { get; set; }
        public string Numero { get; set; }
        public string TipoVeiculo { get; set; }
        public Nullable<System.DateTime> DataDeEmissao { get; set; }
        public Nullable<System.DateTime> DataDeColeta { get; set; }
        public Nullable<System.DateTime> DataDeEntrega { get; set; }
        public string NumeroOrcamento { get; set; }
        public string MotivoAte { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public string Solicitante { get; set; }
        public string CentroCusto { get; set; }
        public string MesFaturamento { get; set; }
        public Nullable<decimal> CustoATE { get; set; }
        public string Obs { get; set; }
        public string TipoEmissao { get; set; }
        public string TipoColeta { get; set; }
        public string TipoEntrega { get; set; }
    }
}
