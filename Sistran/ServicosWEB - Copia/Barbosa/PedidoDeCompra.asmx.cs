using Sistecno;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de PedidoDeCompra
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class PedidoDeCompra : System.Web.Services.WebService
    {

        [WebMethod]
        //[SoapLoggerExtensionAttribute(Filename = "PedidoDeCompra_Cadastrar.log")]
        [SoapLoggerExtensionAttribute(Filename = "c:\\temp\\PedidoDeCompra_Cadastrar.log")]
        public Retorno Cadastrar(Autentica Credenciais, PedidoCompra PedidoCompra)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            
            try
            {
                // DataSet ds = (DataSet)PedidoCompra;


                string sql = "Insert Into BarbosaPedidoDeCompra(TipoDePedidoDeCompra,CategoriaDocumento,NumeroDoPedido,DataDoPedido,CodigoFornecedor,DataRemessa,PrecoLiquido,PrecoBrutoUnit,LocalRecebimento, Processo) values " +
                    "('"+ PedidoCompra.TipoDePedidoDeCompra + "', '"+ PedidoCompra .CategoriaDocumento+ "', '"+ PedidoCompra.NumeroDoPedido+ "', '"+ PedidoCompra.DataDoPedido+ "', '"+ PedidoCompra .CodigoFornecedor+ "',  '" + PedidoCompra.DataRemessa + "', "+  PedidoCompra.PrecoLiquido.ToString().Replace(",", ".") + ", "+PedidoCompra.PrecoLiquido.ToString().Replace(",", ".") + ", '"+PedidoCompra.LocalRecebimento+"', 'PedidoDeCompra') ";

                sql += "; select SCOPE_IDENTITY() ";
                string id = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx).Rows[0][0].ToString();

                for (int i = 0; i < PedidoCompra.Items.Count(); i++)
                {
                    sql = "Insert into BarbosaPedidoDeCompraItem (IdPedidoCompra, LinhaItemDoPedido, CodigoMaterial, DescricaoBreveMaterial,UnidadeDeMedidaPedido,NrEANItemDopedido) values "+
                        "("+ id + ", " + PedidoCompra.Items[i].LinhaItemDoPedido + ", '"+ PedidoCompra.Items[i].CodigoMaterial +"', '" + PedidoCompra.Items[i].DescricaoBreveMaterial+"','"+PedidoCompra.Items[i].UnidadeDeMedidaPedido+"','" + PedidoCompra.Items[i].NrEANItemDopedido + "')";
                    Sistran.Library.GetDataTables.RetornarDataTableWS(sql + "; select 1", cnx);

                }


                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }
        }

        public class PedidoCompra
        {
            public string TipoDePedidoDeCompra { get; set; }
            public string CategoriaDocumento { get; set; }
            public string NumeroDoPedido { get; set; }
            public string DataDoPedido { get; set; }
            public string CodigoFornecedor { get; set; }
            public List<PedidoItem> Items { get; set; }
            public string DataRemessa { get; set; }
            public decimal PrecoLiquido { get; set; }
            public decimal PrecoBrutoUnit { get; set; }
            public string LocalRecebimento { get; set; }
        }

        public class PedidoItem
        {
            public int LinhaItemDoPedido { get; set; }
            public string CodigoMaterial { get; set; }
            public string DescricaoBreveMaterial { get; set; }
            public decimal UnidadeDeMedidaPedido { get; set; }
            public string Quantidade { get; set; }
            public string NrEANItemDopedido { get; set; }
        }
    }
}