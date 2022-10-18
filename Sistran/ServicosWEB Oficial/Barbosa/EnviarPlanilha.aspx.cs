using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicosWEB.Barbosa
{
    public partial class EnviarPlanilha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if(!FileUpload1.PostedFile.FileName.ToLower().Contains(".xlsx"))
                {
                    Label1.Text = "Só é permitido arquivos Excel, com extensção .xlsx";
                    return;
                }

                FileUpload1.SaveAs((Server.MapPath("Planilhas") +"\\" + FileUpload1.FileName));
                Label1.Text = "Arquivo Importado com sucesso";
            }
        }
    }
}