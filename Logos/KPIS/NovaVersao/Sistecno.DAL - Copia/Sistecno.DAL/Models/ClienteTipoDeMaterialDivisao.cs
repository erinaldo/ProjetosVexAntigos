using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ClienteTipoDeMaterialDivisao
    {
        public int IdClienteTipoDeMaterialDivisao { get; set; }
        public int IdClienteTipoDeMaterial { get; set; }
        public int IdClienteDivisao { get; set; }
    }
}
