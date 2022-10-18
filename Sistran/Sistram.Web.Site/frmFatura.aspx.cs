using System;
using Sistran;
using System.Data;
using System.Threading;
using System.Globalization;
using SistranBLL;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class frmFatura : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");
        if (!IsPostBack)
        {
            btnImprimirBoleto.Visible = false;

            if (this.Request.QueryString["b"] != null)
            {
                string bd = this.Request.QueryString["b"].ToString();
                //seta a conexao na sessao
                new Sistran.Logins.Acesso().ConexaoPorNomeBase(bd);


                //clicou em fatura
                if (this.Request.QueryString["idtituloDuplicata"] != null)
                {
                    lblTitulo.Text = "Detalhe Fatura";
                    DataTable dt = new SistranBLL.Fatura().Listar(this.Request.QueryString["idtituloDuplicata"].ToString());
                    pnlFatura.Visible = true;

                    string idTitulo = "";
                    foreach (DataRow item in dt.Rows)
                    {
                        idTitulo = item["IdTitulo"].ToString();
                        lblNumeroFatura.Text = item["NUMERO"].ToString();
                        lblRazaoSocial.Text = item["RAZAOSOCIALNOME"].ToString();
                        lblVencimento.Text = Convert.ToDateTime(item["DATADEVENCIMENTOREAL"]).ToShortDateString();
                        lblValor.Text = Convert.ToDecimal(item["VALOR"]).ToString("#0.00");

                        lblVencimento0.Text = DateTime.Now.ToShortDateString();
                        lblValor0.Text = Convert.ToDecimal(item["VALORATUALIZADO"]).ToString("#0.00");
                    }

                    if (idTitulo != "")
                    {
                        DataTable dt2 = new SistranBLL.Fatura().ListarbyIdTitulo(idTitulo);
                        gridFatura.DataSource = dt2;
                        gridFatura.DataBind();


                        DataTable dttemp = new SistranDAO.Fatura().Boleto(idTitulo);

                        if (dttemp.Rows.Count == 0 || dttemp.Rows[0]["NOSSONUMERO"] == DBNull.Value)
                        {
                            btnImprimirBoleto.Visible = false;
                            return;
                        }



                        btnImprimirBoleto.Visible = true;
                        btnImprimirBoleto.Attributes.Add("Onclick", "javascript:window.open('Boletos/boletosBanco.aspx?titulo=" + idTitulo + "')");
                    }

                }
                else if (this.Request.QueryString["iddocumento"] != null)
                {
                    lblTitulo.Text = "Detalhe Nota Fiscal";

                    pnlNfPrinc.Visible = true;
                    rmp.TabIndex = 0;
                    RadTabStrip1.SelectedIndex = 0;
                    rpvStatusGeral.Selected = true;
                    CarregarDadosNota();
                    Button1.Visible = false;
                }
            }
            else
            {
                return;
            }

        }
    }

    private void CarregarDadosNota()
    {
        DataTable dt = NotasFiscais.NotaFiscalSelConsultar(Convert.ToInt32(Request.QueryString["iddocumento"]), "");
        
        if (dt.Rows.Count > 0)
        {
            lblTipoDocumento.Text = dt.Rows[0]["TIPODEDOCUMENTO"].ToString();
            lblNumero.Text = dt.Rows[0]["NUMERO"].ToString();
            lblTipoServico.Text = dt.Rows[0]["TIPODESERVICO"].ToString();
            lblSerie.Text = dt.Rows[0]["SERIE"].ToString();

            lblCliente.Text = dt.Rows[0]["RAZAOSOCIALCLIENTE"].ToString();
            lblEnderecoCliente.Text = string.Format("{0} ,{1} - {2} - {3} - {4}", dt.Rows[0]["ENDERECOCLIENTE"].ToString(), dt.Rows[0]["NUMEROCLIENTE"].ToString(), dt.Rows[0]["COMPLEMENTOCLIENTE"].ToString(), dt.Rows[0]["CIDADECLIENTE"].ToString(), dt.Rows[0]["UFCLIENTE"].ToString());

            lblRemetente.Text = dt.Rows[0]["RAZAONOMEREMETENTE"].ToString();
            lblEnderecoRemetente.Text = string.Format("{0} ,{1} - {2} - {3} - {4}", dt.Rows[0]["ENDERECOREMETENTE"].ToString(), dt.Rows[0]["NUMEROREMETENTE"].ToString(), dt.Rows[0]["COMPLEMENTOREMETENTE"].ToString(), dt.Rows[0]["CIDADEREMETENTE"].ToString(), dt.Rows[0]["UFREMETENTE"].ToString());

            lblDestinatario.Text = dt.Rows[0]["RAZAONOMEDESTINATARIO"].ToString();
            lblEnderecoDestinatadio.Text = string.Format("{0} ,{1} - {2} - {3} - {4}", dt.Rows[0]["ENDERECODESTINATARIO"].ToString(), dt.Rows[0]["NUMERODESTINATARIO"].ToString(), dt.Rows[0]["COMPLEMENTODESTINATARIO"].ToString(), dt.Rows[0]["CIDADEDESTINATARIO"].ToString(), dt.Rows[0]["UFDESTINATARIO"].ToString());


            lblMovimento.Text = dt.Rows[0]["DATADOMOVIMENTO"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADOMOVIMENTO"].ToString();
            lblDataEntrada.Text = dt.Rows[0]["DATADEENTRADA"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADEENTRADA"].ToString();


            lblCancelamento.Text = dt.Rows[0]["DATADECANCELAMENTO"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADECANCELAMENTO"].ToString();
            lblAtivo.Text = dt.Rows[0]["ATIVO"].ToString();

            lblDataEmissao.Text = dt.Rows[0]["DATADEEMISSAO"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADEEMISSAO"].ToString();
            lblDataPlanejada.Text = dt.Rows[0]["DATAPLANEJADA"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATAPLANEJADA"].ToString();

            lblDataConc.Text = dt.Rows[0]["DATADECONCLUSAO"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADECONCLUSAO"].ToString();
            lblCodBar.Text = dt.Rows[0]["CODIGODORECEXP"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["CODIGODORECEXP"].ToString();
            lblDataConcReceb.Text = dt.Rows[0]["DATADECONCLUSAORECEBIMENTO"].ToString() == "01/01/1900" ? "" : dt.Rows[0]["DATADECONCLUSAORECEBIMENTO"].ToString();

            lblEnderecoServ.Text = string.Format("{0} ,{1} - {2} - {3} - {4}", dt.Rows[0]["ENDERECO"].ToString(), dt.Rows[0]["ENDERECONUMERO"].ToString(), dt.Rows[0]["ENDERECOCOMPLEMENTO"].ToString(), dt.Rows[0]["CIDADE"].ToString(), dt.Rows[0]["ESTADO"].ToString());

            int M = 0;
            if (dt.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString() != "")
                M = Convert.ToInt32(dt.Rows[0]["IDDOCUMENTOOCORRENCIA"].ToString());
            
            DataTable dt2 = NotasFiscais.Ocorrencia.OcorrenciaAtualListar(Convert.ToInt32(Request.QueryString["iddocumento"]), M, "");
            

            if (dt2.Rows.Count > 0)
            {
                lblDataOc.Text = dt2.Rows[0]["DATADECONCLUSAO"].ToString() == "01/01/1900" ? "" : dt2.Rows[0]["DATADECONCLUSAO"].ToString();
                lblOco.Text = dt2.Rows[0]["CODIGO"].ToString();
                lblDescricaoOco.Text = dt2.Rows[0]["DESCRICAO"].ToString();
                lblFilial.Text = dt2.Rows[0]["NUMERODAFILIAL"].ToString();
                lblNomeFilial.Text = dt2.Rows[0]["NOME"].ToString();
            }

            DataTable dt3 = NotasFiscais.ListarOcorrenciaSelConsultar(Convert.ToInt32(Request.QueryString["iddocumento"].ToString()), "");
            
            this.RadGrid1.DataSource = dt3;
            this.RadGrid1.DataBind();

            List<SistranMODEL.NotasFiscaisItens> LNotasFiscaisItens = new List<SistranMODEL.NotasFiscaisItens>();
            LNotasFiscaisItens = SistranBLL.NotasFiscaisItens.RetornaNotasFiscaisSaidaItens(Convert.ToInt32(Convert.ToInt32(Request.QueryString["iddocumento"].ToString())), "");
            this.grdItens.DataSource = LNotasFiscaisItens;

            Session["dtItens"] = SistranBLL.NotasFiscaisItens.RetornarNotasFiscaisSaidaItens(Convert.ToInt32(Convert.ToInt32(Request.QueryString["iddocumento"].ToString())), "");

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
                DataTable dtimg = NotasFiscais.RetornarImagemByDocumentoOcorrencia(Convert.ToInt32(lblIdDocOcorrencia.Text), "");

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
