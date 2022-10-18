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
using System.Web.UI.HtmlControls;

public partial class Layout : System.Web.UI.Page
{
    public int intervalo;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            intervalo = Convert.ToInt32( ConfigurationSettings.AppSettings["DiasPesquisa"]);

            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
                CarregarCboFilial();


                //cboDeposito.Enabled = false;
                cboPlanta.Enabled = false;

                if (cboFilial.Items.Count > 1)
                {
                    cboDeposito.Items.Insert(0, new ListItem("Selecione a Filial"));                 
                }


                cboPlanta.Items.Clear();
                cboPlanta.Items.Insert(0, new ListItem("Selecione um Depósito.", "0"));
                Button1.Visible = false;

                cboRua.Items.Clear();
                cboRua.Items.Insert(0, new ListItem("Selecione a Planta.", "0"));
                cboRua.Enabled = false;
               

                //HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
                //tr0.Style.Add("display", "none");
            }
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('"+ ex.Message.Replace("'", "´" ) +"')", true);
        }

    }     

    private void CarregarCboFilial()
    {
        cboFilial = new SistranBLL.Filial().CarregarCboFilialPadraoInternet(cboFilial, Sistran.Library.FuncoesUteis.retornarClientes());

        if (cboFilial.Items.Count == 1)
        {
            cboDeposito.Items.Clear();
            CarregarCboDeposito();
            cboDeposito.Enabled = true;
        }
    }

    protected void cboFilial_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(cboFilial.SelectedValue) > 0)
        {
            CarregarCboDeposito();
        }
        else
        {
            cboDeposito.Enabled = false;

            if (cboFilial.Items.Count == 1)
            {
                cboDeposito.Enabled = true;

            }
            cboDeposito.Items.Clear();
            cboDeposito.Items.Insert(0, new ListItem("Selecione uma Filial.", "0"));

            cboPlanta.Items.Clear();
            cboPlanta.Items.Insert(0, new ListItem("Selecione Deposito.", "0"));

            cboRua.Items.Clear();
            cboRua.Items.Insert(0, new ListItem("Selecione a Planta.", "0"));            
        }
    }

    private void CarregarCboDeposito()
    {
        cboDeposito = new SistranBLL.Deposito().CarregarCboDeposiito(int.Parse(cboFilial.SelectedValue), cboDeposito);
    }    
   
    protected void cboContagem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboDeposito.SelectedIndex > 0)
        {
            Button1.Attributes.Add("onclick", " FullWindow('frmpopinventario.aspx?S="+Guid.NewGuid().ToString()+"&Contagem=" + cboDeposito.SelectedValue + "&opc=" + Server.UrlDecode("Acompanhamento de Inventário - Contagem: "+ cboDeposito.SelectedItem.Text ) + "', 'inv"+ DateTime.Now.ToString().Replace("/","").Replace(" ","").Replace(":","").Replace("-","") +"', 'yes')");
            Button1.Visible = true;
        }
        else
            Button1.Visible = false;
    }

    protected void cboDeposito_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (int.Parse(cboDeposito.SelectedValue) > 0)
        {
            CarregarCboPlanta();
        }
        else
        {
            cboPlanta.Enabled = false;
            cboPlanta.Items.Clear();
            cboPlanta.Items.Insert(0, new ListItem("Selecione um Depósito.", "0"));
        }
    }

    private void CarregarCboPlanta()
    {
        cboPlanta = new SistranBLL.Deposito().CarregarCboPlanta(int.Parse(cboDeposito.SelectedValue), cboPlanta);
        
    }

    protected void cboPlanta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboPlanta.SelectedIndex > 0)
        {
            cboRua = new SistranBLL.Deposito().CarregarCboRua(int.Parse(cboPlanta.SelectedValue), cboRua);
            //cboRua.Items.Insert(0, new ListItem("Selecione", "0"));      
            
        }
        else
        {
            Button1.Visible = false;       

            cboRua.Items.Clear();
            cboRua.Items.Insert(0, new ListItem("Selecione a Planta.", "0"));      
        }
    }

    protected void cboRua_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboRua.SelectedIndex > 0 || cboRua.SelectedItem.Text.ToUpper()=="TODOS")
        {
            Button1.Attributes.Add("onclick", " FullWindow('frmpopLayout.aspx?rua="+cboRua.SelectedValue+"&S=" + Guid.NewGuid().ToString() + "&idPlantaLocalizacao=" + cboPlanta.SelectedValue + "&opc=" + Server.UrlDecode("Layout do Depósito: " + cboDeposito.SelectedItem.Text + " - Planta: " + cboPlanta.SelectedItem.Text) + "', 'inv" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").Replace("-", "") + "', 'yes')");
            Button1.Visible = true;
        }
        else
            Button1.Visible = false;
    }
}