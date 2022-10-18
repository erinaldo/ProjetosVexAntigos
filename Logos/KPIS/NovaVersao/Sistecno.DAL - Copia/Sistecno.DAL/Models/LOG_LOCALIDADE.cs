using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LOG_LOCALIDADE
    {
        public LOG_LOCALIDADE()
        {
            this.LOG_BAIRRO = new List<LOG_BAIRRO>();
            this.LOG_FAIXA_LOCALIDADE = new List<LOG_FAIXA_LOCALIDADE>();
        }

        public int LOC_NU { get; set; }
        public string UFE_SG { get; set; }
        public string LOC_NO { get; set; }
        public string CEP { get; set; }
        public string LOC_IN_SIT { get; set; }
        public string LOC_IN_TIPO_LOC { get; set; }
        public Nullable<int> LOC_NU_SUB { get; set; }
        public string LOC_NO_ABREV { get; set; }
        public Nullable<int> MUN_NU { get; set; }
        public Nullable<System.DateTime> Incluido { get; set; }
        public virtual ICollection<LOG_BAIRRO> LOG_BAIRRO { get; set; }
        public virtual ICollection<LOG_FAIXA_LOCALIDADE> LOG_FAIXA_LOCALIDADE { get; set; }
    }
}
