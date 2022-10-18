using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sistran;
using System.Data;

public partial class popHabDesab : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string s = "SELECT   UAL.SALDO FROM    UNIDADEDEARMAZENAGEM UA   INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON (UAL.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM) WHERE UA.IDDEPOSITOPLANTALOCALIZACAO = " + Request.QueryString["IdPlantaLocalizacao"].ToString() + "  AND UAL.SALDO > 0";
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(s, "");

        if (dt.Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "akeye", "javascript:window.alert('ENDERECO COM PRODUTO. PARA DESABILITAR FAÇA UMA TRANFERENCIA');", true);
            ClientScript.RegisterStartupScript(this.GetType(), "akefy", "javascript:window.close();", true);
            return;
        }

        if (Request.QueryString["acao"] == "d")
        {
            Sistran.Library.GetDataTables.ExecutarSemRetorno("UPDATE DEPOSITOPLANTALOCALIZACAO SET ATIVO ='NAO' WHERE IDDEPOSITOPLANTALOCALIZACAO=" + Request.QueryString["IdPlantaLocalizacao"], "");
            ClientScript.RegisterStartupScript(this.GetType(), "akeye", "javascript:window.alert('ITEM DESABILITADO COM SUCESSO');", true);
        }
        else
        {
            Sistran.Library.GetDataTables.ExecutarSemRetorno("UPDATE DEPOSITOPLANTALOCALIZACAO SET ATIVO ='SIM' WHERE IDDEPOSITOPLANTALOCALIZACAO=" + Request.QueryString["IdPlantaLocalizacao"], "");
            ClientScript.RegisterStartupScript(this.GetType(), "akeye", "javascript:window.alert('ITEM HABILITADO COM SUCESSO');", true);
        }
        ClientScript.RegisterStartupScript(this.GetType(), "akefy", "javascript:window.close();", true);       
    }
}
