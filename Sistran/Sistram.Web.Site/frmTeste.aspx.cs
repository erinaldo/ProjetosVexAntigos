using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmTeste : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       Button1.Attributes.Add("onClick", "javascript:NewWindow('frmfotomotorista.aspx?controle=" + lblProprietario.ClientID + "','new','width=400px,height=450px,scrollbars=yes')");
    }
}
