//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistecno.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DocumentoEletronico
    {
        public int IdDocumentoEletronico { get; set; }
        public int IdDocumento { get; set; }
        public string NumeroRecibo { get; set; }
        public string NumeroProtocolo { get; set; }
        public string Status { get; set; }
        public string Lote { get; set; }
        public string IdNota { get; set; }
        public string UltimoArquivoXml { get; set; }
        public string Rejeicao { get; set; }
        public string XMLCancelamento { get; set; }
        public string XMLInutilizacao { get; set; }
        public string TipoDeDocumento { get; set; }
        public string MotivoDoCancelamento { get; set; }
        public string MotivoDaInutilizacao { get; set; }
        public string ArquivoXml { get; set; }
        public string TextoCarta { get; set; }
        public string XMLRps { get; set; }
        public string XMLCarta { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> IdLoteEletronico { get; set; }
        public string CStatus { get; set; }
        public string StatusCompleto { get; set; }
    
        public virtual Documento Documento { get; set; }
    }
}