using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDITransferencia
    {
        public int IDTransferencia { get; set; }
        public int IDDestino { get; set; }
        public string ChaveOrigem { get; set; }
        public string Tabela { get; set; }
        public string Finalizado { get; set; }
    }
}
