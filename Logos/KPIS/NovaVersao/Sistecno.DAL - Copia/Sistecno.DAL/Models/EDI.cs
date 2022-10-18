using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI
    {
        public EDI()
        {
            this.ClienteEDIs = new List<ClienteEDI>();
            this.DocumentoEdis = new List<DocumentoEdi>();
        }

        public int IDEDI { get; set; }
        public string Descricao { get; set; }
        public string Metodo { get; set; }
        public string TabelasEnvolvidas { get; set; }
        public string EntradaSaida { get; set; }
        public string Sistema { get; set; }
        public string NomePadraoDoArquivo { get; set; }
        public string TipoDeDocumento { get; set; }
        public virtual ICollection<ClienteEDI> ClienteEDIs { get; set; }
        public virtual ICollection<DocumentoEdi> DocumentoEdis { get; set; }
    }
}
