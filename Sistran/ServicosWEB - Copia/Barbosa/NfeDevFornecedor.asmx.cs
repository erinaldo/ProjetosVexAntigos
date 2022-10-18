using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descricao resumida de NfeDevFornecedor
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class NfeDevFornecedor : System.Web.Services.WebService
    {

        public Retorno Cadastrar(Autentica Credenciais, PedidosTransferencia PedidosTransferencia)
        {
            try
            {
                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }

        }
    }


    public class NfeDevolucaoFornecedor
    {
        public string Operacao { get; set; }//(Sentido Movimento)     {get;set;}//
        public int NrDocumento { get; set; }//(Nr docto SAP)  {get;set;}
        public string CategoriaDaNotaFiscal { get; set; }//get; set;	{get;set;}
        public string TipoDeDocumento { get; set; }
        public DateTime DataDocumento { get; set; }
        public DateTime DataLancamento { get; set; }
        public string CriadoPor { get; set; }//(usuario)     {get;set;}
        public int ModeloNF { get; set; }// get; set;	{get;set;}
        public string Series { get; set; }
        public string Subseries { get; set; }
        public int NrNF { get; set; }// get; set;	{get;set;}
        public string MoedaNF { get; set; }// get; set;	{get;set;}
        public int Exercicio { get; set; }
        public string Empresa { get; set; }
        public string LocNeg { get; set; }// (Filial)Recebedor get; set;	{get;set;}
        public string IDParceiro { get; set; }//(Cod.Fornecedor)     {get;set;}
        public int NrDoctoOriginal { get; set; }//(doct entrada) get; set;	{get;set;}
        public decimal PesoBrutoTotalNF { get; set; }//get; set;	{get;set;}
        public decimal PesoLiquidoTotalNF { get; set; }//get; set;	{get;set;}
        public decimal UnidadeDePeso { get; set; }
        public decimal ValorTotalNF { get; set; }
        public string NrNFe { get; set; }//get; set;	{get;set;}
        public string StatusDoc { get; set; }// get; set;	{get;set;}
        public decimal VersaoXML { get; set; }//get; set;	{get;set;}
        public string NomeFornecedor { get; set; }//get; set;	{get;set;}
        public string RuaFornecedor { get; set; }//get; set;	{get;set;}
        public string CidadeFornecedor { get; set; }// get; set;	{get;set;}
        public string BairroFornecedor { get; set; }//get; set;	{get;set;}
        public string RegiaoFornecedor { get; set; }// get; set;	{get;set;}
        public string PaisFornecedor { get; set; }//get; set;	{get;set;}
        public string CEPFornecedor { get; set; }// get; set;	{get;set;}
        public int CodeFornecedor { get; set; }// get; set;	{get;set;}
        public string NIFRegionalFornecedor { get; set; }
        public string InscrMun { get; set; }//
        public string NaturezaDaOperacao { get; set; }
        public int CNPJEmit { get; set; }//
        public string CodImpEst { get; set; }//(Centro Recebedor)   {get;set;}
        public int NrDocumentoItem { get; set; }//(Nr SAP)   {get;set;}
        public int Item { get; set; }
        public string Material { get; set; }
        public string GrupoMercadorias { get; set; }// get; set;	{get;set;}
        public string DescricaoBreve { get; set; }//
        public int NrdoctoOriginal { get; set; }//(doc.entrada) get; set;	{get;set;}
        public int ItemRefNFOriginal { get; set; }//(doc entr)  {get;set;}
        public string CFOP { get; set; }
        public string NCM { get; set; }//(Cod.Controle SAP)   {get;set;}
        public decimal Quantidade { get; set; }//   {get;set;}
        public decimal UnidadeDeMedidaDoItem { get; set; }//
        public decimal PrecoUnitarioItemSEMImpostos { get; set; }
        public decimal PrecoTotalItemSEMImpostos { get; set; }
        public string CentroRecebedorMercadoria { get; set; }
        public decimal PrecoUnitItemCOMImpostos { get; set; }//
        public decimal PrecoTotalItemCOMImpostos { get; set; }
        public string CodigoEAN { get; set; }//


        public string NrPedidoDeCompra { get; set; }// get; set;	{get;set;}
        public string LinhaItemPedidodeCompra { get; set; }
        public int NrDocumentoParaNfe { get; set; }//(SAP)  {get;set;}
        public string StatusDocParaNfe { get; set; }//(SAP)    {get;set;}
        public string RegiaoParaNfe { get; set; }//(SAP)    {get;set;}
        public string AnoDoDocumentoParaNfe { get; set; }//(SAP)  {get;set;}
        public string MesDocumentoParaNfe { get; set; }//(SAP)     {get;set;}
        public string CNPJRecebedorBarbosa { get; set; }//
        public int ModeloNFparaNfe { get; set; }//(SAP)     {get;set;}
        public string SeriesParaNfe { get; set; }//(SAP)    {get;set;}
        public string NrDaNFe { get; set; }//(SAP)     {get;set;}
                                           //public string Empresa  {get;set;}
        public string LocalNeg { get; set; }// (Recebedor Mercad) get; set;	{get;set;}

    }
}