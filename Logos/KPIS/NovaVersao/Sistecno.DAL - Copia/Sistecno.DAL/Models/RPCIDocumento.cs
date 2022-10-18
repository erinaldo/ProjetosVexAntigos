using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RPCIDocumento
    {
        public int IDRPCIDocumento { get; set; }
        public int IDRPCI { get; set; }
        public Nullable<int> IDDT { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual DT DT { get; set; }
        public virtual RPCI RPCI { get; set; }
    }
}
