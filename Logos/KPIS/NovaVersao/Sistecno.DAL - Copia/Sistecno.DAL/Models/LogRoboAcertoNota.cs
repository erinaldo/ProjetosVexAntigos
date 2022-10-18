using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LogRoboAcertoNota
    {
        public int IdLogRoboAcertoNota { get; set; }
        public int IdDocumento { get; set; }
        public string TipoServicoAnterior { get; set; }
        public string SerieAnterior { get; set; }
        public System.DateTime DataHora { get; set; }
    }
}
