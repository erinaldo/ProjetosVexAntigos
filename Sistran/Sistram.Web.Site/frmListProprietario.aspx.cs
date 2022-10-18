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

public partial class Intranet_frmListProprietario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregarGrid();
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
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());


        }
       
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CarregarGrid();        
    }

    private void CarregarGrid()
    {
        DataTable dt = new SistranBLL.Cadastro.Proprietario().Pesquisar(txtNome.Text.Trim(), txtCpf.Text.Trim(), null, null);
        RadGrid17.DataSource = dt.DefaultView;
        RadGrid17.DataBind();
        Session["filtros"] = txtNome.Text.Trim()+"|"+ txtCpf.Text.Trim();
    }
    
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmCadProprietario.aspx?opc=Destinatario&novo=s");
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
