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


public partial class Intranet_frmListMotorista : System.Web.UI.Page                                                                                                                                                                                                                                                                                            
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button3.Visible = false;
            cboFilial.DataSource = new SistranBLL.Filial().ListarDisponiveisByIDMotorista("", 0);
            cboFilial.DataTextField = "NOME";
            cboFilial.DataValueField = "IDFILIAL";
            cboFilial.DataBind();
            cboFilial.Items.Insert(0, new ListItem("::: Todas ::: ", "0"));
        }
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtCpf.Text != "")
        {
            txtCpf.Text = Sistran.Library.Helpers.FormatarCnpj(txtCpf.Text);
        }
        CarregarGrid();    
        
    }

    private void CarregarGrid()
    {
        DateTime? data1 = null;
        DateTime? data2 = null;
        DateTime? data3 = null;
        DateTime? data4 = null;

        if (txtGerenciamento.Text != "")
            data1 = DateTime.Parse(txtGerenciamento.Text);
        if (txtGerenciamento.Text != "")
            data2 = DateTime.Parse(txtGerenciamentoFim.Text);
        if (txtDataDeBloqueio.Text != "")
            data3 = DateTime.Parse(txtDataDeBloqueio.Text);
        if (txtDataDeBloqueioFim.Text != "")
            data4 = DateTime.Parse(txtDataDeBloqueioFim.Text);



        //DataTable dt = new SistranBLL.Cadastro.Motorista().Pesquisar(txtNome.Text.Trim(), txtCpf.Text.Trim(), chkativo.Checked == true ? "SIM" : "NAO", chkInativo.Checked == true ? "SIM" : "NAO", null, chkLiberado.Checked == true ? "SIM" : "NAO", chkNaoLiberado.Checked == true ? "SIM" : "NAO", chkVencida.Checked);
        DataTable dt = new SistranBLL.Cadastro.Motorista().Pesquisar(txtNome.Text.Trim(), txtCpf.Text.Trim(), chkativo.Checked == true ? "SIM" : "NAO", chkInativo.Checked == true ? "SIM" : "NAO", null, chkLiberado.Checked == true ? "SIM" : "NAO",
                                                                     chkNaoLiberado.Checked == true ? "SIM" : "NAO", chkVencida.Checked, cboGerenciamento.SelectedValue,
                                                                     data1,
                                                                     data2,
                                                                     cboTipoFavorecido.SelectedItem.Text,
                                                                     txtFavorecido.Text, cboContratado.SelectedItem.Text, int.Parse(cboFilial.SelectedValue),
                                                                     data3,
                                                                     data4);
        Session["dtResult"] = dt;

        int count = 0;
        foreach (DataRow item in dt.Rows)
        {
            dt.Rows[count]["RazaoSocialNome"] = (item["RazaoSocialNome"].ToString().Length >= 25 ? item["RazaoSocialNome"].ToString().Substring(0, 24) + "..." : item["RazaoSocialNome"].ToString());

            count += 1;
        }

        RadGrid17.DataSource = dt.DefaultView;
        RadGrid17.DataBind();
        RadGrid17.PageSize = 20;

        if (dt.Rows.Count > 0)
        {
            Button3.Attributes.Add("onClick", "FullWindow('frmRptMotoristas.aspx?tipo=TELA&tit=" + "Relatório de Motoristas" + "', 'NovaJanela2', 'yes')");
            Button3.Visible = true;
        }
        else
            Button3.Visible = false;

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmCadMotorista.aspx?novo=s");
    }

    protected void RadGrid17_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        CarregarGrid();
    }

    protected void RadGrid17_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        CarregarGrid();
    }

    protected void RadGrid17_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        
    }
}
