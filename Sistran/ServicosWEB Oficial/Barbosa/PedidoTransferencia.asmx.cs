using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using static ServicosWEB.Barbosa.PedidoDeCompra;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de PedidoTransferencia
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class PedidoTransferencia : System.Web.Services.WebService
    {

        [WebMethod]
        public Retorno Cadastrar(Autentica Credenciais, PedidosTransferencia PedidosTransferencia)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            try
            {
                string sql = "Insert Into BarbosaPedidoDeCompra(TipoDePedidoDeCompra,CategoriaDocumento,NumeroDoPedido,DataDoPedido,CodigoFornecedor,DataRemessa,PrecoLiquido,PrecoBrutoUnit,LocalRecebimento, Processo) values " +
                   "('" + PedidosTransferencia.TipoDePedidoDeCompra + "', '" + PedidosTransferencia.CategoriaDocumento + "', '" + PedidosTransferencia.NumeroDoPedido + "', '" + PedidosTransferencia.DataDoPedido + "', '" + PedidosTransferencia.CodigoFornecedor + "',  '" + PedidosTransferencia.DataRemessa + "', " + PedidosTransferencia.PrecoLiquidoUnitTransf.ToString().Replace(",", ".") + ", " + PedidosTransferencia.PrecoLiquidoUnitTransf.ToString().Replace(",", ".") + ", '" + PedidosTransferencia.LocalRecebimento + "', 'PedidoDeTransf') ";

                sql += "; select SCOPE_IDENTITY() ";
                string id = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx).Rows[0][0].ToString();

                for (int i = 0; i < PedidosTransferencia.Items.Count(); i++)
                {
                    sql = "Insert into BarbosaPedidoDeCompraItem (IdPedidoCompra, LinhaItemDoPedido, CodigoMaterial, DescricaoBreveMaterial,UnidadeDeMedidaPedido,NrEANItemDopedido) values " +
                        "(" + id + ", " + PedidosTransferencia.Items[i].LinhaItemDoPedido + ", '" + PedidosTransferencia.Items[i].CodigoMaterial + "', '" + PedidosTransferencia.Items[i].DescricaoBreveMaterial + "','" + PedidosTransferencia.Items[i].UnidadeDeMedidaPedido + "','" + PedidosTransferencia.Items[i].NrEANItemDopedido + "')";
                    Sistran.Library.GetDataTables.RetornarDataTableWS(sql + "; select 1", cnx);
                }
                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }

        }
    }


    public class PedidosTransferencia
    {
        public string TipoDePedidoDeCompra { get; set; }
        public string CategoriaDocumento { get; set; }
        public string NumeroDoPedido { get; set; }
        public string DataDoPedido { get; set; }
        public string CodigoFornecedor { get; set; }
        public string DataRemessa { get; set; } 
        public decimal PrecoLiquidoUnitTransf { get; set; }
        public string LocalRecebimento { get; set; }
        public List<PedidoItem> Items { get; set; }
    }

    
}