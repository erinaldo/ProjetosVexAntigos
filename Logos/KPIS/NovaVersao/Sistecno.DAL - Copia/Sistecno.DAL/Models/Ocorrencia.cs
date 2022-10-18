using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Ocorrencia
    {
        public Ocorrencia()
        {
            this.DocumentoOcorrencias = new List<DocumentoOcorrencia>();
            this.OcorrenciaCodigoes = new List<OcorrenciaCodigo>();
            this.OcorrenciaDeParas = new List<OcorrenciaDePara>();
            this.RomaneioOcorrencias = new List<RomaneioOcorrencia>();
            this.TES = new List<TE>();
        }

        public int IDOcorrencia { get; set; }
        public Nullable<int> IDEmpresa { get; set; }
        public int IDOcorrenciaAcao { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Responsabilidade { get; set; }
        public string NomeReduzido { get; set; }
        public string PagaEntrega { get; set; }
        public string Finalizador { get; set; }
        public string Sistema { get; set; }
        public string Ativo { get; set; }
        public string RestringirCarregamento { get; set; }
        public string AbrirFecharOcorrencia { get; set; }
        public string ApareceSiteCliente { get; set; }
        public Nullable<int> IdOcorrenciaSerie { get; set; }
        public string Situacao { get; set; }
        public virtual ICollection<DocumentoOcorrencia> DocumentoOcorrencias { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual OcorrenciaAcao OcorrenciaAcao { get; set; }
        public virtual ICollection<OcorrenciaCodigo> OcorrenciaCodigoes { get; set; }
        public virtual ICollection<OcorrenciaDePara> OcorrenciaDeParas { get; set; }
        public virtual ICollection<RomaneioOcorrencia> RomaneioOcorrencias { get; set; }
        public virtual ICollection<TE> TES { get; set; }
    }
}
