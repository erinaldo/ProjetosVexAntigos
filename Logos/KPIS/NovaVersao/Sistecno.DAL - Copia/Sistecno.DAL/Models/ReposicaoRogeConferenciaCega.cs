using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ReposicaoRogeConferenciaCega
    {
        public int ID { get; set; }
        public string CodigoRoge { get; set; }
        public Nullable<int> IdConferenciaItem { get; set; }
        public string CodigoDeBarrasLido { get; set; }
        public Nullable<int> Quantidade { get; set; }
        public string PertenceANota { get; set; }
    }
}
