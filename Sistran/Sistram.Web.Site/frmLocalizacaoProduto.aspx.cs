using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Diagnostics;

public partial class frmLocalizacaoProduto : System.Web.UI.Page
{
    public int intervalo;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (!IsPostBack)
                MontarTable();

            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alerts", "<script> alert('" + ex.Message.Replace("'", "´") + "'); </script>");
        }
    }

    protected void MontarTable()
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        
        SistranBLL.Usuario.LogBDBLL.GravarLog(
            ILusuario[0].UsuarioId, ILusuario[0].Login, "PESQUISOU " 
            + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), 
            System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

        PlaceHolder1.Controls.Add(new LiteralControl(@"<table class='table' cellspacing=2 celpanding=4 style='width:99%' >"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='LEFT' style='font-size:7pt; nowrap='nowrap'>UNIDADE DE ARMAZENAGEM"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' align='LEFT' style='font-size:7pt; nowrap='nowrap'>CÓDIGO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;' nowrap=nowrap align='left'>CÓDIGO DO CLIENTE"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;' nowrap=nowrap align='left'>DESCRIÇÃO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;' nowrap=nowrap align='center'>DATA DE ENTRADA"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;' nowrap=nowrap align='left'>NÚMERO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;' nowrap=nowrap align='left'>SALDO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;' nowrap=nowrap align='center'>ENDEREÇO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdpCabecalho' style='font-size:7pt;' nowrap=nowrap align='right'>Vl. Unit."));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

        

         DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable("exec PRC_LocalizaoProdutoClienteV2 '" + Sistran.Library.FuncoesUteis.retornarClientes() + "', 15, '" + txtFiltro.Text + "'");

        Debug.Write(Sistran.Library.FuncoesUteis.retornarClientes());

        DataView dv = dt.DefaultView;
        dv.Sort = DropDownList1.SelectedValue + " asc";
        dt = dv.ToTable();
        
        Session["dt"] = dt;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>" + dt.Rows[i]["IdUnidadeDeArmazenagem"].ToString() + "</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>" + dt.Rows[i]["Codigo"].ToString() + "</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>" + dt.Rows[i]["CodigoDoCliente"].ToString() + "</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px'>" + dt.Rows[i]["Descricao"].ToString() + "</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px; text-align:center;'>" + (dt.Rows[i]["DataDeEntrada"].ToString() == "" ? "" : DateTime.Parse(dt.Rows[i]["DataDeEntrada"].ToString()).ToString("dd/MM/yyyy")) + "</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px; text-align:right;' >" + dt.Rows[i]["Numero"].ToString() + "</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px; text-align:right;'>" + (dt.Rows[i]["Saldo"].ToString() == "" ? "0" : float.Parse(dt.Rows[i]["Saldo"].ToString()).ToString("#0")) + "</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px; text-align:center;'>" + dt.Rows[i]["Endereco"].ToString() + "</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td class='tdp' nowrap=nowrap style='font-size:7pt;height:10px; text-align:right;'>" + (dt.Rows[i]["ValorUnitario"].ToString() == "" ? "0" : float.Parse(dt.Rows[i]["ValorUnitario"].ToString()).ToString("#0.00")) + "</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
        return;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        MontarTable();
    }
}