using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
////using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class frmCadAviso : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
               
                if (Request.QueryString["opc"] != null)
                    lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

                Button2.Attributes.Add("OnClick", "javascript:window.open('frmDetalheAviso.aspx?opc=" + Server.UrlEncode(lblTitulo.Text).ToUpper() + "');");

                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Pesquisar();
    }

    private void Pesquisar()
    {
        try
        {
            DataTable dt = new SistranBLL.Aviso().Listar(txtNome.Text, txtLogin.Text, txtOperacao.Text, "");
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }




    protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        Pesquisar();
    }
    protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        Pesquisar();
    }
    protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "Deletar")
        {
            LinkButton LinkDeletar = (LinkButton)e.Item.FindControl("LinkDeletar");
            new SistranBLL.Aviso().ApagarAviso(LinkDeletar.CommandName);
            Pesquisar();
        }
    }
}