using Sistecno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de OL014
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]   

    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class OL014 : System.Web.Services.WebService
    {
        string sql = "";
        string sqlItens = "";
        [WebMethod]
        // [SoapLoggerExtensionAttribute(Filename = "C:\\temp\\ol014\\Cadastrar014.log")]
        [SoapLoggerExtensionAttribute(Filename = "C:\\temp\\ol014\\Cadastrar014.log")]

        public Retorno Cadastrar(Autentica Credenciais, cOL014 cOL014)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
             sql = "";
             sqlItens = "";
            try
            {
                Guid cvPrincipal = Guid.NewGuid();

                if(cOL014.Chave == "")                
                    throw new Exception("Favor informar a chave da nota fiscal.");
                               

                sql = "insert into BarbosaOL014(Chave,";
                sql += "ID,Operacao,NrDocumento,CategoriaDaNotaFiscal,TipoDeDocumento,DataDocumento,DataLancamento,Series,Subseries,NrNF,Empresa,LocNeg,";
                sql += " IDParceiro,PesoBrutoTotalNF,PesoLiquidoTotalNF,UnidadeDePeso,ValorTotalNF,StatuDoc,NomeLojaRecebedor,RuaLojaRecebedor,CidadeLojaRecebedor,";
                sql += "RegiaoLojaRecebedor,PaisLojaRecebedor,CEPLojaRecebedor,CodeCGC, NaturezaDaOperacao,CNPJEmit ";
                sql += ") values ('"+ cOL014.Chave +"', ";
                sql += "'" + cvPrincipal + "',";//ID
                sql += "'" + cOL014.Operacao + "',";//Operacao
                sql += "'" + cOL014.NrDocumento + "',";//NrDocumento
                sql += "'" + (cOL014.CategoriaDaNotaFiscal == null? "": cOL014.CategoriaDaNotaFiscal) + "',";//CategoriaDaNotaFiscal
                sql += "'" + (cOL014.TipoDeDocumento == null ? "" : cOL014.TipoDeDocumento) + "',"; //TipoDeDocumento
                sql += "'" + (cOL014.DataDocumento == null ? "" : cOL014.DataDocumento) + "',";//DataDocumento
                sql += "'" + (cOL014.DataLancamento == null ? "" : cOL014.DataLancamento) + "',";//DataLancamento
                sql += "'" + (cOL014.Series == null ? "" : cOL014.Series) + "',";//Series
                sql += "'" + (cOL014.Subseries == null ? "" : cOL014.Subseries) + "',";//Subseries
                sql += (cOL014.NrNF == null ? "" : cOL014.NrNF.ToString()) + ",";//NrNF
                sql += "'" + (cOL014.Empresa == null ? "" : cOL014.Empresa) + "',";//Empresa
                sql += "'" + (cOL014.LocNeg == null ? "" : cOL014.LocNeg) + "',";//LocNeg               
                sql += "'" + (cOL014.IDParceiro == null ? "" : cOL014.IDParceiro) + "',";//IDParceiro
                sql += cOL014.PesoBrutoTotalNF.ToString().Replace(",", ".") + ",";//PesoBrutoTotalNF
                sql += cOL014.PesoLiquidoTotalNF.ToString().Replace(",", ".") + ",";//PesoLiquidoTotalNF
                sql += "'" + (cOL014.UnidadeDePeso ==null? "0" : cOL014.UnidadeDePeso.ToString().Replace(",", ".")) + "',";//UnidadeDePeso
                sql += cOL014.ValorTotalNF.ToString().Replace(",", ".") + ",";//ValorTotalNF
                sql += "'" + (cOL014.StatuDoc == null ? "" : cOL014.StatuDoc) + "',";//StatuDoc
                sql += "'" + (cOL014.NomeLojaRecebedor == null ? "" : cOL014.NomeLojaRecebedor) + "',";//NomeLojaRecebedor
                sql += "'" + (cOL014.RuaLojaRecebedor == null ? "" : cOL014.RuaLojaRecebedor) + "',";//RuaLojaRecebedor
                sql += "'" + (cOL014.CidadeLojaRecebedor == null ? "" : cOL014.CidadeLojaRecebedor) + "',";//CidadeLojaRecebedor
                 sql += "'" + (cOL014.RegiaoLojaRecebedor == null ? "" : cOL014.RegiaoLojaRecebedor) + "',";//RegiaoLojaRecebedor
                sql += "'" + (cOL014.PaisLojaRecebedor == null ? "" : cOL014.PaisLojaRecebedor) + "',";//PaisLojaRecebedor
                sql += "'" + (cOL014.CEPLojaRecebedor == null ? "" : cOL014.CEPLojaRecebedor) + "',";//CEPLojaRecebedor
                sql += "'" + (cOL014.CodeCGC == null ? "" : cOL014.CodeCGC) + "',";//CodeCGC
                sql += "'" + (cOL014.NaturezaDaOperacao == null ? "" : cOL014.NaturezaDaOperacao) + "',";//NaturezaDaOperacao
                sql += "'" + (cOL014.CNPJEmit == null ? "" : cOL014.CNPJEmit) + "'";//CNPJEmit
                sql += ");";
                

                for (int i = 0; i < cOL014.Itens.Count; i++)
                {
                    sqlItens += "Insert into BarbosaOl014Itens(Id,IdOl014,CodImpEst,NrDocumentoItem,Item,Material,DescricaoBreve,CFOP,NCM,Quantidade,UnidadeDeMedidaItem,PrecoUnitarioItemSEMImpostos,PrecoTotalItemSEMImpostos,CentroRecebedorMercadoria,PrecoUnitItemCOMImpostos,PrecoTotalItemCOMImpostos,CodigoEAN) values(";
                    sqlItens += "'" + Guid.NewGuid() + "',";
                    sqlItens += "'" + cvPrincipal + "',";
                    sqlItens += "'" + (cOL014.Itens[i].CodImpEst == null ? "" : cOL014.Itens[i].CodImpEst) + "',";
                    sqlItens += "'" + cOL014.Itens[i].NrDocumentoItem + "',";
                    sqlItens += "'" + cOL014.Itens[i].Item + "',";
                    sqlItens += "'" + (cOL014.Itens[i].Material == null ? "" : cOL014.Itens[i].Material) + "',";
                    sqlItens += "'" + (cOL014.Itens[i].DescricaoBreve == null ? "" : cOL014.Itens[i].DescricaoBreve) + "',";
                    sqlItens += "'" + (cOL014.Itens[i].CFOP == null ? "" : cOL014.Itens[i].CFOP) + "',";
                    sqlItens += "'" + (cOL014.Itens[i].NCM == null ? "" : cOL014.Itens[i].NCM) + "',";
                    sqlItens += cOL014.Itens[i].Quantidade.ToString().Replace(",", ".") + ",";
                    sqlItens += "'"+cOL014.Itens[i].UnidadeDeMedidaItem.ToString().Replace(",", ".") + "',";
                    sqlItens += cOL014.Itens[i].PrecoUnitarioItemSEMImpostos.ToString().Replace(",", ".") + ",";
                    sqlItens += cOL014.Itens[i].PrecoTotalItemSEMImpostos.ToString().Replace(",", ".") + ",";
                    sqlItens += "'" + cOL014.Itens[i].CentroRecebedorMercadoria.ToString() + "',";
                    sqlItens += cOL014.Itens[i].PrecoUnitItemCOMImpostos.ToString().Replace(",", ".") + ",";
                    sqlItens += cOL014.Itens[i].PrecoTotalItemCOMImpostos.ToString().Replace(",", ".") + ",";
                    sqlItens += "'" + (cOL014.Itens[i].CodigoEAN == null ? "" : cOL014.Itens[i].CodigoEAN) + "'";
                    sqlItens += "); ";
                }
                

                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql + sqlItens, cnx);

                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }
        }


        public class cOL014Item
        {
            public string CodImpEst { get; set; } //(Centro Recebedor) { get; set; }
            public int NrDocumentoItem { get; set; } //(Nr SAP) { get; set; }
            public int Item { get; set; }
            public string Material { get; set; }
            public string DescricaoBreve { get; set; }
            public string CFOP { get; set; }
            public string NCM { get; set; } //(Cod.Controle SAP) { get; set; }
            public decimal Quantidade { get; set; }
            public string UnidadeDeMedidaItem { get; set; }
            public decimal PrecoUnitarioItemSEMImpostos { get; set; }
            public decimal PrecoTotalItemSEMImpostos { get; set; }
            public string CentroRecebedorMercadoria { get; set; }
            public decimal PrecoUnitItemCOMImpostos { get; set; }
            public decimal PrecoTotalItemCOMImpostos { get; set; }
            public string CodigoEAN { get; set; }

        }
            public class cOL014
        {
            //public int MyProperty { get; set; }
            public string Operacao { get; set; }
            public string Chave { get; set; }
            public int NrDocumento { get; set; }
            public string CategoriaDaNotaFiscal { get; set; }
            public string TipoDeDocumento { get; set; }
            public string DataDocumento { get; set; }
            public string DataLancamento { get; set; }
            public string Series { get; set; }
            public string Subseries { get; set; }
            public int NrNF { get; set; }
            public string Empresa { get; set; } //
            public string LocNeg { get; set; } //(Filial)Recebedor	{get;set;}
            public string IDParceiro { get; set; } //(Empresa+Centro Fornec) { get; set; }
            public decimal PesoBrutoTotalNF { get; set; }
            public decimal PesoLiquidoTotalNF { get; set; }
            public string UnidadeDePeso { get; set; }
            public decimal ValorTotalNF { get; set; }
            public string StatuDoc { get; set; }
            public string NomeLojaRecebedor { get; set; }
            public string RuaLojaRecebedor { get; set; }
            public string CidadeLojaRecebedor { get; set; }
            public string BairroLojaRecebedor { get; set; }
            public string RegiaoLojaRecebedor { get; set; }
            public string PaisLojaRecebedor { get; set; }
            public string CEPLojaRecebedor { get; set; }
            public string CodeCGC { get; set; } //(Centro Forneced) { get; set; }
            public string NaturezaDaOperacao { get; set; }
            public string CNPJEmit { get; set; } //. (Centro Recebedor)	{get;set;}

            public List<cOL014Item> Itens { get; set; }
        }
    }
}
