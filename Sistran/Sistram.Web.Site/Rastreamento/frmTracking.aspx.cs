using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Rastreamento_frmTracking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] conexoes = FuncoesGerais.CarregarConexoesRastreamento();
        string conn ="";
        DataTable dt_tracking = new DataTable();

        for (int i = 0; i < conexoes.Length; i++)
        {
            dt_tracking = SistranBLL.Rastreamento.RetornarTracking(Request.QueryString["id"].ToString(), conexoes[i]);

            if (dt_tracking.Rows.Count > 0)
            {
                //lblDataOc.Text = dt2.Rows[0]["DATADECONCLUSAO"].ToString() == "01/01/1900" ? "" : dt2.Rows[0]["DATADECONCLUSAO"].ToString();
                //lblOco.Text = dt2.Rows[0]["CODIGO"].ToString();
                //lblDescricaoOco.Text = dt2.Rows[0]["DESCRICAO"].ToString();
                //lblFilial.Text = dt2.Rows[0]["NUMERODAFILIAL"].ToString();
                //lblNomeFilial.Text = dt2.Rows[0]["NOME"].ToString();

                dt_tracking.Columns.Add("DATADECONCLUSAO_OBS");
                dt_tracking.Columns.Add("CODIGO_OBS");
                dt_tracking.Columns.Add("DESCRICAO_OBS");
                dt_tracking.Columns.Add("NUMERODAFILIAL_OBS");
                dt_tracking.Columns.Add("NOME_OBS");
                conn = conexoes[i];
                break;
            }
        }

        
        
        for (int i = 0; i < dt_tracking.Rows.Count; i++)
        {
            DataTable dt_OBS = SistranBLL.NotasFiscais.Ocorrencia.OcorrenciaAtualListar(Convert.ToInt32( Request.QueryString["id"].ToString()), Convert.ToInt32(dt_tracking.Rows[i]["IDDocumentoOcorrencia"]), conn);
            if(dt_OBS.Rows.Count>0)
            {
                dt_tracking.Rows[i]["DATADECONCLUSAO_OBS"] = dt_OBS.Rows[0]["DATADECONCLUSAO"];
                dt_tracking.Rows[i]["CODIGO_OBS"] = dt_OBS.Rows[0]["CODIGO"];
                dt_tracking.Rows[i]["DESCRICAO_OBS"] = dt_OBS.Rows[0]["DESCRICAO"];
                dt_tracking.Rows[i]["NUMERODAFILIAL_OBS"] = dt_OBS.Rows[0]["NUMERODAFILIAL"];
                dt_tracking.Rows[i]["NOME_OBS"] = dt_OBS.Rows[0]["NOME"];
            }

        }

        GridView1.DataSource = dt_tracking;
        GridView1.DataBind();

    }
}
