using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using SistranBLL;
using Sistran;
public partial class frmEntregas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(Request.QueryString["tp"] != null)
            {
                RadTabStrip1.Tabs[0].Text = "Pedido";
                RadTabStrip1.Tabs[1].Text = "Itens";
            }

            rmp.TabIndex = 0;
            RadTabStrip1.SelectedIndex = 0;
            rpvStatusGeral.Selected = true;
            CarregarDadosNota();
            Button1.Attributes.Add("onClick", "window.open('frmRptDetalheNF.aspx?tipo=TELA&tit=Report', 'NovaJanela2', 'yes')");           
        }
    }

    private void CarregarDadosNota()
    {
        if (Session["Conn"] == null)
        {
            Session["Conn"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            HttpContext.Current.Session["ConnLogin"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
        }


        DataTable dt = NotasFiscais.NotaFiscalSelConsultar(Convert.ToInt32(Request.QueryString["idDoc"]), Session["Conn"].ToString());
        Session["dtNF"] = dt;

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["especial"].ToString() == "SIM")
            {
                lblClienteEspecial.Text = "ENTREGA ESPECIAL";
                
            }


            lblTipoDocumento.Text = dt.Rows[0]["TIPODEDOCUMENTO"].ToString();
            lblNumero.Text = dt.Rows[0]["NUMERO"].ToString();
            lblTipoServico.Text = dt.Rows[0]["TIPODESERVICO"].ToString();
            lblSerie.Text = dt.Rows[0]["SERIE"].ToString();

            lblCliente.Text = dt.Rows[0]["RAZAOSOCIALCLIENTE"].ToString();
            lblEnderecoCliente.Text = string.Format("{0} ,{1} - {2} - {3} - {4}", dt.Rows[0]["ENDERECOCLIENTE"].ToString(), dt.Rows[0]["NUMEROCLIENTE"].ToString(), dt.Rows[0]["COMPLEMENTOCLIENTE"].ToString(), dt.Rows[0]["CIDADECLIENTE"].ToString(), dt.Rows[0]["UFCLIENTE"].ToString());

            lblRemetente.Text ="<b>"+ dt.Rows[0]["CNPJCPFREMETENTE"].ToString() + "</b> - " + dt.Rows[0]["RAZAONOMEREMETENTE"].ToString();
            lblEnderecoRemetente.Text = string.Format("{0} ,{1} - {2} - {3} - {4}", dt.Rows[0]["ENDERECOREMETENTE"].ToString(), dt.Rows[0]["NUMEROREMETENTE"].ToString(), dt.Rows[0]["COMPLEMENTOREMETENTE"].ToString(), dt.Rows[0]["CIDADEREMETENTE"].ToString(), dt.Rows[0]["UFREMETENTE"].ToString());

            lblDestinatario.Text = "<b>" + dt.Rows[0]["CNPJCPFDESTINATARIO"].ToString() + "</b> - "  +dt.Rows[0]["RAZAONOMEDESTINATARIO"].ToString();
            lblEnderecoDestinatadio.Text = string.Format("{0} ,{1} - {2} - {3} - {4}", dt.Rows[0]["ENDERECODESTINATARIO"].ToString(), dt.Rows[0]["NUMERODESTINATARIO"].ToString(), dt.Rows[0]["COMPLEMENTODESTINATARIO"].ToString(), dt.Rows[0]["CIDADEDESTINATARIO"].ToString(), dt.Rows[0]["UFDESTINATARIO"].ToString());

            lblMotorista.Text = dt.Rows[0]["Motorista"].ToString();


            lblMovimento.Text = dt.Rows[0]["DATADOMOVIMENTO"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADOMOVIMENTO"].ToString().Replace(" 00:00:00", "");
            lblDataEntrada.Text = dt.Rows[0]["DATADEENTRADA"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADEENTRADA"].ToString().Replace(" 00:00:00", "");


            lblCancelamento.Text = dt.Rows[0]["DATADECANCELAMENTO"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADECANCELAMENTO"].ToString().Replace(" 00:00:00", "");
            lblAtivo.Text = dt.Rows[0]["ATIVO"].ToString();

            lblDataEmissao.Text = dt.Rows[0]["DATADEEMISSAO"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADEEMISSAO"].ToString().Replace(" 00:00:00", "");
            
            
            lblDataPlanejada.Text = dt.Rows[0]["DATAPLANEJADA"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATAPLANEJADA"].ToString().Replace(" 00:00:00", "");

            if (lblDataPlanejada.Text.Length > 0)
                lblDataPlanejada.Text = DateTime.Parse(lblDataPlanejada.Text).ToString("dd/MM/yyyy");

            
            lblDataDeConclusao.Text = dt.Rows[0]["DATADECONCLUSAO"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADECONCLUSAO"].ToString().Replace(" 00:00:00", "");
            lblCodBar.Text = dt.Rows[0]["CODIGODORECEXP"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["CODIGODORECEXP"].ToString();
            lblDataConcReceb.Text = dt.Rows[0]["DATADECONCLUSAORECEBIMENTO"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADECONCLUSAORECEBIMENTO"].ToString().Replace(" 00:00:00", "");

            lblEnderecoServ.Text = string.Format("{0} ,{1} - {2} - {3} - {4}", dt.Rows[0]["ENDERECO"].ToString(), dt.Rows[0]["ENDERECONUMERO"].ToString(), dt.Rows[0]["ENDERECOCOMPLEMENTO"].ToString(), dt.Rows[0]["CIDADE"].ToString(), dt.Rows[0]["ESTADO"].ToString());

            if (dt.Rows[0]["DATADEAGENDAMENTO"].ToString() != "")
                lblDataAgendamento.Text = DateTime.Parse(dt.Rows[0]["DATADEAGENDAMENTO"].ToString()).ToString("dd/MM/yyyy");

            if (lblDataPlanejada.Text.Length > 0)
            {
                //se for cliente especial e estiver em atraso 
                if (lblDataDeConclusao.Text.Length == 0 && lblClienteEspecial.Text.Length > 0)
                {
                    lblClienteEspecial.Text = "ENTREGA ESPECIAL";
                    lblClienteEspecial.ForeColor = System.Drawing.Color.Red;
                    tdDets.BgColor = "red";
                    tdDetsEnd.BgColor = "red";
                }

                if (lblDataDeConclusao.Text.Length > 0 && lblClienteEspecial.Text.Length > 0) 
                {
                    lblClienteEspecial.Text = "ENTREGA ESPECIAL";
                    lblClienteEspecial.ForeColor = System.Drawing.Color.Green;

                    tdDets.BgColor = "green";
                    tdDetsEnd.BgColor = "green";
                }

                //if (lblDataPlanejada.Text.Length > 0 && DateTime.Parse(lblDataPlanejada.Text) >= DateTime.Now && lblDataDeConclusao.Text.Length == 0 && lblClienteEspecial.Text.Length > 0)
                //{
                //    lblClienteEspecial.Text = "ENTREGA ESPECIAL";
                //    lblClienteEspecial.ForeColor = System.Drawing.Color.Green;

                //    tdDets.BgColor = "green";
                //    tdDetsEnd.BgColor = "green";
                //}
            }

            int M = 0;
            if (dt.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() != "")
                M = Convert.ToInt32(dt.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString());


            DataTable dt2 = NotasFiscais.Ocorrencia.OcorrenciaAtualListar(Convert.ToInt32(Request.QueryString["idDoc"]), M, Session["Conn"].ToString());
            Session["dtUltimaOcorrencia"] = dt2;

            if (dt2.Rows.Count > 0)
            {
                lblDataOc.Text = dt2.Rows[0]["DATADECONCLUSAO"].ToString() == "01/01/1900" ? "" : dt2.Rows[0]["DATADECONCLUSAO"].ToString().Replace(" 00:00:00", "");
                lblOco.Text = dt2.Rows[0]["CODIGO"].ToString();
                lblDescricaoOco.Text = dt2.Rows[0]["DESCRICAO"].ToString();
                lblFilial.Text = dt2.Rows[0]["NUMERODAFILIAL"].ToString();
                lblNomeFilial.Text = dt2.Rows[0]["NOME"].ToString();
            }
            else
            {
                lblDataOc.Text = "--";
                lblOco.Text = "--";
                lblDescricaoOco.Text = "--";
                lblFilial.Text = "--";
                lblNomeFilial.Text = "--";
            }

            DataTable dt3 = NotasFiscais.ListarOcorrenciaSelConsultar(Convert.ToInt32(Request.QueryString["idDoc"].ToString()), Session["Conn"].ToString());
            Session["dtOcorrencia"] = dt3;


            this.RadGrid1.DataSource = dt3;
            this.RadGrid1.DataBind();

            List<SistranMODEL.NotasFiscaisItens> LNotasFiscaisItens = new List<SistranMODEL.NotasFiscaisItens>();
            LNotasFiscaisItens = SistranBLL.NotasFiscaisItens.RetornaNotasFiscaisSaidaItens(Convert.ToInt32(Convert.ToInt32(Request.QueryString["idDoc"].ToString())), Session["Conn"].ToString());
            this.grdItens.DataSource = LNotasFiscaisItens;

            Session["dtItens"] = SistranBLL.NotasFiscaisItens.RetornarNotasFiscaisSaidaItens(Convert.ToInt32(Convert.ToInt32(Request.QueryString["idDoc"].ToString())), Session["Conn"].ToString());

            this.grdItens.DataBind();
            lblNumero0.Text = lblNumero.Text;
            lblAtivo0.Text = lblAtivo.Text;

            decimal totalItens = 0;
            decimal qtdeTotalItens = 0;

            foreach (var item in LNotasFiscaisItens)
            {
                totalItens = totalItens + item.Qtde;
                qtdeTotalItens = qtdeTotalItens + Convert.ToDecimal(item.Valor.ToString().Replace("R$", ""));
            }



            lblqtdTotal.Text = totalItens.ToString("N2");
            lblValortotal.Text = dt.Rows[0]["ValorDaNota"].ToString();
        }
    }

    protected void RadTabStrip1_TabClick(object sender, Telerik.Web.UI.RadTabStripEventArgs e)
    {
        if (e.Tab.Index == 3)
        {
        }
    }   
    
    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        HyperLink lnkFoto = (HyperLink)e.Item.FindControl("lnkFoto");
        Label lblIdDocOcorrencia = (Label)e.Item.FindControl("lblIdDocOcorrencia");

        if (lnkFoto != null)
        {
            lnkFoto.Visible = false;
            lblIdDocOcorrencia.Visible = false;

            if (lblIdDocOcorrencia.Text == "")
                lnkFoto.Visible = false;
            else
            {
                DataTable dtimg = NotasFiscais.RetornarImagemByDocumentoOcorrencia(Convert.ToInt32(lblIdDocOcorrencia.Text), Session["Conn"].ToString());

                if (dtimg.Rows.Count == 0)
                    lnkFoto.Visible = false;
                else
                {
                    byte[] imagem = (byte[])dtimg.Rows[0][0];
                    Session["imagem"] = imagem;
                    lnkFoto.Visible = true;
                }
            }
        }
    }
}