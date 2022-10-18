using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
//using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using ChartDirector;

public partial class frmAcompOnLine : System.Web.UI.Page
{
    private void CarregarCboFilial()
    {
        cboFilial.DataSource = new SistranDAO.Filial().ListarTodasFiliais(Session["Conn"].ToString());
        cboFilial.DataValueField = "VALOR";
        cboFilial.DataTextField = "NOME";
        cboFilial.DataBind();

        cboFilial.Items.Insert(0, new ListItem("## SELECIONE ##", ""));
     
    }

    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = FuncoesGerais.RetornarIntervaloDiasPesqusa();           

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath),  Session["Conn"].ToString());
                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
                CarregarCboFilial();
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Session["DataConf"] = txtI.Text + "|" + txtF.Text;
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "tt", "<script> alert('" + ex.Message.Replace("'", "´") + "'); </script>");

        }
    }
      

    protected void Button1_Click(object sender, EventArgs e)
    {
        MontarTable();
    }

    protected void MontarTable()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

        decimal total = Convert.ToDecimal(0);
        TimeSpan ts = Convert.ToDateTime(txtF.Text) - Convert.ToDateTime(txtI.Text);
        if (Convert.ToDateTime(txtF.Text) < Convert.ToDateTime(txtI.Text))
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('A data inicial não pode ser maior que a data final.')", true);
            return;
        }

        if (ts.Days > intervalo)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas não pode ultrapassar " + intervalo.ToString() + " dias.')", true);
            return;
        }
               

        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=2 celpanding=4>"));        
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='LEFT' style='font-size:7pt; nowrap='nowrap'>DESCRIÇÃO&nbsp;&nbsp;&nbsp;"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='LEFT' style='font-size:7pt; nowrap='nowrap'>UNIDADE"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;' nowrap=nowrap align='right'>VALOR"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        //PALETES RECEBIDOS
        DataTable dt = new SistranBLL.Faturamento().PalletsRecebidos(txtI.Text, txtF.Text);
        Session["dtPalletsRecebidos"] = dt;

        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>PALETES RECEBIDOS"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>PALETES"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'><a href='detFaturamentoPaletesRecebidos.aspx' class='link' target='_blank'>" + dt.Compute("COUNT(IDDOCUMENTO)", "") + "</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));



        //LINHAS EXPEDIDAS
        DataTable dtLinhasExpedidas = new SistranBLL.Faturamento().LinhasExpedidas(txtI.Text, txtF.Text);
        Session["dtLinhasExpedidas"] = dtLinhasExpedidas;
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>LINHAS EXPEDIDAS"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>PICO DO QUINZ"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'><a href='detFaturamentoLinhasExpedidas.aspx' class='link' target='_blank'>" + dtLinhasExpedidas.Compute("COUNT(IDDOCUMENTO)", "") + "</a>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
    }
    
}