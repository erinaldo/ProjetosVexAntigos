﻿using System;
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

public partial class DesempenhoEntregaCidade : System.Web.UI.Page
{
    #region Events

    public int intervalo;

    protected void Page_Load(object sender, EventArgs e)
    {       
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            intervalo = FuncoesGerais.RetornarIntervaloDiasPesqusa();
            //intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);

           tbTotais.Visible = false;
            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                btnGerarReport.Visible = false;
                btnPDF.Visible = false;
                btnGerarReport.Attributes.Add("onClick", "window.open('frmRptDesempenhoCidade.aspx?tipo=TELA&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela2', 'yes')");
                btnPDF.Attributes.Add("onClick", "window.open('frmRptDesempenhoCidade.aspx?tipo=PDF&tit=" + Server.UrlEncode(Request.QueryString["opc"].ToString()) + "', 'NovaJanela22', 'yes')");

          
                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
            } 
            
            if( btnGerarReport.Visible == true)
                MontarTable();

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

    #endregion

    #region Methods

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

       

        DataTable dt = NotasFiscais.ListarDesempenhoEntregaCidade(Convert.ToDateTime(txtI.Text), Convert.ToDateTime(txtF.Text), Sistran.Library.FuncoesUteis.retornarClientesResumoFilial(false),DropDownList1.SelectedValue, DropDownList2.SelectedValue, Session["Conn"].ToString(), chkIncluirTransportadora.Checked);
        PlaceHolder1.Controls.Clear();

      

        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 width='200px'>"));
        if (dt.Rows.Count > 0)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>Cidade"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>Mapa"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>Filial"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            if (chkIncluirTransportadora.Checked)
            {
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>Transportadora"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            }

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>N.F. Entregue"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>Transit Time"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                        
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>% N.F. Entregue"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>N.F. Acumulado"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>% Acumulado"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>N.F. Não Entregue"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            total = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", ""));
            decimal itac = Convert.ToDecimal("0.00");
            decimal it2 = Convert.ToDecimal("0.00");
            decimal calc = Convert.ToDecimal("0.00");

            foreach (DataRow item in dt.Rows)
            {

                decimal it = Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]);

                if (total > 0)
                    calc = (it / total) * 100;

                itac += calc;
                it2 += it;

                PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["DestinatarioCidade"].ToString() + "-" + item["DestinatarioUf"].ToString())) ;
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap  style='font-size:7pt;height:10px' align='center'><center>"));
                PlaceHolder1.Controls.Add(CriarLink(item["DestinatarioCidade"].ToString(), item["DestinatarioUf"].ToString()));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</center></td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["FILIAL"].ToString() ));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                if (chkIncluirTransportadora.Checked)
                {

                    PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + item["FantasiaApelido"].ToString()));
                    PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
                }

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["NOTASFISCAIS_ENTREGUE"]).ToString("#0")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["Media"]).ToString("#0.00")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + calc.ToString("#0.00").ToString() + "%"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='right' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(it2).ToString("#0")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + itac.ToString("#0.00").ToString() + "%"));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

                PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap align='left' style='font-size:7pt;height:10px'>" + Convert.ToDecimal(item["NOTASFISCAIS_NAO_ENTREGUE"]).ToString("#0")));
                PlaceHolder1.Controls.Add(new LiteralControl(@"</td>")); 

                PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


            }

            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>TOTAL"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"+ total.ToString("#0")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_NAO_ENTREGUE)", "")).ToString("#0")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        }
        else
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));

        if (dt.Rows.Count > 0)
        {
            btnGerarReport.Visible = true;
            btnPDF.Visible = true;
            Session["dt"] = dt;

            tbTotais.Visible = true;
            Label3.Text = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_NAO_ENTREGUE)", "")).ToString("#0");
            Label2.Text = Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", "")).ToString("#0");
            Label1.Text = (Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_NAO_ENTREGUE)", "")) + Convert.ToDecimal(dt.Compute("SUM(NOTASFISCAIS_ENTREGUE)", ""))).ToString();
        }
       
    }

    private ImageButton CriarLink(string p, string p_2)
    {
        ImageButton m = new ImageButton();

        m.Attributes.Add("onClick", "javascript:window.open('popMapaCidade.aspx?tipo=" + Guid.NewGuid().ToString() + "&cidade=" + Server.UrlEncode(p) + "&Uf=" + Server.UrlEncode(p_2) + "','janela1','width=800,height=600,scrollbars=yes, ,resizable=1 ')");
        m.ImageUrl = "Imagens/GOOLGLE.jpg";
        m.ToolTip = "Clique para ver onde se localiza a cidade: " + p ;
        m.Height = 20;
        return m;
    }
    
    protected void btnGerarReport_Click(object sender, EventArgs e)
    {
        MontarTable();
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        MontarTable();
    }
}
    #endregion