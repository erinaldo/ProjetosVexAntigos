using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LogMetodo
    {
        public string NomeMetodo { get; set; }
        public Nullable<System.DateTime> DataHoraInicio { get; set; }
        public Nullable<System.DateTime> DataHoraTermino { get; set; }
        public string TempoGasto { get; set; }
        public string Obs { get; set; }
        public int idLogMetodo { get; set; }
        public Nullable<int> idUsuario { get; set; }
    }
}
