using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ClienteDivisao
    {
        public ClienteDivisao()
        {
            this.Avisoes = new List<Aviso>();
            this.ClienteDivisao1 = new List<ClienteDivisao>();
            this.EstoqueDivisaos = new List<EstoqueDivisao>();
            this.UsuarioClienteDivisaos = new List<UsuarioClienteDivisao>();
        }

        public int IDClienteDivisao { get; set; }
        public int IDCliente { get; set; }
        public string Nome { get; set; }
        public Nullable<int> IDParente { get; set; }
        public string BaseExterna { get; set; }
        public string Sistema { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public string Ativo { get; set; }
        public Nullable<int> Sequencia { get; set; }
        public virtual ICollection<Aviso> Avisoes { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ClienteDivisao> ClienteDivisao1 { get; set; }
        public virtual ClienteDivisao ClienteDivisao2 { get; set; }
        public virtual ICollection<EstoqueDivisao> EstoqueDivisaos { get; set; }
        public virtual ICollection<UsuarioClienteDivisao> UsuarioClienteDivisaos { get; set; }
    }
}
