using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VW_LIBERACAO_PEDIDOS
    {
        public int IDCLIENTE { get; set; }
        public Nullable<System.DateTime> DATA { get; set; }
        public Nullable<int> QUANTIDADEDEPEDIDOS { get; set; }
        public Nullable<int> AGUARDANDO_LIBERACAO { get; set; }
        public Nullable<int> LIBERADO_PARA_SEPARACAO { get; set; }
        public Nullable<int> EM_SEPARACAO { get; set; }
        public Nullable<int> SEPARACAO_FINALIZADA { get; set; }
        public Nullable<int> LIBERADO_PARA_EMBALAGEM { get; set; }
        public Nullable<int> PEDIDO_FATURADO { get; set; }
        public Nullable<int> AGUARDADO_EMISSAO_NFE { get; set; }
        public Nullable<int> NFE_EMITIDAS { get; set; }
        public Nullable<int> AGUARDANDO_EMBARQUE { get; set; }
        public Nullable<int> EM_ENTREGA { get; set; }
        public Nullable<int> ENTREGUE { get; set; }
    }
}
