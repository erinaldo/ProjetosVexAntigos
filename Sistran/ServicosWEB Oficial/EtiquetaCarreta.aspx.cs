using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;

namespace ServicosWEB
{
    public partial class EtiquetaCarreta : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
                Response.Redirect("logingaiola.aspx?i="+ Guid.NewGuid());

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");
            cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            if (!IsPostBack)
            {
                txtData.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                CarregarFiliais();
                CarregarGrid();
            }           

        }

        private void CarregarGrid()
        {
            if(txtData.Text=="")
                txtData.Text = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");

            string sql = "select dt.IDDT [CÓDIGO], dt.Numero [NÚMERO], dt.Andamento [STATUS] from Dt where Andamento='LIBERADO PARA CARREGAMENTO' and IDFilial=12 and dt.Emissao>=" + DateTime.Parse(txtData.Text).ToString("yyyy-MM-dd") + " Order by Andamento";
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            GridView1.DataSource = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);
            GridView1.DataBind();
        }
                
        string cnx = "";
        private void carregarReport(string iddt)
        {
            try
            {
                string sql = "select  Dt.IDdt, v.Placa, Ltrim(rtrim(f.NomeComprovei)) NomeComprovei, c.RazaoSocialNome Motorista, DT.Emissao" +
                " from Dt" +
                " Inner join DTTransferencia df on df.IdDt = dt.IDDT" +
                " Inner join Filial f on f.IDFilial = df.IdFilial" +
                " Inner join Veiculo v on v.IDVeiculo = dt.IDPrimeiroVeiculo" +
                " Inner join Cadastro c on c.IDCadastro = v.IDMotorista" +
                " where dt.IDDT =" + iddt +
                " order by f.Nome";
                
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);

                dt.Columns.Add("Filiais");
                string cdsFil = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                    cdsFil += dt.Rows[i]["NomeComprovei"].ToString().Trim().Substring(0, 2).Trim() + "-";
                
                dt.Rows[0]["Filiais"] = cdsFil;

                this.rptViewer.Width = new Unit("100%");
                this.rptViewer.Height = 600;
                this.rptViewer.ZoomMode = ZoomMode.PageWidth;
                this.rptViewer.LocalReport.EnableHyperlinks = true;
                this.rptViewer.Reset();
                this.rptViewer.LocalReport.ReportPath = Server.MapPath("EtiquetaCarreta.rdlc");
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);
                this.rptViewer.LocalReport.DataSources.Clear();
                this.rptViewer.LocalReport.DataSources.Add(rds);
                this.rptViewer.DataBind();
                this.rptViewer.LocalReport.Refresh();
                this.rptViewer.ShowPrintButton = true;
                this.rptViewer.ShowZoomControl = true;
                this.rptViewer.LocalReport.ReportPath = Server.MapPath("EtiquetaCarreta.rdlc");

                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streamids;

                byte[] exportBytes = rptViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streamids, out warnings);
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = mimeType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + iddt + DateTime.Now.ToString() + "." + fileNameExtension);
                HttpContext.Current.Response.BinaryWrite(exportBytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + ex.InnerException + ex.Source);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() == "imprimir")
            {
                try
                {
                    carregarReport(e.CommandName.ToString());
                }
                catch (Exception)
                { }
            }
            else if (e.CommandArgument.ToString() == "apagar")
            {
                try
                {
                    string sql = "Delete from DtTransferencia where IdDt=" + e.CommandName + " ; Delete from dt where Iddt=" + e.CommandName;
                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    CarregarGrid();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message + ex.InnerException + ex.Source);
                }
            }
        }
        
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            System.Data.DataRowView ed = (System.Data.DataRowView)e.Row.DataItem;
            ImageButton ii = (ImageButton)e.Row.FindControl("btnexcluir");
            ImageButton ImageButton1 = (ImageButton)e.Row.FindControl("ImageButton1");
        }
            

        protected void GridView1_Unload(object sender, EventArgs e)
        {
            //GridView x = (GridView)sender;
            //lblQuantidade.Text = x.Rows.Count.ToString() +  " Gaiolas";

            //btnPesquisar0.Text = "Atualizar (" + x.Rows.Count.ToString() + " Gaiolas)";
        }

        protected void GridView1_Load(object sender, EventArgs e)
        {
            //GridView x = (GridView)sender;
            //lblQuantidade.Text = x.Rows.Count.ToString() + " Gaiolas";
        }

        #region Etiqueta Carreta
        string IdVeiculo = "";
        protected void txtPlaca_TextChanged1(object sender, EventArgs e)
        {
            lblMotorista.Text = "";

            if (txtPlaca.Text.Length == 8)
            {
                string sql = "select IDVeiculo, mot.RazaoSocialNome from Veiculo v inner join Cadastro mot  on mot.IDCadastro = v.IDMotorista  where Placa='" + txtPlaca.Text + "'";
                DataTable ret = Sistran.Library.GetDataTables.RetornarDataTable(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

                if (ret.Rows.Count > 0)
                {
                    lblMotorista.Text = ret.Rows[0]["RazaoSocialNome"].ToString();
                    lblIdVeiculo.Text = ret.Rows[0]["IDVeiculo"].ToString();
                }
                else
                {
                    lblMotorista.Text = "Veículo não encontrado";
                    txtPlaca.Text = "";
                    txtPlaca.Focus();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool selecionados = false;
                List<int> idsFiliais = new List<int>();

                for (int i = 0; i < chkFiliais.Items.Count; i++)
                {
                    if (chkFiliais.Items[i].Selected)
                    {
                        selecionados = true;
                        idsFiliais.Add(int.Parse(chkFiliais.Items[i].Value));
                    }
                }

                if (txtPlaca.Text.Length == 8 && selecionados)
                {
                    var num = this.Numerador(12, "DT");
                    var iddt = Sistran.Library.GetDataTables.RetornarIdTabela("dt", cnx);
                    string sql = "Insert into Dt (IDDT,IDFilial,Numero,IDDTTipo, Emissao, Andamento, IDPrimeiroVeiculo) values (" + iddt + ",12," + num + ",3, getdate(), 'LIBERADO PARA CARREGAMENTO', " + lblIdVeiculo.Text + ");";

                    for (int i = 0; i < idsFiliais.Count; i++)
                    {
                        sql += " insert into DTTransferencia (IdDt,IdFilial,Cliente,Ordem) values (" + iddt + "," + idsFiliais[i] + ",Null,0); ";
                    }

                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                    lblMotorista.Text = "Veículo não encontrado";
                    txtPlaca.Text = "";
                    CarregarGrid();
                    CarregarFiliais();
                    txtPlaca.Focus();
                }
            }
            catch (Exception EX)
            {
                lblMotorista.Text = EX.Message;
            }
        }

        private void CarregarFiliais()
        {
            string sql = "select IdFilial, Nome + ' => Reg.: ' +NomeComprovei Nome from Filial where Ativo='SIM' Order by 2";
            DataTable ret = Sistran.Library.GetDataTables.RetornarDataTable(sql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            chkFiliais.DataSource = ret;
            chkFiliais.DataTextField = "Nome";
            chkFiliais.DataValueField = "IdFilial";
            chkFiliais.DataBind();
        }        

        public int Numerador(int IdFilial, string Nome)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("SELECT PROXIMONUMERO FROM NUMERADOR WHERE IDFILIAL=" + IdFilial.ToString() + " AND NOME='" + Nome + "'");
            int ret = Sistran.Library.GetDataTables.ExecutarRetornoID(s.ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());

            if (ret == 0)
            {
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("numerador");

                string ssql = "INSERT INTO NUMERADOR(IDNUMERADOR, IDFILIAL, NOME, SERIE, PROXIMONUMERO) VALUES (" + ID + ", " + IdFilial.ToString() + ", '" + Nome.ToString() + "', 'Ped', (SELECT ISNULL(MAX(PROXIMONUMERO),0)+2 FROM NUMERADOR))";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(ssql, Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
                ret = 1;
            }
            else
            {
                string ssql = "UPDATE NUMERADOR SET PROXIMONUMERO = PROXIMONUMERO+1  WHERE IDFILIAL=" + IdFilial.ToString() + " AND Nome='" + Nome + "'";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(ssql.ToString(), Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos());
            }
            return ret;
        }

        #endregion

        protected void txtData_TextChanged(object sender, EventArgs e)
        {
            CarregarGrid();
        }
    }
}