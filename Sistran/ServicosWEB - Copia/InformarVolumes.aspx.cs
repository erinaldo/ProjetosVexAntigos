using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ServicosWEB
{
    public partial class InformarVolumes : System.Web.UI.Page
    {
        string cnx = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            lbltitulo.Text = "Filial -  Informar Quantidade de Volumes";
        }

        private void carregar()
        {
            string SQL = "";
            SQL = "SELECT DISTINCT  G.IDGAIOLA GAIOLA, DATAFECHAMENTO [DATA DO FECHAMENTO] ,  U.LOGIN [USUÁRIO] , G.SITUACAO [STATUS GAIOLA], VW.NOMEREGIAO FILIAL, QTDCONFIRMADASITE ";
            SQL += " FROM GAIOLA G WITH (NOLOCK) ";
            SQL += " INNER JOIN GAIOLACONFERENCIA GC ON GC.IDGAIOLA = G.IDGAIOLA ";
            SQL += " INNER JOIN VWREGIOESROGE VW ON CAST(VW.CODIGOREGIAO AS INT) = CAST(G.FILIAL AS INT) ";
            SQL += " INNER JOIN USUARIO U ON U.IDUSUARIO = GC.IDUSUARIO ";
            SQL += " WHERE G.IDGAIOLA =" + TextBox1.Text;
            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(SQL, cnx);
            GridView1.DataBind();
        }


        protected void Button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox1.Text))
                carregar();
            else
            {
                lblMensagem.Text = "Inmforme o Código da Gaiola";                
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                lblMensagem.Font.Bold = true;
                TextBox1.Focus();
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            lblMensagem.Text = "";
            e.Row.Cells[6].Visible = false;

            var linha = e.Row.DataItem;

            if (linha != null)
            {
                System.Data.DataRowView dv = (System.Data.DataRowView)linha;

                TextBox txtQunatidadeGrid = (TextBox)e.Row.FindControl("txtQunatidadeGrid");
                Button btnSalvarGrid = (Button)e.Row.FindControl("btnSalvarGrid");

                txtQunatidadeGrid.Text = dv.DataView[0]["QtdConfirmadaSite"].ToString();
                btnSalvarGrid.CommandArgument = "Salvar";
                btnSalvarGrid.CommandName = dv.DataView[0]["Gaiola"].ToString();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index =0;
            GridViewRow row = GridView1.Rows[index];
            TextBox txtGridData = (TextBox)row.FindControl("txtQunatidadeGrid");

            if (string.IsNullOrEmpty(txtGridData.Text))
            {
                lblMensagem.Text = "Informe o Valor";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                lblMensagem.Font.Bold = true;
                return;
            }

            try
            {
                int i = int.Parse(txtGridData.Text);
            }
            catch (Exception)
            {
                lblMensagem.Text = "Valor inválido";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                lblMensagem.Font.Bold = true;
                return;
            }
            string sql = "Update Gaiola set QtdConfirmadaSite=" + txtGridData.Text + ", IdUsuarioConfirmadaSite=" + ((DataTable)Session["User"]).Rows[0]["IdUsuario"].ToString() + " where IdGaiola=" + e.CommandName.ToString() + "; Select 1";
            Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            carregar();

            lblMensagem.Text = "Quantdade alterada com sucesso!";
        }
    }
}