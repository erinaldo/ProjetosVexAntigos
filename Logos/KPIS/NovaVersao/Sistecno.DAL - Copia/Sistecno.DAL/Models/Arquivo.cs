using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Arquivo
    {
        public Arquivo()
        {
            this.ArquivoItems = new List<ArquivoItem>();
        }

        public int IdArquivo { get; set; }
        public string Nome { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public virtual ICollection<ArquivoItem> ArquivoItems { get; set; }
    }
}
