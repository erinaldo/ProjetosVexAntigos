using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ServicosWEB
{
    public partial class GaiolaDiiver : System.Web.UI.Page
    {
        string cnx = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if (!IsPostBack)
            {
                CarregarGridPendencia();
            }
        }

        private void CarregarGridPendencia()
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            //string sql = "SELECT DISTINCT GC.IDGAIOLACONFERENCIA,";
            //sql += " G.IDGAIOLA [CÓDIGO DA GAIOLA], G.DATA, (SELECT TOP 1 NOMEREGIAO FROM REPOSICAOROGE WHERE CAST(CODIGOREGIAO AS INT) =CAST(G.FILIAL AS INT) ) [NOME DA FILIAL], U.LOGIN [USUÁRIO], GC.CODIGODEBARRAS VOLUME, GC.SITUACAO [STATUS], ";
            //sql += " (SELECT TOP 1 IDGAIOLA FROM GAIOLACONFERENCIA GG WHERE GG.CODIGODEBARRAS = GC.CODIGODEBARRAS AND GC.IDGAIOLACONFERENCIA <> GG.IDGAIOLACONFERENCIA ORDER BY 1 DESC) [CÓDIGO GAIOLA LIDA] ,";
            //sql += " (SELECT TOP 1 IDGAIOLACONFERENCIA FROM GAIOLACONFERENCIA GG WHERE GG.CODIGODEBARRAS = GC.CODIGODEBARRAS AND GC.IDGAIOLACONFERENCIA <> GG.IDGAIOLACONFERENCIA ORDER BY 1 DESC) GAIOLACONFERENCIALIDA";
            //sql += " FROM GAIOLACONFERENCIA GC ";
            //sql += " INNER JOIN GAIOLA G ON G.IDGAIOLA =Gc.IDGAIOLA ";
            //sql += " INNER JOIN USUARIO U ON U.IDUSUARIO = G.IDUSUARIO ";
            //sql += " WHERE GC.ATIVO = 'SIM' ";
            //sql += " AND GC.PERTENCEAFILIAL = 'SIM' ";
            //sql += " AND GC.SITUACAO LIKE  'PENDENCIA: VOLUME JA LIDO NA GAIOLA:%'  ";
            //sql += " AND G.SITUACAO='PENDENCIA' ";
            //sql += " ORDER BY 2";


            string sql = "SELECT DISTINCT GC.IDGAIOLACONFERENCIA, G.IDGAIOLA [CÓDIGO DA GAIOLA], G.DATA, ";
            sql += " (SELECT TOP 1 NOMEREGIAO FROM REPOSICAOROGE WHERE CAST(CODIGOREGIAO AS INT) =CAST(G.FILIAL AS INT) ) [NOME DA FILIAL],  ";
            sql += " U.LOGIN [USUÁRIO],  ";
            sql += " GC.CODIGODEBARRAS VOLUME,  ";

            sql += " GC.SITUACAO [STATUS],   ";
            sql += " isnull((SELECT TOP 1 IDGAIOLA FROM GAIOLACONFERENCIA GG  WITH (NOLOCK) WHERE GG.CODIGODEBARRAS = GC.CODIGODEBARRAS AND GC.IDGAIOLACONFERENCIA <> GG.IDGAIOLACONFERENCIA  AND GG.PERTENCEAFILIAL='SIM' ORDER BY 1 DESC), G.IDGAIOLA) [CÓDIGO GAIOLA LIDA] ,  ";

            sql += "isnull((SELECT TOP 1 IDGAIOLACONFERENCIA FROM GAIOLACONFERENCIA GG  WITH (NOLOCK) WHERE GG.CODIGODEBARRAS = GC.CODIGODEBARRAS AND GC.IDGAIOLACONFERENCIA <> GG.IDGAIOLACONFERENCIA AND GG.PERTENCEAFILIAL='SIM' ORDER BY 1 DESC), GC.IDGAIOLACONFERENCIA) GAIOLACONFERENCIALIDA  ";

            
            sql += " FROM GAIOLACONFERENCIA GC  WITH (NOLOCK) ";
            sql += " INNER JOIN GAIOLA G  WITH (NOLOCK) ON G.IDGAIOLA =GC.IDGAIOLA   ";
            sql += " INNER JOIN USUARIO U  WITH (NOLOCK) ON U.IDUSUARIO = G.IDUSUARIO   ";
            sql += " WHERE GC.ATIVO = 'SIM'  AND GC.SITUACAO LIKE  'PENDENCIA: VOLUME JA LIDO NA GAIOLA:%'   AND G.SITUACAO='PENDENCIA'  AND GC.PERTENCEAFILIAL='SIM' ";
            //sql += " AND (SELECT TOP 1 IDGAIOLA FROM GAIOLACONFERENCIA GG  WITH (NOLOCK) WHERE GG.CODIGODEBARRAS = GC.CODIGODEBARRAS AND GC.IDGAIOLACONFERENCIA <> GG.IDGAIOLACONFERENCIA  AND GG.PERTENCEAFILIAL='SIM' ORDER BY 1 DESC) IS NOT NULL ";
            sql += "and g.DataFechamento>='2016-08-25'";
            sql += " ORDER BY 2 ";

            GridView2.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            GridView2.DataBind();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string gaiola = e.CommandName.ToString().Split('|')[0];
            string gaiolaLida = e.CommandName.ToString().Split('|')[1];
            string IDgaiola = e.CommandName.ToString().Split('|')[2];


            if (e.CommandArgument.ToString() == "DeletarGaiola")
            {

                if (gaiola == gaiolaLida)
                {
                    string sql = "UPDATE GAIOLACONFERENCIA SET ATIVO='NAO' WHERE IDGAIOLACONFERENCIA=" + gaiola;
                    //sql += "; UPDATE GAIOLACONFERENCIA SET ATIVO='SIM' WHERE IDGAIOLACONFERENCIA=" + gaiolaLida;
                    sql += "; INSERT INTO GaiolaHistorico VALUES (" + IDgaiola + ", '" + gaiola + "', GETDATE(), 'EXCLUIU  VOLUME TELA DE DIVERGENCIA', " + ((DataTable)Session["User"]).Rows[0]["IDUSUARIO"].ToString() + ") ";
                    sql += "; UPDATE GAIOLA SET DATAFECHAMENTO=GETDATE(), SITUACAO='FECHADO' WHERE IDGAIOLA =" + IDgaiola + " AND (SELECT COUNT(*) FROM gaiolaConferencia WHERE IDGAIOLA=" + IDgaiola + " AND ISNULL(ATIVO, 'SIM')='SIM' AND SITUACAO LIKE '%PENDENCIA%')=0";

                    //sql += "; UPDATE GAIOLA SET DATAFECHAMENTO=NULL, SITUACAO='ABERTO' WHERE IDGAIOLA =" + IDgaiola + " AND (SELECT COUNT(*) FROM gaiolaConferencia WHERE IDGAIOLA=" + IDgaiola + " AND ISNULL(ATIVO, 'SIM')='SIM')=0";

                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);
                }
                else
                {
                    string sql = "UPDATE GAIOLACONFERENCIA SET ATIVO='NAO' WHERE IDGAIOLACONFERENCIA=" + gaiola;
                    sql += "; UPDATE GAIOLACONFERENCIA SET ATIVO='SIM' WHERE IDGAIOLACONFERENCIA=" + gaiolaLida;
                    sql += "; INSERT INTO GaiolaHistorico VALUES (" + IDgaiola + ", '" + gaiola + "', GETDATE(), 'EXCLUIU  VOLUME TELA DE DIVERGENCIA', " + ((DataTable)Session["User"]).Rows[0]["IDUSUARIO"].ToString() + ") ";
                    sql += "; UPDATE GAIOLA SET DATAFECHAMENTO=GETDATE(), SITUACAO='FECHADO' WHERE IDGAIOLA =" + IDgaiola + " AND (SELECT COUNT(*) FROM gaiolaConferencia WHERE IDGAIOLA=" + IDgaiola + " AND ISNULL(ATIVO, 'SIM')='SIM' AND SITUACAO LIKE '%PENDENCIA%')=0";

                    sql += "; UPDATE GAIOLA SET DATAFECHAMENTO=NULL, SITUACAO='ABERTO' WHERE IDGAIOLA =" + IDgaiola + " AND (SELECT COUNT(*) FROM gaiolaConferencia WHERE IDGAIOLA=" + IDgaiola + " AND ISNULL(ATIVO, 'SIM')='SIM')=0";

                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);
                }
            }
            else
            {
                string sql = "UPDATE GAIOLACONFERENCIA SET ATIVO='NAO' WHERE IDGAIOLACONFERENCIA=" + gaiolaLida;
                sql += "; UPDATE GAIOLACONFERENCIA SET ATIVO='SIM', SITUACAO='OK' WHERE IDGAIOLACONFERENCIA=" + gaiola;
                sql += "; INSERT INTO GaiolaHistorico VALUES (" + IDgaiola + ", '" + IDgaiola + "', GETDATE(), 'EXCLUIU  VOLUME TELA DE DIVERGENCIA', " + ((DataTable)Session["User"]).Rows[0]["IDUSUARIO"].ToString() + ") ";
                sql += "; UPDATE GAIOLA SET DATAFECHAMENTO=GETDATE(), SITUACAO='FECHADO' WHERE IDGAIOLA =" + IDgaiola + " AND (SELECT COUNT(*) FROM gaiolaConferencia WHERE IDGAIOLA=" + IDgaiola + " AND ISNULL(ATIVO, 'SIM')='SIM' AND SITUACAO LIKE '%PENDENCIA%')=0";
                
                sql += "; UPDATE GAIOLA SET DATAFECHAMENTO=NULL, SITUACAO='ABERTO' WHERE IDGAIOLA =" + IDgaiola + " AND (SELECT COUNT(*) FROM gaiolaConferencia WHERE IDGAIOLA=" + IDgaiola + " AND ISNULL(ATIVO, 'SIM')='SIM')=0";
                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);
            }
            CarregarGridPendencia();
        }
    }
}