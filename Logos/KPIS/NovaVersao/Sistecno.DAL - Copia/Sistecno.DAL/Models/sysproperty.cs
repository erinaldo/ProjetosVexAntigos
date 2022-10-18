using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class sysproperty
    {
        public int id { get; set; }
        public short smallid { get; set; }
        public byte type { get; set; }
        public string name { get; set; }
    }
}
