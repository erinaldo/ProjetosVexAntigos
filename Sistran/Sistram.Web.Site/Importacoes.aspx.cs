using System;
using System.Web.UI;
using SistranBLL;
using System.IO;

public partial class Importacoes : System.Web.UI.Page
{
      

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);                      
        }

    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        try
        {
            if (uplArquivo.HasFile)
            {
                if (Path.GetExtension(uplArquivo.FileName) == ".txt")
                {
                    string endereco = System.Web.HttpContext.Current.Server.MapPath("~/imgReport");
                    string n = uplArquivo.FileName;
                    string datahora = DateTime.Now.ToString("yyyyMMddmmss");
                    n = "txt" + datahora + DateTime.Now.Minute + n.Substring(n.LastIndexOf('.'));
                    uplArquivo.SaveAs(endereco + "\\" + n);
                    System.IO.StreamReader objStreamReader;
                    objStreamReader = File.OpenText(endereco + "\\" + n);
                    Importacao bll = new Importacao();
                    bll.ProcessarArquivo(objStreamReader, endereco + "\\" + n);
                    throw new Exception("Processo concluído com sucesso!");

                }
                else
                    throw new Exception("Selecione um arquivo txt");

            }
            else
                throw new Exception("Selecione um arquivo .txt.");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);                      
            
        }
    }
}