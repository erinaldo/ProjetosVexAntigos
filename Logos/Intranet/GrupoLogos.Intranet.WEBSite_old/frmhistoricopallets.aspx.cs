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

public partial class frmhistoricopallets : System.Web.UI.Page
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
                txtF.Text = DataConf[1];

            }
            else
            {
                MontarTableClientes();
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
        //Session["dt"] = null;
    }

    private void MontarTableClientes()
    {
        string strsql = "";
        strsql += "  SELECT  ";
        strsql += " DISTINCT  ";
        strsql += " MVC.IDCLIENTE, ";
        strsql += " CNPJCPF,  ";
        strsql += " RAZAOSOCIALNOME, ";
        strsql += " CIDCLI.NOME CIDADE, ";
        strsql += " ESTCLI.UF , '' FILIAL";
        //strsql += " ( ";
        //strsql += " SELECT TOP 1 FI.NOME FROM PRODUTOCLIENTE PC ";
        //strsql += " INNER JOIN ESTOQUE E ON E.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE ";
        //strsql += " INNER JOIN FILIAL FI ON FI.IDFILIAL = E.IDFILIAL ";
        //strsql += " WHERE PC.IDCLIENTE = MVC.IDCLIENTE AND PC.ATIVO='SIM' ";
        //strsql += " ) FILIAL ";

        strsql += " FROM  MOVIMENTACAOCLIENTE MVC   ";
        strsql += " INNER JOIN CADASTRO CDCLI ON CDCLI.IDCADASTRO = MVC.IDCLIENTE  ";
        strsql += " LEFT JOIN CIDADE CIDCLI ON CIDCLI.IDCIDADE = CDCLI.IDCIDADE ";
        strsql += " LEFT JOIN ESTADO ESTCLI ON ESTCLI.IDESTADO = CIDCLI.IDESTADO ";
        strsql += " where data between  convert(datetime, '" + txtI.Text + "',103)   and   convert(datetime, '" + txtF.Text + "',103) ";

        //strsql += " and CNPJCPF in ( ";

        //strsql += "'45.985.371/0001-08',";
        //strsql += "'43.112.531/0001-89',";
        //strsql += "'06.034.119/0001-61',";
        //strsql += "'12.867.391/0003-97',";
        //strsql += "'01.027.058/0001-91',";
        //strsql += "'89.637.490/0001-45',";
        //strsql += "'67.506.105/0001-98',";
        //strsql += "'67.506.105/0015-93',";
        //strsql += "'60.409.075/0305-74',";
        //strsql += "'60.409.075/0111-97',";
        //strsql += "'60.409.075/0001-52',";
        //strsql += "'60.409.075/0162-37',";
        //strsql += "'08.544.703/0001-92',";
        //strsql += "'02.333.707/0001-45',";
        //strsql += "'43.447.044/0004-10',";
        //strsql += "'56.992.951/0010-30',";
        //strsql += "'54.360.656/0001-44',";
        //strsql += "'53.249.017/0003-05',";
        //strsql += "'59.109.165/0001-49',";
        //strsql += "'48.539.407/0001-18',";
        //strsql += "'84.046.101/0371-94',";
        //strsql += "'19.900.000/0024-62',";
        //strsql += "'05.207.076/0001-06',";
        //strsql += "'62.258.884/0001-36',";
        //strsql += "'14.998.371/0053-40',";
        //strsql += "'06.020.318/0001-10',";
        //strsql += "'29.737.368/0014-33',";
        //strsql += "'71.673.990/0001-77',";
        //strsql += "'12.484.782/0001-60',";
        //strsql += "'43.996.693/0001-27',";
        //strsql += "'33.252.156/0104-24',";
        //strsql += "'42.274.696/0025-61',";
        //strsql += "'43.816.719/0005-31',";
        //strsql += "'74.299.660/0001-51',";
        //strsql += "'00.593.542/0001-15',";
        //strsql += "'01.625.223/0002-98',";
        //strsql += "'44.127.355/0004-64',";
        //strsql += "'75.104.422/0001-06',";
        //strsql += "'07.558.313/0001-09',";
        //strsql += "'03.234.748/0002-28',";
        //strsql += "'70.940.994/0052-51',";
        //strsql += "'63.035.117/0002-01',";
        //strsql += "'00.063.960/0027-30',";
        //strsql += "'05.074.615/0001-86',";
        //strsql += "'61.192.522/0004-70',";
        //strsql += "'07.716.156/0001-12',";
        //strsql += "'04.008.977/0001-06',";
        //strsql += "'60.333.267/0006-37',";
        //strsql += "'58.273.491/0001-24',";
        //strsql += "'03.585.974/0009-20',";
        //strsql += "'60.934.551/0004-05',";
        //strsql += "'60.934.551/0001-54',";
        //strsql += "'63.639.868/0001-56',";
        //strsql += "'47.080.619/0012-70',";
        //strsql += "'09.663.065/0001-91',";
        //strsql += "'02.340.424/0001-20',";
        //strsql += "'35.402.759/0001-85',";
        //strsql += "'79.788.766/0001-32',";
        //strsql += "'50.140.540/0001-49',";
        //strsql += "'72.840.002/0001-08',";
        //strsql += "'09.398.635/0001-63',";
        //strsql += "'66.616.970/0001-24',";
        //strsql += "'16.900.302/0001-56',";
        //strsql += "'68.929.413/0001-99',";
        //strsql += "'90.049.289/0003-98',";
        //strsql += "'90.049.289/0005-50',";
        //strsql += "'55.630.289/0001-14',";
        //strsql += "'61.297.784/0001-56',";
        //strsql += "'61.297.784/0003-18',";
        //strsql += "'66.079.609/0001-06',";
        //strsql += "'67.506.105/0014-02',";
        //strsql += "'08.081.791/0001-33',";
        //strsql += "'12.045.593/0001-91',";
        //strsql += "'01.306.088/0001-37',";
        //strsql += "'01.835.143/0001-86',";
        //strsql += "'71.615.330/0001-30',";
        //strsql += "'05.083.799/0001-40',";
        //strsql += "'04.195.658/0001-57',";
        //strsql += "'00.740.600/0001-96',";
        //strsql += "'02.214.604/0002-47',";
        //strsql += "'00.063.960/0122-98'";
        //strsql += ")";

        strsql += " ORDER BY 3";

        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

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

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap='nowrap' align='left' width='1%'>" + "&nbsp;&nbsp;"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap='nowrap' align='right' width='1%'>Código do Cliente" + "&nbsp;&nbsp;"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap='nowrap' align='left' width='1%'>Cnpj Cliente"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap='nowrap' align='left' width='99%'>Cliente"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' nowrap='nowrap' align='left' width='1%'>cidade/uf"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));



        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap='nowrap' width='1%'>" + dt.Rows[i]["filial"].ToString() + "&nbsp;&nbsp;"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap='nowrap' width='1%'>"));

            PlaceHolder1.Controls.Add(criarLinkButtonCliente(dt.Rows[i]["IDCLIENTE"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"&nbsp;&nbsp;</td>"));



            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap='nowrap' width='1%'>" + dt.Rows[i]["CNPJCPF"].ToString() + "&nbsp;&nbsp;"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap='nowrap' width='99%'>" + dt.Rows[i]["RAZAOSOCIALNOME"].ToString()));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap='nowrap' width='1%'>" + dt.Rows[i]["cidade"].ToString() + " / " + dt.Rows[i]["uf"].ToString() + "&nbsp;&nbsp;"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
        Session["dtEx"] = dt;

    }

    private string criarDVCliente(string idCliente)
    {
        string m = "";
        PlaceHolder1.Controls.Add(new LiteralControl(@"<center><div id='dv" + idCliente + "' runat='server'  style='font-size:9px;cursor:Hand;background-image:url(Images/seta.jpg); height:12px; width:14px;' OnClick=ExpandirAll('" + "" + "','" + "" + "');>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</div></center>"));
        m = "";

        return m;
    }
     

    private LinkButton criarLinkButtonCliente(string IdCliente)
    {
        LinkButton bot = new LinkButton();
        bot.BorderStyle = BorderStyle.None;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        bot.Click += new EventHandler(btnMostrarDados_Click);
        bot.ID = IdCliente;
        bot.Text = IdCliente;
        bot.CssClass = "link";
        bot.ToolTip = "Clique aqui para ver o detalhe";
        bot.CommandArgument = IdCliente;
        return bot;
    }


    private void btnMostrarDados_Click(object sender, System.EventArgs e)
    {
        LinkButton lk = (LinkButton)sender;
        dvCliente.Visible = true;
        MontarTableDados(lk.Text);
        btnFoco.Focus();
        dvCliente.Focus();
    }

    #endregion

    #region Methods

    protected void MontarTableDados(string idCliente)
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];        

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


        phDados.Controls.Clear();

        string strsql = "";
        strsql += " SELECT distinct ";
        strsql += " MVCC.*, ";
        strsql += " CDCLI.CNPJCPF, ";
        strsql += " CDCLI.RAZAOSOCIALNOME ";
        strsql += " FROM MOVIMENTACAOCLIENTECONSOLIDADO  MVCC ";
        strsql += " INNER JOIN CADASTRO CDCLI ON CDCLI.IDCADASTRO = MVCC.IDCLIENTE ";
        strsql += " WHERE CONVERT(DATETIME, DATA, 103) BETWEEN CONVERT(DATE,'" + Convert.ToDateTime(txtI.Text) + "', 103) AND CONVERT(DATE,'" + Convert.ToDateTime(txtF.Text) + "', 103) ";
        strsql += " AND  MVCC.IDCLIENTE IN (" + idCliente + ")";
        strsql += " ORDER BY DATA";


        Session["idCliente_pallets"] = idCliente;

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataSetWS(strsql, HttpContext.Current.Session["ConnLogin"].ToString()).Tables[0];

        phDados.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=1 celpanding=1 width='200px'>"));
        if (dt.Rows.Count > 0)
        {

            phDados.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>Data"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>POSIÇÕES PALLETS ARMAZENAGEM"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'> UA´S ENTRADA **"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'> UA´s SAÍDAS **"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>M3 ARMAZENAGEM"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>M3 ENTRADA"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>M3 SAÍDAS"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow item in dt.Rows)
            {

                phDados.Controls.Add(new LiteralControl(@"<tr>"));
                lblDivCliente.Text = item["CNPJCPF"].ToString().Trim() + " - " + item["RAZAOSOCIALNOME"].ToString().Trim();

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpCenter' nowrap=nowrap style='font-size:7pt;height:10px'><a href='frmhistoricopalletsDetalhe.aspx?data=" + Convert.ToDateTime(item["Data"]).ToString("dd/MM/yyyy") + "&idcliente=" + (item["idcliente"]).ToString() + "' target='_blank' class='link'>" + Convert.ToDateTime(item["Data"]).ToString("dd/MM/yyyy") + "</a>"));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + item["PalletsArmazenagem"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + item["PalletsEntrada"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + item["PalletsSaida"].ToString()));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + decimal.Parse(item["M3Armazenagem"].ToString()).ToString("#,0.000")));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + decimal.Parse(item["M3Entrada"].ToString()).ToString("#,0.000")));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

                phDados.Controls.Add(new LiteralControl(@"<td class='tdpR' nowrap=nowrap style='font-size:7pt;height:10px'>" + decimal.Parse(item["M3Saida"].ToString()).ToString("#,0.000")));
                phDados.Controls.Add(new LiteralControl(@"</td>"));

            }

            phDados.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>"));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>(Média) " + dt.Compute("AVG(PalletsArmazenagem)", "")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + dt.Compute("SUM(PalletsEntrada)", "")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>" + dt.Compute("SUM(PalletsSaida)", "")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>(Média) " + decimal.Parse(dt.Compute("AVG(M3Armazenagem)", "").ToString()).ToString("#,0.000")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' nowrap=nowrap style='font-size:7pt;'>" + decimal.Parse(dt.Compute("SUM(M3Entrada)", "").ToString()).ToString("#,0.000")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));

            phDados.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='right' style='font-size:7pt;'>" + decimal.Parse(dt.Compute("SUM(M3Saida)", "").ToString()).ToString("#,0.000")));
            phDados.Controls.Add(new LiteralControl(@"</td>"));
            phDados.Controls.Add(new LiteralControl(@"</tr>"));
        }
        else
        {
            phDados.Controls.Add(new LiteralControl(@"<tr>"));
            phDados.Controls.Add(new LiteralControl(@"<td class='tdp'>Nenhum item encontrado."));
            phDados.Controls.Add(new LiteralControl(@"</td>"));
            phDados.Controls.Add(new LiteralControl(@"</tr>"));
        }

        phDados.Controls.Add(new LiteralControl(@"</table>"));

    }

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