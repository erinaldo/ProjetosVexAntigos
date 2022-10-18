using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ReposicaoRogeEan
    {
        public string CodigoDeBarras { get; set; }
        public Nullable<System.DateTime> DataInclusao { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }
    }
}
