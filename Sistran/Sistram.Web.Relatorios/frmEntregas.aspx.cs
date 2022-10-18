using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using SistranBLL;
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
            Button1.Attributes.Add("onClick", "FullWindow('frmRptDetalheNF.aspx?tipo=TELA&tit=Report', 'NovaJanela2', 'yes')");
            //List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU DETALHE DA NOTA FISCAL(IDDOCUMENTO=" + Request.QueryString["idDoc"].ToString() + ")", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
        }
    }

    private void CarregarDadosNota()
    {
        DataTable dt = NotasFiscais.NotaFiscalSelConsultar(Convert.ToInt32(Request.QueryString["idDoc"]), "");
        Session["dtNF"] = dt;

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


            DataTable dt2 = NotasFiscais.Ocorrencia.OcorrenciaAtualListar(Convert.ToInt32(Request.QueryString["idDoc"]), M,"");
            Session["dtUltimaOcorrencia"] = dt2;

            if (dt2.Rows.Count > 0)
            {
                lblDataOc.Text = dt2.Rows[0]["DATADECONCLUSAO"].ToString() == "01/01/1900" ? "" : dt2.Rows[0]["DATADECONCLUSAO"].ToString();
                lblOco.Text = dt2.Rows[0]["CODIGO"].ToString();
                lblDescricaoOco.Text = dt2.Rows[0]["DESCRICAO"].ToString();
                lblFilial.Text = dt2.Rows[0]["NUMERODAFILIAL"].ToString();
                lblNomeFilial.Text = dt2.Rows[0]["NOME"].ToString();
            }

            DataTable dt3 = NotasFiscais.ListarOcorrenciaSelConsultar(Convert.ToInt32(Request.QueryString["idDoc"].ToString()), "");
            Session["dtOcorrencia"] = dt3;


            this.RadGrid1.DataSource = dt3;
            this.RadGrid1.DataBind();

            List<SistranMODEL.NotasFiscaisItens> LNotasFiscaisItens = new List<SistranMODEL.NotasFiscaisItens>();
            LNotasFiscaisItens = SistranBLL.NotasFiscaisItens.RetornaNotasFiscaisSaidaItens(Convert.ToInt32(Convert.ToInt32(Request.QueryString["idDoc"].ToString())), "");
            this.grdItens.DataSource = LNotasFiscaisItens;

            Session["dtItens"] = SistranBLL.NotasFiscaisItens.RetornarNotasFiscaisSaidaItens(Convert.ToInt32(Convert.ToInt32(Request.QueryString["idDoc"].ToString())), "");

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