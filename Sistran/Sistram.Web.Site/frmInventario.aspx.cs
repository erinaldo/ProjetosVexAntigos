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

public partial class frmInventario : System.Web.UI.Page
{
    #region Events

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
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
                cboContagem.Enabled = false;
                cboContagem.Items.Insert(0, new ListItem("Selecione a Filial"));
                Button1.Visible = false;
                CarregarCboFilial();                  


                HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
                tr0.Style.Add("display", "none");
            }
            lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('"+ ex.Message.Replace("'", "´" ) +"')", true);
        }

    } 

    public string CriarBotaoExpandir(string rua)
    {
        string m = "<b><div style='font-size:11px;cursor:Hand' OnClick=Expandir('"+ rua +"');>";
        m+= rua +  "</div></b>";
             
        return m;
    }

  

    #endregion

    #region Methods

    public string acertar(string tp, int posicoes)
    {
        if (tp.Length < posicoes)
        {
            for (int i = tp.Length; i < posicoes; i++)
            {
                tp = "&nbsp;" + tp;
            }
        }
        return tp;
        }

    private void CarregarCboFilial()
    {
        cboFilial.DataSource = new SistranDAO.Filial().ListarFiliais(Session["Conn"].ToString());
        cboFilial.DataValueField = "VALOR";
        cboFilial.DataTextField = "NOME";
        cboFilial.DataBind();

        if (cboFilial.Items.Count == 1)
        {
            //carregarcboContagem();
            carregarcboInventario();
            cboContagem.Enabled = true;
        }
        else
            cboFilial.Items.Insert(0, new ListItem("Selecione", "0"));
    }
     
    #endregion   

    protected void cboFilial_SelectedIndexChanged(object sender, EventArgs e)
    {
        //resetCombos();       
        if (int.Parse(cboFilial.SelectedValue) > 0)
        {
           // carregarcboContagem();
            carregarcboInventario();
        }
        else
        {        

            cboInventario.Enabled = false;
            cboInventario.Items.Clear();
            cboInventario.Items.Insert(0, new ListItem("Selecione a Filial", "0"));

            cboContagem.Enabled = false;
            cboContagem.Items.Clear();
            cboContagem.Items.Insert(0, new ListItem("Selecione um inventario.", "0"));    
        }
    }    

    private void carregarcboContagem()
    {
        cboContagem = new SistranBLL.Deposito().CarregarCboContagem(cboContagem, cboFilial.SelectedValue, Sistran.Library.FuncoesUteis.retornarClientes(), cboInventario.SelectedValue);
    }

    private void carregarcboInventario()
    {
        cboInventario = new SistranBLL.Deposito().CarregarCboIventario(cboInventario, cboFilial.SelectedValue, Sistran.Library.FuncoesUteis.retornarClientes());
    } 
    
    
    protected void cboContagem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboContagem.SelectedIndex > 0)
        {
            //Button1.Attributes.Add("onclick", " window.open('frmpopinventario.aspx?idinventario="+cboInventario.SelectedValue+"&S="+Guid.NewGuid().ToString()+"&Contagem=" + cboContagem.SelectedValue + "&opc=" + Server.UrlDecode("Acompanhamento de Inventário - Contagem: "+ cboContagem.SelectedItem.Text ) + "', 'inv"+ DateTime.Now.ToString().Replace("/","").Replace(" ","").Replace(":","").Replace("-","") +"', 'yes'); return false;");
            //Button1.Attributes.Add("OnClick", "javascript:window.open('frmpopinventario.aspx?idinventario=" + cboInventario.SelectedValue + "&S=" + Guid.NewGuid().ToString() + "&Contagem=" + cboContagem.SelectedValue + "&opc=" + Server.UrlDecode("Acompanhamento de Inventário - Contagem: " + cboContagem.SelectedItem.Text) + "');return false;");
            Button1.Attributes.Add("OnClick", "javascript:window.open('frmInv.aspx?idinventario=" + cboInventario.SelectedValue + "&S=" + Guid.NewGuid().ToString() + "&idinventariocontagem=" + cboContagem.SelectedValue + "&opc=" + Server.UrlDecode("Acompanhamento de Inventário - Contagem: " + cboContagem.SelectedItem.Text) + "');return false;");
            Button1.Visible = true;
        }
        else
            Button1.Visible = false;
    }
    protected void cboInventario_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboInventario.SelectedIndex > 0)
        {
            carregarcboContagem();
        }
        else
        {
            cboContagem.Enabled = false;
            cboContagem.Items.Clear();
            cboContagem.Items.Insert(0, new ListItem("Selecione o Inventário", "0"));     
        }
    }
}