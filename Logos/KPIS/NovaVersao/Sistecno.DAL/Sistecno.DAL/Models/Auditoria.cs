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
    
    public partial class Auditoria
    {
        public string TABELA { get; set; }
        public int IDREGISTRO { get; set; }
        public string OPERACAO { get; set; }
        public string CAMPO { get; set; }
        public string VALORANTERIOR { get; set; }
        public string VALORNOVO { get; set; }
        public int IDUSUARIO { get; set; }
        public string USUARIO { get; set; }
        public string PROCEDIMENTO { get; set; }
        public Nullable<System.DateTime> DATAHORA { get; set; }
    }
}
