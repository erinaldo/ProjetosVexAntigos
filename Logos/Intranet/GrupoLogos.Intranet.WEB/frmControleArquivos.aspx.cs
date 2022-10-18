using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;

public partial class frmControleArquivos : System.Web.UI.Page
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
            CarregarGrid();
            CarregarCBOTipoDeArquivo();

            cmbTipo.Attributes.Add("onchange", "OnChange(this, '"+txtNovoNome.ClientID+"')");
            
            
        }
    }

    private void CarregarCBOTipoDeArquivo()
    {
        cmbTipo.DataSource = Sistran.Library.GetDataTables.RetornarDataTable("SELECT IDARQUIVO, NOME FROM ARQUIVO ORDER BY 2");
        cmbTipo.DataTextField = "NOME";
        cmbTipo.DataValueField = "IDARQUIVO";
        cmbTipo.DataBind();
        cmbTipo.Items.Insert(0, new ListItem("SELECIONE", "SELECIONE"));
        cmbTipo.Items.Insert(1, new ListItem("DIGITAR NOVO...", "DIGITAR NOVO..."));
        
    }

    private void CarregarGrid()
    {
        DataTable dt = new SistranBLL.Arquivo().Filtrar(null);
        RadGridUsuarios.DataSource = dt;
        RadGridUsuarios.DataBind();
    }

    protected void RadGrid16_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }
    
    protected void RadGrid16_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

    }

    protected void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbTipo.SelectedItem.Text == "DIGITAR NOVO...")
        {
            txtNovoNome.Visible = true;
            txtNovoNome.Focus();
        }
        else
        {
            txtNovoNome.Visible = false;            
        }
    }

    protected void RadGridUsuarios_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        
    }

    protected void btnConfirma_Click(object sender, EventArgs e)
    {
        string id = "";
        if (cmbTipo.SelectedIndex == 1 && FileUpload1.HasFile)
        {
            //grava a tabela arquivo
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            id = Sistran.Library.GetDataTables.RetornarIdTabela("Arquivo");
            Sistran.Library.GetDataTables.ExecutarComandoSql("INSERT INTO ARQUIVO VALUES (" + id + ", '" + (txtNovoNome.Text == "" ? FileUpload1.FileName.Trim().ToUpper().Replace(" ", "").Replace(".", "") : txtNovoNome.Text.ToUpper().Trim()) + "', GETDATE(), " + ILusuario[0].UsuarioId + ")", HttpContext.Current.Session["ConnLogin"].ToString());
        }

        if (FileUpload1.HasFile)
        {
            if (id == "")
                id = cmbTipo.SelectedValue;
            int intTamanho = System.Convert.ToInt32(FileUpload1.PostedFile.InputStream.Length);
            byte[] imageBytes = new byte[intTamanho];
            FileUpload1.PostedFile.InputStream.Read(imageBytes, 0, intTamanho);
            Sistran.Library.GetDataTables.InserirArquivos(id, imageBytes, FileUpload1.FileName.ToLower());
        }

        CarregarGrid();
        cmbTipo.Items.Clear();
        CarregarCBOTipoDeArquivo();
        txtNovoNome.Text = "";
    }
}