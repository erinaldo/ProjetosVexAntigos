using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioCentroDeCustoOperacaoLog
    {
        public int idUsuarioCentroDeCustoOperacaolog { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public string obs { get; set; }
        public Nullable<System.DateTime> DataAprovacao { get; set; }
        public Nullable<int> idUsuarioCentroDeCustoOperacao { get; set; }
    }
}
