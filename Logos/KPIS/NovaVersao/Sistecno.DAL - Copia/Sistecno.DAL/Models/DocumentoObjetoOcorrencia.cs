using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class DocumentoObjetoOcorrencia
    {
        public int IdDocumentoObjetoOcorrencia { get; set; }
        public int IdDocumentoObjeto { get; set; }
        public string Descricao { get; set; }
        public System.DateTime DataHoraAtualizacao { get; set; }
        public string Local { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Hora { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string Destino { get; set; }
        public string UfDestino { get; set; }
        public string CidadeDestino { get; set; }
        public virtual DocumentoObjeto DocumentoObjeto { get; set; }
    }
}
