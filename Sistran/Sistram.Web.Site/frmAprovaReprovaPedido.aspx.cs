using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
////using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;

public partial class frmAprovaReprovaPedido : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new Pedido().AprovarPedidoPrepararDadosEmail(Request.QueryString["id_documento"]);

        if (dt.Rows.Count > 0)
        {
            //Autorizar
            if (Request.QueryString["tipo"] == "1")
            {
                if (dt.Rows[0]["SITUACAO"].ToString().ToUpper() == "AGUARDANDO O CLIENTE AUTORIZAR")
                {
                    if (Autorizado())
                    {

                        Label1.Text = "O PEDIDO FOI AUTORIZADO COM SUCESSO.";

                        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                        string strsql = MontarCarta(Request.QueryString["id_documento"].ToString(), "Pedido Aprovado: " + dt.Rows[0]["NUMERO"].ToString() + " Por: " +  ILusuario[0].UsuarioNome  + " em: " + DateTime.Now.ToString(), dt) ; //"<html> ";
                        //strsql += " <body > ";
                        //strsql += " <p><strong><font face='Verdana, Arial, Helvetica, sans-serif'>Pedido Aprovado </font></strong> </p> ";
                        //strsql += " <p><font face='Verdana, Arial, Helvetica, sans-serif'><strong>Número: " + dt.Rows[0]["NUMERO"] + " </strong></font> </p> ";
                        //strsql += " <p>&nbsp;</p> ";
                        //strsql += " <p><font face='Verdana, Arial, Helvetica, sans-serif'><strong>Este pedido foi  aprovado por <font color='#FF0000'>" + ILusuario[0].UsuarioNome + "</font> em <font color='#FF0000'>" + DateTime.Now.ToString() + "</font></strong></font></p> ";
                        //strsql += " </body> ";
                        //strsql += " </html> ";

                        Sistran.Library.EnviarEmails.EnviarEmail(dt.Rows[0]["ENDERECO"].ToString(), ConfigurationSettings.AppSettings["emailPedido"].ToString(), "AVISO: APROVAÇÃO DO PEDIDO DE NUMERO " + dt.Rows[0]["NUMERO"].ToString(), strsql, ConfigurationSettings.AppSettings["smtp"].ToString(), ConfigurationSettings.AppSettings["senhasmtp"].ToString());

                        DataTable dtAprovadores = new SistranBLL.Cadastro.CadastroContato().ListarEmailsAprovadoresPedidos();

                        foreach (DataRow item in dtAprovadores.Rows)
                        {
                            Sistran.Library.EnviarEmails.EnviarEmail(item["ENDERECO"].ToString(), ConfigurationSettings.AppSettings["emailPedido"].ToString(), "AVISO: APROVAÇÃO DO PEDIDO DE NUMERO " + dt.Rows[0]["NUMERO"].ToString(), strsql, ConfigurationSettings.AppSettings["smtp"].ToString(), ConfigurationSettings.AppSettings["senhasmtp"].ToString());
                        }

                    }
                }
                else
                {
                    Label1.Text = "ESTE PEDIDO JA FOI AUTORIZADO OU ESTA CANCELADO.";
                }
            }
            else
            {
                //Cancelar
                if (dt.Rows[0]["SITUACAO"].ToString().ToUpper() == "ENVIAR PARA FATURAMENTO" || dt.Rows[0]["SITUACAO"].ToString().ToUpper() == "AGUARDANDO LIBERACAO" || dt.Rows[0]["SITUACAO"].ToString().ToUpper() == "AGUARDANDO O CLIENTE AUTORIZAR" || dt.Rows[0]["SITUACAO"].ToString().ToUpper() == "AGUARDANDO AUTORIZAR")
                {
                    SistranBLL.Pedido op = new Pedido();
                    op.CancelarPedido(Request.QueryString["id_documento"]);
                    op.AlterarSituacaoPedido("CANCELADO", Request.QueryString["id_documento"]);
                    op.IncluirOcorrencia(Request.QueryString["id_documento"], "PEDIDO CANCELADO");
                    Label1.Text = "PEDIDO CANCELADO.";

                    List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                    string strsql =  MontarCarta(Request.QueryString["id_documento"].ToString(), "Pedido Cancelado: " + dt.Rows[0]["NUMERO"].ToString() + " Por: " + ILusuario[0].UsuarioNome + " em: " + DateTime.Now.ToString(), dt); //"<html> ";
                    //string strsql = ; //"<html> ";
                    //strsql += " <body > ";
                    //strsql += " <p><strong><font face='Verdana, Arial, Helvetica, sans-serif'>Pedido Cancelado </font></strong> </p> ";
                    //strsql += " <p><font face='Verdana, Arial, Helvetica, sans-serif'><strong>Número: " + dt.Rows[0]["NUMERO"] + " </strong></font> </p> ";
                    //strsql += " <p>&nbsp;</p> ";
                    //strsql += " <p><font face='Verdana, Arial, Helvetica, sans-serif'><strong>Este pedido foi  cancelado por <font color='#FF0000'>" + ILusuario[0].UsuarioNome + "</font> em <font color='#FF0000'>" + DateTime.Now.ToString() + "</font></strong></font></p> ";
                    //strsql += " </body> ";
                    //strsql += " </html> ";

                    Sistran.Library.EnviarEmails.EnviarEmail(dt.Rows[0]["ENDERECO"].ToString(), ConfigurationSettings.AppSettings["emailPedido"].ToString(), "AVISO: CANCELAMENTO DO PEDIDO DE NUMERO " + dt.Rows[0]["NUMERO"].ToString(), strsql, ConfigurationSettings.AppSettings["smtp"].ToString(), ConfigurationSettings.AppSettings["senhasmtp"].ToString());

                    DataTable dtAprovadores = new SistranBLL.Cadastro.CadastroContato().ListarEmailsAprovadoresPedidos();

                    foreach (DataRow item in dtAprovadores.Rows)
                    {
                        Sistran.Library.EnviarEmails.EnviarEmail(item["ENDERECO"].ToString(), ConfigurationSettings.AppSettings["emailPedido"].ToString(), "AVISO: CANCELAMENTO DO PEDIDO DE NUMERO " + dt.Rows[0]["NUMERO"].ToString(), strsql, ConfigurationSettings.AppSettings["smtp"].ToString(), ConfigurationSettings.AppSettings["senhasmtp"].ToString());                        
                    }
                }
                else
                {
                    Label1.Text = "ESTE PEDIDO JA FOI AUTORIZADO OU ESTA CANCELADO.";
                }
            }
        }
        else
        {
            Label1.Text = "ESTE PEDIDO JA FOI AUTORIZADO OU ESTA CANCELADO.";
        }
    }

    private bool Autorizado()
    {
        SistranBLL.Pedido op = new Pedido();
        op.AlterarSituacaoPedido("ENVIAR PARA FATURAMENTO", Request.QueryString["id_documento"]);
        op.IncluirOcorrencia(Request.QueryString["id_documento"], "PEDIDO AUTORIZADO");
        return true; 		
    }


    private string MontarCarta(string idDocumento, string texto, DataTable DadosEmailPedido)
    {

        DataTable dtDoc = new SistranBLL.Pedido().ConsultarByDocumento(Convert.ToInt32(idDocumento));
        string m = " <html>  <head></head> <body >  ";

        m += " <table border='0' cellpadding='1' cellspacing='0' width='95%'>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='4'> <p align='center'><font size='4' face='Verdana'><strong>";

        m += texto + "</strong></font></p></td>  ";
        m += " </tr>  ";
        m += " <tr>   ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>Efetuado Por:</strong></font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>E-Mail:</strong></font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + DadosEmailPedido.Rows[0]["Nome"] + "</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + DadosEmailPedido.Rows[0]["ENDERECO"] + "</font></td>  ";
        m += " </tr>  ";
        m += " <tr>   ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += "  <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='4'><font size='2' face='Verdana'><strong>Remetente:</strong></font><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='4'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["RAZAONOMEREMETENTE"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr>   ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>Destinatário:</strong></font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>Fantasia:</strong></font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["RAZAONOMEDESTINATARIO"].ToString() + "</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["FANTASIAAPELIDODESTINATARIO"].ToString() + "</font></td>  ";
        m += " </tr>  ";
        m += " <tr>   ";
        m += " <td width='1%'><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>CNPJ:</strong></font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>I.E:</strong></font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["CNPJCPFDESTINATARIO"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["IEDEST"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr>   ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>Endereço:</strong></font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>Cidade</strong>:</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["ENDERECODESTINATARIO"].ToString() + "," + dtDoc.Rows[0]["NUMERODESTINATARIO"].ToString() + "," + dtDoc.Rows[0]["COMPLEMENTODESTINATARIO"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>" + dtDoc.Rows[0]["CIDADEDEST"].ToString() + "-" + dtDoc.Rows[0]["ESTADODEST"].ToString() + "</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td><font size='2' face='Verdana'><strong>Cep:</strong></font></td>  ";
        m += " <td colspan='3'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["CEPDEST"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " </table>  ";
        m += " <p><font face='Verdana'><strong>Itens do Pedido:</strong></font></p>  ";
        m += " <table border=0 cellpadding='1' cellspacing='1' width='95%'>  ";
        m += " <tr bgcolor='#999999' >   ";
        m += " <td><font size='2' face='Verdana'><strong>Código</strong></font></td>  ";
        m += " <td><font size='2' face='Verdana'><strong>Descrição</strong></font></td>  ";
        m += " <td><font size='2' face='Verdana'><strong>Divisão</strong></font></td>  ";
        m += " <td>   ";
        m += " <div align='right'><font size='2' face='Verdana'><strong>Quantidade</strong></font></div></td>  ";
        m += " <td>   ";
        m += " <div align='right'><font size='2' face='Verdana'><strong>Valor Unitario</strong></font></div></td>  ";
        m += " <td>   ";
        m += " <div align='right'><font size='2' face='Verdana'><strong>Valor Total</strong></font></div></td>  ";
        m += " </tr>  ";

        decimal x = Convert.ToDecimal(0);
        DataTable Lcarrinho = new Pedido().ItensByDocumento(idDocumento);

        foreach (DataRow item in Lcarrinho.Rows)
        {
            m += " <tr>   ";
            m += " <td><font size='2' face='Verdana'>" + item["IDPRODUTOCLIENTE"].ToString() + "</font></td>  ";
            m += " <td><font size='2' face='Verdana'>" + item["DESCRICAO"].ToString() + "</font></td>  ";
            m += " <td><font size='2' face='Verdana'>" + item["NOME"].ToString() + "</font></td>  ";
            m += " <td><div align='right'><font size='2' face='Verdana'>" + Convert.ToDecimal(item["QUANTIDADE"]).ToString("#0.00") + "</font></div></td>  ";
            m += " <td><div align='right'><font size='2' face='Verdana'>" + Convert.ToDecimal(item["VALORUNITARIO"]).ToString("#0.00") + "</font></div></td>  ";
            m += " <td><div align='right'><font size='2' face='Verdana'>" + (Convert.ToDecimal(item["QUANTIDADE"]) * Convert.ToDecimal(item["VALORUNITARIO"])).ToString() + "</font></div></td>  ";
            m += " </tr>  ";

            x += (Convert.ToDecimal(item["QUANTIDADE"]) * Convert.ToDecimal(item["VALORUNITARIO"]));
        }

        m += "  <tr>   ";
        m += "  <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += "  <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += "   <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><div align='right'><font size='2' face='Verdana'><strong>Total:</strong></font></div></td>  ";
        m += " <td > <div align='right'><font size='2' face='Verdana'><strong>" + x.ToString("#0.00") + "</strong></font></div></td>  ";
        m += " </tr>  ";
        m += " </table>  ";
        m += " </body> </html>   ";

              return m;
    }
}