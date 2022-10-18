using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Cadastro
    {
        public Cadastro()
        {
            this.Agenda = new List<Agendum>();
            this.Agendamentoes = new List<Agendamento>();
            this.Apolice = new List<Apolouse>();
            this.Apolice1 = new List<Apolouse>();
            this.CadastroComplementoes = new List<CadastroComplemento>();
            this.CadastroContatoEnderecoes = new List<CadastroContatoEndereco>();
            this.CadastroContatoes = new List<CadastroContato>();
            this.CadastroContatoes1 = new List<CadastroContato>();
            this.CadastroEnderecoes = new List<CadastroEndereco>();
            this.CadastroEntregas = new List<CadastroEntrega>();
            this.CadastroImagems = new List<CadastroImagem>();
            this.CadastroHistoricoes = new List<CadastroHistorico>();
            this.CadastroReferencias = new List<CadastroReferencia>();
            this.CadastroRegiaos = new List<CadastroRegiao>();
            this.DTs = new List<DT>();
            this.DTs1 = new List<DT>();
            this.DTs2 = new List<DT>();
            this.DTs3 = new List<DT>();
            this.EdiTrocaDeArquivoes = new List<EdiTrocaDeArquivo>();
            this.EdiTrocaDeArquivoes1 = new List<EdiTrocaDeArquivo>();
            this.Filials = new List<Filial>();
            this.Inventarios = new List<Inventario>();
            this.PortariaVisitantes = new List<PortariaVisitante>();
            this.RegiaoItems = new List<RegiaoItem>();
            this.RomaneioDocumentoFretes = new List<RomaneioDocumentoFrete>();
            this.RPCIs = new List<RPCI>();
            this.Usuarios = new List<Usuario>();
            this.Veiculoes = new List<Veiculo>();
        }

        public int IDCadastro { get; set; }
        public string CnpjCpf { get; set; }
        public string InscricaoRG { get; set; }
        public string RazaoSocialNome { get; set; }
        public string FantasiaApelido { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public Nullable<int> IDCidade { get; set; }
        public Nullable<int> IDBairro { get; set; }
        public string Cep { get; set; }
        public string CnpjCpfErrado { get; set; }
        public string InscricaoErrada { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string CEPValido { get; set; }
        public string Aniversario { get; set; }
        public string SUFRAMA { get; set; }
        public Nullable<System.DateTime> SUFRAMAVALIDADE { get; set; }
        public string OrgaoEmissor { get; set; }
        public string TipoDeCadastro { get; set; }
        public string SituacaoFiscal { get; set; }
        public string GrupoDePreco { get; set; }
        public string Regional { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string ClassificacaoFiscal { get; set; }
        public Nullable<System.DateTime> DataDaUltimaAtualizacaoEDI { get; set; }
        public string RecebeFinalSemana { get; set; }
        public string PermiteAlteracaoNoCadastro { get; set; }
        public Nullable<int> IdTipoDeOperacao { get; set; }
        public virtual ICollection<Agendum> Agenda { get; set; }
        public virtual ICollection<Agendamento> Agendamentoes { get; set; }
        public virtual Agregado Agregado { get; set; }
        public virtual ICollection<Apolouse> Apolice { get; set; }
        public virtual ICollection<Apolouse> Apolice1 { get; set; }
        public virtual Bairro Bairro { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual ICollection<CadastroComplemento> CadastroComplementoes { get; set; }
        public virtual CadastroCondicaoEntrega CadastroCondicaoEntrega { get; set; }
        public virtual ICollection<CadastroContatoEndereco> CadastroContatoEnderecoes { get; set; }
        public virtual ICollection<CadastroContato> CadastroContatoes { get; set; }
        public virtual ICollection<CadastroContato> CadastroContatoes1 { get; set; }
        public virtual ICollection<CadastroEndereco> CadastroEnderecoes { get; set; }
        public virtual ICollection<CadastroEntrega> CadastroEntregas { get; set; }
        public virtual ICollection<CadastroImagem> CadastroImagems { get; set; }
        public virtual ICollection<CadastroHistorico> CadastroHistoricoes { get; set; }
        public virtual ICollection<CadastroReferencia> CadastroReferencias { get; set; }
        public virtual ICollection<CadastroRegiao> CadastroRegiaos { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual ICollection<DT> DTs1 { get; set; }
        public virtual ICollection<DT> DTs2 { get; set; }
        public virtual ICollection<DT> DTs3 { get; set; }
        public virtual ICollection<EdiTrocaDeArquivo> EdiTrocaDeArquivoes { get; set; }
        public virtual ICollection<EdiTrocaDeArquivo> EdiTrocaDeArquivoes1 { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<Filial> Filials { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual ICollection<Inventario> Inventarios { get; set; }
        public virtual ICollection<PortariaVisitante> PortariaVisitantes { get; set; }
        public virtual Redespacho Redespacho { get; set; }
        public virtual ICollection<RegiaoItem> RegiaoItems { get; set; }
        public virtual Representante Representante { get; set; }
        public virtual ICollection<RomaneioDocumentoFrete> RomaneioDocumentoFretes { get; set; }
        public virtual ICollection<RPCI> RPCIs { get; set; }
        public virtual Transportadora Transportadora { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Veiculo> Veiculoes { get; set; }
    }
}
