using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class PercursoMdfe
    {
        public int Ordem { get; set; }
        public string UfOrigem { get; set; }
        public string UfDestino { get; set; }
        public string UfPercurso { get; set; }
    }
}
