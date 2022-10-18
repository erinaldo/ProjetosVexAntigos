using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoModelo
    {
        public int IdDocumentoModelo { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Modelo { get; set; }
    }
}
