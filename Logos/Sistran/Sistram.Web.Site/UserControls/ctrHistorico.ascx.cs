using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;

public partial class UserControls_ctrHistorico : System.Web.UI.UserControl
{    
    public int IDS { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {  
            RadGrid1.DataSource = new SistranBLL.Cadastro.Motorista.MotoristaHistorico().RetornarHistorico(IDS);
            RadGrid1.DataBind();           
        }
    }
}
