using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
//using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;

public partial class frmDetalheAviso : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (Request.QueryString["opc"] != null)
                lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());


            if (!IsPostBack)
            {
                carregarCombos();

                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                if (Request.QueryString["Codigo"] != null)
                {
                    CarregarDados();
                }
                else
                {
                    lblIdAviso.Text = "0";
                    lblIdUsuario.Text = "0";
                }
                cboUsuario.Focus();
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    private void carregarCombos()
    {
        DataTable DivisoesCompleta = (DataTable)Session["DivisoesCompleta"];
        cboDivisao.DataSource = DivisoesCompleta;
        cboDivisao.DataTextField = "NOME";
        cboDivisao.DataValueField = "IdClienteDivisao";
        cboDivisao.DataBind();
        cboDivisao.Items.Insert(0, new ListItem("Nenhuma", "0"));

        DataTable dtUsers = new Usuario().Listar("", "", "");
        cboUsuario.DataSource = dtUsers;
        cboUsuario.DataTextField = "Nome";
        cboUsuario.DataValueField = "IDUsuario";
        cboUsuario.DataBind();
        cboUsuario.Items.Insert(0, new ListItem("Selecione Usuário", "0"));


        //DataTable dtCanalDevendas = new SistranBLL.Aviso.CanalDeVenda().Listar();
        //cboCanalVenda.DataSource = dtCanalDevendas;
        //cboCanalVenda.DataTextField = "Nome";
        //cboCanalVenda.DataValueField = "IDUSUARIO";
        //cboCanalVenda.DataBind();
        cboCanalVenda.Items.Insert(0, new ListItem("INTERNET", "1"));

    }

    private void CarregarDados()
    {
        DataTable dt = new Aviso().Listar("", "", "", Request.QueryString["Codigo"].ToString());

        if (dt.Rows.Count > 0)
        {
            cboOperacao.SelectedValue = dt.Rows[0]["OPERACAO"].ToString();
            cboUsuario.SelectedValue = dt.Rows[0]["IDUSUARIO"].ToString();
            cboCanalVenda.SelectedValue = dt.Rows[0]["IDREPRESENTANTE"].ToString();
            cboDivisao.SelectedValue = dt.Rows[0]["IDClienteDivisao"].ToString();
            lblIdUsuario.Text = dt.Rows[0]["IDClienteDivisao"].ToString();
            lblIdAviso.Text = dt.Rows[0]["idaviso"].ToString();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (cboOperacao.SelectedItem.Text != "" && cboUsuario.SelectedIndex > 0)
            {
                if (VerificarDuplicidade()==false)
                {
                    cboUsuario.Focus();
                    throw new Exception("Já existe um usuário com estas configurações");
                }
                    if (Request["Codigo"] == null)
                        lblIdAviso.Text = new SistranBLL.Aviso().Inserir(cboOperacao.SelectedItem.Text, cboDivisao.SelectedValue, cboCanalVenda.SelectedValue, cboUsuario.SelectedValue).ToString();
                    else
                        new SistranBLL.Aviso().Alterar(cboOperacao.SelectedItem.Text, cboDivisao.SelectedValue, cboCanalVenda.SelectedValue, cboUsuario.SelectedValue, lblIdAviso.Text);

                    ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Operação Efetuada com Sucesso.')", true);
                    ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Close", "window.close()", true);
                    return;
            }
            else
                {
                    throw new Exception("Informe o Usuário e a Operação");
                }
            }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private bool VerificarDuplicidade()
    {
        if(new SistranBLL.Aviso().VerificarDuplicidade(cboUsuario.SelectedValue, cboDivisao.SelectedValue, cboOperacao.SelectedItem.Text, lblIdAviso.Text) ==0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
