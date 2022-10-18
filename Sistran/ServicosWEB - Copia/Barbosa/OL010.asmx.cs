using Sistecno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de OL010
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class OL010 : System.Web.Services.WebService
    {

        [WebMethod]
        [SoapLoggerExtensionAttribute(Filename = "C:\\temp\\ol014\\Cadastrar010.log")]
        public Retorno Cadastrar(Autentica Credenciais, cOL010 cOL010)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            //if(Credenciais.login != "wsbarbosa_prod")
            //{
            //    cnx = "Data Source=192.168.10.10;Initial Catalog=vex;User ID=sa;Password=WERasd27;  ";
            //}

            try
            {
                Guid cvPrincipal = Guid.NewGuid();

                string s = "Insert into BarbosaOL010(Id,NumeroDaRemessa,CriadoPor,CriadoEm,TipoDeRemessa,NrNotaFiscal,Fornecedor) ";
                s += "values ('"+cvPrincipal+"','"+ cOL010.NumeroDaRemessa + "','" + cOL010.CriadoPor + "','" + cOL010.CriadoEm + "','" + cOL010.TipoDeRemessa + "','" + cOL010.NrNotaFiscal + "','" + cOL010.Fornecedor + "'); ";


                for (int i = 0; i < cOL010.Itens.Count; i++)
                {
                    string cd = cOL010.Itens[i].CodigoMaterial;
                    try
                    {
                        cd = int.Parse(cd).ToString();
                    }
                    catch (Exception)
                    {
                    }

                    s += "Insert into BarbosaOL010Itens (Id,IdOL010,Centro,Deposito,ItemDaRemessa,CodigoMaterial,TextoMaterial,UnidadeDeMedidaBasica,Fator,QuantidadeNF,QuantidadeConferida,DataVencimento) ";
                    s += " values ('" + Guid.NewGuid() + "','" + cvPrincipal+ "','" + cOL010.Itens[i].Centro + "','" + cOL010.Itens[i].Deposito + "','" + cOL010.Itens[i].ItemDaRemessa + "','" +cd + "','" + cOL010.Itens[i].TextoMaterial + "','" + cOL010.Itens[i].UnidadeDeMedidaBasica + "','" + cOL010.Itens[i].Fator + "','" + cOL010.Itens[i].QuantidadeNF + "','" + cOL010.Itens[i].QuantidadeConferida + "','" + cOL010.Itens[i].DataVencimento + "'); ";
                }


                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(s, cnx);

                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }
        }


        public class cOL010
        {
            public string NumeroDaRemessa { get; set; }

            public string CriadoPor { get; set; }

            public string CriadoEm { get; set; }

            public string TipoDeRemessa { get; set; }

            public string NrNotaFiscal { get; set; }

            public string Fornecedor { get; set; }

            public List<cOL010Itens> Itens { get; set; }

        }
    }
}
