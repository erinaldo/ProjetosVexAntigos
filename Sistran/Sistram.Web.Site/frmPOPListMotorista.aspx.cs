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

public partial class Intranet_frmPOPListMotorista : System.Web.UI.Page
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
            //    chkativo.Checked = filtros[2] == "SIM" ? true : false;
            //    CarregarGrid();
            //}            
        }
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCpf.Text.Trim().Length == 0 && txtNome.Text.Trim().Length == 0)
            {
                throw new Exception("Informe algum filtro");
            }

            CarregarGrid();        

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('"+ ex.Message.Trim().Replace("'","´") +"')", true);

        }
    }

    private void CarregarGrid()
    {
        DataTable dt = new SistranBLL.Cadastro.Motorista().Pesquisar(txtNome.Text.Trim(), txtCpf.Text.Trim(), chkativo.Checked == true ? "SIM" : "NAO", "", null,"","", false);
        RadGrid17.DataSource = dt;
        RadGrid17.DataBind();
        RadGrid17.Visible = true;
        Session["filtros"] = txtNome.Text.Trim()+"|"+ txtCpf.Text.Trim()+ "|"+ (chkativo.Checked==true?"SIM":"NAO");
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmPOPCadMotorista.aspx?novo=s&controle=" + Request["controle"] + "&controleCpf=" + Request["controleCpf"] + "&controleNome=" + Request["controleNome"]);
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
        HyperLink hpCodigo = (HyperLink)e.Item.FindControl("hpCodigo");
        Label CnpjCpfLabel = (Label)e.Item.FindControl("CnpjCpfLabel");
        Label RazaoSocialNomeLabel = (Label)e.Item.FindControl("RazaoSocialNomeLabel");

        if (hpCodigo != null)
        {
            hpCodigo.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
            hpCodigo.Attributes.Add("Onclick", "javascript:window.opener.document.getElementById('" + Request["controle"] + "').value = '" + hpCodigo.Text + "'; window.opener.document.getElementById('" + Request["controleCpf"] + "').value = '" + CnpjCpfLabel.Text + "'; window.opener.document.getElementById('" + Request["controleNome"] + "').value = '" + RazaoSocialNomeLabel.Text + "'; window.close();");
        }
    }
}
