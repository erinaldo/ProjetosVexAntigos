using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class vwIrwinPedidosEmAndamento
    {
        public string EMISSAOPEDIDO { get; set; }
        public int IDDOCUMENTO { get; set; }
        public Nullable<int> NUMERO { get; set; }
        public Nullable<System.DateTime> DATAHORARECEBIMENTOINTERFACE { get; set; }
        public Nullable<System.DateTime> INICIODASEPARACAO { get; set; }
        public Nullable<System.DateTime> DATAHORATERMINOCONFERENCIA { get; set; }
        public Nullable<int> CONCLUSAOPALETS { get; set; }
        public Nullable<System.DateTime> DATAHORABAIXADOESTOQUE { get; set; }
        public Nullable<System.DateTime> DATAHORAPEDIDONOTAFISCAL { get; set; }
        public string NOMEDOARQUIVO { get; set; }
        public Nullable<System.DateTime> HORANF { get; set; }
        public Nullable<int> NF { get; set; }
    }
}
