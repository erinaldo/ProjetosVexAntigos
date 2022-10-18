using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ReposicaoRogeVolume
    {
        public int IdReposicaoRogeVolume { get; set; }
        public Nullable<int> IdResposicaoRoge { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Conferido { get; set; }
        public Nullable<System.DateTime> DataDaInclusao { get; set; }
        public Nullable<System.DateTime> DataConferido { get; set; }
        public virtual ReposicaoRoge ReposicaoRoge { get; set; }
    }
}
