using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ClienteFilial
    {
        public int IDClienteFilial { get; set; }
        public int IDCliente { get; set; }
        public int IDFilial { get; set; }
        public string ClienteLogistica { get; set; }
        public string ColetaAutomatica { get; set; }
        public string FilialRobo { get; set; }
        public string RealizaConferencia { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Filial Filial { get; set; }
    }
}
