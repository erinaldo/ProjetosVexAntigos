using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Filial
    {
        public Filial()
        {
            this.Agendamentoes = new List<Agendamento>();
            this.AgendaRecebimentoes = new List<AgendaRecebimento>();
            this.BancoContas = new List<BancoConta>();
            this.Borderoes = new List<Bordero>();
            this.CadastroRegiaos = new List<CadastroRegiao>();
            this.CentroDeCustoFilials = new List<CentroDeCustoFilial>();
            this.CidadeDistancias = new List<CidadeDistancia>();
            this.ClienteEDIs = new List<ClienteEDI>();
            this.ClienteFilials = new List<ClienteFilial>();
            this.ClienteSetorFilials = new List<ClienteSetorFilial>();
            this.ColetorConferencias = new List<ColetorConferencia>();
            this.ColetorConferenciaLogs = new List<ColetorConferenciaLog>();
            this.ContaContabilFilials = new List<ContaContabilFilial>();
            this.ContaContabilLancamentoes = new List<ContaContabilLancamento>();
            this.Depositoes = new List<Deposito>();
            this.DocumentoAFaturars = new List<DocumentoAFaturar>();
            this.DocumentoAguardandoCTRCs = new List<DocumentoAguardandoCTRC>();
            this.DocumentoFilials = new List<DocumentoFilial>();
            this.DocumentoOcorrencias = new List<DocumentoOcorrencia>();
            this.DTs = new List<DT>();
            this.DTs1 = new List<DT>();
            this.Estoques = new List<Estoque>();
            this.FilialCidadeSetors = new List<FilialCidadeSetor>();
            this.FornecedorFilials = new List<FornecedorFilial>();
            this.Funcionarios = new List<Funcionario>();
            this.Inventarios = new List<Inventario>();
            this.LancamentoContabils = new List<LancamentoContabil>();
            this.Mapas = new List<Mapa>();
            this.MotoristaFilials = new List<MotoristaFilial>();
            this.Movimentacaos = new List<Movimentacao>();
            this.MovimentacaoBancarias = new List<MovimentacaoBancaria>();
            this.Numeradors = new List<Numerador>();
            this.Numeradors1 = new List<Numerador>();
            this.Plantas = new List<Planta>();
            this.Portarias = new List<Portaria>();
            this.Projetoes = new List<Projeto>();
            this.ProjetoFilials = new List<ProjetoFilial>();
            this.Regiaos = new List<Regiao>();
            this.Representantes = new List<Representante>();
            this.Romaneios = new List<Romaneio>();
            this.Romaneios1 = new List<Romaneio>();
            this.RomaneioDocumentoFretes = new List<RomaneioDocumentoFrete>();
            this.Roteirizacaos = new List<Roteirizacao>();
            this.RPCIs = new List<RPCI>();
            this.Sobras = new List<Sobra>();
            this.TabelaDeFreteRotas = new List<TabelaDeFreteRota>();
            this.TabelaDeFreteRotas1 = new List<TabelaDeFreteRota>();
            this.Tituloes = new List<Titulo>();
            this.UnidadeDeArmazenagems = new List<UnidadeDeArmazenagem>();
            this.UsuarioDeTabelaDeFretes = new List<UsuarioDeTabelaDeFrete>();
            this.UsuarioDeTabelaDeFretes1 = new List<UsuarioDeTabelaDeFrete>();
            this.UsuarioFilials = new List<UsuarioFilial>();
            this.UsuarioPerfils = new List<UsuarioPerfil>();
            this.VeiculoFilials = new List<VeiculoFilial>();
            this.VeiculoTabelas = new List<VeiculoTabela>();
        }

        public int IDFilial { get; set; }
        public int IDEmpresa { get; set; }
        public int IDCadastro { get; set; }
        public Nullable<int> IDContaContabilCredito { get; set; }
        public Nullable<int> IDContaContabilDebito { get; set; }
        public Nullable<int> IDCentroDeCustoCredito { get; set; }
        public Nullable<int> IDCentroDeCustoDebito { get; set; }
        public Nullable<int> NumeroDaFilial { get; set; }
        public Nullable<int> Unidade { get; set; }
        public string Nome { get; set; }
        public string PermiteCNPJErrado { get; set; }
        public string PermiteIEErrada { get; set; }
        public Nullable<short> FusoHorario { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<int> CodigoContabil { get; set; }
        public string CodigoDipam { get; set; }
        public string DadosLogisticosObrigatorio { get; set; }
        public string PermiteDestinatarioDiferenteRE { get; set; }
        public Nullable<int> QtdeDTAberto { get; set; }
        public Nullable<int> IdCadastroCompra { get; set; }
        public string NomeCertificado { get; set; }
        public string TipoCertificado { get; set; }
        public string ObrigaEnviarEmailDT { get; set; }
        public string Ativo { get; set; }
        public string EmitirNotaFiscalServico { get; set; }
        public string DT_ExigePortaria { get; set; }
        public string EmiteNotaFiscalServicoDeTransporte { get; set; }
        public Nullable<decimal> Imposto { get; set; }
        public Nullable<decimal> Seguro { get; set; }
        public Nullable<decimal> TaxaAdministrativa { get; set; }
        public Nullable<decimal> TaxaDeTranferencia { get; set; }
        public string CalcularFreteDT { get; set; }
        public Nullable<decimal> TaxaDeOcupacaoDoVeiculo { get; set; }
        public string IcmsSuspenso { get; set; }
        public string NomeComprovei { get; set; }
        public string Sigla { get; set; }
        public Nullable<decimal> PercentualLucratividadeExigitoPorDT { get; set; }
        public string GerenteNome { get; set; }
        public string GerenteFone { get; set; }
        public string GerenteCelular { get; set; }
        public string GerenteEmail { get; set; }
        public byte[] GerenteFoto { get; set; }
        public Nullable<decimal> PercentualMaximoFrete { get; set; }
        public Nullable<int> Gerente { get; set; }
        public Nullable<int> Assistente { get; set; }
        public Nullable<int> LiderOperacional { get; set; }
        public Nullable<int> Conferente { get; set; }
        public Nullable<int> Separador { get; set; }
        public Nullable<int> Limpeza { get; set; }
        public Nullable<int> Outros { get; set; }
        public Nullable<int> Empilhador { get; set; }
        public virtual ICollection<Agendamento> Agendamentoes { get; set; }
        public virtual ICollection<AgendaRecebimento> AgendaRecebimentoes { get; set; }
        public virtual ICollection<BancoConta> BancoContas { get; set; }
        public virtual ICollection<Bordero> Borderoes { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual ICollection<CadastroRegiao> CadastroRegiaos { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual CentroDeCusto CentroDeCusto1 { get; set; }
        public virtual ICollection<CentroDeCustoFilial> CentroDeCustoFilials { get; set; }
        public virtual ICollection<CidadeDistancia> CidadeDistancias { get; set; }
        public virtual ICollection<ClienteEDI> ClienteEDIs { get; set; }
        public virtual ICollection<ClienteFilial> ClienteFilials { get; set; }
        public virtual ICollection<ClienteSetorFilial> ClienteSetorFilials { get; set; }
        public virtual ICollection<ColetorConferencia> ColetorConferencias { get; set; }
        public virtual ICollection<ColetorConferenciaLog> ColetorConferenciaLogs { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual ContaContabil ContaContabil1 { get; set; }
        public virtual ICollection<ContaContabilFilial> ContaContabilFilials { get; set; }
        public virtual ICollection<ContaContabilLancamento> ContaContabilLancamentoes { get; set; }
        public virtual ICollection<Deposito> Depositoes { get; set; }
        public virtual ICollection<DocumentoAFaturar> DocumentoAFaturars { get; set; }
        public virtual ICollection<DocumentoAguardandoCTRC> DocumentoAguardandoCTRCs { get; set; }
        public virtual ICollection<DocumentoFilial> DocumentoFilials { get; set; }
        public virtual ICollection<DocumentoOcorrencia> DocumentoOcorrencias { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual ICollection<DT> DTs1 { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<Estoque> Estoques { get; set; }
        public virtual ICollection<FilialCidadeSetor> FilialCidadeSetors { get; set; }
        public virtual ICollection<FornecedorFilial> FornecedorFilials { get; set; }
        public virtual ICollection<Funcionario> Funcionarios { get; set; }
        public virtual ICollection<Inventario> Inventarios { get; set; }
        public virtual ICollection<LancamentoContabil> LancamentoContabils { get; set; }
        public virtual ICollection<Mapa> Mapas { get; set; }
        public virtual ICollection<MotoristaFilial> MotoristaFilials { get; set; }
        public virtual ICollection<Movimentacao> Movimentacaos { get; set; }
        public virtual ICollection<MovimentacaoBancaria> MovimentacaoBancarias { get; set; }
        public virtual ICollection<Numerador> Numeradors { get; set; }
        public virtual ICollection<Numerador> Numeradors1 { get; set; }
        public virtual ICollection<Planta> Plantas { get; set; }
        public virtual ICollection<Portaria> Portarias { get; set; }
        public virtual ICollection<Projeto> Projetoes { get; set; }
        public virtual ICollection<ProjetoFilial> ProjetoFilials { get; set; }
        public virtual ICollection<Regiao> Regiaos { get; set; }
        public virtual ICollection<Representante> Representantes { get; set; }
        public virtual ICollection<Romaneio> Romaneios { get; set; }
        public virtual ICollection<Romaneio> Romaneios1 { get; set; }
        public virtual ICollection<RomaneioDocumentoFrete> RomaneioDocumentoFretes { get; set; }
        public virtual ICollection<Roteirizacao> Roteirizacaos { get; set; }
        public virtual ICollection<RPCI> RPCIs { get; set; }
        public virtual ICollection<Sobra> Sobras { get; set; }
        public virtual ICollection<TabelaDeFreteRota> TabelaDeFreteRotas { get; set; }
        public virtual ICollection<TabelaDeFreteRota> TabelaDeFreteRotas1 { get; set; }
        public virtual ICollection<Titulo> Tituloes { get; set; }
        public virtual ICollection<UnidadeDeArmazenagem> UnidadeDeArmazenagems { get; set; }
        public virtual ICollection<UsuarioDeTabelaDeFrete> UsuarioDeTabelaDeFretes { get; set; }
        public virtual ICollection<UsuarioDeTabelaDeFrete> UsuarioDeTabelaDeFretes1 { get; set; }
        public virtual ICollection<UsuarioFilial> UsuarioFilials { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; }
        public virtual ICollection<VeiculoFilial> VeiculoFilials { get; set; }
        public virtual ICollection<VeiculoTabela> VeiculoTabelas { get; set; }
    }
}
