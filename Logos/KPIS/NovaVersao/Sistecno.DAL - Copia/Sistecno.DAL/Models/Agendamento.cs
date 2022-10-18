using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Agendamento
    {
        public Agendamento()
        {
            this.AgendamentoDocumentoes = new List<AgendamentoDocumento>();
            this.AgendaRecebimentoes = new List<AgendaRecebimento>();
            this.Portarias = new List<Portaria>();
        }

        public int IdAgendamento { get; set; }
        public int IdFilial { get; set; }
        public int IdCadastro { get; set; }
        public int IdAgendaRecebimento { get; set; }
        public Nullable<int> IdVeiculoTipo { get; set; }
        public string TipoDeCarga { get; set; }
        public Nullable<int> QuantidadeDeVeiculos { get; set; }
        public string EDI { get; set; }
        public Nullable<int> NotasFiscais { get; set; }
        public Nullable<decimal> Peso { get; set; }
        public Nullable<System.DateTime> DataGeracaoDoArquivo { get; set; }
        public string NomeDoArquivo { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Email { get; set; }
        public string EmailCopia { get; set; }
        public string Pessoa { get; set; }
        public string Fone { get; set; }
        public string Documento { get; set; }
        public Nullable<decimal> ValorDaNota { get; set; }
        public string Senha { get; set; }
        public string Transportadora { get; set; }
        public virtual AgendaRecebimento AgendaRecebimento { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual VeiculoTipo VeiculoTipo { get; set; }
        public virtual ICollection<AgendamentoDocumento> AgendamentoDocumentoes { get; set; }
        public virtual ICollection<AgendaRecebimento> AgendaRecebimentoes { get; set; }
        public virtual ICollection<Portaria> Portarias { get; set; }
    }
}
