using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class vwLiberacaoDePedidos_P1
    {
        public string TIPODEDOCUMENTO { get; set; }
        public int IDDOCUMENTO { get; set; }
        public int IDFILIAL { get; set; }
        public int IDCLIENTE { get; set; }
        public string RAZAOSOCIALNOME { get; set; }
        public Nullable<System.DateTime> DATADEEMISSAO { get; set; }
        public string FILIAL { get; set; }
        public string SITUACAO_DF { get; set; }
        public Nullable<System.DateTime> DATA_DF { get; set; }
        public string CnpjCpf { get; set; }
        public string EntradaSaida { get; set; }
    }
}
