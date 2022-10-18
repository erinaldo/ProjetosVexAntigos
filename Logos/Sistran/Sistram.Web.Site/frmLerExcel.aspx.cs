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
using Telerik.Web.UI;
using System.IO;
using SistranBLL;

public partial class frmLerExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        try
        {
            if (uplArquivo.UploadedFiles.Count > 0)
            {
                UploadedFile file;
                file = uplArquivo.UploadedFiles[0];

                if (file.GetExtension() == ".txt")
                {
                    string endereco = System.Web.HttpContext.Current.Server.MapPath("~/imgReport");
                    string n = uplArquivo.UploadedFiles[0].FileName;
                    string datahora = DateTime.Now.ToString("yyyyMMddmmss");
                    n = "txt" + datahora + DateTime.Now.Minute + n.Substring(n.LastIndexOf('.'));
                    file.SaveAs(endereco + "\\" + n);
                    System.IO.StreamReader objStreamReader;
                    objStreamReader = File.OpenText(endereco + "\\" + n);
                    Importacao bll = new Importacao();
                    bll.ProcessarArquivo(objStreamReader, endereco + "\\" + n);
                }
            }
        }
        catch (Exception )
        {
            throw;
        }
    }
}
