using Sistecno;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
//mongodb+srv://admin:<password>@cluster0-s2myx.mongodb.net/test?retryWrites=true&w=majority
namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de OL016
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class OL016 : System.Web.Services.WebService
    {

        //[WebMethod]

        //public Retorno Cadastrar(Autentica Credenciais, cOL016 c)
        //{
        //    string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

        //    try
        //    {
        //        string sql = "Insert into BarbosaOL016(PedidoDeCompra,ItemPedCompra,PedidoDeTransferencia,ItemPedTransf,TipoDeProcesso,Material,QtdeCrossDocking,UndMedidaCrossDocking,CentroCornecedor,CentroRecebedor,StatusDeProcessamento) ";
        //        sql += " Values('"+ c.PedidoDeCompra+ "','" + c.ItemPedCompra + "','" + c.PedidoDeTransferencia + "','" + c.ItemPedTransf + "','" + c.TipoDeProcesso + "','" + c.Material + "','" + c.QtdeCrossDocking + "','" + c.UndMedidaCrossDocking + "','" + c.CentroCornecedor + "','" + c.CentroRecebedor + "','" + c.StatusDeProcessamento + "') ";


        //        Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);

        //        return new Retorno() { Erro = "", Sucesso = true };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Retorno() { Erro = ex.Message, Sucesso = false };
        //    }
        //}


        [WebMethod]
        [SoapLoggerExtensionAttribute(Filename = "C:\\temp\\ol014\\Cadastrar016.log")]

        public Retorno CadastrarGrade(Autentica Credenciais, List<cOL016> c)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            //if (Credenciais.login != "wsbarbosa_prod")
            //{
            //    cnx = "Data Source=192.168.10.10;Initial Catalog=vex;User ID=sa;Password=WERasd27;  ";
            //}

            try
            {

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(
                    "Select NumeroRecebimento from BarbosaOl016 where  NumeroRecebimento='" + c[0].NumeroRecebimento + "'",
                    cnx);


                if(dt.Rows.Count>0)
                {
                    return new Retorno() { Erro = "Recebimento já existente", Sucesso = false };
                }


                for (int i = 0; i < c.Count; i++)
                {

                    string cd = c[i].Material;
                    try
                    {
                        cd = int.Parse(cd).ToString();
                    }
                    catch (Exception)
                    {
                    }

                    string sql = "Insert into BarbosaOL016(Deposito, NumeroItemRecebimento, NumeroRecebimento, PedidoDeCompra,ItemPedCompra,PedidoDeTransferencia,ItemPedTransf,TipoDeProcesso,Material,QtdeCrossDocking,UndMedidaCrossDocking,CentroCornecedor,CentroRecebedor,StatusDeProcessamento) ";
                    sql += " Values('"+ c[i].Deposito + "', '" + c[i].NumeroItemRecebimento + "', '"+ c[i].NumeroRecebimento +"','" + c[i].PedidoDeCompra + "','" + c[i].ItemPedCompra + "','" + c[i].PedidoDeTransferencia + "','" + c[i].ItemPedTransf + "','" + c[i].TipoDeProcesso + "','" + cd + "','" + c[i].QtdeCrossDocking + "','" + c[i].UndMedidaCrossDocking + "','" + c[i].CentroCornecedor + "','" + c[i].CentroRecebedor + "','" + c[i].StatusDeProcessamento + "') ";
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);
                }
                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }
        }
    }


    public class cOL016
    {
        
        public string NumeroRecebimento { get; set; }
        public string NumeroItemRecebimento { get; set; }
        public string Deposito { get; set; }

        public string PedidoDeCompra { get; set; }
        public string ItemPedCompra { get; set; }
        public string PedidoDeTransferencia { get; set; }
        public string ItemPedTransf { get; set; }
        public string TipoDeProcesso { get; set; }
        public string Material { get; set; }
        public string QtdeCrossDocking { get; set; }
        public string UndMedidaCrossDocking { get; set; }
        public string CentroCornecedor { get; set; }
        public string CentroRecebedor { get; set; }
        public string StatusDeProcessamento { get; set; }
    }
}
