using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

namespace ServicosWEB.Josapar
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class WS : System.Web.Services.WebService
    {

        [WebMethod]
        public List<PedidoSep> PedidosSeparados(string Login, string Senha, string CNPJ)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string IdCliente = "";
            try
            {
                string sql = "SELECT top 1 Cli.IDCliente FROM  Cadastro c ";
                sql += " INNER JOIN Cliente cli on cli.IdCliente = c.IDCadastro  ";
                sql += " WHERE c.CNPJCPF='" + Util.Validacao.FormatarCnpj(CNPJ) + "' ";
                sql += " and cli.Ativo = 'sim' ";
                sql += " order by 1 desc ";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dt.Rows.Count == 0)
                    throw new Exception("O CNPJ Informado Não Corresponde a um Cliente Válido");

                IdCliente = dt.Rows[0]["IdCliente"].ToString();

                sql = "exec PRC_IntergarPedidoClienteSep " + IdCliente;
                dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dt.Rows.Count == 0)
                    throw new Exception("Não Há Pedidos Pendentes Para Envio.");

                //Dados do Pedido
                List<PedidoSep> lPed = new List<PedidoSep>();

                for (int i = 0; i < dt.Rows.Count; i++) // Pedido
                {
                    PedidoSep Ped = new PedidoSep();
                    
                    Ped.CNPJCliente = Util.Validacao.FormatarCnpj(CNPJ);
                    Ped.Numero = dt.Rows[i]["Numero"].ToString();
                    Ped.Serie = dt.Rows[i]["Serie"].ToString();
                  

                    sql = "exec PRC_IntergarPedidoClienteItens " + dt.Rows[i]["IdDocumento"].ToString();
                    DataTable dtItens = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
                    List<PedidoItens> lPedItens = new List<PedidoItens>();

                    for (int ii = 0; ii < dtItens.Rows.Count; ii++) //Itens de Pedido
                    {
                        PedidoItens PedItens = new PedidoItens();
                        PedItens.Codigo = dtItens.Rows[ii]["Codigo"].ToString();
                        PedItens.CodigoDeBarras = dtItens.Rows[ii]["CodigoDeBarras"].ToString();
                        PedItens.Descricao = dtItens.Rows[ii]["Descricao"].ToString();
                        PedItens.Quantidade = dtItens.Rows[ii]["Quantidade"].ToString();
                        lPedItens.Add(PedItens);

                    }
                    Ped.Itens = lPedItens;
                    lPed.Add(Ped);

                    sql = "Update Documento set IntegradoClienteSeparacao = 'Integrado: " + DateTime.Now.ToString() + "'  Where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString();
                    Sistran.Library.GetDataTables.ExecutarSemRetornoWin(sql, cnx);
                }
                return lPed;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public List<Pedido> ConsultarPedidos(string Login, string Senha, string CNPJ)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string IdCliente = "";
            try
            {
                string sql = "SELECT top 1 Cli.IDCliente FROM  Cadastro c ";
                sql += " INNER JOIN Cliente cli on cli.IdCliente = c.IDCadastro  ";
                sql += " WHERE c.CNPJCPF='" + Util.Validacao.FormatarCnpj(CNPJ) + "' ";
                sql += " and cli.Ativo = 'sim' ";
                sql += " order by 1 desc ";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dt.Rows.Count == 0)
                    throw new Exception("O CNPJ Informado Não Corresponde a um CLiente Válido");

                IdCliente = dt.Rows[0]["IdCliente"].ToString();

                sql = "exec PRC_IntergarPedidoCliente " + IdCliente;
                dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);

                if (dt.Rows.Count == 0)
                    throw new Exception("Não Há Pedidos Pendentes Para Envio.");

                //Dados do Pedido
                List<Pedido> lPed = new List<Pedido>();

                for (int i = 0; i < dt.Rows.Count; i++) // Pedido
                {
                    Pedido Ped = new Pedido();
                    Ped.CEPDestinatario = dt.Rows[i]["CEPDestinatario"].ToString();
                    Ped.CidadeDestinatario = dt.Rows[i]["CidadeDestinatario"].ToString();
                    Ped.CNPJDestinatario = dt.Rows[i]["CNPJDestinatario"].ToString();
                    Ped.DataDeEmissao = dt.Rows[i]["DataDeEmissao"].ToString();
                    Ped.EnderecoDestinatario = dt.Rows[i]["EnderecoDestinatario"].ToString();
                    Ped.EnderecoNumeroDestinatario = dt.Rows[i]["EnderecoNumeroDestinatario"].ToString();
                    Ped.FantasiaDestinatario = dt.Rows[i]["FantasiaDestinatario"].ToString();
                    Ped.Numero = dt.Rows[i]["Numero"].ToString();
                    Ped.RazaoSocialDestinatario = dt.Rows[i]["RazaoSocialDestinatario"].ToString();
                    Ped.Serie = dt.Rows[i]["Serie"].ToString();
                    Ped.UFDestinatario = dt.Rows[i]["UFDestinatario"].ToString();

                    sql = "exec PRC_IntergarPedidoClienteItens " + dt.Rows[i]["IdDocumento"].ToString();
                    DataTable dtItens = Sistran.Library.GetDataTables.RetornarDataTableWS(sql, cnx);
                    List<PedidoItens> lPedItens = new List<PedidoItens>();

                    for (int ii = 0; ii < dtItens.Rows.Count; ii++) //Itens de Pedido
                    {
                        PedidoItens PedItens = new PedidoItens();
                        PedItens.Codigo = dtItens.Rows[ii]["Codigo"].ToString();
                        PedItens.CodigoDeBarras = dtItens.Rows[ii]["CodigoDeBarras"].ToString();
                        PedItens.Descricao = dtItens.Rows[ii]["Descricao"].ToString();
                        PedItens.Quantidade = dtItens.Rows[ii]["Quantidade"].ToString();
                        lPedItens.Add(PedItens);

                    }
                    Ped.Itens = lPedItens;
                    lPed.Add(Ped);

                    sql = "Update Documento set IntegradoCliente = 'Integrado: " + DateTime.Now.ToString() + "'  Where IdDocumento=" + dt.Rows[i]["IdDocumento"].ToString() ;
                    Sistran.Library.GetDataTables.ExecutarSemRetornoWin(sql, cnx);
                }
                return lPed;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class Pedido
    {
        public string Numero { get; set; }
        public string Serie { get; set; }
        public string DataDeEmissao { get; set; }
        public string CNPJDestinatario { get; set; }
        public string RazaoSocialDestinatario { get; set; }
        public string FantasiaDestinatario { get; set; }
        public string EnderecoDestinatario { get; set; }
        public string EnderecoNumeroDestinatario { get; set; }
        public string CEPDestinatario { get; set; }
        public string CidadeDestinatario { get; set; }
        public string UFDestinatario { get; set; }
        public List<PedidoItens> Itens { get; set; }
    }

    public class PedidoItens
    {
        public string Codigo { get; set; }
        public string CodigoDeBarras { get; set; }
        public string Quantidade { get; set; }
        public string Descricao { get; set; }
    }

    public class PedidoSep
    {
        public string Numero { get; set; }
        public string Serie { get; set; }
        public string DataDeEmissao { get; set; }
        public string CNPJCliente { get; set; }       
        public List<PedidoItens> Itens { get; set; }
    }
}
