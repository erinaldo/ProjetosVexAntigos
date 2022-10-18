using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
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
