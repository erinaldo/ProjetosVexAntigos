using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de Ol013
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class OL011 : System.Web.Services.WebService
    {
        [WebMethod]
        [Sistecno.SoapLoggerExtensionAttribute(Filename = "C:\\temp\\ol014\\Cadastrar011.log")]

        public Retorno Cadastrar(Autentica Credenciais, cOL011 c)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
         
            try
            {
                //if (!Autentica.Autenticar(Credenciais.login, Credenciais.senha))
                //    throw new Exception("Nao Autorizado");

                Guid cvPrincipal = Guid.NewGuid();

                string sql = "Insert into barbosaOL011 (ID, TipodePedidodeCompra ,NumerodoPedido , DatadoPedido , pedidoeliminado , CodigoFornecedor  )";
                sql += " VALUES ('"+cvPrincipal+"', '"+ c.TipodePedidodeCompra + "','"+ c.NumerodoPedido + "' , '" + c.DatadoPedido + "' , '" + c.pedidoeliminado + "' , '" + c.CodigoFornecedor + "' ); ";



                for (int i = 0; i < c.Itens.Count; i++)
                {
                    string cd = c.Itens[i].CodigoMaterial;
                    try
                    {
                        cd = int.Parse(cd).ToString();
                    }
                    catch (Exception)
                    {
                    }

                    sql += "Insert into BarbosaOL011Itens(Id, IdOl011, Linhaitemdopedido , CodigoMaterial , DescricaoBreveMaterial , UnidadedeMedidaPedido , Fator , Quantidade , LocalRecebimento , itemeliminado , itemfinalizado , QuantidadeConferida, DataVencimento , Embarque, Deposito  ) ";
                    sql += "Values ('" + Guid.NewGuid() + "', '" + cvPrincipal + "', '" + c.Itens[i].Linhaitemdopedido + "' , '" + cd + "' , '" + c.Itens[i].DescricaoBreveMaterial + "', '" + c.Itens[i].UnidadedeMedidaPedido + "', '" + c.Itens[i].Fator + "', '" + c.Itens[i].Quantidade + "', '" + c.Itens[i].LocalRecebimento + "', '" + c.Itens[i].itemeliminado + "', '" + c.Itens[i].itemfinalizado + "', '" + c.Itens[i].QuantidadeConferida + "', '" + c.Itens[i].DataVencimento + "', '" + c.Itens[i].Embarque + "', '"+c.Itens[i].Deposito+"' );";
                }


                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);

                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }
        }


        public class cOL011
        {
            public string TipodePedidodeCompra { get; set; }
            public string NumerodoPedido { get; set; }
            public string DatadoPedido { get; set; }
            public string pedidoeliminado { get; set; }
            public string CodigoFornecedor { get; set; }
            public List<cOL011Itens> Itens { get; set; }

        }


        public class cOL011Itens { 
            public string Deposito { get; set; }
            public string Linhaitemdopedido { get; set; }
            public string CodigoMaterial { get; set; }
            public string DescricaoBreveMaterial { get; set; }
            public string UnidadedeMedidaPedido { get; set; }
            public string Fator { get; set; }
            public string Quantidade { get; set; }
            public string LocalRecebimento { get; set; }
            public string itemeliminado { get; set; }
            public string itemfinalizado { get; set; }
            public string QuantidadeConferida { get; set; }
            public string DataVencimento { get; set; }
            public string Embarque { get; set; }
        }
    }
}
