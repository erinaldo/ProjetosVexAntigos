using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Bairro
    {
        public Bairro()
        {
            this.BairroFaixaDeCeps = new List<BairroFaixaDeCep>();
            this.Cadastroes = new List<Cadastro>();
            this.CadastroEnderecoes = new List<CadastroEndereco>();
        }

        public int IDBairro { get; set; }
        public int IDCidade { get; set; }
        public string Nome { get; set; }
        public string Origem { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual ICollection<BairroFaixaDeCep> BairroFaixaDeCeps { get; set; }
        public virtual ICollection<Cadastro> Cadastroes { get; set; }
        public virtual ICollection<CadastroEndereco> CadastroEnderecoes { get; set; }
    }
}
