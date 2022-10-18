using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Cidade
    {
        public Cidade()
        {
            this.Bairroes = new List<Bairro>();
            this.Cadastroes = new List<Cadastro>();
            this.CadastroEnderecoes = new List<CadastroEndereco>();
            this.CidadeDistancias = new List<CidadeDistancia>();
            this.DTs = new List<DT>();
            this.DTs1 = new List<DT>();
            this.Feriadoes = new List<Feriado>();
            this.FilialCidadeSetors = new List<FilialCidadeSetor>();
            this.RegiaoItems = new List<RegiaoItem>();
            this.TabelaDeFreteRotas = new List<TabelaDeFreteRota>();
            this.TabelaDeFreteRotas1 = new List<TabelaDeFreteRota>();
            this.Veiculoes = new List<Veiculo>();
        }

        public int IDCidade { get; set; }
        public int IDEstado { get; set; }
        public string Nome { get; set; }
        public string Cep { get; set; }
        public string Tipo { get; set; }
        public string CodificarPor { get; set; }
        public string Regiao { get; set; }
        public Nullable<int> PrazoDeEntregaPadrao { get; set; }
        public string CodigoDoIBGE { get; set; }
        public string CodigoDipam { get; set; }
        public Nullable<decimal> AliquotaDeIss { get; set; }
        public string NomeCidadeIbge { get; set; }
        public string Ativo { get; set; }
        public Nullable<System.DateTime> Conferido { get; set; }
        public virtual ICollection<Bairro> Bairroes { get; set; }
        public virtual ICollection<Cadastro> Cadastroes { get; set; }
        public virtual ICollection<CadastroEndereco> CadastroEnderecoes { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<CidadeDistancia> CidadeDistancias { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual ICollection<DT> DTs1 { get; set; }
        public virtual ICollection<Feriado> Feriadoes { get; set; }
        public virtual ICollection<FilialCidadeSetor> FilialCidadeSetors { get; set; }
        public virtual ICollection<RegiaoItem> RegiaoItems { get; set; }
        public virtual ICollection<TabelaDeFreteRota> TabelaDeFreteRotas { get; set; }
        public virtual ICollection<TabelaDeFreteRota> TabelaDeFreteRotas1 { get; set; }
        public virtual ICollection<Veiculo> Veiculoes { get; set; }
    }
}
