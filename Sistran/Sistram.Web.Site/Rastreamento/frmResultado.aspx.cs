using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;

public partial class Rastreamento_frmResultado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                rmp.TabIndex = 0;
                RadTabStrip1.SelectedIndex = 0;
                rpvStatusGeral.Selected = true;
                MontarGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Up, this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private void MontarGrid()
    {
        try
        {
            string[] Valores = Session["Valores"].ToString().Split('|');
            string[] conexoes = FuncoesGerais.CarregarConexoesRastreamento();
            DataTable dt = new DataTable("dtRastreamento");
            //dt.Columns.Add(new DataColumn("NUMERO"));
            dt.Columns.Add(new DataColumn("IDDOCUMENTO"));
            dt.Columns.Add(new DataColumn("TIPODEDOCUMENTO"));
            dt.Columns.Add(new DataColumn("DESTINATARIO"));
            dt.Columns.Add(new DataColumn("REMETENTE"));
            dt.Columns.Add(new DataColumn("REMESSA"));
            dt.Columns.Add(new DataColumn("DATAREMESSA", typeof(string)));
            dt.Columns.Add(new DataColumn("PEDIDO"));
            dt.Columns.Add(new DataColumn("NOTAFISCAL"));
            dt.Columns.Add(new DataColumn("STATUS"));

            for (int i = 0; i < conexoes.Length; i++)
            {
                if (conexoes[i] != "")
                {
                    DataTable dtRestTemp = SistranBLL.Rastreamento.Rastrear(Valores[1], Valores[0], conexoes[i]);

                    foreach (DataRow item in dtRestTemp.Rows)
                    {
                        DataRow rw;
                        rw = dt.NewRow();
                        rw[0] = item[0];
                        rw[1] = item[1];
                        rw[2] = item[2];
                        rw[3] = item[3];
                        rw[4] = item[4];
                        rw[5] = (item[5].ToString()==""?DateTime.Now.ToString(): Convert.ToDateTime(item[5]).ToShortDateString());
                        rw[6] = item[6];
                        rw[7] = item[7];
                        rw[8] = item[8];                        
                        dt.Rows.Add(rw);
                    }
                }
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();   
            Session["dt"] = dt;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Up, this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument.ToString() != "")
            {                
                rmp.TabIndex = 1;
                RadTabStrip1.SelectedIndex = 1;
                rpvListaEmp.Selected = true;
                lblIdDocumento.Text = e.CommandName.ToString();
                lblRemessa.Text = e.CommandArgument.ToString();
                CarregarCampos();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Up, this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private void CarregarCampos()
    {
        if (Session["dt"] != null)
        {
            DataTable dt = (DataTable)Session["dt"];
            DataRow[] orw = dt.Select("IDDOCUMENTO=" + lblIdDocumento.Text + " AND REMESSA='"+ lblRemessa.Text +"'" );

            if (orw.Length > 0)
            {
                lblNumeroRemessa.Text = orw[0]["REMESSA"].ToString();
                pnlCorreios.Visible = false;

                if (lblNumeroRemessa.Text.Trim().Length == 13 && lblNumeroRemessa.Text.Trim().Contains("BR"))
                {
                    PlaceHolder1.Controls.Add(new LiteralControl(@"<iframe id='I1' frameborder='0' height='500' name='I1' src='http://websro.correios.com.br/sro_bin/txect01$.Inexistente?P_LINGUA=001&P_TIPO=002&P_COD_LIS="+lblNumeroRemessa.Text+"'  width='600'></iframe>"));
                    pnlCorreios.Visible = true;
                }
            }

            string[] conexoes = FuncoesGerais.CarregarConexoesRastreamento();
            
            for (int i = 0; i < conexoes.Length; i++)
			{
                DataTable dtDemaisDados = SistranBLL.NotasFiscais.NotaFiscalSelConsultar(Convert.ToInt32(lblIdDocumento.Text), conexoes[i]);
                GridView2.DataSource = dtDemaisDados;
                GridView2.DataBind();
               
                if (dtDemaisDados.Rows.Count > 0)
                {

                    lblNumero.Text = dtDemaisDados.Rows[0]["NUMERO"].ToString();
                    lblRazoRemtente.Text = dtDemaisDados.Rows[0]["RAZAONOMEREMETENTE"].ToString();
                    lblEnderecoRemtente.Text = string.Format("{0},  {1}", dtDemaisDados.Rows[0]["ENDERECOREMETENTE"].ToString(), dtDemaisDados.Rows[0]["NUMEROREMETENTE"].ToString());
                    lblComplementoRemtente.Text = (dtDemaisDados.Rows[0]["COMPLEMENTOREMETENTE"].ToString() == "" ? "-" : dtDemaisDados.Rows[0]["COMPLEMENTOREMETENTE"].ToString());
                    lblCidadeRemtente.Text = string.Format("{0}  -  {1}", dtDemaisDados.Rows[0]["CIDADEREMETENTE"].ToString(), dtDemaisDados.Rows[0]["UFREMETENTE"].ToString());

                    lblRazoRemtente0.Text = dtDemaisDados.Rows[0]["RAZAONOMEDESTINATARIO"].ToString();
                    lblEnderecoRemtente0.Text = string.Format("{0},  {1}", dtDemaisDados.Rows[0]["ENDERECODESTINATARIO"].ToString(), dtDemaisDados.Rows[0]["NUMERODESTINATARIO"].ToString());
                    lblComplementoRemtente0.Text = (dtDemaisDados.Rows[0]["COMPLEMENTODESTINATARIO"].ToString() == "" ? "-" : dtDemaisDados.Rows[0]["COMPLEMENTODESTINATARIO"].ToString());
                    lblCidadeRemtente0.Text = string.Format("{0}  -  {1}", dtDemaisDados.Rows[0]["CIDADEDESTINATARIO"].ToString(), dtDemaisDados.Rows[0]["UFDESTINATARIO"].ToString());

                    lblDataEmissao.Text = Convert.ToDateTime(dtDemaisDados.Rows[0]["DATADEEMISSAO"]).ToShortDateString();
                    lblDataEntrada.Text = Convert.ToDateTime(dtDemaisDados.Rows[0]["DATADEENTRADA"]).ToShortDateString();
                    lblDataEntrega.Text = Convert.ToDateTime(dtDemaisDados.Rows[0]["DATADECONCLUSAO"]).ToShortDateString();
                    lblDataMovimento.Text = Convert.ToDateTime(dtDemaisDados.Rows[0]["DATADOMOVIMENTO"]).ToShortDateString();
                    lblDataPlanejada.Text = Convert.ToDateTime(dtDemaisDados.Rows[0]["DATAPLANEJADA"]).ToShortDateString();

                    lblDataEmissao.Text = (lblDataEmissao.Text == "01/01/1900" ? "-" : lblDataEmissao.Text);
                    lblDataEntrada.Text = (lblDataEntrada.Text == "01/01/1900" ? "-" : lblDataEntrada.Text);
                    lblDataEntrega.Text = (lblDataEntrega.Text == "01/01/1900" ? "-" : lblDataEntrega.Text);
                    lblDataMovimento.Text = (lblDataMovimento.Text == "01/01/1900" ? "-" : lblDataMovimento.Text);
                    lblDataPlanejada.Text = (lblDataPlanejada.Text == "01/01/1900" ? "-" : lblDataPlanejada.Text);

                    lblDataEmissao.Text = (lblDataEmissao.Text == "1/1/1900" ? "-" : lblDataEmissao.Text);
                    lblDataEntrada.Text = (lblDataEntrada.Text == "1/1/1900" ? "-" : lblDataEntrada.Text);
                    lblDataEntrega.Text = (lblDataEntrega.Text == "1/1/1900" ? "-" : lblDataEntrega.Text);
                    lblDataMovimento.Text = (lblDataMovimento.Text == "1/1/1900" ? "-" : lblDataMovimento.Text);
                    lblDataPlanejada.Text = (lblDataPlanejada.Text == "1/1/1900" ? "-" : lblDataPlanejada.Text);
                    
                    lblPesoBruto.Text = Convert.ToDecimal(dtDemaisDados.Rows[0]["PESOBRUTO"] == DBNull.Value ? 0.00 : dtDemaisDados.Rows[0]["PESOBRUTO"]).ToString("#0.00");
                    lblPesoLiquido.Text = Convert.ToDecimal(dtDemaisDados.Rows[0]["PESOLIQUIDO"] == DBNull.Value ? 0.00 : dtDemaisDados.Rows[0]["PESOLIQUIDO"]).ToString("#0.00");
                    lblVolumes.Text = Convert.ToDecimal(dtDemaisDados.Rows[0]["volumes"] == DBNull.Value ? 0.00 : dtDemaisDados.Rows[0]["volumes"]).ToString("#0.00");
                    lblMetragemCubica.Text = Convert.ToDecimal(dtDemaisDados.Rows[0]["MetragemCubica"] == DBNull.Value ? 0.00 : dtDemaisDados.Rows[0]["MetragemCubica"]).ToString("#0.00");

                    DataTable dtimagem = Sistran.Library.GetDataTables.PesquisarImagensDocumentoOcorrencia(lblIdDocumento.Text, conexoes[i]);
                                     


                    DataList1.DataSource = dtimagem;
                    DataList1.DataBind();


                    break;
                }
			 
			}

        }
    }

    protected void RadTabStrip1_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
    {
        try
        {
            if (RadTabStrip1.SelectedIndex == 0)
            {
                lblIdDocumento.Text = "";
                limparCampos();
            }
            else if (lblIdDocumento.Text == "" && RadTabStrip1.SelectedIndex == 1)
            {
                rmp.TabIndex = 0;
                RadTabStrip1.SelectedIndex = 0;
                rpvStatusGeral.Selected = true;
                throw new Exception("Clique sobre uma linha");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Up, this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    private void limparCampos()
    {
        lblNumero.Text = "";
        lblIdDocumento.Text = "";
        lblNumeroRemessa.Text = "";
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         
    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            ImageButton ImgbtVisualizar = (ImageButton)e.Row.FindControl("ImgbtVisualizar");
            ImgbtVisualizar.Attributes.Add("OnClick", "javascript:window.open('frmVerFotoDocumento.aspx?IDDocumentoImagem=" + ImgbtVisualizar.CommandName.ToString() + "')");
        }
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        dvFoto.Visible = true;

    }
    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        ImageButton Image1 = (ImageButton)e.Item.FindControl("ImageButton1");

        if (Image1 != null)
        {
            DataRowView r = (DataRowView)e.Item.DataItem;

            if (r != null)
            {
                byte[] v =(byte[]) r[0];
                MemoryStream ms = new MemoryStream(v);

                string nome = Guid.NewGuid().ToString();
                Bitmap m = new Bitmap(ms);
                m.Save(Server.MapPath("~\\imgReport\\" + nome + ".jpg"));
                Image1.ImageUrl = Server.MapPath("~\\imgReport\\" + nome + ".jpg");
                Image1.ImageUrl = "../imgReport/" + nome + ".jpg";
                Image2.ImageUrl = "../imgReport/" + nome + ".jpg";
               // Image1.Attributes.Add("onClick", "javascript:window.open('imagem.aspx?i='" + nome + "')");
                //Image1.OnClientClick = "javascript:window.open('imagem.aspx?i='"+nome+"')";
            }

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        dvFoto.Visible = false;

    }
}