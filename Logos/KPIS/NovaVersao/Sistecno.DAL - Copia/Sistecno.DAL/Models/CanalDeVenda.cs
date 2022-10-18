using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CanalDeVenda
    {
        public int IdCanalDeVenda { get; set; }
        public Nullable<int> IdSupervisor { get; set; }
        public Nullable<int> IdRepresentante { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public Nullable<int> IdDivisao { get; set; }
    }
}
