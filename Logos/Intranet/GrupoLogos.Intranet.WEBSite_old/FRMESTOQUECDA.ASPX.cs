using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using ChartDirector;

public partial class FRMESTOQUECDA : System.Web.UI.Page
{
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            MontarTableClientes();


            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            //Session["DataConf"] = txtI.Text + "|" + txtF.Text;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script> alert('" + ex.Message.Replace("'", "´") + "'); </script>");
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Session["dt"] = null;
    }

    private void MontarTableClientes()
    {
        string strsql = "";
        strsql += " SELECT DISTINCT";
        strsql += " CAD.IDCADASTRO IDCLIENTE,  ";
        strsql += " CAD.RAZAOSOCIALNOME , ";
        strsql += " CAD.CNPJCPF ";
        strsql += " FROM CADASTROCDA CDA ";
        strsql += " INNER JOIN CADASTROCDAUSUARIOCLIENTEDIVISAO CDAUSU ON CDAUSU.IDCADASTROCDA = CDA.IDCADASTROCDA ";
        strsql += " INNER JOIN USUARIOCLIENTEDIVISAO UCD ON UCD.IDUSUARIOCLIENTEDIVISAO = CDAUSU.IDUSUARIOCLIENTEDIVISAO ";
        strsql += " INNER JOIN CADASTRO CADCDA ON CADCDA.IDCADASTRO = CDA.IDCADASTROCDA ";
        strsql += " INNER JOIN USUARIOCLIENTE UC ON UC.IDUSUARIOCLIENTE = UCD.IDUSUARIOCLIENTE ";
        strsql += " INNER JOIN CLIENTE CLI ON CLI.IDCLIENTE = UC.IDCLIENTE ";
        strsql += " INNER JOIN CLIENTEDIVISAO CD ON CD.IDCLIENTEDIVISAO = UCD.IDCLIENTEDIVISAO AND CD.IDCLIENTE = CLI.IDCLIENTE ";
        strsql += " INNER JOIN CADASTRO CAD ON CAD.IDCADASTRO = CLI.IDCLIENTE ";
        strsql += " INNER JOIN ESTOQUEDIVISAO ED ON ED.IDCLIENTEDIVISAO = CD.IDCLIENTEDIVISAO  ";
        strsql += " INNER JOIN ESTOQUE ES ON ES.IDESTOQUE = ED.IDESTOQUE ";
        strsql += " INNER JOIN CLIENTEDIVISAO CDP ON CDP.IDCLIENTEDIVISAO = CD.IDPARENTE ";
        strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = ES.IDPRODUTOCLIENTE ";
        strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
        strsql += " INNER JOIN PRODUTO PR ON PR.IDPRODUTO = PE.IDPRODUTO ";
        strsql += " LEFT JOIN GRUPODEPRODUTO GP ON GP.IDGRUPODEPRODUTO = PC.IDGRUPODEPRODUTO ";
        strsql += " WHERE  ";
        strsql += " ED.SALDOBASEEXTERNA > 0 ";
        strsql += " ORDER BY CAD.RAZAOSOCIALNOME ";


        DataTable dt = new DataTable();

        dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, HttpContext.Current.Session["ConnLogin"].ToString()).Tables[0];
        Session["dtEx"] = dt;


        if (dt.Rows.Count == 0)
        {
            dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, HttpContext.Current.Session["ConnLogin"].ToString()).Tables[0];
            Session["dtEx"] = dt;
        }

        PlaceHolder1.Controls.Clear();

        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap='nowrap' align='right' width='1%'>Código do Cliente" + "&nbsp;&nbsp;"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap='nowrap' align='left' width='1%'>Cnpj Cliente"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap='nowrap' align='left' width='99%'>Cliente"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap='nowrap' width='1%'>"));
            PlaceHolder1.Controls.Add(criarLinkButtonCliente(dt.Rows[i]["IDCLIENTE"].ToString(), dt.Rows[i]["RAZAOSOCIALNOME"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"&nbsp;&nbsp;</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap='nowrap' width='1%'>" + dt.Rows[i]["CNPJCPF"].ToString() + "&nbsp;&nbsp;"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap='nowrap' width='99%'>" + dt.Rows[i]["RAZAOSOCIALNOME"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
    }

    private LinkButton criarLinkButtonCliente(string IdCliente, string nome)
    {
        LinkButton bot = new LinkButton();
        bot.BorderStyle = BorderStyle.None;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        //bot.Click += new EventHandler(btnMostrarDados_Click);
        bot.Attributes.Add("onclick", "javascript:window.open('frmEstoqueCdaDetalhe.aspx?idCliente=" + IdCliente + "&nome=" + nome + "')");
        bot.ID = IdCliente;
        bot.Text = IdCliente;
        bot.CssClass = "link";
        bot.ToolTip = "Clique aqui para ver o detalhe";
        bot.CommandArgument = IdCliente;
        return bot;
    }

    #endregion
    
}