using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class SetorCep
    {
        public int IDSetorCep { get; set; }
        public int IDSetor { get; set; }
        public string CepInicial { get; set; }
        public string CepFinal { get; set; }
        public string Origem { get; set; }
        public virtual Setor Setor { get; set; }
    }
}
