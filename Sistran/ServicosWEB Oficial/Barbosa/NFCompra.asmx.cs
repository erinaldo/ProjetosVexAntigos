using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de NFCompra
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class NFCompra : System.Web.Services.WebService
    {

        [WebMethod]
        public Retorno Cadastrar(Autentica Credenciais, NfeCompra NfeCompra)
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

        public class NfeCompra
        {
            public string Operacao { get; set; }////(Sentido Movimento)
            public decimal NrDocumento { get; set; }//(Nr docto SAP) { get; set; }//
            public string CategoriaDaNotaFiscal { get; set; }
            public string TipodeDocumento { get; set; }
            public DateTime DataDocumento { get; set; }
            public DateTime DataLançamento { get; set; }
            public string CriadoPor { get; set; }//(usuario) { get; set; }
            public decimal ModeloNF { get; set; }
            public string Series { get; set; }
            public string Subseries { get; set; }
            public int NrNF { get; set; }
            public string MoedaNF { get; set; }
            public string Empresa { get; set; }
            public string LocNeg { get; set; }// (Filial)Recebedor	 {get;set;}
            public string IDParceiro { get; set; }//(Centro Fornecedor) { get; set; }
            public decimal PesoBrutoTotalNF { get; set; }
            public decimal PesoLiquidoTotalNF { get; set; }
            public decimal UnidadeDePeso { get; set; }
            public decimal ValorTotalNF { get; set; }
            public string NrNFe { get; set; }
            public string StatusDoc { get; set; }
            public decimal VersaoXML { get; set; }
            public string NomeLojaRecebedor { get; set; }
            public string RuaLojaRecebedor { get; set; }
            public string CidadeLojaRecebedor { get; set; }
            public string BairroLojaRecebedor { get; set; }
            public string RegiaoLojaRecebedor { get; set; }
            public string PaisLojaRecebedor { get; set; }
            public string CEPLojaRecebedor { get; set; }
            public decimal CodeCGC { get; set; }//(Centro Forneced) { get; set; }
            public string NIFRegiona { get; set; }//(Centro Forneced) { get; set; }
            public string InscrMun { get; set; }// (Centro Fornec) { get; set; }
            public int DomicilioFiscal { get; set; }//(Codigo IBGE) { get; set; }
            public string RuaEmitente { get; set; }//(Centro Forneced) { get; set; }
            public string NrRuaEmitente { get; set; }//(Centr.Fornec) { get; set; }
            public decimal CNPJEmit { get; set; }//. (Centro Recebedor)	 {get;set;}
            public string CodImpEst { get; set; }//(Centro Recebedor) { get; set; }
            public decimal NrDocumentoItem { get; set; }//(Nr SAP) { get; set; }
            public decimal Item { get; set; }
            public string Material { get; set; }
            public string GrupoMercadorias { get; set; }
            public string DescricaoBreve { get; set; }
            public string CFOP { get; set; }
            public string NCM { get; set; }//(Cod.Controle SAP) { get; set; }
            public decimal decimalimaltidade { get; set; }
            public decimal UnidadeDeMedidaItem { get; set; }//{get;set;}
            public decimal PrecoDecimalimalarioItemSEMImpostos { get; set; }//sem impostos         { get; set; }
            public decimal PrecoTotalItemSEMImpostos { get; set; }
            public string CentroRecebedorMercadoria { get; set; }
            public decimal PrecodecimalimalItemCOMImpostos { get; set; }
            public decimal PrecoTotalItemCOMImpostos { get; set; }
            public string CodigoEAN { get; set; }
            public string NrPedidoDeCompra { get; set; }
            public string LinhaItemPedidoDeCompra { get; set; }
            public decimal NrdocumentoParaNfe { get; set; }//(SAP) { get; set; }
            public string StatusDocParaNfe { get; set; }//(SAP) { get; set; }
            public string RegiaoParaNfe { get; set; }//(SAP) { get; set; }
            public int AnoDoDocumentoParaNfe { get; set; }//(SAP) { get; set; }
            public int MesDocumentoParaNfe { get; set; }//SAP) { get; set; }
            public string CNPJEmissor { get; set; }///Fornecedor Nfe { get; set; }//(SAP) { get; set; }
            //public int ModeloNF { get; set; }// para Nfe{ get; set; }//(SAP) { get; set; }
            //public string Series { get; set; }// para Nfe { get; set; }//(SAP) { get; set; }
            //public string NrNFe { get; set; }//{ get; set; }//(SAP) { get; set; }

        }
    }
}
