using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using SistranBLL;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.IO;


public partial class frmKPIMovimentacao : System.Web.UI.Page
{

    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {
        ChartDirector.WebChartViewer.OnPageInit(Page);
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = 30;
            //intervalo = Convert.ToInt32(ConfigurationSettings.AppSettings["DiasPesquisa"]);

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//               SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));


                string[] DataConf = FuncoesGerais.DataConf();
                txtI.Text = DataConf[0];
                txtF.Text = DataConf[1];
                CarregarCboFilial();
                cboCliente.Items.Add(new ListItem("SELECIONE A FILIAL", ""));
            }

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
            Session["DataConf"] = txtI.Text + "|" + txtF.Text;           

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
            return;
        }
    }

    private void CarregarCboFilial()
    {
        cboFilial.DataSource = new SistranDAO.Filial().ListarTodasFiliais(Session["Conn"].ToString());
        cboFilial.DataValueField = "VALOR";
        cboFilial.DataTextField = "NOME";
        cboFilial.DataBind();
        cboFilial.Items.Insert(0, new ListItem("SELECIONE"));

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MontarTable();
    }

    protected void MontarTable()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//       SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));

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

        string strsql = "";
        strsql += " SELECT PC.IDPRODUTOCLIENTE,PC.CODIGO, PC.DESCRICAO, PC.MARCA,COALESCE(SUM(CASE WHEN EM.IDESTOQUEOPERACAO = 1 THEN EM.QUANTIDADE ELSE NULL END),0) AS ENTRADA, COALESCE(SUM(CASE WHEN EM.IDESTOQUEOPERACAO = 2 THEN EM.QUANTIDADE ELSE NULL END),0) AS SAIDA, ES.SALDO, ";
        strsql += " (SELECT TOP 1 ISNULL(P.ALTURA, 0) FROM PRODUTOEMBALAGEM PE  INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ORDER BY PE.UNIDADEDOCLIENTE) ALTURA, ";
        strsql += " (SELECT TOP 1 ISNULL(P.LARGURA, 0) FROM PRODUTOEMBALAGEM PE  INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ORDER BY PE.UNIDADEDOCLIENTE) LARGURA, ";
        strsql += " (SELECT TOP 1 ISNULL(P.COMPRIMENTO, 0) FROM PRODUTOEMBALAGEM PE  INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ORDER BY PE.UNIDADEDOCLIENTE) COMPRIMENTO, ";
        strsql += " (SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA,0) * ISNULL(P.COMPRIMENTO,0) FROM PRODUTOEMBALAGEM PE  INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ORDER BY PE.UNIDADEDOCLIENTE) M3, ";
        strsql += " (SELECT TOP 1  ISNULL(DII.VALORUNITARIO,0)		FROM PRODUTOCLIENTE PCI   		 INNER JOIN LOTE LTI ON LTI.IDPRODUTOCLIENTE = PCI.IDPRODUTOCLIENTE  		 INNER JOIN UNIDADEDEARMAZENAGEMLOTE UALI ON UALI.IDLOTE = LTI.IDLOTE  		 INNER JOIN DOCUMENTOITEM DII ON (DII.IDDOCUMENTO = LTI.IDDOCUMENTO)  		 WHERE PCI.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE  AND LTI.IDPRODUTOEMBALAGEM =  DII.IDPRODUTOEMBALAGEM) [VL.UNITARIO] ,		  ";
        strsql += " COALESCE(SUM(CASE WHEN EM.IDESTOQUEOPERACAO = 1 THEN EM.QUANTIDADE ELSE NULL END),0) * 	(SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA,0) * ISNULL(P.COMPRIMENTO,0) FROM PRODUTOEMBALAGEM PE  INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ORDER BY PE.UNIDADEDOCLIENTE) [ENTRADA M3], ";
        strsql += " COALESCE(SUM(CASE WHEN EM.IDESTOQUEOPERACAO = 2 THEN EM.QUANTIDADE ELSE NULL END),0) * 	(SELECT TOP 1    ISNULL(P.ALTURA, 0) * ISNULL(P.LARGURA,0) * ISNULL(P.COMPRIMENTO,0) FROM PRODUTOEMBALAGEM PE  INNER JOIN PRODUTO P ON P.IDPRODUTO = PE.IDPRODUTO  WHERE PE.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE   ORDER BY PE.UNIDADEDOCLIENTE) [SAIDA M3] ";
        strsql += " FROM PRODUTOCLIENTE PC  ";
        strsql += " LEFT JOIN ESTOQUE ES ON (ES.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE)  ";
        strsql += " LEFT JOIN ESTOQUEMOV EM ON (EM.IDESTOQUE=ES.IDESTOQUE)  ";
        strsql += " WHERE PC.IDCLIENTE = " + cboCliente.SelectedValue;
        strsql += " AND ES.IDFILIAL =" + cboFilial.SelectedValue;
        strsql += " AND EM.DATAHORA BETWEEN '" + DateTime.Parse(txtI.Text).ToString("yyyy-MM-dd") + "' AND '" + DateTime.Parse(txtF.Text).ToString("yyyy-MM-dd") + "' ";
        strsql += " GROUP BY PC.IDPRODUTOCLIENTE, PC.CODIGO, PC.DESCRICAO, ES.SALDO , PC.MARCA ";
        strsql += " ORDER BY PC.DESCRICAO , PC.MARCA ";

        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        GridView1.DataSource = dt;
        GridView1.DataBind();

        if (dt.Rows.Count > 0)
        {
            btnGerarExcel.Visible = true;
            Session["dt"] = dt;

            btnGerarExcel.Attributes.Add("onClick", "window.open('gerarexcel.aspx', 'NovaJanela2', 'yes'); return false;");

        }
    }

    

    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        MontarTable();
    }

    protected void cboFilial_SelectedIndexChanged(object sender, EventArgs e)
    {
        carregarComboClientes();
    }

    private void carregarComboClientes()
    {

    }

    protected void cboFilial_SelectedIndexChanged1(object sender, EventArgs e)
    {
        cboCliente.Items.Clear();
        GridView1.DataSource = null;

        if (cboFilial.SelectedIndex > 0)
        {
            string strsql = "";

            strsql += " SELECT C.IDCLIENTE, cnpjcpf+ ' - ' +  FANTASIAAPELIDO FANTASIAAPELIDO";
            strsql += " FROM CLIENTEFILIAL CF ";
            strsql += " INNER JOIN CLIENTE C ON C.IDCLIENTE = CF.IDCLIENTE ";
            strsql += " INNER JOIN CADASTRO CD ON CD.IDCADASTRO = C.IDCLIENTE ";
            strsql += " WHERE IDFILIAL=" + cboFilial.SelectedValue;
            strsql += " AND CF.CLIENTELOGISTICA = 'SIM' order by 2";

            cboCliente.DataSource = Sistran.Library.GetDataTables.RetornarDataTable(strsql);
            cboCliente.DataTextField = "FANTASIAAPELIDO";
            cboCliente.DataValueField = "IDCLIENTE";
            cboCliente.DataBind();

            cboCliente.Items.Insert(0, new ListItem("SELECIONE"));

        }
        else
        {
            cboCliente.Items.Add(new ListItem("SELECIONE A FILIAL", ""));
        }
    }


    public static void ExportToExcel(DataTable data, string fileName)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName + Guid.NewGuid() + ".xls"));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(GenerateTable(data));
        HttpContext.Current.Response.End();
    }

    private static string GenerateTable(DataTable source)
    {

        HtmlTable table = new HtmlTable();
        HtmlTableRow headerRow = new HtmlTableRow();

        for (int x = 0; x < source.Columns.Count; x++)
        {
            HtmlTableCell th = new HtmlTableCell("th");
            th.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#337490");
            th.Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
            th.InnerText = source.Columns[x].ColumnName;
            headerRow.Cells.Add(th);
        }
        table.Rows.Add(headerRow);

        foreach (DataRow x in source.Rows)
        {
            HtmlTableRow tableRow = new HtmlTableRow();

            for (int y = 0; y < source.Columns.Count; y++)
            {
                System.Type rowType;
                rowType = x[y].GetType();
                HtmlTableCell td = new HtmlTableCell();

                switch (rowType.ToString())
                {
                    case "System.String":
                        string XMLstring = x[y].ToString();
                        XMLstring = XMLstring.Trim();
                        XMLstring = XMLstring.Replace("&", "&");
                        XMLstring = XMLstring.Replace(">", ">");
                        XMLstring = XMLstring.Replace("<", "<");
                        td.InnerText = XMLstring;
                        break;

                    case "System.DateTime":
                        td.InnerText = ((DateTime)x[y]).ToString("dd/MM/yyyy");
                        break;

                    case "System.Boolean":
                        td.InnerText = x[y].ToString();
                        break;

                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Byte":
                        td.InnerText = x[y].ToString();
                        break;
                    case "System.Decimal":
                    case "System.Double":
                        td.InnerText = string.Format("{0:n}", x[y]);
                        break;
                    case "System.DBNull":
                        td.InnerText = string.Empty;
                        break;
                }
                tableRow.Cells.Add(td);
            }
            table.Rows.Add(tableRow);
        }

        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        table.RenderControl(htw);
        return sw.ToString();
    }

    protected void btnGerarExcel_Click(object sender, EventArgs e)
    {
        ExportToExcel((DataTable)Session["dt"], "Movimentacao" + Guid.NewGuid());
    }
    protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}