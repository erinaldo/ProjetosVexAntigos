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

public partial class Intranet_frmPOPListProprietario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Request.QueryString["l"] != null && Request.QueryString["l"] == "s")
            //    Session["filtros"] = null;

            //if (Session["filtros"] != null)
            //{
            //    string[] filtros = Session["filtros"].ToString().Split('|');
            //    Session["filtros"] = null;
            //    txtNome.Text = filtros[0];
            //    txtCpf.Text = filtros[1];                
            //    CarregarGrid();
            //}            
        }
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CarregarGrid();        
    }

    private void CarregarGrid()
    {
        DataTable dt = new SistranBLL.Cadastro.Proprietario().Pesquisar(txtNome.Text.Trim(), txtCpf.Text.Trim(), null, null );
        RadGrid17.DataSource = dt.DefaultView;
        RadGrid17.DataBind();
        Session["filtros"] = txtNome.Text.Trim()+"|"+ txtCpf.Text.Trim();
    }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmPOPCadProprietario.aspx?novo=s&controle=" + Request["controle"] + "&controleCpf=" + Request["controleCpf"] + "&controleNome=" + Request["controleNome"]);
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
        HyperLink HyperLink1 = (HyperLink)e.Item.FindControl("HyperLink1");
        Label lblCpf = (Label)e.Item.FindControl("lblCpf");
        Label lblNome = (Label)e.Item.FindControl("lblNome");

        if (HyperLink1 != null)
        {

            HyperLink1.CssClass = "link";
            HyperLink1.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
            HyperLink1.Attributes.Add("Onclick", "javascript:window.opener.document.getElementById('" + Request["controle"] + "').value = '" + HyperLink1.Text + "'; window.opener.document.getElementById('" + Request["controleCpf"] + "').value = '" + lblCpf.Text + "'; window.opener.document.getElementById('" + Request["controleNome"] + "').value = '" + lblNome.Text + "'; window.close();");
        }
    }
}
