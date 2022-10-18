using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class LabelPicking
    {
        public int IdLabelPicking { get; set; }
        public Nullable<int> IdRomaneio { get; set; }
        public Nullable<int> IdDocumento { get; set; }
        public Nullable<int> Numero { get; set; }
        public string Codigo { get; set; }
        public string Picking { get; set; }
        public string Embalagem { get; set; }
        public string Empresa { get; set; }
        public string Transportadora { get; set; }
        public string Cliente { get; set; }
        public string Destinatario { get; set; }
        public string Destino { get; set; }
        public Nullable<int> Volumes { get; set; }
        public Nullable<int> Quantidade { get; set; }
        public Nullable<int> Ordem { get; set; }
        public Nullable<int> Impressoes { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<System.DateTime> DataDeImpressao { get; set; }
        public virtual Documento Documento { get; set; }
    }
}
