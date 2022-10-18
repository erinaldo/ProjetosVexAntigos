using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProjetoArquivo
    {
        public int IdProjetoArquivo { get; set; }
        public int IdProjeto { get; set; }
        public string Descricao { get; set; }
        public System.DateTime Data { get; set; }
        public byte[] Arquivo { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}
