using System;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Sistran.Library;
using Sistran.Library.Fatura;

namespace Sistecno.Web.Fatura
{
    public partial class Fatura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    if (Request.QueryString["b"] == null)
                        Session["cnx"] = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                    else
                        Session["cnx"] = new Sistran.Logins.Acesso().ConexaoPorNomeBase(Request.QueryString["b"].ToString());
                    

                    Carregar();

                    //Sistran.Library.EnviarEmails.EnviarEmailx("edvaldo@sistecno.com.br", "moises@sistecno.com.br", "### Acessou a Fatura ###", "Acessou o IdTitulo: " + Request.QueryString["idtitulo"] + "<br>IP: " + Request.UserHostAddress, "Fatura  Site");

                    FaturaHistorico.gravarLog("ACESSOU NA FATURA", Request.QueryString["idtitulo"], Session["cnx"].ToString());
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
        }
     

        private void Carregar()
        {
            //Acerta a data de emissao dos documento com a data do xml

            try
            {
                if(Session["cnx"].ToString().ToLower().Contains("grupologos"))
                 Sistran.Library.GetDataTables.ExecutarComandoSql("EXEC AcertaFaturaDocCob " + Request.QueryString["idtitulo"], Session["cnx"].ToString());
            }
            catch (Exception ex)
            { }
            

            //Dados da Filial

            DataSet ds = Sistran.Library.GetDataTables.RetornarDataSetWS("EXEC RetornaDadosFatura " + Request.QueryString["idtitulo"], Session["cnx"].ToString());
            Session["idtitulo"] = Request.QueryString["idtitulo"];

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
                lblSacadoCNPJ.Text = ds.Tables[1].Rows[0]["CNPJCPF"].ToString();
                lblSacadoIE.Text = ds.Tables[1].Rows[0]["INSCRICAORG"].ToString();
               

            }

            Sistran.Library.Fatura.NumeroPorExtenso v = new Sistran.Library.Fatura.NumeroPorExtenso();
                
                v.SetNumero(Decimal.Parse(ds.Tables[1].Rows[0]["VALOR"].ToString()));
                lblValorExt.Text = v.ToString().ToUpper();

            rptNotas.DataSource = ds.Tables[2];
            rptNotas.DataBind();

            int qtd = int.Parse(ds.Tables[2].Compute("COUNT(COMPROVANTEENTREGA)", "COMPROVANTEENTREGA >0").ToString());

            if (qtd == 0)
                btnBaixar.Enabled = false;
            else
                btnBaixar.Enabled = true;


            if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
            {
                byte[] imagem = (byte[])ds.Tables[3].Rows[0]["Imagem"];
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);

                if (File.Exists(MapPath("XmlGerados/") + ds.Tables[3].Rows[0]["IDCadastroImagem"] + ".jpg"))
                    File.Delete(MapPath("XmlGerados/") + ds.Tables[3].Rows[0]["IDCadastroImagem"] + ".jpg");

                returnImage.Save(Server.MapPath(@"XmlGerados/" + ds.Tables[3].Rows[0]["IDCadastroImagem"].ToString() + ".jpg"));
                

                returnImage.Dispose();
                ms.Dispose();
                imagem = null;

                Image imgLogoPrincipal = (Image)Master.FindControl("imgLogoPrincipal");

                if (imgLogoPrincipal != null)
                {
                    imgLogoPrincipal.ImageUrl = "XmlGerados/" + ds.Tables[3].Rows[0]["IDCadastroImagem"].ToString() + ".jpg";
                    Session["img"] = imgLogoPrincipal.ImageUrl;
                }


            }
                
        }

        string ctrc = "";
        bool jaImprimiu = false;
        bool existeCompEntrega = false;

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
            LinkButton lnkBaixarDacte = (LinkButton)e.Item.FindControl("lnkBaixarDacte");
            Label lblIdDocumento = (Label)e.Item.FindControl("lblIdDocumento");
            Label lblIdDocumentoOcorrenciaArquivo = (Label)e.Item.FindControl("lblIdDocumentoOcorrenciaArquivo");
            LinkButton lnkComprovanteDeEntrega = (LinkButton)e.Item.FindControl("lnkComprovanteDeEntrega");

            if (lblCtr != null)
            {
                if (ctrc != lblCtr.Text)
                {

                    if (string.IsNullOrEmpty(lblCtrcIdNota.Text))
                    {
                        lnkBaixarDacte.Visible = false;
                        lnkBaixarXML.Visible = false;
                        lnkRastreamento.Visible = false;
                    }

                    ctrc = lblCtr.Text;
                    jaImprimiu = true;
                    lnkBaixarXML.Attributes.Add("onClick", "javasript:window.open('frmBaixarXML.aspx?idnota=" + lblCtrcIdNota.Text + "'); return false;");
                    lnkBaixarDacte.Attributes.Add("onClick", "javasript:window.open('frmBaixarXMLDacte.aspx?idnota=" + lblCtrcIdNota.Text + "'); return false;");

                    if (Request.QueryString["b"] == null)
                    {
                        lnkRastreamento.Attributes.Add("onClick", "javasript:window.open('http://www2.logoslogistica.com.br/SistranWeb.NET/frmEntregas.aspx?idDoc=" + lblIdDocumento.Text + "'); return false;");
                        lnkRastreamento.Visible = true;
                    }
                    else
                    {
                        lnkRastreamento.Visible = false;
                    }
                     

                    if (lblIdDocumentoOcorrenciaArquivo.Text == "")
                    {
                        lnkComprovanteDeEntrega.Visible = false;
                    }
                    else
                    {
                        lnkComprovanteDeEntrega.Visible = true;
                        lnkComprovanteDeEntrega.Attributes.Add("onClick", "javasript:window.open('frmComprovanteEntrega.aspx?idDoc=" + lblIdDocumentoOcorrenciaArquivo.Text + "'); return false;");
                        existeCompEntrega = true;
                    }
                    return;
                }
                
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
                    lnkBaixarDacte.Visible = false;
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
