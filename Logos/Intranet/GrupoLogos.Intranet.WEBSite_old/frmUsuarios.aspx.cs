using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;

public partial class frmUsuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (!IsPostBack)
        {
            rmp.TabIndex = 0;
            RadTabStrip1.SelectedIndex = 0;
            rpvUsuarios.Selected = true;
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
//           SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
            //CarregarGrid();
            //CarregarGridPerfil();
            btnNovo.Attributes.Add("OnClick", "javascript:window.open('frmUsuariosDetalhe.aspx?id=novo&ic=0&opc=" + Server.UrlEncode(Request.QueryString["opc"].ToString()).ToUpper() + "');");
            btnNovoPerfil.Attributes.Add("OnClick", "javascript:window.open('frmPerfisDetalhe.aspx?id=novo&opc=" + Server.UrlEncode("Novo Perfil").ToUpper() + "');");            
        }      
    }

    private void CarregarGrid()
    {
        DataTable dt = new SistranBLL.Usuario().Listar(txtLogin.Text.Trim(), txtNome.Text.Trim(), txtCPF.Text.Trim());
        
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["CnpjCpf"].ToString().Trim().Replace(".", "").Replace("/", "").Replace("-", "").Length != 14 && dt.Rows[i]["CnpjCpf"].ToString().Trim().Replace(".", "").Replace("/", "").Replace("-", "").Length != 11)
            {
                dt.Rows[i]["CnpjCpf"] = "";
            }                
        }
        RadGridUsuarios.DataSource = dt;
        RadGridUsuarios.DataBind();
    }
     
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        CarregarGrid();
    }
    
    protected void RadGrid16_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        CarregarGrid();
    }
    
    protected void RadGrid16_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        CarregarGrid();
    }

    protected void RadGrid16_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        HyperLink HyperLink1 = (HyperLink)e.Item.FindControl("HyperLink1");
        Label lblIdCadastro = (Label)e.Item.FindControl("lblIdCadastro");
        Label lblAtivo = (Label)e.Item.FindControl("lblAtivo");
        LinkButton lnkHabilitar = (LinkButton)e.Item.FindControl("lnkHabilitar");


        if (HyperLink1 != null)
        {
            HyperLink1.Attributes.Add("tit", Server.UrlEncode(Request.QueryString["opc"].ToString().ToUpper()));
            HyperLink1.NavigateUrl = HyperLink1.NavigateUrl + "&ic=" + lblIdCadastro.Text + "&opc=" + Server.UrlEncode(Request.QueryString["opc"].ToString().ToUpper());

            lnkHabilitar.CommandArgument = "del";
            lnkHabilitar.CommandName = lblIdCadastro.Text;

            if (lblAtivo.Text == "SIM")
            {
                lnkHabilitar.Text = "Desabilitar";
            }
            else
            {
                lnkHabilitar.Text = "Habilitar";
            }
        }
    }

    protected void RadGrid16_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "del")
        {
            LinkButton lnkHabilitar = (LinkButton)e.Item.FindControl("lnkHabilitar");
            SistranBLL.Usuario o = new SistranBLL.Usuario();

            if (lnkHabilitar.Text == "Desabilitar")
                o.HabDesbUusario(e.CommandName, "NAO");
            else
                o.HabDesbUusario(e.CommandName, "SIM");


            CarregarGrid();

        }
    }

    protected void btnPesquisarPerfil_Click(object sender, EventArgs e)
    {
        CarregarGridPerfil();
    }

    private void CarregarGridPerfil()
    {
        RadGridPerfis.DataSource = new SistranBLL.Usuario().ListarPerfil(txtPerfil.Text.Trim());
        RadGridPerfis.DataBind();
    }
    protected void RadGridPerfis_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "del")
        {
            LinkButton lnkHabilitarPerfil = (LinkButton)e.Item.FindControl("lnkHabilitarPerfil");
            SistranBLL.Usuario o = new SistranBLL.Usuario();

            if (lnkHabilitarPerfil.Text == "Desabilitar")
                o.HabDesbPeril(e.CommandName, "NAO");
            else
                o.HabDesbPeril(e.CommandName, "SIM");


            CarregarGridPerfil();
        }
    }

    protected void RadGridPerfis_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        HyperLink HyperLinkPefil = (HyperLink)e.Item.FindControl("HyperLinkPefil");
        Label lblIdPerfil = (Label)e.Item.FindControl("lblIdPerfil");
        Label lblAtivoPerfil = (Label)e.Item.FindControl("lblAtivoPerfil");
        LinkButton lnkHabilitarPerfil = (LinkButton)e.Item.FindControl("lnkHabilitarPerfil");

        if (HyperLinkPefil != null)
        {
            HyperLinkPefil.Attributes.Add("tit", Server.UrlEncode(Request.QueryString["opc"].ToString().ToUpper()));
            HyperLinkPefil.NavigateUrl = HyperLinkPefil.NavigateUrl + "&opc=" + Server.UrlEncode("Perfil");

            lnkHabilitarPerfil.CommandArgument = "del";
            lnkHabilitarPerfil.CommandName = lblIdPerfil.Text;

            if (lblAtivoPerfil.Text == "SIM")
            {
                lnkHabilitarPerfil.Text = "Desabilitar";
            }
            else
            {
                lnkHabilitarPerfil.Text = "Habilitar";
            }
        }
    }

    protected void RadGridPerfis_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
    {
        CarregarGridPerfil();
    }
    protected void RadGridPerfis_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        CarregarGridPerfil();
    }


}