using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Intranet_frmListVeiculo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button3.Visible = false;           
        }       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CarregarGrid();        
    }

    private void CarregarGrid()
    {
        DataTable dt = new SistranBLL.Veiculo().Pesquisar(null, txtPlaca.Text.Trim(), chkAnttVencido.Checked, chkLicenciamento.Checked);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["TIPO"] = (dt.Rows[i]["TIPO"].ToString().Length>10?dt.Rows[i]["TIPO"].ToString().Substring(0,10)+ "...":dt.Rows[i]["TIPO"].ToString() );
        }

        RadGrid17.DataSource = dt.DefaultView;
        RadGrid17.DataBind();
        //Session["filtros"] = txtPlaca.Text.Trim();
        Session["dtResult"] = dt;

        if (dt.Rows.Count > 0)
        {
            Button3.Attributes.Add("onClick", "window.open('frmRptMotoristas.aspx?tipoReport=Veiculo&tipo=TELA&tit=" + "Relatório de Motoristas" + "', 'NovaJanela2', 'yes')");
            Button3.Visible = true;
        }
        else
            Button3.Visible = false;


    }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmCadVeiculo.aspx?novo=s");
    }

    protected void RadGrid17_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        CarregarGrid();
    }

    protected void RadGrid17_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        CarregarGrid();
    }
}
