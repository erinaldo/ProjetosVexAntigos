using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using SistranBLL;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using ChartDirector;
using System.Web.UI.WebControls;

public partial class CagasDisponiveis : System.Web.UI.Page
{

    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {        
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = 365;           

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
                Session["chave"] = DateTime.Now.ToString("mmss");
             
                carregarGrid();
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            //verficarFiliaisChecadas();            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
            return;
        }
    }

    private void verficarFiliaisChecadas()
    {
        lstEscolhidos.Items.Clear();
        grd.DataSource = null;
        grd.DataBind();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            //Response.Write(Session["chave"]);

            CheckBox chkGridPedido = (CheckBox)GridView1.Rows[i].FindControl("chkGridPedido");
            CheckBox chkGridNF = (CheckBox)GridView1.Rows[i].FindControl("chkGridNF");
            Label lblIdFilial = (Label)GridView1.Rows[i].FindControl("lblIdFilial");

            if (chkGridPedido.Checked == true || chkGridNF.Checked == true)
            {
                lstEscolhidos.Items.Insert((lstEscolhidos.Items.Count), new ListItem(lblIdFilial.Text + "|" + (chkGridPedido.Checked == true ? "PedSIM" : "PedNAO") + "|" + (chkGridNF.Checked == true ? "NfSIM" : "NfNAO")));
            }

        }

        if (lstEscolhidos.Items.Count == 0)
        {
            lblPasso2.Visible = false;
            grd.Visible = false;
            lblPasso3.Visible = false;
            lblPasso3.Visible = false;
            btnGerar.Visible = false;
            return;
        }

        string swl = "";
        for (int i = 0; i < lstEscolhidos.Items.Count; i++)
        {
            swl += lstEscolhidos.Items[i].Text.Split('|')[0] + ",";
        }
        
        swl = swl.Substring(0, swl.Length - 1);

        string stsrql = "";
        stsrql += "  SELECT   DF.IDFILIAL,  F.NUMERODAFILIAL,  F.UNIDADE,  F.NOME FILIAL,  CADCLI.RAZAOSOCIALNOME, D.IDCLIENTE , ";
        stsrql += "  SUM(CASE D.TIPODEDOCUMENTO WHEN 'PEDIDO' THEN 1 ELSE 0 END) PEDIDOS,   ";
        stsrql += "  SUM(CASE D.TIPODEDOCUMENTO WHEN 'NOTA FISCAL' THEN 1 ELSE 0 END) NF    ";
        stsrql += "  FROM DOCUMENTOFILIAL DF     ";
        stsrql += "  INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = DF.IDDOCUMENTO    ";
        stsrql += "  INNER JOIN FILIAL F ON F.IDFILIAL = DF.IDFILIAL   ";
        stsrql += "  INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = D.IDCLIENTE   ";
        stsrql += "  WHERE DF.SITUACAO='AGUARDANDO EMBARQUE'    ";
        stsrql += "  AND D.TIPODEDOCUMENTO IN ('NOTA FISCAL','PEDIDO')    ";
        stsrql += "  AND DF.IDFILIAL  IN ("+swl+")  ";
        stsrql += "  GROUP BY DF.IDFILIAL, NUMERODAFILIAL, F.UNIDADE,F.NOME,CADCLI.RAZAOSOCIALNOME, D.IDCLIENTE ";
        stsrql += "  ORDER BY F.NUMERODAFILIAL, F.UNIDADE, F.NOME ,CADCLI.RAZAOSOCIALNOME   ";


        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(stsrql, Sistran.Library.Robo.Robo.RetornarStringBaseSTNNovo());

        for (int i = 0; i < lstEscolhidos.Items.Count; i++)
        {
            string[] partes = lstEscolhidos.Items[i].Text.Split('|');

            if (partes[1].Contains("PedNAO"))
            {
                for (int ii = 0; ii < dt.Rows.Count; ii++)
                {
                    if (dt.Rows[ii]["IDFILIAL"].ToString() == partes[0].ToString())
                        dt.Rows[ii]["PEDIDOS"] = 0;
                }
            }

            if (partes[2].Contains("NfNAO"))
            {
                for (int ii = 0; ii < dt.Rows.Count; ii++)
                {
                    if (dt.Rows[ii]["IDFILIAL"].ToString() == partes[0].ToString())
                        dt.Rows[ii]["NF"] = 0;
                }
            }

        }


        DataTable dtFinal = new DataTable();

        dtFinal.Columns.Add("IDFILIAL");
        dtFinal.Columns.Add("NUMERODAFILIAL");
        dtFinal.Columns.Add("UNIDADE");
        dtFinal.Columns.Add("FILIAL");
        dtFinal.Columns.Add("RAZAOSOCIALNOME");
        dtFinal.Columns.Add("PEDIDOS");
        dtFinal.Columns.Add("NF");
        dtFinal.Columns.Add("IDCLIENTE");


        DataRow[] o = dt.Select("(nf + pedidos) >0");

        for (int i = 0; i < o.Length; i++)
        {
            DataRow oi = dtFinal.NewRow();

            oi["IDFILIAL"] = o[i]["IDFILIAL"];
            oi["NUMERODAFILIAL"] = o[i]["NUMERODAFILIAL"];
            oi["UNIDADE"] = o[i]["UNIDADE"];
            oi["FILIAL"] = o[i]["FILIAL"];
            oi["RAZAOSOCIALNOME"] = o[i]["RAZAOSOCIALNOME"];
            oi["PEDIDOS"] = o[i]["PEDIDOS"];
            oi["NF"] = o[i]["NF"];
            oi["IDCLIENTE"] = o[i]["IDCLIENTE"];


            dtFinal.Rows.Add(oi);        
        }

        grd.DataSource = dtFinal;
        grd.DataBind();
        lblPasso2.Visible = true;
        grd.Visible = true;
        lblPasso3.Visible = true;
        btnGerar.Visible = true;
    }

    private void carregarGrid()
    {

        string stsrql = "";
        stsrql += " SELECT  ";
        stsrql += " DF.IDFILIAL, ";
        stsrql += " F.NUMERODAFILIAL, ";
        stsrql += " F.UNIDADE, ";
        stsrql += " F.NOME FILIAL, ";
        stsrql += " SUM(CASE D.TIPODEDOCUMENTO WHEN 'PEDIDO' THEN 1 ELSE 0 END) PEDIDOS, ";
        stsrql += " SUM(CASE D.TIPODEDOCUMENTO WHEN 'NOTA FISCAL' THEN 1 ELSE 0 END) NF ";
        stsrql += " FROM DOCUMENTOFILIAL DF  ";
        stsrql += " INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = DF.IDDOCUMENTO ";
        stsrql += " INNER JOIN FILIAL F ON F.IDFILIAL = DF.IDFILIAL ";
        stsrql += " WHERE DF.SITUACAO='AGUARDANDO EMBARQUE' ";
        stsrql += " AND D.TIPODEDOCUMENTO IN ('NOTA FISCAL','PEDIDO') ";
        stsrql += " GROUP BY DF.IDFILIAL, NUMERODAFILIAL, F.UNIDADE,F.NOME ";
        stsrql += " ORDER BY F.NUMERODAFILIAL, F.UNIDADE, F.NOME ";

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(stsrql, Sistran.Library.Robo.Robo.RetornarStringBaseSTNNovo());

        GridView1.DataSource = dt;
        GridView1.DataBind();



    }
     
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox chkGridPedido = (CheckBox)e.Row.FindControl("chkGridPedido");
        CheckBox chkGridNF = (CheckBox)e.Row.FindControl("chkGridNF");

        if (chkGridPedido!=null)
        {
            if (chkGridNF.Text == "0")
                chkGridNF.Enabled = false;

            if (chkGridPedido.Text == "0")
                chkGridPedido.Enabled = false;

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {               
        int countChecados = 0;
        string strsql = "";

        for (int i = 0; i < grd.Rows.Count; i++)
        {
            CheckBox chkGridSel = (CheckBox)grd.Rows[i].FindControl("chkGridSel");
            Label lblIdFilial = (Label)grd.Rows[i].FindControl("lblIdFilial");
            Label lblIdCliente = (Label)grd.Rows[i].FindControl("lblIdCliente");

            if (chkGridSel.Checked == true)
            {
                countChecados++;
                strsql += " delete from REGIAOPARAMETROS where chavePesquisa=" + Session["Chave"];
                strsql += " INSERT INTO REGIAOPARAMETROS VALUES (" + Session["chave"] + ", " + lblIdCliente.Text + ",  'Cliente', getdate())  ";
                strsql += " INSERT INTO REGIAOPARAMETROS VALUES (" + Session["chave"] + ", " + lblIdFilial.Text + ",  'Filial', getdate())";
            }
        }

        if (countChecados > 0)
        {            
            Sistran.Library.GetDataTables.ExecutarComandoSql(strsql, Sistran.Library.Robo.Robo.RetornarStringBaseSTNNovo());
            Response.Redirect("CargasDisponiveisDetalhe.aspx?opc=Cargas Dispiniveis Detalhe", false);
        }
    }
    protected void chkGridPedido_CheckedChanged(object sender, EventArgs e)
    {
        verficarFiliaisChecadas();
    }
    protected void chkGridNF_CheckedChanged(object sender, EventArgs e)
    {
        verficarFiliaisChecadas();
    }
}
