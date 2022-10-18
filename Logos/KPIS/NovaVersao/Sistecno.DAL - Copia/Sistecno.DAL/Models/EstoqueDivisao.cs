using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EstoqueDivisao
    {
        public EstoqueDivisao()
        {
            this.EstoqueDivisaoMovs = new List<EstoqueDivisaoMov>();
            this.EstoqueDivisaoMovs1 = new List<EstoqueDivisaoMov>();
            this.EstoqueDivisaoMovs2 = new List<EstoqueDivisaoMov>();
        }

        public int IDEstoqueDivisao { get; set; }
        public int IDEstoque { get; set; }
        public int IDClienteDivisao { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<decimal> SaldoBaseExterna { get; set; }
        public Nullable<decimal> PercentualRateio { get; set; }
        public Nullable<System.DateTime> data_limite { get; set; }
        public Nullable<decimal> PercentualRateioCda { get; set; }
        public Nullable<System.DateTime> Inventariado { get; set; }
        public virtual ClienteDivisao ClienteDivisao { get; set; }
        public virtual Estoque Estoque { get; set; }
        public virtual ICollection<EstoqueDivisaoMov> EstoqueDivisaoMovs { get; set; }
        public virtual ICollection<EstoqueDivisaoMov> EstoqueDivisaoMovs1 { get; set; }
        public virtual ICollection<EstoqueDivisaoMov> EstoqueDivisaoMovs2 { get; set; }
    }
}
