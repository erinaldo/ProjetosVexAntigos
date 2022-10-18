using System;
using System.Web.UI.WebControls;
using System.Data;
using Sistran.Library.Fatura;




namespace Sistecno.Web.Fatura
{
    public partial class PrintFatura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carregar();
            }
            Sistecno.Web.Fatura.PrintWeb.PrintWebControl(pnl1);
            //Printweb.PrintWebControl(pnl1);
            FaturaHistorico.gravarLog("imprimiu FATURA", Session["idtitulo"].ToString(), Session["cnx"].ToString());
        }

        private void Carregar()
        {
            //Dados da Filial

            DataSet ds = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC RetornaDadosFatura " + Session["idtitulo"], Session["cnx"].ToString());

            lblTotais.Text = ds.Tables[2].Rows.Count.ToString() + " Conhecimentos. Total: " + ds.Tables[2].Compute("Sum(Frete)", "");


            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                lblFilial.Text = ds.Tables[0].Rows[0]["RAZAOSOCIALNOME"].ToString();
                lblFilialNumeroFatura.Text = "DUPLICATA: " + ds.Tables[0].Rows[0]["NUMERODUPLICATA"].ToString();
                lblEndereco.Text = ds.Tables[0].Rows[0]["ENDERECO"].ToString() + " " + ds.Tables[0].Rows[0]["NUMERO"].ToString() + " " + ds.Tables[0].Rows[0]["CIDADEFILIAL"].ToString() + " " + ds.Tables[0].Rows[0]["UF"].ToString();
                lblCep.Text = ds.Tables[0].Rows[0]["CEP"].ToString();
                lblCNPJ.Text = ds.Tables[0].Rows[0]["CNPJCPF"].ToString();
                lblIE.Text = ds.Tables[0].Rows[0]["INSCRICAORG"].ToString();
                lblTelefone.Text = "TELEFONE: " + ds.Tables[0].Rows[0]["TELEFONE"].ToString();
                lblEmail.Text = "E-MAIL: " + ds.Tables[0].Rows[0]["EMAIL"].ToString();
                lblDataDeEmissao.Text = "DATA DE EMISSÃO: " + DateTime.Parse(ds.Tables[0].Rows[0]["DATADEEMISSAO"].ToString()).ToString("dd/MM/yyyy");

                this.Title = "DUPLICATA: " + ds.Tables[0].Rows[0]["NUMERODUPLICATA"].ToString();

                Label lblTitulo = (Label)Master.FindControl("lblTitulo");
                lblTitulo.Text = "DUPLICATA: " + ds.Tables[0].Rows[0]["NUMERODUPLICATA"].ToString();

            }
            
            Repeater1.DataSource = ds.Tables[1];
            Repeater1.DataBind();
            
            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                lblDesconto.Text = ds.Tables[1].Rows[0]["DESCONTO"].ToString();
                lblValorSobreDesconto.Text = ds.Tables[1].Rows[0]["VALOR"].ToString();
                lblNomeSacado.Text = ds.Tables[1].Rows[0]["RAZAOSOCIALNOME"].ToString();
                lblSacadoEndereco.Text = ds.Tables[1].Rows[0]["ENDERECO"].ToString() + ds.Tables[1].Rows[0]["NUMERO"].ToString();
                lblSacadoCidadeUf.Text = ds.Tables[1].Rows[0]["CIDADE"].ToString() + ds.Tables[1].Rows[0]["UF"].ToString();
                lblSacadoPracaPagamento.Text = ds.Tables[1].Rows[0]["CIDADE"].ToString() + ds.Tables[1].Rows[0]["UF"].ToString();
                lblSacadoCNPJ.Text = ds.Tables[1].Rows[0]["CNPJCPF"].ToString() + " - " + ds.Tables[1].Rows[0]["INSCRICAORG"].ToString();
            }
            
            Sistran.Library.Fatura.NumeroPorExtenso v = new Sistran.Library.Fatura.NumeroPorExtenso();
            v.SetNumero(Decimal.Parse(ds.Tables[1].Rows[0]["VALOR"].ToString()));
            lblValorExt.Text = v.ToString().ToUpper();


            rptNotas.DataSource = ds.Tables[2];
            rptNotas.DataBind();
        }

        string ctrc = "";
        bool jaImprimiu = false;

        protected void rptNotas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblCtr = (Label)e.Item.FindControl("lblCtr");
            Label lblCtrcEmissao = (Label)e.Item.FindControl("lblCtrcEmissao");
            Label lblCtrcNomeArquivo = (Label)e.Item.FindControl("lblCtrcNomeArquivo");
            Label lblCtrcFrete = (Label)e.Item.FindControl("lblCtrcFrete");
            Label lblCtrcDataGeracao = (Label)e.Item.FindControl("lblCtrcDataGeracao");
            Label lblCtrcIdNota = (Label)e.Item.FindControl("lblCtrcIdNota");
            Label lblCtrcCodigo = (Label)e.Item.FindControl("lblCtrcCodigo");
            Label lblDataDeEntrega = (Label)e.Item.FindControl("lblCtrcCodigo");
            LinkButton lnkRastreamento = (LinkButton)e.Item.FindControl("lnkRastreamento");
            LinkButton lnkBaixarXML = (LinkButton)e.Item.FindControl("lnkBaixarXML");
            Label lblIdDocumento = (Label)e.Item.FindControl("lblIdDocumento");
            Label lblIdDocumentoOcorrenciaArquivo = (Label)e.Item.FindControl("lblIdDocumentoOcorrenciaArquivo");
            LinkButton lnkComprovanteDeEntrega = (LinkButton)e.Item.FindControl("lnkComprovanteDeEntrega");

            
            if (lblCtr != null)
            {
                //if (ctrc != lblCtr.Text)
                //{
                //    ctrc = lblCtr.Text;
                //    jaImprimiu = true;
                //    lnkBaixarXML.Attributes.Add("onClick", "javasript:window.open('frmBaixarXML.aspx?idnota=" + lblCtrcIdNota.Text + "'); return false;");
                //    lnkRastreamento.Attributes.Add("onClick", "javasript:window.open('http://www.grupologos.com.br/SistranWeb.NET/frmEntregas.aspx?idDoc=" + lblIdDocumento.Text + "'); return false;");

                //    if (lblIdDocumentoOcorrenciaArquivo.Text == "")
                //    {
                //        lnkComprovanteDeEntrega.Visible = false;
                //    }
                //    else
                //    {
                //        lnkComprovanteDeEntrega.Visible = true;
                //        lnkComprovanteDeEntrega.Attributes.Add("onClick", "javasript:window.open('frmComprovanteEntrega.aspx?idDoc=" + lblIdDocumentoOcorrenciaArquivo.Text + "'); return false;");

                //    }
                //    return;
                //}

                if (lblCtr.Text == ctrc && jaImprimiu == true)
                {
                    lblCtr.Visible = false;
                    lblCtrcEmissao.Visible = false;
                    lblCtrcNomeArquivo.Visible = false;
                    lblCtrcFrete.Visible = false;
                    lblCtrcDataGeracao.Visible = false;
                    lblCtrcIdNota.Visible = false;
                    lblCtrcCodigo.Visible = false;
                    lblDataDeEntrega.Visible = false;
                    lnkBaixarXML.Visible = false;
                    lblIdDocumento.Visible = false;

                    if (lblIdDocumentoOcorrenciaArquivo.Text == "")
                    {
                        lnkComprovanteDeEntrega.Visible = false;
                    }
                    else
                    {
                        lnkComprovanteDeEntrega.Visible = true;
                        lnkComprovanteDeEntrega.Attributes.Add("onClick", "javasript:window.open('frmComprovanteEntrega.aspx?idDoc=" + lblIdDocumentoOcorrenciaArquivo.Text + "'); return false;");
                    }
                }
            }
        }
    }
}