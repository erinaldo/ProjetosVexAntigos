using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Configuration;
using System.Data;
////using Microsoft.Reporting.WebForms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using AjaxControlToolkit;

public partial class detFaturamentoLinhasExpedidas : System.Web.UI.Page
{
       protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");


            if (!IsPostBack)
            {
                DataTable st = (DataTable)Session["dtLinhasExpedidas"];
                Panel1 = new cReport().gerarLinhasExpedidas("xxxx", st, Panel1);
                lblResultado.Text = st.Compute("COUNT(IDDOCUMENTO)", "").ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

   
 
 
 

}