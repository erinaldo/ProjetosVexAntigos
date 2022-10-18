using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Sistecno.DAL.Models.Mapping;

namespace Sistecno.DAL.Models
{
    public partial class SistecnoContext : DbContext
    {
        static SistecnoContext()
        {
            Database.SetInitializer<SistecnoContext>(null);
        }

        public SistecnoContext()
            : base("Name=SistecnoContext")
        {
        }

        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<C_CheckList_Espacodisco> C_CheckList_Espacodisco { get; set; }
        public DbSet<Agendum> Agenda { get; set; }
        public DbSet<Agendamento> Agendamentoes { get; set; }
        public DbSet<AgendamentoDocumento> AgendamentoDocumentoes { get; set; }
        public DbSet<AgendaRecebimento> AgendaRecebimentoes { get; set; }
        public DbSet<Agregado> Agregadoes { get; set; }
        public DbSet<Agrupamento> Agrupamentoes { get; set; }
        public DbSet<AgrupamentoRegiao> AgrupamentoRegiaos { get; set; }
        public DbSet<Ajuda> Ajudas { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Apolouse> Apolice { get; set; }
        public DbSet<ApoliceAtuadore> ApoliceAtuadores { get; set; }
        public DbSet<ApoliceItem> ApoliceItems { get; set; }
        public DbSet<Arquivo> Arquivoes { get; set; }
        public DbSet<ArquivoClienteEDI> ArquivoClienteEDIs { get; set; }
        public DbSet<ArquivoItem> ArquivoItems { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Aviso> Avisoes { get; set; }
        public DbSet<AvisoDocumento> AvisoDocumentoes { get; set; }
        public DbSet<AvisoRoboEmail> AvisoRoboEmails { get; set; }
        public DbSet<Bairro> Bairroes { get; set; }
        public DbSet<BairroFaixaDeCep> BairroFaixaDeCeps { get; set; }
        public DbSet<BAIRRO1> BAIRROS { get; set; }
        public DbSet<Balanca> Balancas { get; set; }
        public DbSet<Banco> Bancoes { get; set; }
        public DbSet<BancoCarteira> BancoCarteiras { get; set; }
        public DbSet<BancoConta> BancoContas { get; set; }
        public DbSet<BancoContaBloqueto> BancoContaBloquetoes { get; set; }
        public DbSet<BancoEspecie> BancoEspecies { get; set; }
        public DbSet<BancoInstrucao> BancoInstrucaos { get; set; }
        public DbSet<BancoInstrucaoDeProtesto> BancoInstrucaoDeProtestoes { get; set; }
        public DbSet<BancoOcorrenciaRejeicao> BancoOcorrenciaRejeicaos { get; set; }
        public DbSet<BancoOcorrenciaRemessa> BancoOcorrenciaRemessas { get; set; }
        public DbSet<BancoOcorrenciaRetorno> BancoOcorrenciaRetornoes { get; set; }
        public DbSet<BASE> BASEs { get; set; }
        public DbSet<BaseDoCredito> BaseDoCreditoes { get; set; }
        public DbSet<Bordero> Borderoes { get; set; }
        public DbSet<BorderoTituloDuplicata> BorderoTituloDuplicatas { get; set; }
        public DbSet<Cadastro> Cadastroes { get; set; }
        public DbSet<CadastroCDA> CadastroCDAs { get; set; }
        public DbSet<CadastroCdaUsuarioClienteDivisao> CadastroCdaUsuarioClienteDivisaos { get; set; }
        public DbSet<CadastroComplemento> CadastroComplementoes { get; set; }
        public DbSet<CadastroCondicaoEntrega> CadastroCondicaoEntregas { get; set; }
        public DbSet<CadastroContato> CadastroContatoes { get; set; }
        public DbSet<CadastroContatoAlerta> CadastroContatoAlertas { get; set; }
        public DbSet<CadastroContatoEndereco> CadastroContatoEnderecoes { get; set; }
        public DbSet<CadastroDepartamento> CadastroDepartamentoes { get; set; }
        public DbSet<CadastroEndereco> CadastroEnderecoes { get; set; }
        public DbSet<CadastroEntrega> CadastroEntregas { get; set; }
        public DbSet<CadastroHistorico> CadastroHistoricoes { get; set; }
        public DbSet<CadastroImagem> CadastroImagems { get; set; }
        public DbSet<CadastroReferencia> CadastroReferencias { get; set; }
        public DbSet<CadastroRegiao> CadastroRegiaos { get; set; }
        public DbSet<CadastroTipoDeContato> CadastroTipoDeContatoes { get; set; }
        public DbSet<CanalDeVenda> CanalDeVendas { get; set; }
        public DbSet<CentroDeCusto> CentroDeCustoes { get; set; }
        public DbSet<CentroDeCustoFilial> CentroDeCustoFilials { get; set; }
        public DbSet<Cfop> Cfops { get; set; }
        public DbSet<Cheque> Cheques { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<CidadeDistancia> CidadeDistancias { get; set; }
        public DbSet<CidadeFaixaDeCep> CidadeFaixaDeCeps { get; set; }
        public DbSet<CIDADE1> CIDADES1 { get; set; }
        public DbSet<CidadeSedex> CidadeSedexes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteDivisao> ClienteDivisaos { get; set; }
        public DbSet<ClienteEDI> ClienteEDIs { get; set; }
        public DbSet<ClienteFilial> ClienteFilials { get; set; }
        public DbSet<ClienteJustificativa> ClienteJustificativas { get; set; }
        public DbSet<ClienteMeta> ClienteMetas { get; set; }
        public DbSet<ClienteRegra> ClienteRegras { get; set; }
        public DbSet<ClientesComprovei> ClientesComproveis { get; set; }
        public DbSet<ClienteSetorFilial> ClienteSetorFilials { get; set; }
        public DbSet<ClienteTipoDeMaterial> ClienteTipoDeMaterials { get; set; }
        public DbSet<ClienteTipoDeMaterialDivisao> ClienteTipoDeMaterialDivisaos { get; set; }
        public DbSet<ColetorConferencia> ColetorConferencias { get; set; }
        public DbSet<ColetorConferenciaCDEAN> ColetorConferenciaCDEANs { get; set; }
        public DbSet<ColetorConferenciaItem> ColetorConferenciaItems { get; set; }
        public DbSet<ColetorConferenciaLog> ColetorConferenciaLogs { get; set; }
        public DbSet<ColetorConferenciaVolume> ColetorConferenciaVolumes { get; set; }
        public DbSet<Conciliacao> Conciliacaos { get; set; }
        public DbSet<ConciliacaoLote> ConciliacaoLotes { get; set; }
        public DbSet<ConciliacaoLoteDetalhe> ConciliacaoLoteDetalhes { get; set; }
        public DbSet<CondicaoDePagamento> CondicaoDePagamentoes { get; set; }
        public DbSet<CondicaoDePagamentoItem> CondicaoDePagamentoItems { get; set; }
        public DbSet<Conferencia> Conferencias { get; set; }
        public DbSet<ConferenciaItemNaoCadastrado> ConferenciaItemNaoCadastradoes { get; set; }
        public DbSet<ConferenciaPallet> ConferenciaPallets { get; set; }
        public DbSet<ConferenciaPalletDoc> ConferenciaPalletDocs { get; set; }
        public DbSet<ConferenciaPalletDocVol> ConferenciaPalletDocVols { get; set; }
        public DbSet<ConferenciaPalletDocVolItem> ConferenciaPalletDocVolItems { get; set; }
        public DbSet<ConferenciaPalletEntrada> ConferenciaPalletEntradas { get; set; }
        public DbSet<ConferenciaPalletEntradaLote> ConferenciaPalletEntradaLotes { get; set; }
        public DbSet<ConferenciaPalletProdutoXXXXX> ConferenciaPalletProdutoXXXXXes { get; set; }
        public DbSet<ContaContabil> ContaContabils { get; set; }
        public DbSet<ContaContabilCentroDeCusto> ContaContabilCentroDeCustoes { get; set; }
        public DbSet<ContaContabilFilial> ContaContabilFilials { get; set; }
        public DbSet<ContaContabilLancamento> ContaContabilLancamentoes { get; set; }
        public DbSet<ContaDeEmail> ContaDeEmails { get; set; }
        public DbSet<ContaDespesa> ContaDespesas { get; set; }
        public DbSet<ContaDespesaCapa> ContaDespesaCapas { get; set; }
        public DbSet<ContaDespesaEvento> ContaDespesaEventoes { get; set; }
        public DbSet<ContaDespesaImagem> ContaDespesaImagems { get; set; }
        public DbSet<ContaDespesaItem> ContaDespesaItems { get; set; }
        public DbSet<ContaDespesaObservacao> ContaDespesaObservacaos { get; set; }
        public DbSet<Contrato> Contratoes { get; set; }
        public DbSet<ContratoCapa> ContratoCapas { get; set; }
        public DbSet<ContratoEvento> ContratoEventoes { get; set; }
        public DbSet<ContratoImagem> ContratoImagems { get; set; }
        public DbSet<ContratoItem> ContratoItems { get; set; }
        public DbSet<ContratoItemCCusto> ContratoItemCCustoes { get; set; }
        public DbSet<ContratoObservacao> ContratoObservacaos { get; set; }
        public DbSet<ControleBriefing> ControleBriefings { get; set; }
        public DbSet<ControleBriefingItem> ControleBriefingItems { get; set; }
        public DbSet<CotacaoDeCompra> CotacaoDeCompras { get; set; }
        public DbSet<CotacaoDeCompraItem> CotacaoDeCompraItems { get; set; }
        public DbSet<CotacaoFornecedor> CotacaoFornecedors { get; set; }
        public DbSet<CotacaoFornecedorCondPgto> CotacaoFornecedorCondPgtoes { get; set; }
        public DbSet<DDA> DDAs { get; set; }
        public DbSet<DDAImagem> DDAImagems { get; set; }
        public DbSet<Departamento> Departamentoes { get; set; }
        public DbSet<Deposito> Depositoes { get; set; }
        public DbSet<DepositoPlanta> DepositoPlantas { get; set; }
        public DbSet<DepositoPlantaLeiaute> DepositoPlantaLeiautes { get; set; }
        public DbSet<DepositoPlantaLocalizacao> DepositoPlantaLocalizacaos { get; set; }
        public DbSet<DepositoPlantaLocalizacaoRegra> DepositoPlantaLocalizacaoRegras { get; set; }
        public DbSet<Dica> Dicas { get; set; }
        public DbSet<Dicionario> Dicionarios { get; set; }
        public DbSet<Documento> Documentoes { get; set; }
        public DbSet<DocumentoAFaturar> DocumentoAFaturars { get; set; }
        public DbSet<DocumentoAguardandoCTRC> DocumentoAguardandoCTRCs { get; set; }
        public DbSet<DocumentoAprovacao> DocumentoAprovacaos { get; set; }
        public DbSet<DocumentoAte> DocumentoAtes { get; set; }
        public DbSet<DocumentoAteItem> DocumentoAteItems { get; set; }
        public DbSet<DocumentoCDA> DocumentoCDAs { get; set; }
        public DbSet<DocumentoCfop> DocumentoCfops { get; set; }
        public DbSet<DocumentoComprovante> DocumentoComprovantes { get; set; }
        public DbSet<DocumentoCondicaoDePagamento> DocumentoCondicaoDePagamentoes { get; set; }
        public DbSet<DocumentoCotacao> DocumentoCotacaos { get; set; }
        public DbSet<DocumentoCubagem> DocumentoCubagems { get; set; }
        public DbSet<DocumentoEdi> DocumentoEdis { get; set; }
        public DbSet<DocumentoEletronico> DocumentoEletronicoes { get; set; }
        public DbSet<DocumentoEletronicoParametro> DocumentoEletronicoParametroes { get; set; }
        public DbSet<DocumentoEmbalagem> DocumentoEmbalagems { get; set; }
        public DbSet<DocumentoFilial> DocumentoFilials { get; set; }
        public DbSet<DocumentoFilialPedido> DocumentoFilialPedidoes { get; set; }
        public DbSet<DocumentoFrete> DocumentoFretes { get; set; }
        public DbSet<DocumentoImagem> DocumentoImagems { get; set; }
        public DbSet<DocumentoImposto> DocumentoImpostoes { get; set; }
        public DbSet<DocumentoItem> DocumentoItems { get; set; }
        public DbSet<DocumentoItemComplemento> DocumentoItemComplementoes { get; set; }
        public DbSet<DocumentoItemEmpenhado> DocumentoItemEmpenhadoes { get; set; }
        public DbSet<DocumentoItemLabelPicking> DocumentoItemLabelPickings { get; set; }
        public DbSet<DocumentoMigrado> DocumentoMigradoes { get; set; }
        public DbSet<DocumentoModelo> DocumentoModeloes { get; set; }
        public DbSet<DocumentoNotaFiscal> DocumentoNotaFiscals { get; set; }
        public DbSet<DocumentoObjeto> DocumentoObjetoes { get; set; }
        public DbSet<DocumentoObjetoOcorrencia> DocumentoObjetoOcorrencias { get; set; }
        public DbSet<DocumentoObservacao> DocumentoObservacaos { get; set; }
        public DbSet<DocumentoOcorrencia> DocumentoOcorrencias { get; set; }
        public DbSet<DocumentoOcorrenciaArquivo> DocumentoOcorrenciaArquivoes { get; set; }
        public DbSet<DocumentoOcorrenciaItem> DocumentoOcorrenciaItems { get; set; }
        public DbSet<DocumentoOrcamento> DocumentoOrcamentoes { get; set; }
        public DbSet<DOCUMENTOPEDIDO> DOCUMENTOPEDIDOes { get; set; }
        public DbSet<DOCUMENTOPEDIDOITEM> DOCUMENTOPEDIDOITEMs { get; set; }
        public DbSet<DocumentoProtocolo> DocumentoProtocoloes { get; set; }
        public DbSet<DocumentoRecebimento> DocumentoRecebimentoes { get; set; }
        public DbSet<DocumentoReclamacao> DocumentoReclamacaos { get; set; }
        public DbSet<DocumentoRelacionado> DocumentoRelacionadoes { get; set; }
        public DbSet<DocumentoRemessa> DocumentoRemessas { get; set; }
        public DbSet<DocumentoReprogramado> DocumentoReprogramadoes { get; set; }
        public DbSet<DocumentoSituacao> DocumentoSituacaos { get; set; }
        public DbSet<DocumentoTipo> DocumentoTipoes { get; set; }
        public DbSet<DT> DTs { get; set; }
        public DbSet<DtCheque> DtCheques { get; set; }
        public DbSet<DTContaCorrente> DTContaCorrentes { get; set; }
        public DbSet<DTEletronico> DTEletronicoes { get; set; }
        public DbSet<DTFaturamento> DTFaturamentoes { get; set; }
        public DbSet<DTFaturamentoCliente> DTFaturamentoClientes { get; set; }
        public DbSet<DTFaturamentoClienteDocumento> DTFaturamentoClienteDocumentoes { get; set; }
        public DbSet<DTHistorico> DTHistoricoes { get; set; }
        public DbSet<DTLancamento> DTLancamentoes { get; set; }
        public DbSet<dtproperty> dtproperties { get; set; }
        public DbSet<DTRomaneio> DTRomaneios { get; set; }
        public DbSet<DTTipo> DTTipoes { get; set; }
        public DbSet<EDI> EDIs { get; set; }
        public DbSet<EDI_Bairro> EDI_Bairro { get; set; }
        public DbSet<EDI_Cadastro> EDI_Cadastro { get; set; }
        public DbSet<EDI_CadastroContatoEndereco> EDI_CadastroContatoEndereco { get; set; }
        public DbSet<EDI_Cidade> EDI_Cidade { get; set; }
        public DbSet<EDI_Documento> EDI_Documento { get; set; }
        public DbSet<EDI_DocumentoAguardandoCtrc> EDI_DocumentoAguardandoCtrc { get; set; }
        public DbSet<EDI_DocumentoEdi> EDI_DocumentoEdi { get; set; }
        public DbSet<EDI_DocumentoFilial> EDI_DocumentoFilial { get; set; }
        public DbSet<EDI_DocumentoFrete> EDI_DocumentoFrete { get; set; }
        public DbSet<EDI_DocumentoItem> EDI_DocumentoItem { get; set; }
        public DbSet<EDI_DocumentoNFE> EDI_DocumentoNFE { get; set; }
        public DbSet<EDI_DocumentoNotaFiscal> EDI_DocumentoNotaFiscal { get; set; }
        public DbSet<EDI_DocumentoRelacionado> EDI_DocumentoRelacionado { get; set; }
        public DbSet<EDI_Edi> EDI_Edi { get; set; }
        public DbSet<EDI_Estado> EDI_Estado { get; set; }
        public DbSet<Edi_Gerado> Edi_Gerado { get; set; }
        public DbSet<edi_gerado_antigo> edi_gerado_antigo { get; set; }
        public DbSet<EDI_Produto> EDI_Produto { get; set; }
        public DbSet<EDI_ProdutoCliente> EDI_ProdutoCliente { get; set; }
        public DbSet<EDI_ProdutoEmbalagem> EDI_ProdutoEmbalagem { get; set; }
        public DbSet<EdiArquivo> EdiArquivoes { get; set; }
        public DbSet<EdiControleDeArquivo> EdiControleDeArquivoes { get; set; }
        public DbSet<EdiControleDeArquivoLog> EdiControleDeArquivoLogs { get; set; }
        public DbSet<EDILayOut> EDILayOuts { get; set; }
        public DbSet<EDILayOutPergunta> EDILayOutPerguntas { get; set; }
        public DbSet<EDILayOutReg> EDILayOutRegs { get; set; }
        public DbSet<EDILayOutRegCpo> EDILayOutRegCpoes { get; set; }
        public DbSet<EDILayoutRegTabela> EDILayoutRegTabelas { get; set; }
        public DbSet<EDILayoutTabela> EDILayoutTabelas { get; set; }
        public DbSet<EDIPergunta> EDIPerguntas { get; set; }
        public DbSet<EdiPlanilha> EdiPlanilhas { get; set; }
        public DbSet<EdiPlanilhaDetalhe> EdiPlanilhaDetalhes { get; set; }
        public DbSet<EDITransferencia> EDITransferencias { get; set; }
        public DbSet<EdiTrocaDeArquivo> EdiTrocaDeArquivoes { get; set; }
        public DbSet<EmailPendente> EmailPendentes { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EnquadramentoIPI> EnquadramentoIPIs { get; set; }
        public DbSet<Estado> Estadoes { get; set; }
        public DbSet<EstadoFaixaDeCep> EstadoFaixaDeCeps { get; set; }
        public DbSet<ESTADO1> ESTADOS { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<EstoqueComprasMov> EstoqueComprasMovs { get; set; }
        public DbSet<EstoqueDivisao> EstoqueDivisaos { get; set; }
        public DbSet<EstoqueDivisaoMov> EstoqueDivisaoMovs { get; set; }
        public DbSet<EstoqueMov> EstoqueMovs { get; set; }
        public DbSet<EstoqueOperacao> EstoqueOperacaos { get; set; }
        public DbSet<ExtratoParaConciliacao> ExtratoParaConciliacaos { get; set; }
        public DbSet<Face_RE> Face_RE { get; set; }
        public DbSet<Feriado> Feriadoes { get; set; }
        public DbSet<Filial> Filials { get; set; }
        public DbSet<FilialCidadeSetor> FilialCidadeSetors { get; set; }
        public DbSet<FilialPortaria> FilialPortarias { get; set; }
        public DbSet<FinalidadeDocTed> FinalidadeDocTeds { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Fornecedor> Fornecedors { get; set; }
        public DbSet<FornecedorFilial> FornecedorFilials { get; set; }
        public DbSet<Frota> Frotas { get; set; }
        public DbSet<FrotaGrupoItemControle> FrotaGrupoItemControles { get; set; }
        public DbSet<FrotaGrupoItemControleItem> FrotaGrupoItemControleItems { get; set; }
        public DbSet<FrotaItemControle> FrotaItemControles { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Gaiola> Gaiolas { get; set; }
        public DbSet<GaiolaConferencia> GaiolaConferencias { get; set; }
        public DbSet<GrandeUsuario> GrandeUsuarios { get; set; }
        public DbSet<GrandeUsuarioEndereco> GrandeUsuarioEnderecoes { get; set; }
        public DbSet<Grupo> Grupoes { get; set; }
        public DbSet<GrupoDeProduto> GrupoDeProdutoes { get; set; }
        public DbSet<HistoricoPadrao> HistoricoPadraos { get; set; }
        public DbSet<Icm> Icms { get; set; }
        public DbSet<IdTabela> IdTabelas { get; set; }
        public DbSet<Inss> Insses { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<InventarioContagem> InventarioContagems { get; set; }
        public DbSet<InventarioContagemProduto> InventarioContagemProdutoes { get; set; }
        public DbSet<InventarioUa> InventarioUas { get; set; }
        public DbSet<Irrf> Irrfs { get; set; }
        public DbSet<KPIIRWIN_V2> KPIIRWIN_V2 { get; set; }
        public DbSet<KPIIRWINProcessamento> KPIIRWINProcessamentoes { get; set; }
        public DbSet<LabelPicking> LabelPickings { get; set; }
        public DbSet<Lancamento> Lancamentoes { get; set; }
        public DbSet<LancamentoContabil> LancamentoContabils { get; set; }
        public DbSet<LancamentoContabilCC> LancamentoContabilCCs { get; set; }
        public DbSet<LancamentoOcorrencia> LancamentoOcorrencias { get; set; }
        public DbSet<LancamentoPadrao> LancamentoPadraos { get; set; }
        public DbSet<LancamentoPadraoConfiguracao> LancamentoPadraoConfiguracaos { get; set; }
        public DbSet<LeiauteBoleto> LeiauteBoletoes { get; set; }
        public DbSet<LeiauteCheque> LeiauteCheques { get; set; }
        public DbSet<Licenciamento> Licenciamentoes { get; set; }
        public DbSet<LicenciamentoMe> LicenciamentoMes { get; set; }
        public DbSet<LOG_BAIRRO> LOG_BAIRRO { get; set; }
        public DbSet<LOG_CPC> LOG_CPC { get; set; }
        public DbSet<LOG_FAIXA_BAIRRO> LOG_FAIXA_BAIRRO { get; set; }
        public DbSet<LOG_FAIXA_CPC> LOG_FAIXA_CPC { get; set; }
        public DbSet<LOG_FAIXA_LOCALIDADE> LOG_FAIXA_LOCALIDADE { get; set; }
        public DbSet<LOG_FAIXA_UF> LOG_FAIXA_UF { get; set; }
        public DbSet<LOG_FAIXA_UOP> LOG_FAIXA_UOP { get; set; }
        public DbSet<LOG_GRANDE_USUARIO> LOG_GRANDE_USUARIO { get; set; }
        public DbSet<LOG_LOCALIDADE> LOG_LOCALIDADE { get; set; }
        public DbSet<LOG_LOGRADOURO> LOG_LOGRADOURO { get; set; }
        public DbSet<LOG_NUM_SEC> LOG_NUM_SEC { get; set; }
        public DbSet<LOG_PAIS> LOG_PAIS { get; set; }
        public DbSet<LOG_UNID_OPER> LOG_UNID_OPER { get; set; }
        public DbSet<LOG_VAR_BAI> LOG_VAR_BAI { get; set; }
        public DbSet<LOG_VAR_LOC> LOG_VAR_LOC { get; set; }
        public DbSet<LOG_VAR_LOG> LOG_VAR_LOG { get; set; }
        public DbSet<LogComprovei> LogComproveis { get; set; }
        public DbSet<LoginPerfil> LoginPerfils { get; set; }
        public DbSet<LoginPerfilPermissao> LoginPerfilPermissaos { get; set; }
        public DbSet<LogMetodo> LogMetodoes { get; set; }
        public DbSet<LOGRADOURO> LOGRADOUROS { get; set; }
        public DbSet<LogRoboAcertoNota> LogRoboAcertoNotas { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<LoteEletronico> LoteEletronicoes { get; set; }
        public DbSet<Mapa> Mapas { get; set; }
        public DbSet<MenuCliente> MenuClientes { get; set; }
        public DbSet<MenuSite> MenuSites { get; set; }
        public DbSet<mMotoristaProprietario> mMotoristaProprietarios { get; set; }
        public DbSet<Modal> Modals { get; set; }
        public DbSet<Modalidade> Modalidades { get; set; }
        public DbSet<ModalidadeDePagamento> ModalidadeDePagamentoes { get; set; }
        public DbSet<ModalidadeDocTed> ModalidadeDocTeds { get; set; }
        public DbSet<ModalidadeFinalidadeDocTed> ModalidadeFinalidadeDocTeds { get; set; }
        public DbSet<Modulo> Moduloes { get; set; }
        public DbSet<ModuloMenu> ModuloMenus { get; set; }
        public DbSet<ModuloMenuBkp> ModuloMenuBkps { get; set; }
        public DbSet<ModuloOpcao> ModuloOpcaos { get; set; }
        public DbSet<ModuloOpcaoAcao> ModuloOpcaoAcaos { get; set; }
        public DbSet<ModuloOpcaoTabela> ModuloOpcaoTabelas { get; set; }
        public DbSet<Moeda> Moedas { get; set; }
        public DbSet<MoedaCotacao> MoedaCotacaos { get; set; }
        public DbSet<Motivo> Motivoes { get; set; }
        public DbSet<MotivoRejeicao> MotivoRejeicaos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<MotoristaFilial> MotoristaFilials { get; set; }
        public DbSet<MotoristaHistorico> MotoristaHistoricoes { get; set; }
        public DbSet<Movimentacao> Movimentacaos { get; set; }
        public DbSet<MovimentacaoBancaria> MovimentacaoBancarias { get; set; }
        public DbSet<MovimentacaoCliente> MovimentacaoClientes { get; set; }
        public DbSet<movimentacaocliente_bkp> movimentacaocliente_bkp { get; set; }
        public DbSet<MovimentacaoClienteConsolidado> MovimentacaoClienteConsolidadoes { get; set; }
        public DbSet<MovimentacaoClienteDivisao> MovimentacaoClienteDivisaos { get; set; }
        public DbSet<MovimentacaoItem> MovimentacaoItems { get; set; }
        public DbSet<MovimentacaoRomaneio> MovimentacaoRomaneios { get; set; }
        public DbSet<MovimentacaoRomaneioItem> MovimentacaoRomaneioItems { get; set; }
        public DbSet<Natureza> Naturezas { get; set; }
        public DbSet<Numerador> Numeradors { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<OcorrenciaAcao> OcorrenciaAcaos { get; set; }
        public DbSet<OcorrenciaAndamento> OcorrenciaAndamentoes { get; set; }
        public DbSet<OcorrenciaCodigo> OcorrenciaCodigoes { get; set; }
        public DbSet<OcorrenciaDePara> OcorrenciaDeParas { get; set; }
        public DbSet<OcorrenciaSerie> OcorrenciaSeries { get; set; }
        public DbSet<Operacao> Operacaos { get; set; }
        public DbSet<Orcamento> Orcamentoes { get; set; }
        public DbSet<OrcamentoPedido> OrcamentoPedidoes { get; set; }
        public DbSet<OrcamentoPedidoItem> OrcamentoPedidoItems { get; set; }
        public DbSet<Pai> Pais { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        public DbSet<PalletDocumento> PalletDocumentoes { get; set; }
        public DbSet<PalletDocumentoItem> PalletDocumentoItems { get; set; }
        public DbSet<PalletDocumentoItemCarregamento> PalletDocumentoItemCarregamentoes { get; set; }
        public DbSet<ParametroFluxoDeCaixa> ParametroFluxoDeCaixas { get; set; }
        public DbSet<PercursoMdfe> PercursoMdfes { get; set; }
        public DbSet<PlanoDeContas_Ant> PlanoDeContas_Ant { get; set; }
        public DbSet<Planta> Plantas { get; set; }
        public DbSet<Portaria> Portarias { get; set; }
        public DbSet<PortariaVisitante> PortariaVisitantes { get; set; }
        public DbSet<PreFatura> PreFaturas { get; set; }
        public DbSet<PreFaturaDocumento> PreFaturaDocumentoes { get; set; }
        public DbSet<PreFaturaRoge> PreFaturaRoges { get; set; }
        public DbSet<PrevisaoDeMaterial> PrevisaoDeMaterials { get; set; }
        public DbSet<Produto> Produtoes { get; set; }
        public DbSet<ProdutoCliente> ProdutoClientes { get; set; }
        public DbSet<ProdutoClienteAvaria> ProdutoClienteAvarias { get; set; }
        public DbSet<ProdutoClienteRegra> ProdutoClienteRegras { get; set; }
        public DbSet<ProdutoEmbalagem> ProdutoEmbalagems { get; set; }
        public DbSet<ProdutoEstrutura> ProdutoEstruturas { get; set; }
        public DbSet<ProdutoFoto> ProdutoFotoes { get; set; }
        public DbSet<Projeto> Projetoes { get; set; }
        public DbSet<ProjetoArquivo> ProjetoArquivoes { get; set; }
        public DbSet<ProjetoFilial> ProjetoFilials { get; set; }
        public DbSet<ProjetoItem> ProjetoItems { get; set; }
        public DbSet<ProjetoProducao> ProjetoProducaos { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<RamoDeAtividade> RamoDeAtividades { get; set; }
        public DbSet<Rastreador> Rastreadors { get; set; }
        public DbSet<Rastreamento> Rastreamentoes { get; set; }
        public DbSet<Reclamacao> Reclamacaos { get; set; }
        public DbSet<Redespacho> Redespachoes { get; set; }
        public DbSet<Regiao> Regiaos { get; set; }
        public DbSet<RegiaoItem> RegiaoItems { get; set; }
        public DbSet<ReposicaoRoge> ReposicaoRoges { get; set; }
        public DbSet<ReposicaoRogeCB> ReposicaoRogeCBs { get; set; }
        public DbSet<ReposicaoRogeConferenciaCega> ReposicaoRogeConferenciaCegas { get; set; }
        public DbSet<ReposicaoRogeEan> ReposicaoRogeEans { get; set; }
        public DbSet<ReposicaoRogeItem> ReposicaoRogeItems { get; set; }
        public DbSet<ReposicaoRogeVolume> ReposicaoRogeVolumes { get; set; }
        public DbSet<Representante> Representantes { get; set; }
        public DbSet<RepresentanteCliente> RepresentanteClientes { get; set; }
        public DbSet<RequisicaoDeMaterial> RequisicaoDeMaterials { get; set; }
        public DbSet<RequisicaoDeMaterialDocumento> RequisicaoDeMaterialDocumentoes { get; set; }
        public DbSet<RequisicaoDeMaterialItem> RequisicaoDeMaterialItems { get; set; }
        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<RetornoComprovei> RetornoComproveis { get; set; }
        public DbSet<Romaneio> Romaneios { get; set; }
        public DbSet<RomaneioConferencia> RomaneioConferencias { get; set; }
        public DbSet<RomaneioConferenciaItem> RomaneioConferenciaItems { get; set; }
        public DbSet<RomaneioDocumento> RomaneioDocumentoes { get; set; }
        public DbSet<RomaneioDocumentoConferencia> RomaneioDocumentoConferencias { get; set; }
        public DbSet<RomaneioDocumentoFrete> RomaneioDocumentoFretes { get; set; }
        public DbSet<RomaneioDocumentoItem> RomaneioDocumentoItems { get; set; }
        public DbSet<RomaneioOcorrencia> RomaneioOcorrencias { get; set; }
        public DbSet<RomaneioPrevisao> RomaneioPrevisaos { get; set; }
        public DbSet<RomaneioPrevisaoRegiao> RomaneioPrevisaoRegiaos { get; set; }
        public DbSet<Roteirizacao> Roteirizacaos { get; set; }
        public DbSet<RoteirizacaoTipo> RoteirizacaoTipoes { get; set; }
        public DbSet<RPCI> RPCIs { get; set; }
        public DbSet<RPCIDocumento> RPCIDocumentoes { get; set; }
        public DbSet<RPCIImagem> RPCIImagems { get; set; }
        public DbSet<Rua> Ruas { get; set; }
        public DbSet<Servico> Servicoes { get; set; }
        public DbSet<Setor> Setors { get; set; }
        public DbSet<SetorCep> SetorCeps { get; set; }
        public DbSet<SituacaoTributariaCofin> SituacaoTributariaCofins { get; set; }
        public DbSet<SituacaoTributariaIcm> SituacaoTributariaIcms { get; set; }
        public DbSet<SituacaoTributariaIPI> SituacaoTributariaIPIs { get; set; }
        public DbSet<SituacaoTributariaPI> SituacaoTributariaPIS { get; set; }
        public DbSet<Sobra> Sobras { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<sysproperty> sysproperties { get; set; }
        public DbSet<TabelaDeFrete> TabelaDeFretes { get; set; }
        public DbSet<TabelaDeFreteProduto> TabelaDeFreteProdutoes { get; set; }
        public DbSet<TabelaDeFreteRota> TabelaDeFreteRotas { get; set; }
        public DbSet<TabelaDeFreteRotaModal> TabelaDeFreteRotaModals { get; set; }
        public DbSet<TabelaDeFreteRotaModalValor> TabelaDeFreteRotaModalValors { get; set; }
        public DbSet<TabelaDeFreteVigencia> TabelaDeFreteVigencias { get; set; }
        public DbSet<TB_AUX_RE> TB_AUX_RE { get; set; }
        public DbSet<tblBairro> tblBairros { get; set; }
        public DbSet<tblCidade> tblCidades { get; set; }
        public DbSet<tblLogradouro> tblLogradouros { get; set; }
        public DbSet<tblUF> tblUFs { get; set; }
        public DbSet<TE> TES { get; set; }
        public DbSet<TESCFOP> TESCFOPs { get; set; }
        public DbSet<TESControle> TESControles { get; set; }
        public DbSet<TipoDeEscolta> TipoDeEscoltas { get; set; }
        public DbSet<TipoDeItem> TipoDeItems { get; set; }
        public DbSet<TipoDeMonitoramento> TipoDeMonitoramentoes { get; set; }
        public DbSet<TipoDeMovimento> TipoDeMovimentoes { get; set; }
        public DbSet<TipoDeOperacao> TipoDeOperacaos { get; set; }
        public DbSet<TipoDeTitulo> TipoDeTituloes { get; set; }
        public DbSet<TipoDeTituloDuplicata> TipoDeTituloDuplicatas { get; set; }
        public DbSet<TipoDeVolume> TipoDeVolumes { get; set; }
        public DbSet<Titulo> Tituloes { get; set; }
        public DbSet<TituloDocumento> TituloDocumentoes { get; set; }
        public DbSet<TituloDuplicata> TituloDuplicatas { get; set; }
        public DbSet<TituloDuplicataHistorico> TituloDuplicataHistoricoes { get; set; }
        public DbSet<TituloDuplicataRemessa> TituloDuplicataRemessas { get; set; }
        public DbSet<TituloHistorico> TituloHistoricoes { get; set; }
        public DbSet<TituloImagem> TituloImagems { get; set; }
        public DbSet<TituloItem> TituloItems { get; set; }
        public DbSet<Transportadora> Transportadoras { get; set; }
        public DbSet<UnidadeDeArmazenagem> UnidadeDeArmazenagems { get; set; }
        public DbSet<UnidadeDeArmazenagemAgrup> UnidadeDeArmazenagemAgrups { get; set; }
        public DbSet<UnidadeDeArmazenagemLote> UnidadeDeArmazenagemLotes { get; set; }
        public DbSet<UnidadeDeArmazenagemMov> UnidadeDeArmazenagemMovs { get; set; }
        public DbSet<UnidadeDeMedida> UnidadeDeMedidas { get; set; }
        public DbSet<UnidadeDeNegocio> UnidadeDeNegocios { get; set; }
        public DbSet<UnidadeFuncional> UnidadeFuncionals { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioAlerta> UsuarioAlertas { get; set; }
        public DbSet<UsuarioCentroDeCusto> UsuarioCentroDeCustoes { get; set; }
        public DbSet<UsuarioCentroDeCustoOperacao> UsuarioCentroDeCustoOperacaos { get; set; }
        public DbSet<UsuarioCentroDeCustoOperacaoLog> UsuarioCentroDeCustoOperacaoLogs { get; set; }
        public DbSet<UsuarioCliente> UsuarioClientes { get; set; }
        public DbSet<UsuarioClienteDivisao> UsuarioClienteDivisaos { get; set; }
        public DbSet<UsuarioCompra> UsuarioCompras { get; set; }
        public DbSet<UsuarioCota> UsuarioCotas { get; set; }
        public DbSet<UsuarioDeTabelaDeFrete> UsuarioDeTabelaDeFretes { get; set; }
        public DbSet<UsuarioFavorito> UsuarioFavoritos { get; set; }
        public DbSet<UsuarioFilial> UsuarioFilials { get; set; }
        public DbSet<UsuarioGrade> UsuarioGrades { get; set; }
        public DbSet<UsuarioGradeCampo> UsuarioGradeCampoes { get; set; }
        public DbSet<UsuarioModal> UsuarioModals { get; set; }
        public DbSet<UsuarioOpcao> UsuarioOpcaos { get; set; }
        public DbSet<UsuarioOpcaoAcesso> UsuarioOpcaoAcessoes { get; set; }
        public DbSet<UsuarioOperacao> UsuarioOperacaos { get; set; }
        public DbSet<UsuarioOperacaoLog> UsuarioOperacaoLogs { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfils { get; set; }
        public DbSet<Veiculo> Veiculoes { get; set; }
        public DbSet<VeiculoFilial> VeiculoFilials { get; set; }
        public DbSet<VeiculoFoto> VeiculoFotoes { get; set; }
        public DbSet<VeiculoLicenciamento> VeiculoLicenciamentoes { get; set; }
        public DbSet<VeiculoMarca> VeiculoMarcas { get; set; }
        public DbSet<VeiculoModelo> VeiculoModeloes { get; set; }
        public DbSet<VeiculoRastreador> VeiculoRastreadors { get; set; }
        public DbSet<VeiculoTabela> VeiculoTabelas { get; set; }
        public DbSet<VeiculoTabelaAgregado> VeiculoTabelaAgregadoes { get; set; }
        public DbSet<VeiculoTabelaRegiao> VeiculoTabelaRegiaos { get; set; }
        public DbSet<VeiculoTabelaRegiaoItem> VeiculoTabelaRegiaoItems { get; set; }
        public DbSet<VeiculoTipo> VeiculoTipoes { get; set; }
        public DbSet<XmlComprovei> XmlComproveis { get; set; }
        public DbSet<ZID_AgendaRecebimento> ZID_AgendaRecebimento { get; set; }
        public DbSet<ZID_Agrupamento> ZID_Agrupamento { get; set; }
        public DbSet<ZID_AgrupamentoRegiao> ZID_AgrupamentoRegiao { get; set; }
        public DbSet<ZID_AVISO> ZID_AVISO { get; set; }
        public DbSet<ZID_BAIRRO> ZID_BAIRRO { get; set; }
        public DbSet<ZID_BANCOCONTA> ZID_BANCOCONTA { get; set; }
        public DbSet<ZID_BANCOCONTABLOQUETO> ZID_BANCOCONTABLOQUETO { get; set; }
        public DbSet<ZID_bordero> ZID_bordero { get; set; }
        public DbSet<ZID_BorderoTituloDuplicata> ZID_BorderoTituloDuplicata { get; set; }
        public DbSet<ZID_CADASTRO> ZID_CADASTRO { get; set; }
        public DbSet<ZID_CadastroCdaUsuarioClienteDivisao> ZID_CadastroCdaUsuarioClienteDivisao { get; set; }
        public DbSet<ZID_CADASTROCOMPLEMENTO> ZID_CADASTROCOMPLEMENTO { get; set; }
        public DbSet<ZID_CadastroCondicaoEntrega> ZID_CadastroCondicaoEntrega { get; set; }
        public DbSet<ZID_cadastrocontato> ZID_cadastrocontato { get; set; }
        public DbSet<ZID_CadastroContatoEndereco> ZID_CadastroContatoEndereco { get; set; }
        public DbSet<ZID_CadastroEndereco> ZID_CadastroEndereco { get; set; }
        public DbSet<ZID_CadastroEntrega> ZID_CadastroEntrega { get; set; }
        public DbSet<ZID_CadastroHistorico> ZID_CadastroHistorico { get; set; }
        public DbSet<ZID_CadastroImagem> ZID_CadastroImagem { get; set; }
        public DbSet<ZID_CadastroReferencia> ZID_CadastroReferencia { get; set; }
        public DbSet<ZID_CentrodeCustoFilial> ZID_CentrodeCustoFilial { get; set; }
        public DbSet<ZID_Cheque> ZID_Cheque { get; set; }
        public DbSet<ZID_CIDADE> ZID_CIDADE { get; set; }
        public DbSet<ZID_clientedivisao> ZID_clientedivisao { get; set; }
        public DbSet<ZID_ClienteEdi> ZID_ClienteEdi { get; set; }
        public DbSet<ZID_ClienteFilial> ZID_ClienteFilial { get; set; }
        public DbSet<ZID_ClienteSetorFilial> ZID_ClienteSetorFilial { get; set; }
        public DbSet<ZID_ClienteTipoDeMaterial> ZID_ClienteTipoDeMaterial { get; set; }
        public DbSet<ZID_ClienteTipoDeMaterialDivisao> ZID_ClienteTipoDeMaterialDivisao { get; set; }
        public DbSet<ZID_COLETORCONFERENCIA> ZID_COLETORCONFERENCIA { get; set; }
        public DbSet<ZID_COLETORCONFERENCIAITEM> ZID_COLETORCONFERENCIAITEM { get; set; }
        public DbSet<ZID_COLETORCONFERENCIALOG> ZID_COLETORCONFERENCIALOG { get; set; }
        public DbSet<ZID_COLETORCONFERENCIAVOLUME> ZID_COLETORCONFERENCIAVOLUME { get; set; }
        public DbSet<ZID_CONFERENCIA> ZID_CONFERENCIA { get; set; }
        public DbSet<ZID_CONFERENCIAPALLET> ZID_CONFERENCIAPALLET { get; set; }
        public DbSet<ZID_CONFERENCIAPALLETDOC> ZID_CONFERENCIAPALLETDOC { get; set; }
        public DbSet<ZID_CONFERENCIAPALLETDOCVOL> ZID_CONFERENCIAPALLETDOCVOL { get; set; }
        public DbSet<ZID_CONFERENCIAPALLETDOCVOLITEM> ZID_CONFERENCIAPALLETDOCVOLITEM { get; set; }
        public DbSet<ZID_CONFERENCIAPALLETENTRADA> ZID_CONFERENCIAPALLETENTRADA { get; set; }
        public DbSet<ZID_CONFERENCIAPALLETENTRADALOTE> ZID_CONFERENCIAPALLETENTRADALOTE { get; set; }
        public DbSet<ZID_contacontabil> ZID_contacontabil { get; set; }
        public DbSet<ZID_ContaContabilFilial> ZID_ContaContabilFilial { get; set; }
        public DbSet<ZID_CONTRATO> ZID_CONTRATO { get; set; }
        public DbSet<ZID_ContratoImagem> ZID_ContratoImagem { get; set; }
        public DbSet<ZID_ContratoItem> ZID_ContratoItem { get; set; }
        public DbSet<ZID_ContratoItemCCusto> ZID_ContratoItemCCusto { get; set; }
        public DbSet<ZID_ContratoObservacao> ZID_ContratoObservacao { get; set; }
        public DbSet<ZID_CotacaoDeCompra> ZID_CotacaoDeCompra { get; set; }
        public DbSet<ZID_CotacaoDeCompraItem> ZID_CotacaoDeCompraItem { get; set; }
        public DbSet<ZID_CotacaoFornecedor> ZID_CotacaoFornecedor { get; set; }
        public DbSet<ZID_Deposito> ZID_Deposito { get; set; }
        public DbSet<ZID_DepositoPlanta> ZID_DepositoPlanta { get; set; }
        public DbSet<ZID_DepositoPlantaLeiaute> ZID_DepositoPlantaLeiaute { get; set; }
        public DbSet<ZID_DepositoPlantaLocalizacao> ZID_DepositoPlantaLocalizacao { get; set; }
        public DbSet<ZID_DepositoPlantaLocalizacaoRegra> ZID_DepositoPlantaLocalizacaoRegra { get; set; }
        public DbSet<ZID_DOCUMENTO> ZID_DOCUMENTO { get; set; }
        public DbSet<ZID_DocumentoAFaturar> ZID_DocumentoAFaturar { get; set; }
        public DbSet<ZID_DOCUMENTOAGUARDANDOCTRC> ZID_DOCUMENTOAGUARDANDOCTRC { get; set; }
        public DbSet<ZID_DocumentoAte> ZID_DocumentoAte { get; set; }
        public DbSet<ZID_DocumentoAteItem> ZID_DocumentoAteItem { get; set; }
        public DbSet<ZID_DocumentoCda> ZID_DocumentoCda { get; set; }
        public DbSet<ZID_DocumentoCFOP> ZID_DocumentoCFOP { get; set; }
        public DbSet<ZID_DOCUMENTOCOMPROVANTE> ZID_DOCUMENTOCOMPROVANTE { get; set; }
        public DbSet<ZID_DocumentoCondicaoDePagamento> ZID_DocumentoCondicaoDePagamento { get; set; }
        public DbSet<ZID_DocumentoCotacao> ZID_DocumentoCotacao { get; set; }
        public DbSet<ZID_DOCUMENTOCUBAGEM> ZID_DOCUMENTOCUBAGEM { get; set; }
        public DbSet<ZID_DocumentoEdi> ZID_DocumentoEdi { get; set; }
        public DbSet<ZID_DocumentoEletronico> ZID_DocumentoEletronico { get; set; }
        public DbSet<ZID_DocumentoEmbalagem> ZID_DocumentoEmbalagem { get; set; }
        public DbSet<ZID_DocumentoFilial> ZID_DocumentoFilial { get; set; }
        public DbSet<ZID_DocumentoFrete> ZID_DocumentoFrete { get; set; }
        public DbSet<ZID_DocumentoImposto> ZID_DocumentoImposto { get; set; }
        public DbSet<ZID_DOCUMENTOITEM> ZID_DOCUMENTOITEM { get; set; }
        public DbSet<ZID_DocumentoItemComplemento> ZID_DocumentoItemComplemento { get; set; }
        public DbSet<ZID_DocumentoObjetoOcorrencia> ZID_DocumentoObjetoOcorrencia { get; set; }
        public DbSet<ZID_DocumentoObservacao> ZID_DocumentoObservacao { get; set; }
        public DbSet<ZID_DocumentoOcorrencia> ZID_DocumentoOcorrencia { get; set; }
        public DbSet<ZID_DOCUMENTOOCORRENCIAARQUIVO> ZID_DOCUMENTOOCORRENCIAARQUIVO { get; set; }
        public DbSet<ZID_DocumentoOcorrenciaItem> ZID_DocumentoOcorrenciaItem { get; set; }
        public DbSet<ZID_DocumentoOrcamento> ZID_DocumentoOrcamento { get; set; }
        public DbSet<ZID_DocumentoRecebimento> ZID_DocumentoRecebimento { get; set; }
        public DbSet<ZID_DocumentoRelacionado> ZID_DocumentoRelacionado { get; set; }
        public DbSet<ZID_DOCUMENTOREMESSA> ZID_DOCUMENTOREMESSA { get; set; }
        public DbSet<ZID_DT> ZID_DT { get; set; }
        public DbSet<ZID_DtContaCorrente> ZID_DtContaCorrente { get; set; }
        public DbSet<ZID_DTEletronico> ZID_DTEletronico { get; set; }
        public DbSet<ZID_DTFaturamento> ZID_DTFaturamento { get; set; }
        public DbSet<ZID_DtFaturamentoCliente> ZID_DtFaturamentoCliente { get; set; }
        public DbSet<ZID_DtFaturamentoClienteDocumento> ZID_DtFaturamentoClienteDocumento { get; set; }
        public DbSet<ZID_DTHistorico> ZID_DTHistorico { get; set; }
        public DbSet<ZID_DTROMANEIO> ZID_DTROMANEIO { get; set; }
        public DbSet<ZID_EDI> ZID_EDI { get; set; }
        public DbSet<ZID_EDI_Bairro> ZID_EDI_Bairro { get; set; }
        public DbSet<ZID_EDI_Cadastro> ZID_EDI_Cadastro { get; set; }
        public DbSet<ZID_EDI_CadastroContatoEndereco> ZID_EDI_CadastroContatoEndereco { get; set; }
        public DbSet<ZID_EDI_Cidade> ZID_EDI_Cidade { get; set; }
        public DbSet<ZID_EDI_Documento> ZID_EDI_Documento { get; set; }
        public DbSet<ZID_EDI_DocumentoAguardandoCtrc> ZID_EDI_DocumentoAguardandoCtrc { get; set; }
        public DbSet<ZID_EDI_DocumentoEdi> ZID_EDI_DocumentoEdi { get; set; }
        public DbSet<ZID_EDI_DocumentoFilial> ZID_EDI_DocumentoFilial { get; set; }
        public DbSet<ZID_EDI_DocumentoFrete> ZID_EDI_DocumentoFrete { get; set; }
        public DbSet<ZID_EDI_DocumentoItem> ZID_EDI_DocumentoItem { get; set; }
        public DbSet<ZID_EDI_DocumentoNFE> ZID_EDI_DocumentoNFE { get; set; }
        public DbSet<ZID_EDI_DocumentoNotaFiscal> ZID_EDI_DocumentoNotaFiscal { get; set; }
        public DbSet<ZID_EDI_DocumentoRelacionado> ZID_EDI_DocumentoRelacionado { get; set; }
        public DbSet<ZID_EDI_Edi> ZID_EDI_Edi { get; set; }
        public DbSet<ZID_EDI_Estado> ZID_EDI_Estado { get; set; }
        public DbSet<ZID_EDI_Produto> ZID_EDI_Produto { get; set; }
        public DbSet<ZID_EDI_ProdutoCliente> ZID_EDI_ProdutoCliente { get; set; }
        public DbSet<ZID_EDI_ProdutoEmbalagem> ZID_EDI_ProdutoEmbalagem { get; set; }
        public DbSet<ZID_EdiControleDeArquivo> ZID_EdiControleDeArquivo { get; set; }
        public DbSet<ZID_EdiControleDeArquivoLog> ZID_EdiControleDeArquivoLog { get; set; }
        public DbSet<ZID_EdiPlanilha> ZID_EdiPlanilha { get; set; }
        public DbSet<ZID_EdiPlanilhaDetalhe> ZID_EdiPlanilhaDetalhe { get; set; }
        public DbSet<ZID_EdiTrocaDeArquivo> ZID_EdiTrocaDeArquivo { get; set; }
        public DbSet<ZID_ESTADO> ZID_ESTADO { get; set; }
        public DbSet<ZID_ESTOQUE> ZID_ESTOQUE { get; set; }
        public DbSet<ZID_EstoqueComprasMov> ZID_EstoqueComprasMov { get; set; }
        public DbSet<ZID_ESTOQUEDIVISAO> ZID_ESTOQUEDIVISAO { get; set; }
        public DbSet<ZID_ESTOQUEDIVISAOMOV> ZID_ESTOQUEDIVISAOMOV { get; set; }
        public DbSet<ZID_ESTOQUEMOV> ZID_ESTOQUEMOV { get; set; }
        public DbSet<ZID_ExtratoParaConciliacao> ZID_ExtratoParaConciliacao { get; set; }
        public DbSet<ZID_feriado> ZID_feriado { get; set; }
        public DbSet<ZID_FilialCidadeSetor> ZID_FilialCidadeSetor { get; set; }
        public DbSet<ZID_FilialPortaria> ZID_FilialPortaria { get; set; }
        public DbSet<ZID_Fornecedorfilial> ZID_Fornecedorfilial { get; set; }
        public DbSet<ZID_GAIOLA> ZID_GAIOLA { get; set; }
        public DbSet<ZID_GAIOLACONFERENCIA> ZID_GAIOLACONFERENCIA { get; set; }
        public DbSet<ZID_grupodeproduto> ZID_grupodeproduto { get; set; }
        public DbSet<ZID_icms> ZID_icms { get; set; }
        public DbSet<ZID_Inventario> ZID_Inventario { get; set; }
        public DbSet<ZID_InventarioContagem> ZID_InventarioContagem { get; set; }
        public DbSet<ZID_inventariocontagemproduto> ZID_inventariocontagemproduto { get; set; }
        public DbSet<ZID_Lancamento> ZID_Lancamento { get; set; }
        public DbSet<ZID_LANCAMENTOCONTABIL> ZID_LANCAMENTOCONTABIL { get; set; }
        public DbSet<ZID_LancamentoContabilCC> ZID_LancamentoContabilCC { get; set; }
        public DbSet<ZID_Lote> ZID_Lote { get; set; }
        public DbSet<ZID_LoteEletronico> ZID_LoteEletronico { get; set; }
        public DbSet<ZID_MAPA> ZID_MAPA { get; set; }
        public DbSet<ZID_Modal> ZID_Modal { get; set; }
        public DbSet<ZID_modulomenu> ZID_modulomenu { get; set; }
        public DbSet<ZID_ModuloOpcao> ZID_ModuloOpcao { get; set; }
        public DbSet<ZID_Motivo> ZID_Motivo { get; set; }
        public DbSet<ZID_MOTORISTAFILIAL> ZID_MOTORISTAFILIAL { get; set; }
        public DbSet<ZID_MotoristaHistorico> ZID_MotoristaHistorico { get; set; }
        public DbSet<ZID_MOVIMENTACAO> ZID_MOVIMENTACAO { get; set; }
        public DbSet<ZID_movimentacaobancaria> ZID_movimentacaobancaria { get; set; }
        public DbSet<ZID_MOVIMENTACAOITEM> ZID_MOVIMENTACAOITEM { get; set; }
        public DbSet<ZID_Numerador> ZID_Numerador { get; set; }
        public DbSet<ZID_ocorrencia> ZID_ocorrencia { get; set; }
        public DbSet<ZID_ocorrenciaserie> ZID_ocorrenciaserie { get; set; }
        public DbSet<ZID_Operacao> ZID_Operacao { get; set; }
        public DbSet<ZID_PAIS> ZID_PAIS { get; set; }
        public DbSet<ZID_PalletDocumento> ZID_PalletDocumento { get; set; }
        public DbSet<ZID_PalletDocumentoItem> ZID_PalletDocumentoItem { get; set; }
        public DbSet<ZID_ParametroFluxoDeCaixa> ZID_ParametroFluxoDeCaixa { get; set; }
        public DbSet<ZID_Portaria> ZID_Portaria { get; set; }
        public DbSet<ZID_PortariaVisitante> ZID_PortariaVisitante { get; set; }
        public DbSet<ZID_PreFatura> ZID_PreFatura { get; set; }
        public DbSet<ZID_PreFaturaDocumento> ZID_PreFaturaDocumento { get; set; }
        public DbSet<ZID_PRODUTO> ZID_PRODUTO { get; set; }
        public DbSet<ZID_PRODUTOCLIENTE> ZID_PRODUTOCLIENTE { get; set; }
        public DbSet<ZID_PRODUTOEMBALAGEM> ZID_PRODUTOEMBALAGEM { get; set; }
        public DbSet<ZID_ProdutoEstrutura> ZID_ProdutoEstrutura { get; set; }
        public DbSet<ZID_produtofoto> ZID_produtofoto { get; set; }
        public DbSet<ZID_RASTREAMENTO> ZID_RASTREAMENTO { get; set; }
        public DbSet<ZID_Regiao> ZID_Regiao { get; set; }
        public DbSet<ZID_regiaoItem> ZID_regiaoItem { get; set; }
        public DbSet<ZID_ReposicaoRoge> ZID_ReposicaoRoge { get; set; }
        public DbSet<ZID_RequisicaoDeMaterial> ZID_RequisicaoDeMaterial { get; set; }
        public DbSet<ZID_RequisicaodeMaterialDocumento> ZID_RequisicaodeMaterialDocumento { get; set; }
        public DbSet<ZID_RequisicaoDeMaterialItem> ZID_RequisicaoDeMaterialItem { get; set; }
        public DbSet<ZID_Romaneio> ZID_Romaneio { get; set; }
        public DbSet<ZID_RomaneioDocumento> ZID_RomaneioDocumento { get; set; }
        public DbSet<ZID_RomaneioDocumentoConferencia> ZID_RomaneioDocumentoConferencia { get; set; }
        public DbSet<ZID_RomaneioDocumentoItem> ZID_RomaneioDocumentoItem { get; set; }
        public DbSet<ZID_RomaneioOcorrencia> ZID_RomaneioOcorrencia { get; set; }
        public DbSet<ZID_RomaneioPrevisao> ZID_RomaneioPrevisao { get; set; }
        public DbSet<ZID_RomaneioPrevisaoRegiao> ZID_RomaneioPrevisaoRegiao { get; set; }
        public DbSet<ZID_RPCI> ZID_RPCI { get; set; }
        public DbSet<ZID_RPCIDocumento> ZID_RPCIDocumento { get; set; }
        public DbSet<ZID_Setor> ZID_Setor { get; set; }
        public DbSet<ZID_SetorCEP> ZID_SetorCEP { get; set; }
        public DbSet<ZID_Sobra> ZID_Sobra { get; set; }
        public DbSet<ZID_TabelaDeFrete> ZID_TabelaDeFrete { get; set; }
        public DbSet<ZID_TabelaDeFreteProduto> ZID_TabelaDeFreteProduto { get; set; }
        public DbSet<ZID_TabelaDeFreteRota> ZID_TabelaDeFreteRota { get; set; }
        public DbSet<ZID_TabelaDeFreteRotaModal> ZID_TabelaDeFreteRotaModal { get; set; }
        public DbSet<ZID_TabelaDeFreteRotaModalValor> ZID_TabelaDeFreteRotaModalValor { get; set; }
        public DbSet<ZID_TabelaDeFreteVigencia> ZID_TabelaDeFreteVigencia { get; set; }
        public DbSet<ZID_TES> ZID_TES { get; set; }
        public DbSet<ZID_TESCFOP> ZID_TESCFOP { get; set; }
        public DbSet<ZID_Titulo> ZID_Titulo { get; set; }
        public DbSet<ZID_TituloDocumento> ZID_TituloDocumento { get; set; }
        public DbSet<ZID_TituloDuplicata> ZID_TituloDuplicata { get; set; }
        public DbSet<ZID_TituloDuplicataHistorico> ZID_TituloDuplicataHistorico { get; set; }
        public DbSet<ZID_TituloDuplicataremessa> ZID_TituloDuplicataremessa { get; set; }
        public DbSet<ZID_TituloHistorico> ZID_TituloHistorico { get; set; }
        public DbSet<ZID_TituloImagem> ZID_TituloImagem { get; set; }
        public DbSet<ZID_TRANSPORTADORA> ZID_TRANSPORTADORA { get; set; }
        public DbSet<ZID_UnidadeDeArmazenagem> ZID_UnidadeDeArmazenagem { get; set; }
        public DbSet<ZID_UnidadeDeArmazenagemAgrup> ZID_UnidadeDeArmazenagemAgrup { get; set; }
        public DbSet<ZID_UnidadeDeArmazenagemLote> ZID_UnidadeDeArmazenagemLote { get; set; }
        public DbSet<ZID_UnidadeDeArmazenagemMov> ZID_UnidadeDeArmazenagemMov { get; set; }
        public DbSet<ZID_usuario> ZID_usuario { get; set; }
        public DbSet<ZID_UsuarioCliente> ZID_UsuarioCliente { get; set; }
        public DbSet<ZID_UsuarioClienteDivisao> ZID_UsuarioClienteDivisao { get; set; }
        public DbSet<ZID_UsuarioCompra> ZID_UsuarioCompra { get; set; }
        public DbSet<ZID_UsuarioDeTabelaDeFrete> ZID_UsuarioDeTabelaDeFrete { get; set; }
        public DbSet<ZID_UsuarioFilial> ZID_UsuarioFilial { get; set; }
        public DbSet<ZID_USUARIOGRADE> ZID_USUARIOGRADE { get; set; }
        public DbSet<ZID_USUARIOGRADECAMPO> ZID_USUARIOGRADECAMPO { get; set; }
        public DbSet<ZID_usuarioopcao> ZID_usuarioopcao { get; set; }
        public DbSet<ZID_USUARIOOPERACAO> ZID_USUARIOOPERACAO { get; set; }
        public DbSet<ZID_UsuarioOperacaoLog> ZID_UsuarioOperacaoLog { get; set; }
        public DbSet<ZID_VEICULO> ZID_VEICULO { get; set; }
        public DbSet<ZID_veiculoFilial> ZID_veiculoFilial { get; set; }
        public DbSet<ZID_veiculomarca> ZID_veiculomarca { get; set; }
        public DbSet<ZID_VEICULOMODELO> ZID_VEICULOMODELO { get; set; }
        public DbSet<ZID_VEICULORASTREADOR> ZID_VEICULORASTREADOR { get; set; }
        public DbSet<ZID_VeiculoTabela> ZID_VeiculoTabela { get; set; }
        public DbSet<ZID_VeiculoTabelaAgregado> ZID_VeiculoTabelaAgregado { get; set; }
        public DbSet<ZID_VeiculoTabelaRegiao> ZID_VeiculoTabelaRegiao { get; set; }
        public DbSet<ZID_VeiculoTabelaRegiaoItem> ZID_VeiculoTabelaRegiaoItem { get; set; }
        public DbSet<ZID_veiculotipo> ZID_veiculotipo { get; set; }
        public DbSet<CalculosRE> CalculosREs { get; set; }
        public DbSet<ProdutosVivo> ProdutosVivoes { get; set; }
        public DbSet<RelControleDePedido> RelControleDePedidos { get; set; }
        public DbSet<ResultadoNFENF> ResultadoNFENFS { get; set; }
        public DbSet<view_relatorioretiracda> view_relatorioretiracda { get; set; }
        public DbSet<VW_LIBERACAO_PEDIDOS> VW_LIBERACAO_PEDIDOS { get; set; }
        public DbSet<VW_LIBERACAO_PEDIDOS_DETALHE> VW_LIBERACAO_PEDIDOS_DETALHE { get; set; }
        public DbSet<VW_RE> VW_RE { get; set; }
        public DbSet<VW_REENTREGA> VW_REENTREGA { get; set; }
        public DbSet<vwIrwinPecasUnidadesEntrada> vwIrwinPecasUnidadesEntradas { get; set; }
        public DbSet<vwIrwinPecasUnidadesRessuprimento> vwIrwinPecasUnidadesRessuprimentoes { get; set; }
        public DbSet<vwIrwinPedidosEmAndamento> vwIrwinPedidosEmAndamentoes { get; set; }
        public DbSet<vwLiberacaoDePedido> vwLiberacaoDePedidos { get; set; }
        public DbSet<vwLiberacaoDePedidos_P1> vwLiberacaoDePedidos_P1 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new HolidayMap());
            modelBuilder.Configurations.Add(new C_CheckList_EspacodiscoMap());
            modelBuilder.Configurations.Add(new AgendumMap());
            modelBuilder.Configurations.Add(new AgendamentoMap());
            modelBuilder.Configurations.Add(new AgendamentoDocumentoMap());
            modelBuilder.Configurations.Add(new AgendaRecebimentoMap());
            modelBuilder.Configurations.Add(new AgregadoMap());
            modelBuilder.Configurations.Add(new AgrupamentoMap());
            modelBuilder.Configurations.Add(new AgrupamentoRegiaoMap());
            modelBuilder.Configurations.Add(new AjudaMap());
            modelBuilder.Configurations.Add(new AlertaMap());
            modelBuilder.Configurations.Add(new ApolouseMap());
            modelBuilder.Configurations.Add(new ApoliceAtuadoreMap());
            modelBuilder.Configurations.Add(new ApoliceItemMap());
            modelBuilder.Configurations.Add(new ArquivoMap());
            modelBuilder.Configurations.Add(new ArquivoClienteEDIMap());
            modelBuilder.Configurations.Add(new ArquivoItemMap());
            modelBuilder.Configurations.Add(new AuditoriaMap());
            modelBuilder.Configurations.Add(new AvisoMap());
            modelBuilder.Configurations.Add(new AvisoDocumentoMap());
            modelBuilder.Configurations.Add(new AvisoRoboEmailMap());
            modelBuilder.Configurations.Add(new BairroMap());
            modelBuilder.Configurations.Add(new BairroFaixaDeCepMap());
            modelBuilder.Configurations.Add(new BAIRRO1Map());
            modelBuilder.Configurations.Add(new BalancaMap());
            modelBuilder.Configurations.Add(new BancoMap());
            modelBuilder.Configurations.Add(new BancoCarteiraMap());
            modelBuilder.Configurations.Add(new BancoContaMap());
            modelBuilder.Configurations.Add(new BancoContaBloquetoMap());
            modelBuilder.Configurations.Add(new BancoEspecieMap());
            modelBuilder.Configurations.Add(new BancoInstrucaoMap());
            modelBuilder.Configurations.Add(new BancoInstrucaoDeProtestoMap());
            modelBuilder.Configurations.Add(new BancoOcorrenciaRejeicaoMap());
            modelBuilder.Configurations.Add(new BancoOcorrenciaRemessaMap());
            modelBuilder.Configurations.Add(new BancoOcorrenciaRetornoMap());
            modelBuilder.Configurations.Add(new BASEMap());
            modelBuilder.Configurations.Add(new BaseDoCreditoMap());
            modelBuilder.Configurations.Add(new BorderoMap());
            modelBuilder.Configurations.Add(new BorderoTituloDuplicataMap());
            modelBuilder.Configurations.Add(new CadastroMap());
            modelBuilder.Configurations.Add(new CadastroCDAMap());
            modelBuilder.Configurations.Add(new CadastroCdaUsuarioClienteDivisaoMap());
            modelBuilder.Configurations.Add(new CadastroComplementoMap());
            modelBuilder.Configurations.Add(new CadastroCondicaoEntregaMap());
            modelBuilder.Configurations.Add(new CadastroContatoMap());
            modelBuilder.Configurations.Add(new CadastroContatoAlertaMap());
            modelBuilder.Configurations.Add(new CadastroContatoEnderecoMap());
            modelBuilder.Configurations.Add(new CadastroDepartamentoMap());
            modelBuilder.Configurations.Add(new CadastroEnderecoMap());
            modelBuilder.Configurations.Add(new CadastroEntregaMap());
            modelBuilder.Configurations.Add(new CadastroHistoricoMap());
            modelBuilder.Configurations.Add(new CadastroImagemMap());
            modelBuilder.Configurations.Add(new CadastroReferenciaMap());
            modelBuilder.Configurations.Add(new CadastroRegiaoMap());
            modelBuilder.Configurations.Add(new CadastroTipoDeContatoMap());
            modelBuilder.Configurations.Add(new CanalDeVendaMap());
            modelBuilder.Configurations.Add(new CentroDeCustoMap());
            modelBuilder.Configurations.Add(new CentroDeCustoFilialMap());
            modelBuilder.Configurations.Add(new CfopMap());
            modelBuilder.Configurations.Add(new ChequeMap());
            modelBuilder.Configurations.Add(new CidadeMap());
            modelBuilder.Configurations.Add(new CidadeDistanciaMap());
            modelBuilder.Configurations.Add(new CidadeFaixaDeCepMap());
            modelBuilder.Configurations.Add(new CIDADE1Map());
            modelBuilder.Configurations.Add(new CidadeSedexMap());
            modelBuilder.Configurations.Add(new ClienteMap());
            modelBuilder.Configurations.Add(new ClienteDivisaoMap());
            modelBuilder.Configurations.Add(new ClienteEDIMap());
            modelBuilder.Configurations.Add(new ClienteFilialMap());
            modelBuilder.Configurations.Add(new ClienteJustificativaMap());
            modelBuilder.Configurations.Add(new ClienteMetaMap());
            modelBuilder.Configurations.Add(new ClienteRegraMap());
            modelBuilder.Configurations.Add(new ClientesComproveiMap());
            modelBuilder.Configurations.Add(new ClienteSetorFilialMap());
            modelBuilder.Configurations.Add(new ClienteTipoDeMaterialMap());
            modelBuilder.Configurations.Add(new ClienteTipoDeMaterialDivisaoMap());
            modelBuilder.Configurations.Add(new ColetorConferenciaMap());
            modelBuilder.Configurations.Add(new ColetorConferenciaCDEANMap());
            modelBuilder.Configurations.Add(new ColetorConferenciaItemMap());
            modelBuilder.Configurations.Add(new ColetorConferenciaLogMap());
            modelBuilder.Configurations.Add(new ColetorConferenciaVolumeMap());
            modelBuilder.Configurations.Add(new ConciliacaoMap());
            modelBuilder.Configurations.Add(new ConciliacaoLoteMap());
            modelBuilder.Configurations.Add(new ConciliacaoLoteDetalheMap());
            modelBuilder.Configurations.Add(new CondicaoDePagamentoMap());
            modelBuilder.Configurations.Add(new CondicaoDePagamentoItemMap());
            modelBuilder.Configurations.Add(new ConferenciaMap());
            modelBuilder.Configurations.Add(new ConferenciaItemNaoCadastradoMap());
            modelBuilder.Configurations.Add(new ConferenciaPalletMap());
            modelBuilder.Configurations.Add(new ConferenciaPalletDocMap());
            modelBuilder.Configurations.Add(new ConferenciaPalletDocVolMap());
            modelBuilder.Configurations.Add(new ConferenciaPalletDocVolItemMap());
            modelBuilder.Configurations.Add(new ConferenciaPalletEntradaMap());
            modelBuilder.Configurations.Add(new ConferenciaPalletEntradaLoteMap());
            modelBuilder.Configurations.Add(new ConferenciaPalletProdutoXXXXXMap());
            modelBuilder.Configurations.Add(new ContaContabilMap());
            modelBuilder.Configurations.Add(new ContaContabilCentroDeCustoMap());
            modelBuilder.Configurations.Add(new ContaContabilFilialMap());
            modelBuilder.Configurations.Add(new ContaContabilLancamentoMap());
            modelBuilder.Configurations.Add(new ContaDeEmailMap());
            modelBuilder.Configurations.Add(new ContaDespesaMap());
            modelBuilder.Configurations.Add(new ContaDespesaCapaMap());
            modelBuilder.Configurations.Add(new ContaDespesaEventoMap());
            modelBuilder.Configurations.Add(new ContaDespesaImagemMap());
            modelBuilder.Configurations.Add(new ContaDespesaItemMap());
            modelBuilder.Configurations.Add(new ContaDespesaObservacaoMap());
            modelBuilder.Configurations.Add(new ContratoMap());
            modelBuilder.Configurations.Add(new ContratoCapaMap());
            modelBuilder.Configurations.Add(new ContratoEventoMap());
            modelBuilder.Configurations.Add(new ContratoImagemMap());
            modelBuilder.Configurations.Add(new ContratoItemMap());
            modelBuilder.Configurations.Add(new ContratoItemCCustoMap());
            modelBuilder.Configurations.Add(new ContratoObservacaoMap());
            modelBuilder.Configurations.Add(new ControleBriefingMap());
            modelBuilder.Configurations.Add(new ControleBriefingItemMap());
            modelBuilder.Configurations.Add(new CotacaoDeCompraMap());
            modelBuilder.Configurations.Add(new CotacaoDeCompraItemMap());
            modelBuilder.Configurations.Add(new CotacaoFornecedorMap());
            modelBuilder.Configurations.Add(new CotacaoFornecedorCondPgtoMap());
            modelBuilder.Configurations.Add(new DDAMap());
            modelBuilder.Configurations.Add(new DDAImagemMap());
            modelBuilder.Configurations.Add(new DepartamentoMap());
            modelBuilder.Configurations.Add(new DepositoMap());
            modelBuilder.Configurations.Add(new DepositoPlantaMap());
            modelBuilder.Configurations.Add(new DepositoPlantaLeiauteMap());
            modelBuilder.Configurations.Add(new DepositoPlantaLocalizacaoMap());
            modelBuilder.Configurations.Add(new DepositoPlantaLocalizacaoRegraMap());
            modelBuilder.Configurations.Add(new DicaMap());
            modelBuilder.Configurations.Add(new DicionarioMap());
            modelBuilder.Configurations.Add(new DocumentoMap());
            modelBuilder.Configurations.Add(new DocumentoAFaturarMap());
            modelBuilder.Configurations.Add(new DocumentoAguardandoCTRCMap());
            modelBuilder.Configurations.Add(new DocumentoAprovacaoMap());
            modelBuilder.Configurations.Add(new DocumentoAteMap());
            modelBuilder.Configurations.Add(new DocumentoAteItemMap());
            modelBuilder.Configurations.Add(new DocumentoCDAMap());
            modelBuilder.Configurations.Add(new DocumentoCfopMap());
            modelBuilder.Configurations.Add(new DocumentoComprovanteMap());
            modelBuilder.Configurations.Add(new DocumentoCondicaoDePagamentoMap());
            modelBuilder.Configurations.Add(new DocumentoCotacaoMap());
            modelBuilder.Configurations.Add(new DocumentoCubagemMap());
            modelBuilder.Configurations.Add(new DocumentoEdiMap());
            modelBuilder.Configurations.Add(new DocumentoEletronicoMap());
            modelBuilder.Configurations.Add(new DocumentoEletronicoParametroMap());
            modelBuilder.Configurations.Add(new DocumentoEmbalagemMap());
            modelBuilder.Configurations.Add(new DocumentoFilialMap());
            modelBuilder.Configurations.Add(new DocumentoFilialPedidoMap());
            modelBuilder.Configurations.Add(new DocumentoFreteMap());
            modelBuilder.Configurations.Add(new DocumentoImagemMap());
            modelBuilder.Configurations.Add(new DocumentoImpostoMap());
            modelBuilder.Configurations.Add(new DocumentoItemMap());
            modelBuilder.Configurations.Add(new DocumentoItemComplementoMap());
            modelBuilder.Configurations.Add(new DocumentoItemEmpenhadoMap());
            modelBuilder.Configurations.Add(new DocumentoItemLabelPickingMap());
            modelBuilder.Configurations.Add(new DocumentoMigradoMap());
            modelBuilder.Configurations.Add(new DocumentoModeloMap());
            modelBuilder.Configurations.Add(new DocumentoNotaFiscalMap());
            modelBuilder.Configurations.Add(new DocumentoObjetoMap());
            modelBuilder.Configurations.Add(new DocumentoObjetoOcorrenciaMap());
            modelBuilder.Configurations.Add(new DocumentoObservacaoMap());
            modelBuilder.Configurations.Add(new DocumentoOcorrenciaMap());
            modelBuilder.Configurations.Add(new DocumentoOcorrenciaArquivoMap());
            modelBuilder.Configurations.Add(new DocumentoOcorrenciaItemMap());
            modelBuilder.Configurations.Add(new DocumentoOrcamentoMap());
            modelBuilder.Configurations.Add(new DOCUMENTOPEDIDOMap());
            modelBuilder.Configurations.Add(new DOCUMENTOPEDIDOITEMMap());
            modelBuilder.Configurations.Add(new DocumentoProtocoloMap());
            modelBuilder.Configurations.Add(new DocumentoRecebimentoMap());
            modelBuilder.Configurations.Add(new DocumentoReclamacaoMap());
            modelBuilder.Configurations.Add(new DocumentoRelacionadoMap());
            modelBuilder.Configurations.Add(new DocumentoRemessaMap());
            modelBuilder.Configurations.Add(new DocumentoReprogramadoMap());
            modelBuilder.Configurations.Add(new DocumentoSituacaoMap());
            modelBuilder.Configurations.Add(new DocumentoTipoMap());
            modelBuilder.Configurations.Add(new DTMap());
            modelBuilder.Configurations.Add(new DtChequeMap());
            modelBuilder.Configurations.Add(new DTContaCorrenteMap());
            modelBuilder.Configurations.Add(new DTEletronicoMap());
            modelBuilder.Configurations.Add(new DTFaturamentoMap());
            modelBuilder.Configurations.Add(new DTFaturamentoClienteMap());
            modelBuilder.Configurations.Add(new DTFaturamentoClienteDocumentoMap());
            modelBuilder.Configurations.Add(new DTHistoricoMap());
            modelBuilder.Configurations.Add(new DTLancamentoMap());
            modelBuilder.Configurations.Add(new dtpropertyMap());
            modelBuilder.Configurations.Add(new DTRomaneioMap());
            modelBuilder.Configurations.Add(new DTTipoMap());
            modelBuilder.Configurations.Add(new EDIMap());
            modelBuilder.Configurations.Add(new EDI_BairroMap());
            modelBuilder.Configurations.Add(new EDI_CadastroMap());
            modelBuilder.Configurations.Add(new EDI_CadastroContatoEnderecoMap());
            modelBuilder.Configurations.Add(new EDI_CidadeMap());
            modelBuilder.Configurations.Add(new EDI_DocumentoMap());
            modelBuilder.Configurations.Add(new EDI_DocumentoAguardandoCtrcMap());
            modelBuilder.Configurations.Add(new EDI_DocumentoEdiMap());
            modelBuilder.Configurations.Add(new EDI_DocumentoFilialMap());
            modelBuilder.Configurations.Add(new EDI_DocumentoFreteMap());
            modelBuilder.Configurations.Add(new EDI_DocumentoItemMap());
            modelBuilder.Configurations.Add(new EDI_DocumentoNFEMap());
            modelBuilder.Configurations.Add(new EDI_DocumentoNotaFiscalMap());
            modelBuilder.Configurations.Add(new EDI_DocumentoRelacionadoMap());
            modelBuilder.Configurations.Add(new EDI_EdiMap());
            modelBuilder.Configurations.Add(new EDI_EstadoMap());
            modelBuilder.Configurations.Add(new Edi_GeradoMap());
            modelBuilder.Configurations.Add(new edi_gerado_antigoMap());
            modelBuilder.Configurations.Add(new EDI_ProdutoMap());
            modelBuilder.Configurations.Add(new EDI_ProdutoClienteMap());
            modelBuilder.Configurations.Add(new EDI_ProdutoEmbalagemMap());
            modelBuilder.Configurations.Add(new EdiArquivoMap());
            modelBuilder.Configurations.Add(new EdiControleDeArquivoMap());
            modelBuilder.Configurations.Add(new EdiControleDeArquivoLogMap());
            modelBuilder.Configurations.Add(new EDILayOutMap());
            modelBuilder.Configurations.Add(new EDILayOutPerguntaMap());
            modelBuilder.Configurations.Add(new EDILayOutRegMap());
            modelBuilder.Configurations.Add(new EDILayOutRegCpoMap());
            modelBuilder.Configurations.Add(new EDILayoutRegTabelaMap());
            modelBuilder.Configurations.Add(new EDILayoutTabelaMap());
            modelBuilder.Configurations.Add(new EDIPerguntaMap());
            modelBuilder.Configurations.Add(new EdiPlanilhaMap());
            modelBuilder.Configurations.Add(new EdiPlanilhaDetalheMap());
            modelBuilder.Configurations.Add(new EDITransferenciaMap());
            modelBuilder.Configurations.Add(new EdiTrocaDeArquivoMap());
            modelBuilder.Configurations.Add(new EmailPendenteMap());
            modelBuilder.Configurations.Add(new EmpresaMap());
            modelBuilder.Configurations.Add(new EnquadramentoIPIMap());
            modelBuilder.Configurations.Add(new EstadoMap());
            modelBuilder.Configurations.Add(new EstadoFaixaDeCepMap());
            modelBuilder.Configurations.Add(new ESTADO1Map());
            modelBuilder.Configurations.Add(new EstoqueMap());
            modelBuilder.Configurations.Add(new EstoqueComprasMovMap());
            modelBuilder.Configurations.Add(new EstoqueDivisaoMap());
            modelBuilder.Configurations.Add(new EstoqueDivisaoMovMap());
            modelBuilder.Configurations.Add(new EstoqueMovMap());
            modelBuilder.Configurations.Add(new EstoqueOperacaoMap());
            modelBuilder.Configurations.Add(new ExtratoParaConciliacaoMap());
            modelBuilder.Configurations.Add(new Face_REMap());
            modelBuilder.Configurations.Add(new FeriadoMap());
            modelBuilder.Configurations.Add(new FilialMap());
            modelBuilder.Configurations.Add(new FilialCidadeSetorMap());
            modelBuilder.Configurations.Add(new FilialPortariaMap());
            modelBuilder.Configurations.Add(new FinalidadeDocTedMap());
            modelBuilder.Configurations.Add(new FolderMap());
            modelBuilder.Configurations.Add(new FornecedorMap());
            modelBuilder.Configurations.Add(new FornecedorFilialMap());
            modelBuilder.Configurations.Add(new FrotaMap());
            modelBuilder.Configurations.Add(new FrotaGrupoItemControleMap());
            modelBuilder.Configurations.Add(new FrotaGrupoItemControleItemMap());
            modelBuilder.Configurations.Add(new FrotaItemControleMap());
            modelBuilder.Configurations.Add(new FuncionarioMap());
            modelBuilder.Configurations.Add(new GaiolaMap());
            modelBuilder.Configurations.Add(new GaiolaConferenciaMap());
            modelBuilder.Configurations.Add(new GrandeUsuarioMap());
            modelBuilder.Configurations.Add(new GrandeUsuarioEnderecoMap());
            modelBuilder.Configurations.Add(new GrupoMap());
            modelBuilder.Configurations.Add(new GrupoDeProdutoMap());
            modelBuilder.Configurations.Add(new HistoricoPadraoMap());
            modelBuilder.Configurations.Add(new IcmMap());
            modelBuilder.Configurations.Add(new IdTabelaMap());
            modelBuilder.Configurations.Add(new InssMap());
            modelBuilder.Configurations.Add(new InventarioMap());
            modelBuilder.Configurations.Add(new InventarioContagemMap());
            modelBuilder.Configurations.Add(new InventarioContagemProdutoMap());
            modelBuilder.Configurations.Add(new InventarioUaMap());
            modelBuilder.Configurations.Add(new IrrfMap());
            modelBuilder.Configurations.Add(new KPIIRWIN_V2Map());
            modelBuilder.Configurations.Add(new KPIIRWINProcessamentoMap());
            modelBuilder.Configurations.Add(new LabelPickingMap());
            modelBuilder.Configurations.Add(new LancamentoMap());
            modelBuilder.Configurations.Add(new LancamentoContabilMap());
            modelBuilder.Configurations.Add(new LancamentoContabilCCMap());
            modelBuilder.Configurations.Add(new LancamentoOcorrenciaMap());
            modelBuilder.Configurations.Add(new LancamentoPadraoMap());
            modelBuilder.Configurations.Add(new LancamentoPadraoConfiguracaoMap());
            modelBuilder.Configurations.Add(new LeiauteBoletoMap());
            modelBuilder.Configurations.Add(new LeiauteChequeMap());
            modelBuilder.Configurations.Add(new LicenciamentoMap());
            modelBuilder.Configurations.Add(new LicenciamentoMeMap());
            modelBuilder.Configurations.Add(new LOG_BAIRROMap());
            modelBuilder.Configurations.Add(new LOG_CPCMap());
            modelBuilder.Configurations.Add(new LOG_FAIXA_BAIRROMap());
            modelBuilder.Configurations.Add(new LOG_FAIXA_CPCMap());
            modelBuilder.Configurations.Add(new LOG_FAIXA_LOCALIDADEMap());
            modelBuilder.Configurations.Add(new LOG_FAIXA_UFMap());
            modelBuilder.Configurations.Add(new LOG_FAIXA_UOPMap());
            modelBuilder.Configurations.Add(new LOG_GRANDE_USUARIOMap());
            modelBuilder.Configurations.Add(new LOG_LOCALIDADEMap());
            modelBuilder.Configurations.Add(new LOG_LOGRADOUROMap());
            modelBuilder.Configurations.Add(new LOG_NUM_SECMap());
            modelBuilder.Configurations.Add(new LOG_PAISMap());
            modelBuilder.Configurations.Add(new LOG_UNID_OPERMap());
            modelBuilder.Configurations.Add(new LOG_VAR_BAIMap());
            modelBuilder.Configurations.Add(new LOG_VAR_LOCMap());
            modelBuilder.Configurations.Add(new LOG_VAR_LOGMap());
            modelBuilder.Configurations.Add(new LogComproveiMap());
            modelBuilder.Configurations.Add(new LoginPerfilMap());
            modelBuilder.Configurations.Add(new LoginPerfilPermissaoMap());
            modelBuilder.Configurations.Add(new LogMetodoMap());
            modelBuilder.Configurations.Add(new LOGRADOUROMap());
            modelBuilder.Configurations.Add(new LogRoboAcertoNotaMap());
            modelBuilder.Configurations.Add(new LoteMap());
            modelBuilder.Configurations.Add(new LoteEletronicoMap());
            modelBuilder.Configurations.Add(new MapaMap());
            modelBuilder.Configurations.Add(new MenuClienteMap());
            modelBuilder.Configurations.Add(new MenuSiteMap());
            modelBuilder.Configurations.Add(new mMotoristaProprietarioMap());
            modelBuilder.Configurations.Add(new ModalMap());
            modelBuilder.Configurations.Add(new ModalidadeMap());
            modelBuilder.Configurations.Add(new ModalidadeDePagamentoMap());
            modelBuilder.Configurations.Add(new ModalidadeDocTedMap());
            modelBuilder.Configurations.Add(new ModalidadeFinalidadeDocTedMap());
            modelBuilder.Configurations.Add(new ModuloMap());
            modelBuilder.Configurations.Add(new ModuloMenuMap());
            modelBuilder.Configurations.Add(new ModuloMenuBkpMap());
            modelBuilder.Configurations.Add(new ModuloOpcaoMap());
            modelBuilder.Configurations.Add(new ModuloOpcaoAcaoMap());
            modelBuilder.Configurations.Add(new ModuloOpcaoTabelaMap());
            modelBuilder.Configurations.Add(new MoedaMap());
            modelBuilder.Configurations.Add(new MoedaCotacaoMap());
            modelBuilder.Configurations.Add(new MotivoMap());
            modelBuilder.Configurations.Add(new MotivoRejeicaoMap());
            modelBuilder.Configurations.Add(new MotoristaMap());
            modelBuilder.Configurations.Add(new MotoristaFilialMap());
            modelBuilder.Configurations.Add(new MotoristaHistoricoMap());
            modelBuilder.Configurations.Add(new MovimentacaoMap());
            modelBuilder.Configurations.Add(new MovimentacaoBancariaMap());
            modelBuilder.Configurations.Add(new MovimentacaoClienteMap());
            modelBuilder.Configurations.Add(new movimentacaocliente_bkpMap());
            modelBuilder.Configurations.Add(new MovimentacaoClienteConsolidadoMap());
            modelBuilder.Configurations.Add(new MovimentacaoClienteDivisaoMap());
            modelBuilder.Configurations.Add(new MovimentacaoItemMap());
            modelBuilder.Configurations.Add(new MovimentacaoRomaneioMap());
            modelBuilder.Configurations.Add(new MovimentacaoRomaneioItemMap());
            modelBuilder.Configurations.Add(new NaturezaMap());
            modelBuilder.Configurations.Add(new NumeradorMap());
            modelBuilder.Configurations.Add(new OcorrenciaMap());
            modelBuilder.Configurations.Add(new OcorrenciaAcaoMap());
            modelBuilder.Configurations.Add(new OcorrenciaAndamentoMap());
            modelBuilder.Configurations.Add(new OcorrenciaCodigoMap());
            modelBuilder.Configurations.Add(new OcorrenciaDeParaMap());
            modelBuilder.Configurations.Add(new OcorrenciaSerieMap());
            modelBuilder.Configurations.Add(new OperacaoMap());
            modelBuilder.Configurations.Add(new OrcamentoMap());
            modelBuilder.Configurations.Add(new OrcamentoPedidoMap());
            modelBuilder.Configurations.Add(new OrcamentoPedidoItemMap());
            modelBuilder.Configurations.Add(new PaiMap());
            modelBuilder.Configurations.Add(new PalletMap());
            modelBuilder.Configurations.Add(new PalletDocumentoMap());
            modelBuilder.Configurations.Add(new PalletDocumentoItemMap());
            modelBuilder.Configurations.Add(new PalletDocumentoItemCarregamentoMap());
            modelBuilder.Configurations.Add(new ParametroFluxoDeCaixaMap());
            modelBuilder.Configurations.Add(new PercursoMdfeMap());
            modelBuilder.Configurations.Add(new PlanoDeContas_AntMap());
            modelBuilder.Configurations.Add(new PlantaMap());
            modelBuilder.Configurations.Add(new PortariaMap());
            modelBuilder.Configurations.Add(new PortariaVisitanteMap());
            modelBuilder.Configurations.Add(new PreFaturaMap());
            modelBuilder.Configurations.Add(new PreFaturaDocumentoMap());
            modelBuilder.Configurations.Add(new PreFaturaRogeMap());
            modelBuilder.Configurations.Add(new PrevisaoDeMaterialMap());
            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new ProdutoClienteMap());
            modelBuilder.Configurations.Add(new ProdutoClienteAvariaMap());
            modelBuilder.Configurations.Add(new ProdutoClienteRegraMap());
            modelBuilder.Configurations.Add(new ProdutoEmbalagemMap());
            modelBuilder.Configurations.Add(new ProdutoEstruturaMap());
            modelBuilder.Configurations.Add(new ProdutoFotoMap());
            modelBuilder.Configurations.Add(new ProjetoMap());
            modelBuilder.Configurations.Add(new ProjetoArquivoMap());
            modelBuilder.Configurations.Add(new ProjetoFilialMap());
            modelBuilder.Configurations.Add(new ProjetoItemMap());
            modelBuilder.Configurations.Add(new ProjetoProducaoMap());
            modelBuilder.Configurations.Add(new ProprietarioMap());
            modelBuilder.Configurations.Add(new RamoDeAtividadeMap());
            modelBuilder.Configurations.Add(new RastreadorMap());
            modelBuilder.Configurations.Add(new RastreamentoMap());
            modelBuilder.Configurations.Add(new ReclamacaoMap());
            modelBuilder.Configurations.Add(new RedespachoMap());
            modelBuilder.Configurations.Add(new RegiaoMap());
            modelBuilder.Configurations.Add(new RegiaoItemMap());
            modelBuilder.Configurations.Add(new ReposicaoRogeMap());
            modelBuilder.Configurations.Add(new ReposicaoRogeCBMap());
            modelBuilder.Configurations.Add(new ReposicaoRogeConferenciaCegaMap());
            modelBuilder.Configurations.Add(new ReposicaoRogeEanMap());
            modelBuilder.Configurations.Add(new ReposicaoRogeItemMap());
            modelBuilder.Configurations.Add(new ReposicaoRogeVolumeMap());
            modelBuilder.Configurations.Add(new RepresentanteMap());
            modelBuilder.Configurations.Add(new RepresentanteClienteMap());
            modelBuilder.Configurations.Add(new RequisicaoDeMaterialMap());
            modelBuilder.Configurations.Add(new RequisicaoDeMaterialDocumentoMap());
            modelBuilder.Configurations.Add(new RequisicaoDeMaterialItemMap());
            modelBuilder.Configurations.Add(new RespostaMap());
            modelBuilder.Configurations.Add(new RetornoComproveiMap());
            modelBuilder.Configurations.Add(new RomaneioMap());
            modelBuilder.Configurations.Add(new RomaneioConferenciaMap());
            modelBuilder.Configurations.Add(new RomaneioConferenciaItemMap());
            modelBuilder.Configurations.Add(new RomaneioDocumentoMap());
            modelBuilder.Configurations.Add(new RomaneioDocumentoConferenciaMap());
            modelBuilder.Configurations.Add(new RomaneioDocumentoFreteMap());
            modelBuilder.Configurations.Add(new RomaneioDocumentoItemMap());
            modelBuilder.Configurations.Add(new RomaneioOcorrenciaMap());
            modelBuilder.Configurations.Add(new RomaneioPrevisaoMap());
            modelBuilder.Configurations.Add(new RomaneioPrevisaoRegiaoMap());
            modelBuilder.Configurations.Add(new RoteirizacaoMap());
            modelBuilder.Configurations.Add(new RoteirizacaoTipoMap());
            modelBuilder.Configurations.Add(new RPCIMap());
            modelBuilder.Configurations.Add(new RPCIDocumentoMap());
            modelBuilder.Configurations.Add(new RPCIImagemMap());
            modelBuilder.Configurations.Add(new RuaMap());
            modelBuilder.Configurations.Add(new ServicoMap());
            modelBuilder.Configurations.Add(new SetorMap());
            modelBuilder.Configurations.Add(new SetorCepMap());
            modelBuilder.Configurations.Add(new SituacaoTributariaCofinMap());
            modelBuilder.Configurations.Add(new SituacaoTributariaIcmMap());
            modelBuilder.Configurations.Add(new SituacaoTributariaIPIMap());
            modelBuilder.Configurations.Add(new SituacaoTributariaPIMap());
            modelBuilder.Configurations.Add(new SobraMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new syspropertyMap());
            modelBuilder.Configurations.Add(new TabelaDeFreteMap());
            modelBuilder.Configurations.Add(new TabelaDeFreteProdutoMap());
            modelBuilder.Configurations.Add(new TabelaDeFreteRotaMap());
            modelBuilder.Configurations.Add(new TabelaDeFreteRotaModalMap());
            modelBuilder.Configurations.Add(new TabelaDeFreteRotaModalValorMap());
            modelBuilder.Configurations.Add(new TabelaDeFreteVigenciaMap());
            modelBuilder.Configurations.Add(new TB_AUX_REMap());
            modelBuilder.Configurations.Add(new tblBairroMap());
            modelBuilder.Configurations.Add(new tblCidadeMap());
            modelBuilder.Configurations.Add(new tblLogradouroMap());
            modelBuilder.Configurations.Add(new tblUFMap());
            modelBuilder.Configurations.Add(new TEMap());
            modelBuilder.Configurations.Add(new TESCFOPMap());
            modelBuilder.Configurations.Add(new TESControleMap());
            modelBuilder.Configurations.Add(new TipoDeEscoltaMap());
            modelBuilder.Configurations.Add(new TipoDeItemMap());
            modelBuilder.Configurations.Add(new TipoDeMonitoramentoMap());
            modelBuilder.Configurations.Add(new TipoDeMovimentoMap());
            modelBuilder.Configurations.Add(new TipoDeOperacaoMap());
            modelBuilder.Configurations.Add(new TipoDeTituloMap());
            modelBuilder.Configurations.Add(new TipoDeTituloDuplicataMap());
            modelBuilder.Configurations.Add(new TipoDeVolumeMap());
            modelBuilder.Configurations.Add(new TituloMap());
            modelBuilder.Configurations.Add(new TituloDocumentoMap());
            modelBuilder.Configurations.Add(new TituloDuplicataMap());
            modelBuilder.Configurations.Add(new TituloDuplicataHistoricoMap());
            modelBuilder.Configurations.Add(new TituloDuplicataRemessaMap());
            modelBuilder.Configurations.Add(new TituloHistoricoMap());
            modelBuilder.Configurations.Add(new TituloImagemMap());
            modelBuilder.Configurations.Add(new TituloItemMap());
            modelBuilder.Configurations.Add(new TransportadoraMap());
            modelBuilder.Configurations.Add(new UnidadeDeArmazenagemMap());
            modelBuilder.Configurations.Add(new UnidadeDeArmazenagemAgrupMap());
            modelBuilder.Configurations.Add(new UnidadeDeArmazenagemLoteMap());
            modelBuilder.Configurations.Add(new UnidadeDeArmazenagemMovMap());
            modelBuilder.Configurations.Add(new UnidadeDeMedidaMap());
            modelBuilder.Configurations.Add(new UnidadeDeNegocioMap());
            modelBuilder.Configurations.Add(new UnidadeFuncionalMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new UsuarioAlertaMap());
            modelBuilder.Configurations.Add(new UsuarioCentroDeCustoMap());
            modelBuilder.Configurations.Add(new UsuarioCentroDeCustoOperacaoMap());
            modelBuilder.Configurations.Add(new UsuarioCentroDeCustoOperacaoLogMap());
            modelBuilder.Configurations.Add(new UsuarioClienteMap());
            modelBuilder.Configurations.Add(new UsuarioClienteDivisaoMap());
            modelBuilder.Configurations.Add(new UsuarioCompraMap());
            modelBuilder.Configurations.Add(new UsuarioCotaMap());
            modelBuilder.Configurations.Add(new UsuarioDeTabelaDeFreteMap());
            modelBuilder.Configurations.Add(new UsuarioFavoritoMap());
            modelBuilder.Configurations.Add(new UsuarioFilialMap());
            modelBuilder.Configurations.Add(new UsuarioGradeMap());
            modelBuilder.Configurations.Add(new UsuarioGradeCampoMap());
            modelBuilder.Configurations.Add(new UsuarioModalMap());
            modelBuilder.Configurations.Add(new UsuarioOpcaoMap());
            modelBuilder.Configurations.Add(new UsuarioOpcaoAcessoMap());
            modelBuilder.Configurations.Add(new UsuarioOperacaoMap());
            modelBuilder.Configurations.Add(new UsuarioOperacaoLogMap());
            modelBuilder.Configurations.Add(new UsuarioPerfilMap());
            modelBuilder.Configurations.Add(new VeiculoMap());
            modelBuilder.Configurations.Add(new VeiculoFilialMap());
            modelBuilder.Configurations.Add(new VeiculoFotoMap());
            modelBuilder.Configurations.Add(new VeiculoLicenciamentoMap());
            modelBuilder.Configurations.Add(new VeiculoMarcaMap());
            modelBuilder.Configurations.Add(new VeiculoModeloMap());
            modelBuilder.Configurations.Add(new VeiculoRastreadorMap());
            modelBuilder.Configurations.Add(new VeiculoTabelaMap());
            modelBuilder.Configurations.Add(new VeiculoTabelaAgregadoMap());
            modelBuilder.Configurations.Add(new VeiculoTabelaRegiaoMap());
            modelBuilder.Configurations.Add(new VeiculoTabelaRegiaoItemMap());
            modelBuilder.Configurations.Add(new VeiculoTipoMap());
            modelBuilder.Configurations.Add(new XmlComproveiMap());
            modelBuilder.Configurations.Add(new ZID_AgendaRecebimentoMap());
            modelBuilder.Configurations.Add(new ZID_AgrupamentoMap());
            modelBuilder.Configurations.Add(new ZID_AgrupamentoRegiaoMap());
            modelBuilder.Configurations.Add(new ZID_AVISOMap());
            modelBuilder.Configurations.Add(new ZID_BAIRROMap());
            modelBuilder.Configurations.Add(new ZID_BANCOCONTAMap());
            modelBuilder.Configurations.Add(new ZID_BANCOCONTABLOQUETOMap());
            modelBuilder.Configurations.Add(new ZID_borderoMap());
            modelBuilder.Configurations.Add(new ZID_BorderoTituloDuplicataMap());
            modelBuilder.Configurations.Add(new ZID_CADASTROMap());
            modelBuilder.Configurations.Add(new ZID_CadastroCdaUsuarioClienteDivisaoMap());
            modelBuilder.Configurations.Add(new ZID_CADASTROCOMPLEMENTOMap());
            modelBuilder.Configurations.Add(new ZID_CadastroCondicaoEntregaMap());
            modelBuilder.Configurations.Add(new ZID_cadastrocontatoMap());
            modelBuilder.Configurations.Add(new ZID_CadastroContatoEnderecoMap());
            modelBuilder.Configurations.Add(new ZID_CadastroEnderecoMap());
            modelBuilder.Configurations.Add(new ZID_CadastroEntregaMap());
            modelBuilder.Configurations.Add(new ZID_CadastroHistoricoMap());
            modelBuilder.Configurations.Add(new ZID_CadastroImagemMap());
            modelBuilder.Configurations.Add(new ZID_CadastroReferenciaMap());
            modelBuilder.Configurations.Add(new ZID_CentrodeCustoFilialMap());
            modelBuilder.Configurations.Add(new ZID_ChequeMap());
            modelBuilder.Configurations.Add(new ZID_CIDADEMap());
            modelBuilder.Configurations.Add(new ZID_clientedivisaoMap());
            modelBuilder.Configurations.Add(new ZID_ClienteEdiMap());
            modelBuilder.Configurations.Add(new ZID_ClienteFilialMap());
            modelBuilder.Configurations.Add(new ZID_ClienteSetorFilialMap());
            modelBuilder.Configurations.Add(new ZID_ClienteTipoDeMaterialMap());
            modelBuilder.Configurations.Add(new ZID_ClienteTipoDeMaterialDivisaoMap());
            modelBuilder.Configurations.Add(new ZID_COLETORCONFERENCIAMap());
            modelBuilder.Configurations.Add(new ZID_COLETORCONFERENCIAITEMMap());
            modelBuilder.Configurations.Add(new ZID_COLETORCONFERENCIALOGMap());
            modelBuilder.Configurations.Add(new ZID_COLETORCONFERENCIAVOLUMEMap());
            modelBuilder.Configurations.Add(new ZID_CONFERENCIAMap());
            modelBuilder.Configurations.Add(new ZID_CONFERENCIAPALLETMap());
            modelBuilder.Configurations.Add(new ZID_CONFERENCIAPALLETDOCMap());
            modelBuilder.Configurations.Add(new ZID_CONFERENCIAPALLETDOCVOLMap());
            modelBuilder.Configurations.Add(new ZID_CONFERENCIAPALLETDOCVOLITEMMap());
            modelBuilder.Configurations.Add(new ZID_CONFERENCIAPALLETENTRADAMap());
            modelBuilder.Configurations.Add(new ZID_CONFERENCIAPALLETENTRADALOTEMap());
            modelBuilder.Configurations.Add(new ZID_contacontabilMap());
            modelBuilder.Configurations.Add(new ZID_ContaContabilFilialMap());
            modelBuilder.Configurations.Add(new ZID_CONTRATOMap());
            modelBuilder.Configurations.Add(new ZID_ContratoImagemMap());
            modelBuilder.Configurations.Add(new ZID_ContratoItemMap());
            modelBuilder.Configurations.Add(new ZID_ContratoItemCCustoMap());
            modelBuilder.Configurations.Add(new ZID_ContratoObservacaoMap());
            modelBuilder.Configurations.Add(new ZID_CotacaoDeCompraMap());
            modelBuilder.Configurations.Add(new ZID_CotacaoDeCompraItemMap());
            modelBuilder.Configurations.Add(new ZID_CotacaoFornecedorMap());
            modelBuilder.Configurations.Add(new ZID_DepositoMap());
            modelBuilder.Configurations.Add(new ZID_DepositoPlantaMap());
            modelBuilder.Configurations.Add(new ZID_DepositoPlantaLeiauteMap());
            modelBuilder.Configurations.Add(new ZID_DepositoPlantaLocalizacaoMap());
            modelBuilder.Configurations.Add(new ZID_DepositoPlantaLocalizacaoRegraMap());
            modelBuilder.Configurations.Add(new ZID_DOCUMENTOMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoAFaturarMap());
            modelBuilder.Configurations.Add(new ZID_DOCUMENTOAGUARDANDOCTRCMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoAteMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoAteItemMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoCdaMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoCFOPMap());
            modelBuilder.Configurations.Add(new ZID_DOCUMENTOCOMPROVANTEMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoCondicaoDePagamentoMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoCotacaoMap());
            modelBuilder.Configurations.Add(new ZID_DOCUMENTOCUBAGEMMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoEdiMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoEletronicoMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoEmbalagemMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoFilialMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoFreteMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoImpostoMap());
            modelBuilder.Configurations.Add(new ZID_DOCUMENTOITEMMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoItemComplementoMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoObjetoOcorrenciaMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoObservacaoMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoOcorrenciaMap());
            modelBuilder.Configurations.Add(new ZID_DOCUMENTOOCORRENCIAARQUIVOMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoOcorrenciaItemMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoOrcamentoMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoRecebimentoMap());
            modelBuilder.Configurations.Add(new ZID_DocumentoRelacionadoMap());
            modelBuilder.Configurations.Add(new ZID_DOCUMENTOREMESSAMap());
            modelBuilder.Configurations.Add(new ZID_DTMap());
            modelBuilder.Configurations.Add(new ZID_DtContaCorrenteMap());
            modelBuilder.Configurations.Add(new ZID_DTEletronicoMap());
            modelBuilder.Configurations.Add(new ZID_DTFaturamentoMap());
            modelBuilder.Configurations.Add(new ZID_DtFaturamentoClienteMap());
            modelBuilder.Configurations.Add(new ZID_DtFaturamentoClienteDocumentoMap());
            modelBuilder.Configurations.Add(new ZID_DTHistoricoMap());
            modelBuilder.Configurations.Add(new ZID_DTROMANEIOMap());
            modelBuilder.Configurations.Add(new ZID_EDIMap());
            modelBuilder.Configurations.Add(new ZID_EDI_BairroMap());
            modelBuilder.Configurations.Add(new ZID_EDI_CadastroMap());
            modelBuilder.Configurations.Add(new ZID_EDI_CadastroContatoEnderecoMap());
            modelBuilder.Configurations.Add(new ZID_EDI_CidadeMap());
            modelBuilder.Configurations.Add(new ZID_EDI_DocumentoMap());
            modelBuilder.Configurations.Add(new ZID_EDI_DocumentoAguardandoCtrcMap());
            modelBuilder.Configurations.Add(new ZID_EDI_DocumentoEdiMap());
            modelBuilder.Configurations.Add(new ZID_EDI_DocumentoFilialMap());
            modelBuilder.Configurations.Add(new ZID_EDI_DocumentoFreteMap());
            modelBuilder.Configurations.Add(new ZID_EDI_DocumentoItemMap());
            modelBuilder.Configurations.Add(new ZID_EDI_DocumentoNFEMap());
            modelBuilder.Configurations.Add(new ZID_EDI_DocumentoNotaFiscalMap());
            modelBuilder.Configurations.Add(new ZID_EDI_DocumentoRelacionadoMap());
            modelBuilder.Configurations.Add(new ZID_EDI_EdiMap());
            modelBuilder.Configurations.Add(new ZID_EDI_EstadoMap());
            modelBuilder.Configurations.Add(new ZID_EDI_ProdutoMap());
            modelBuilder.Configurations.Add(new ZID_EDI_ProdutoClienteMap());
            modelBuilder.Configurations.Add(new ZID_EDI_ProdutoEmbalagemMap());
            modelBuilder.Configurations.Add(new ZID_EdiControleDeArquivoMap());
            modelBuilder.Configurations.Add(new ZID_EdiControleDeArquivoLogMap());
            modelBuilder.Configurations.Add(new ZID_EdiPlanilhaMap());
            modelBuilder.Configurations.Add(new ZID_EdiPlanilhaDetalheMap());
            modelBuilder.Configurations.Add(new ZID_EdiTrocaDeArquivoMap());
            modelBuilder.Configurations.Add(new ZID_ESTADOMap());
            modelBuilder.Configurations.Add(new ZID_ESTOQUEMap());
            modelBuilder.Configurations.Add(new ZID_EstoqueComprasMovMap());
            modelBuilder.Configurations.Add(new ZID_ESTOQUEDIVISAOMap());
            modelBuilder.Configurations.Add(new ZID_ESTOQUEDIVISAOMOVMap());
            modelBuilder.Configurations.Add(new ZID_ESTOQUEMOVMap());
            modelBuilder.Configurations.Add(new ZID_ExtratoParaConciliacaoMap());
            modelBuilder.Configurations.Add(new ZID_feriadoMap());
            modelBuilder.Configurations.Add(new ZID_FilialCidadeSetorMap());
            modelBuilder.Configurations.Add(new ZID_FilialPortariaMap());
            modelBuilder.Configurations.Add(new ZID_FornecedorfilialMap());
            modelBuilder.Configurations.Add(new ZID_GAIOLAMap());
            modelBuilder.Configurations.Add(new ZID_GAIOLACONFERENCIAMap());
            modelBuilder.Configurations.Add(new ZID_grupodeprodutoMap());
            modelBuilder.Configurations.Add(new ZID_icmsMap());
            modelBuilder.Configurations.Add(new ZID_InventarioMap());
            modelBuilder.Configurations.Add(new ZID_InventarioContagemMap());
            modelBuilder.Configurations.Add(new ZID_inventariocontagemprodutoMap());
            modelBuilder.Configurations.Add(new ZID_LancamentoMap());
            modelBuilder.Configurations.Add(new ZID_LANCAMENTOCONTABILMap());
            modelBuilder.Configurations.Add(new ZID_LancamentoContabilCCMap());
            modelBuilder.Configurations.Add(new ZID_LoteMap());
            modelBuilder.Configurations.Add(new ZID_LoteEletronicoMap());
            modelBuilder.Configurations.Add(new ZID_MAPAMap());
            modelBuilder.Configurations.Add(new ZID_ModalMap());
            modelBuilder.Configurations.Add(new ZID_modulomenuMap());
            modelBuilder.Configurations.Add(new ZID_ModuloOpcaoMap());
            modelBuilder.Configurations.Add(new ZID_MotivoMap());
            modelBuilder.Configurations.Add(new ZID_MOTORISTAFILIALMap());
            modelBuilder.Configurations.Add(new ZID_MotoristaHistoricoMap());
            modelBuilder.Configurations.Add(new ZID_MOVIMENTACAOMap());
            modelBuilder.Configurations.Add(new ZID_movimentacaobancariaMap());
            modelBuilder.Configurations.Add(new ZID_MOVIMENTACAOITEMMap());
            modelBuilder.Configurations.Add(new ZID_NumeradorMap());
            modelBuilder.Configurations.Add(new ZID_ocorrenciaMap());
            modelBuilder.Configurations.Add(new ZID_ocorrenciaserieMap());
            modelBuilder.Configurations.Add(new ZID_OperacaoMap());
            modelBuilder.Configurations.Add(new ZID_PAISMap());
            modelBuilder.Configurations.Add(new ZID_PalletDocumentoMap());
            modelBuilder.Configurations.Add(new ZID_PalletDocumentoItemMap());
            modelBuilder.Configurations.Add(new ZID_ParametroFluxoDeCaixaMap());
            modelBuilder.Configurations.Add(new ZID_PortariaMap());
            modelBuilder.Configurations.Add(new ZID_PortariaVisitanteMap());
            modelBuilder.Configurations.Add(new ZID_PreFaturaMap());
            modelBuilder.Configurations.Add(new ZID_PreFaturaDocumentoMap());
            modelBuilder.Configurations.Add(new ZID_PRODUTOMap());
            modelBuilder.Configurations.Add(new ZID_PRODUTOCLIENTEMap());
            modelBuilder.Configurations.Add(new ZID_PRODUTOEMBALAGEMMap());
            modelBuilder.Configurations.Add(new ZID_ProdutoEstruturaMap());
            modelBuilder.Configurations.Add(new ZID_produtofotoMap());
            modelBuilder.Configurations.Add(new ZID_RASTREAMENTOMap());
            modelBuilder.Configurations.Add(new ZID_RegiaoMap());
            modelBuilder.Configurations.Add(new ZID_regiaoItemMap());
            modelBuilder.Configurations.Add(new ZID_ReposicaoRogeMap());
            modelBuilder.Configurations.Add(new ZID_RequisicaoDeMaterialMap());
            modelBuilder.Configurations.Add(new ZID_RequisicaodeMaterialDocumentoMap());
            modelBuilder.Configurations.Add(new ZID_RequisicaoDeMaterialItemMap());
            modelBuilder.Configurations.Add(new ZID_RomaneioMap());
            modelBuilder.Configurations.Add(new ZID_RomaneioDocumentoMap());
            modelBuilder.Configurations.Add(new ZID_RomaneioDocumentoConferenciaMap());
            modelBuilder.Configurations.Add(new ZID_RomaneioDocumentoItemMap());
            modelBuilder.Configurations.Add(new ZID_RomaneioOcorrenciaMap());
            modelBuilder.Configurations.Add(new ZID_RomaneioPrevisaoMap());
            modelBuilder.Configurations.Add(new ZID_RomaneioPrevisaoRegiaoMap());
            modelBuilder.Configurations.Add(new ZID_RPCIMap());
            modelBuilder.Configurations.Add(new ZID_RPCIDocumentoMap());
            modelBuilder.Configurations.Add(new ZID_SetorMap());
            modelBuilder.Configurations.Add(new ZID_SetorCEPMap());
            modelBuilder.Configurations.Add(new ZID_SobraMap());
            modelBuilder.Configurations.Add(new ZID_TabelaDeFreteMap());
            modelBuilder.Configurations.Add(new ZID_TabelaDeFreteProdutoMap());
            modelBuilder.Configurations.Add(new ZID_TabelaDeFreteRotaMap());
            modelBuilder.Configurations.Add(new ZID_TabelaDeFreteRotaModalMap());
            modelBuilder.Configurations.Add(new ZID_TabelaDeFreteRotaModalValorMap());
            modelBuilder.Configurations.Add(new ZID_TabelaDeFreteVigenciaMap());
            modelBuilder.Configurations.Add(new ZID_TESMap());
            modelBuilder.Configurations.Add(new ZID_TESCFOPMap());
            modelBuilder.Configurations.Add(new ZID_TituloMap());
            modelBuilder.Configurations.Add(new ZID_TituloDocumentoMap());
            modelBuilder.Configurations.Add(new ZID_TituloDuplicataMap());
            modelBuilder.Configurations.Add(new ZID_TituloDuplicataHistoricoMap());
            modelBuilder.Configurations.Add(new ZID_TituloDuplicataremessaMap());
            modelBuilder.Configurations.Add(new ZID_TituloHistoricoMap());
            modelBuilder.Configurations.Add(new ZID_TituloImagemMap());
            modelBuilder.Configurations.Add(new ZID_TRANSPORTADORAMap());
            modelBuilder.Configurations.Add(new ZID_UnidadeDeArmazenagemMap());
            modelBuilder.Configurations.Add(new ZID_UnidadeDeArmazenagemAgrupMap());
            modelBuilder.Configurations.Add(new ZID_UnidadeDeArmazenagemLoteMap());
            modelBuilder.Configurations.Add(new ZID_UnidadeDeArmazenagemMovMap());
            modelBuilder.Configurations.Add(new ZID_usuarioMap());
            modelBuilder.Configurations.Add(new ZID_UsuarioClienteMap());
            modelBuilder.Configurations.Add(new ZID_UsuarioClienteDivisaoMap());
            modelBuilder.Configurations.Add(new ZID_UsuarioCompraMap());
            modelBuilder.Configurations.Add(new ZID_UsuarioDeTabelaDeFreteMap());
            modelBuilder.Configurations.Add(new ZID_UsuarioFilialMap());
            modelBuilder.Configurations.Add(new ZID_USUARIOGRADEMap());
            modelBuilder.Configurations.Add(new ZID_USUARIOGRADECAMPOMap());
            modelBuilder.Configurations.Add(new ZID_usuarioopcaoMap());
            modelBuilder.Configurations.Add(new ZID_USUARIOOPERACAOMap());
            modelBuilder.Configurations.Add(new ZID_UsuarioOperacaoLogMap());
            modelBuilder.Configurations.Add(new ZID_VEICULOMap());
            modelBuilder.Configurations.Add(new ZID_veiculoFilialMap());
            modelBuilder.Configurations.Add(new ZID_veiculomarcaMap());
            modelBuilder.Configurations.Add(new ZID_VEICULOMODELOMap());
            modelBuilder.Configurations.Add(new ZID_VEICULORASTREADORMap());
            modelBuilder.Configurations.Add(new ZID_VeiculoTabelaMap());
            modelBuilder.Configurations.Add(new ZID_VeiculoTabelaAgregadoMap());
            modelBuilder.Configurations.Add(new ZID_VeiculoTabelaRegiaoMap());
            modelBuilder.Configurations.Add(new ZID_VeiculoTabelaRegiaoItemMap());
            modelBuilder.Configurations.Add(new ZID_veiculotipoMap());
            modelBuilder.Configurations.Add(new CalculosREMap());
            modelBuilder.Configurations.Add(new ProdutosVivoMap());
            modelBuilder.Configurations.Add(new RelControleDePedidoMap());
            modelBuilder.Configurations.Add(new ResultadoNFENFMap());
            modelBuilder.Configurations.Add(new view_relatorioretiracdaMap());
            modelBuilder.Configurations.Add(new VW_LIBERACAO_PEDIDOSMap());
            modelBuilder.Configurations.Add(new VW_LIBERACAO_PEDIDOS_DETALHEMap());
            modelBuilder.Configurations.Add(new VW_REMap());
            modelBuilder.Configurations.Add(new VW_REENTREGAMap());
            modelBuilder.Configurations.Add(new vwIrwinPecasUnidadesEntradaMap());
            modelBuilder.Configurations.Add(new vwIrwinPecasUnidadesRessuprimentoMap());
            modelBuilder.Configurations.Add(new vwIrwinPedidosEmAndamentoMap());
            modelBuilder.Configurations.Add(new vwLiberacaoDePedidoMap());
            modelBuilder.Configurations.Add(new vwLiberacaoDePedidos_P1Map());
        }
    }
}
