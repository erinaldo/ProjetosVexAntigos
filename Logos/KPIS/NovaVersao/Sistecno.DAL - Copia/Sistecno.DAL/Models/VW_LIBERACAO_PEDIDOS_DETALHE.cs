using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VW_LIBERACAO_PEDIDOS_DETALHE
    {
        public int IDDOCUMENTO { get; set; }
        public Nullable<int> NUMERO { get; set; }
        public string SERIE { get; set; }
        public string STATUS { get; set; }
        public string TIPODEDOCUMENTO { get; set; }
        public Nullable<System.DateTime> DATAPLANEJADA { get; set; }
        public int IDCliente { get; set; }
    }
}
