using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoOcorrencia
    {
        public DocumentoOcorrencia()
        {
            this.Documentoes = new List<Documento>();
            this.DocumentoOcorrenciaArquivoes = new List<DocumentoOcorrenciaArquivo>();
            this.DocumentoOcorrenciaItems = new List<DocumentoOcorrenciaItem>();
        }

        public int IDDocumentoOcorrencia { get; set; }
        public int IDDocumento { get; set; }
        public int IDFilial { get; set; }
        public Nullable<int> IDOcorrencia { get; set; }
        public Nullable<int> IDConhecimento { get; set; }
        public Nullable<int> IDRomaneio { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public Nullable<System.DateTime> DataOcorrencia { get; set; }
        public Nullable<System.DateTime> DataLancamento { get; set; }
        public string Descricao { get; set; }
        public string Pessoa { get; set; }
        public string CpfRg { get; set; }
        public string Sistema { get; set; }
        public string Senha { get; set; }
        public Nullable<int> IdOcorrenciaAndamento { get; set; }
        public string ArquivoDeIntegracao { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<int> IdOcorrenciaComprovei { get; set; }
        public virtual ICollection<Documento> Documentoes { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Ocorrencia Ocorrencia { get; set; }
        public virtual Romaneio Romaneio { get; set; }
        public virtual ICollection<DocumentoOcorrenciaArquivo> DocumentoOcorrenciaArquivoes { get; set; }
        public virtual ICollection<DocumentoOcorrenciaItem> DocumentoOcorrenciaItems { get; set; }
    }
}
