﻿using System;
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

public partial class frmestoquecda_data : System.Web.UI.Page
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
            intervalo = 30;

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                //txtF.Text = DataConf[1];
            }
            else
            {
                MontarTableClientes();
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Session["DataConf"] = txtI.Text + "|" + txtI.Text;
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
        strsql += " SELECT DISTINCT  ";
        strsql += " CADCLI.RAZAOSOCIALNOME , ";
        strsql += " CADCLI.CNPJCPF, ";
        strsql += " CADCLI.Idcadastro IdCliente ";
        strsql += " FROM  MOVIMENTACAOCLIENTE MC ";
        strsql += " LEFT JOIN CLIENTEDIVISAO CVPROMOTOR ON CVPROMOTOR.IDCLIENTEDIVISAO = MC.IDDIVISAOPROMOTOR ";
        strsql += " INNER JOIN CLIENTEDIVISAO CVDIVISAO ON CVDIVISAO.IDCLIENTEDIVISAO = MC.IDDIVISAOCDA ";
        strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = MC.IDPRODUTOCLIENTE ";
        strsql += " INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = MC.IDCLIENTE ";
        strsql += " WHERE IDCDA IS NOT NULL AND DATA = CONVERT(DATETIME, '" + txtI.Text + "',103)  ";
        strsql += " AND SALDOCDA>0 ";
        strsql += " ORDER BY 1 ";

        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];



        DataTable dt = new DataTable();

        if (Session["dtEx"] == null)
        {
            dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, HttpContext.Current.Session["ConnLogin"].ToString()).Tables[0];
            Session["dtEx"] = dt;
        }
        else
            dt = (DataTable)Session["dtEx"];


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
        Session["dtEx"] = dt;

    }
        
    

    private LinkButton criarLinkButtonCliente(string IdCliente, string nome)
    {
        LinkButton bot = new LinkButton();
        bot.BorderStyle = BorderStyle.None;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        bot.Attributes.Add("onclick", "javascript:window.open('frmestoquecda_data_detalhe.aspx?idCliente=" + IdCliente + "&nome=" + nome + "')");
        bot.ID = IdCliente;
        bot.Text = IdCliente;
        bot.CssClass = "link";
        bot.ToolTip = "Clique aqui para ver o detalhe";
        bot.CommandArgument = IdCliente;
        return bot;
    }


        #endregion

    #region Methods

    //protected void MontarTableDadosClick(string idCliente)
    //{
    //    List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
    //    //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

    //    decimal total = Convert.ToDecimal(0);
    //    TimeSpan ts = Convert.ToDateTime(txtF.Text) - Convert.ToDateTime(txtI.Text);
    //    if (Convert.ToDateTime(txtF.Text) < Convert.ToDateTime(txtI.Text))
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('A data inicial não pode ser maior que a data final.')", true);
    //        return;
    //    }

    //    if (ts.Days > intervalo)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('O intervalo entre datas não pode ultrapassar " + intervalo.ToString() + " dias.')", true);
    //        return;
    //    }


    //    phDados.Controls.Clear();

    //    string strsql = "";
    //    strsql += " SELECT distinct ";
    //    strsql += " MVCC.*, ";
    //    strsql += " CDCLI.CNPJCPF, ";
    //    strsql += " CDCLI.RAZAOSOCIALNOME ";
    //    strsql += " FROM MOVIMENTACAOCLIENTECONSOLIDADO  MVCC ";
    //    strsql += " INNER JOIN CADASTRO CDCLI ON CDCLI.IDCADASTRO = MVCC.IDCLIENTE ";
    //    strsql += " WHERE CONVERT(DATETIME, DATA, 103) BETWEEN CONVERT(DATE,'" + Convert.ToDateTime(txtI.Text) + "', 103) AND CONVERT(DATE,'" + Convert.ToDateTime(txtF.Text) + "', 103) ";
    //    strsql += " AND  MVCC.IDCLIENTE IN (" + idCliente + ")";
    //    strsql += " ORDER BY DATA";


    //    Session["idCliente_pallets"] = idCliente;

    //    DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, HttpContext.Current.Session["ConnLogin"].ToString()).Tables[0];

    //    phDados.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 width='200px'>"));
    //    if (dt.Rows.Count > 0)
    //    {

    //        phDados.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>Data"));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>POSIÇÕES PALLETS ARMAZENAGEM"));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'> UA´S ENTRADA **"));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'> UA´s SAÍDAS **"));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>M3 ARMAZENAGEM"));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>M3 ENTRADA"));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>M3 SAÍDAS"));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"</tr>"));

    //        foreach (DataRow item in dt.Rows)
    //        {

    //            phDados.Controls.Add(new LiteralControl(@"<tr>"));
    //            lblDivCliente.Text = item["CNPJCPF"].ToString().Trim() + " - " + item["RAZAOSOCIALNOME"].ToString().Trim();

    //            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap style='font-size:7pt;height:10px'><a href='frmhistoricopalletsDetalhe.aspx?data=" + Convert.ToDateTime(item["Data"]).ToString("dd/MM/yyyy") + "&idcliente=" + (item["idcliente"]).ToString() + "' target='_blank' class='link'>" + Convert.ToDateTime(item["Data"]).ToString("dd/MM/yyyy") + "</a>"));
    //            phDados.Controls.Add(new LiteralControl(@"</td>"));

    //            phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + item["PalletsArmazenagem"].ToString()));
    //            phDados.Controls.Add(new LiteralControl(@"</td>"));

    //            phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + item["PalletsEntrada"].ToString()));
    //            phDados.Controls.Add(new LiteralControl(@"</td>"));

    //            phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + item["PalletsSaida"].ToString()));
    //            phDados.Controls.Add(new LiteralControl(@"</td>"));

    //            phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + decimal.Parse(item["M3Armazenagem"].ToString()).ToString("#,0.000")));
    //            phDados.Controls.Add(new LiteralControl(@"</td>"));

    //            phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + decimal.Parse(item["M3Entrada"].ToString()).ToString("#,0.000")));
    //            phDados.Controls.Add(new LiteralControl(@"</td>"));

    //            phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + decimal.Parse(item["M3Saida"].ToString()).ToString("#,0.000")));
    //            phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        }

    //        phDados.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>"));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>(Média) " + dt.Compute("AVG(PalletsArmazenagem)", "")));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + dt.Compute("SUM(PalletsEntrada)", "")));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>" + dt.Compute("SUM(PalletsSaida)", "")));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>(Média) " + decimal.Parse(dt.Compute("AVG(M3Armazenagem)", "").ToString()).ToString("#,0.000")));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + decimal.Parse(dt.Compute("SUM(M3Entrada)", "").ToString()).ToString("#,0.000")));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));

    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>" + decimal.Parse(dt.Compute("SUM(M3Saida)", "").ToString()).ToString("#,0.000")));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));
    //        phDados.Controls.Add(new LiteralControl(@"</tr>"));
    //    }
    //    else
    //    {
    //        phDados.Controls.Add(new LiteralControl(@"<tr>"));
    //        phDados.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
    //        phDados.Controls.Add(new LiteralControl(@"</td>"));
    //        phDados.Controls.Add(new LiteralControl(@"</tr>"));
    //    }

    //    phDados.Controls.Add(new LiteralControl(@"</table>"));

    //}

    protected void btn_Click(object sender, EventArgs e)
    {
        dvCli.Style.Add("display", "block");
    }

    public EventHandler btnMostrar_Click { get; set; }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        dvCliente.Visible = false;
        phDados.Controls.Clear();
    }
}

    #endregion