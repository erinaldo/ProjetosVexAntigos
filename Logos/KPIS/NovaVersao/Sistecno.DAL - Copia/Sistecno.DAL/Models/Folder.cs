using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Folder
    {
        public int IdFolder { get; set; }
        public string Folder1 { get; set; }
        public System.DateTime Inicio { get; set; }
        public System.DateTime Fim { get; set; }
        public string Ativo { get; set; }
    }
}
