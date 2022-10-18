using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Win32;

public partial class GerarArquivo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["i"] == null)
        {
            return;
        }

        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "Baixou o Arquivo - Troca de Arquivos Transportadora. IdEdiTrocaDeArquivo=" + Request.QueryString["i"], System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

        byte[] readBuffer = Sistran.Library.ArquivoTranportadora.RetornarArquivo(Convert.ToInt32(Request.QueryString["i"]));
        string nome = Sistran.Library.ArquivoTranportadora.RetornarNomeArquivo(Convert.ToInt32(Request.QueryString["i"]));
        
        System.IO.File.WriteAllBytes(Server.MapPath(@"imgReport/" + nome), readBuffer);
        FileAttributes cc = File.GetAttributes(Server.MapPath(@"imgReport/" + nome));

        FileInfo arquivo = new FileInfo(Server.MapPath(@"imgReport/" + nome));
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + arquivo.Name);
        Response.AddHeader("Content-Length", arquivo.Length.ToString());
        Response.Flush();
        Response.WriteFile(arquivo.FullName);

    } 

}
