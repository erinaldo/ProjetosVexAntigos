using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DTEletronico
    {
        public int IdDTEletronico { get; set; }
        public int IdDT { get; set; }
        public Nullable<int> IdLoteEletronico { get; set; }
        public string Lote { get; set; }
        public string Chave { get; set; }
        public string ReciboNumero { get; set; }
        public string ReciboCStatus { get; set; }
        public string ReciboStatus { get; set; }
        public string ProtocoloNumero { get; set; }
        public string ProtocoloCStatus { get; set; }
        public string ProtocoloStatus { get; set; }
        public string XMLRetorno { get; set; }
        public string XMLFinal { get; set; }
        public string DTEletronicoTipo { get; set; }
    }
}
