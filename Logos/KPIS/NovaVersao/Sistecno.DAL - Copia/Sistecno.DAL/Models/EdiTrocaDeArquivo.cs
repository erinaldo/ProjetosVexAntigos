using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EdiTrocaDeArquivo
    {
        public int IdEdiTrocaDeArquivo { get; set; }
        public Nullable<int> IdOrigem { get; set; }
        public Nullable<int> IdDestino { get; set; }
        public string TipoDeArquivo { get; set; }
        public Nullable<System.DateTime> EntradaData { get; set; }
        public Nullable<int> EntradaIdUsuario { get; set; }
        public Nullable<System.DateTime> SaidaData { get; set; }
        public Nullable<int> SaidaIdUsuario { get; set; }
        public byte[] Arquivo { get; set; }
        public string NomeDoArquivo { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual Cadastro Cadastro1 { get; set; }
    }
}
