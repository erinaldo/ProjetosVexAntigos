using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class frmListaArquivoTransp : System.Web.UI.Page
{
   // List<SistranMODEL.Usuario> ILusuario;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];

            if (!IsPostBack)
            {
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper()), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                CarregarGridArquivosRecebidos();
                CarregarGridArquivosEviados();
            }
        }
        catch (Exception ex )
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert3", "alert('" + ex.Message.ToString().Replace("'", "´") + "')", true);
            return;
        }
        
    }

    private void CarregarGridArquivosEviados()
    {
        RadGrid2.DataSource = Sistran.Library.ArquivoTranportadora.RetornarArquivos("E", Convert.ToInt32(Session["IDEmpresa"]));
        RadGrid2.DataBind();
    }

    private void CarregarGridArquivosRecebidos()
    {
        grdRecebidos.DataSource = Sistran.Library.ArquivoTranportadora.RetornarArquivos("R", Convert.ToInt32(Session["IDEmpresa"]));
        grdRecebidos.DataBind();
    }

    protected void RadGrid2_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "Baixar")
        {
            //ImageButton btnBaixarArquivo = (ImageButton)e.Item.FindControl("btnBaixarArquivo");
            //byte[] arq = Sistran.Library.ArquivoTranportadora.RetornarArquivo(Convert.ToInt32(btnBaixarArquivo.CommandName));
            //Response.BinaryWrite(arq);
           // FileInfo arquivo = new FileInfo();
           // Response.Clear();
           //// 'Adiciona um cabeçalho que especifica o nome default para a caixa de diálogos Salvar Como...
           // Response.ContentType = "application/octet-stream";
           // Response.AddHeader("Content-Disposition", "attachment; filename=""" & arquivo.Name & """")
           // //'Adiciona ao cabeçalho o tamanho do arquivo para que o browser possa exibir o progresso do download
           // Response.AddHeader("Content-Length", arquivo.Length.ToString());
           // Response.Flush()
           // Response.WriteFile(arquivo.FullName);

        }
    }

    protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {

    }

    protected void RadGrid2_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ImageButton btnBaixarArquivo = (ImageButton)e.Item.FindControl("btnBaixarArquivo");
        LinkButton lnkArquivo = (LinkButton)e.Item.FindControl("lnkArquivo");

        if (btnBaixarArquivo != null)
        {
            btnBaixarArquivo.Visible = false;
            btnBaixarArquivo.Attributes.Add("onclick", "javascript:window.open('GerarArquivo.aspx?i=" + btnBaixarArquivo.CommandName.ToString() + "')");
            lnkArquivo.Attributes.Add("onclick", "javascript:window.open('GerarArquivo.aspx?i=" + btnBaixarArquivo.CommandName.ToString() + "')");
        }

    }

    protected void grdRecebidos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ImageButton btnBaixarArquivo = (ImageButton)e.Item.FindControl("btnBaixarArquivo");
        LinkButton lnkArquivo = (LinkButton)e.Item.FindControl("lnkArquivo");

        if (btnBaixarArquivo != null)
        {
            btnBaixarArquivo.Visible = false;
            btnBaixarArquivo.Attributes.Add("onclick", "javascript:window.open('GerarArquivo.aspx?i=" + btnBaixarArquivo.CommandName.ToString() + "')");
            lnkArquivo.Attributes.Add("onclick", "javascript:window.open('GerarArquivo.aspx?i=" + btnBaixarArquivo.CommandName.ToString() + "')");
        }
    }

    protected void Button_Confirmar(object sender, System.EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            int intTamanho = System.Convert.ToInt32(FileUpload1.PostedFile.InputStream.Length); 
            byte[] imageBytes = new byte[intTamanho];
            FileUpload1.PostedFile.InputStream.Read(imageBytes, 0, intTamanho);
            Sistran.Library.ArquivoTranportadora.Incluir(imageBytes, Convert.ToInt32(Session["IDEmpresa"]), 2, FileUpload1.FileName, cmbTipo.SelectedItem.Text, ILusuario[0].UsuarioId);
            CarregarGridArquivosEviados();
            CarregarGridArquivosRecebidos();
        }
    }
    protected void grdRecebidos_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "Baixar")
        {
            LinkButton lnkArquivo = (LinkButton)e.Item.FindControl("lnkArquivo");
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            Sistran.Library.ArquivoTranportadora.BaixarArquivo(ILusuario[0].UsuarioId, Convert.ToInt32(lnkArquivo.CommandName.ToString()));
            CarregarGridArquivosEviados();
            CarregarGridArquivosRecebidos();
        }
    }
}
