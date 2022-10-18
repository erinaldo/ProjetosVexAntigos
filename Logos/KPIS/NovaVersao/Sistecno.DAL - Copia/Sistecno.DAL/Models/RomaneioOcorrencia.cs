using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RomaneioOcorrencia
    {
        public int IDRomaneioOcorrencia { get; set; }
        public int IDRomaneio { get; set; }
        public int IDFilial { get; set; }
        public Nullable<int> IDOcorrencia { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public Nullable<System.DateTime> DataOcorrencia { get; set; }
        public string Descricao { get; set; }
        public virtual Ocorrencia Ocorrencia { get; set; }
        public virtual Romaneio Romaneio { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
