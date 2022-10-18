using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class TituloDocumento
    {
        public int IdTituloDocumento { get; set; }
        public int IdTitulo { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<System.DateTime> DataProtocolo { get; set; }
        public Nullable<int> IdDt { get; set; }
        public Nullable<int> IdRpci { get; set; }
        public Nullable<int> IdContaDespesa { get; set; }
        public Nullable<int> IdContrato { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual DT DT { get; set; }
        public virtual RPCI RPCI { get; set; }
        public virtual Titulo Titulo { get; set; }
    }
}
