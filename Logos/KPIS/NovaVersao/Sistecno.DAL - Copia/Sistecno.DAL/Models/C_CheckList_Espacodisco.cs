using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class C_CheckList_Espacodisco
    {
        public string Drive { get; set; }
        public Nullable<int> Tamanho__MB_ { get; set; }
        public Nullable<int> Usado__MB_ { get; set; }
        public Nullable<int> Livre__MB_ { get; set; }
        public Nullable<int> Livre____ { get; set; }
        public Nullable<int> Usado____ { get; set; }
        public int Ocupado_SQL__MB_ { get; set; }
    }
}
