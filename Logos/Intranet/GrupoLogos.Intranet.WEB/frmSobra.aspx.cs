using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;

public partial class frmSobra : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");

        if (!IsPostBack)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text, System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
           

            string[] DataConf = FuncoesGerais.DataConf();
            txtI.Text = DataConf[0];
            txtF.Text = DataConf[1];
            CarregarGrid();
            CarregarCboFilial();
        }
        Session["DataConf"] = txtI.Text + "|" + txtF.Text;
    }


    private void CarregarCboFilial()
    {
        cboFilial.DataSource = new SistranDAO.Filial().ListarTodasFiliais(Session["Conn"].ToString());
        cboFilial.DataValueField = "VALOR";
        cboFilial.DataTextField = "NOME";
        cboFilial.DataBind();
        cboFilial.Items.Insert(0, new ListItem("Todas Filiais"));
    }


    private void CarregarGrid()
    {
        int? m = null;
        if (cboFilial.SelectedIndex > 0)
        {
            m = int.Parse(cboFilial.SelectedValue);
        }

        DataTable dt = new SistranBLL.Sobra().Filtrar(null, m, CheckBox1.Checked, Convert.ToDateTime( txtI.Text), Convert.ToDateTime( txtF.Text));
        RadGridUsuarios.DataSource = dt;
        RadGridUsuarios.DataBind();

        Session["dt"] = dt;

        if(dt.Rows.Count>0)
            Label1.Text = "Total de Volumes: "+ decimal.Parse(dt.Compute("sum(quantidade)", "").ToString()).ToString("#0.00");
        else
            Label1.Text = "Total de Volumes: 0,00";

    }
    protected void RadGrid16_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "Baixar")
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            Sistran.Library.GetDataTables.ExecutarSemRetorno("UPDATE SOBRA SET DataFinalizacao=GETDATE(), IdUsuarioBaizado=" + ILusuario[0].UsuarioId + ", Finalizado='SIM' WHERE IDSOBRA=" + e.CommandName.ToString(), "");
            CarregarGrid();
        }
    }
    protected void RadGrid16_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        LinkButton btnBaixar = (LinkButton)e.Item.FindControl("btnBaixar");
        Label lblStatus = (Label)e.Item.FindControl("lblStatus");

        if (btnBaixar != null)
        {
            if (lblStatus.Text == "SIM")
            {
                btnBaixar.Text = "BAIXADO";
                btnBaixar.Enabled = false;

            }
        }
    }
    protected void cboFilial_SelectedIndexChanged(object sender, EventArgs e)
    {
        CarregarGrid();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        CarregarGrid();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        CarregarGrid();
    }
}