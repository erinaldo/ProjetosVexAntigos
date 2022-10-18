using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TituloItem
    {
        public int IdTituloItem { get; set; }
        public int IdTitulo { get; set; }
        public int Parcela { get; set; }
        public decimal Valor { get; set; }
    }
}
