using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using SistranBLL;
using System.IO;

public partial class frmProjetoDetalhe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
        CultureInfo culture = new CultureInfo("pt-BR");


        if (!IsPostBack)
        {
            RadTabStrip1.SelectedIndex = 0;
            rmp.PageViews[1].Selected = true;
            rmp.PageViews[0].Selected = true;

            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text, System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
            CarregarCboFilial(cboFilial);
            CarregarCboFilial(cboFilialRecebimento);
            CarregarCboFilial(cboFilialEntrega);

            txtFatorCaixa.Attributes.Add("onkeypress", "return SomenteNumero(event)");
            txtFatorPallet.Attributes.Add("onkeypress", "return SomenteNumero(event)");
            txtFeteKit.Attributes.Add("onkeypress", "return SomenteNumero(event)");
            txtMaodeObra.Attributes.Add("onkeypress", "return SomenteNumero(event)");
            txtPessoasTurno.Attributes.Add("onkeypress", "return SomenteNumero(event)");
            txtTempoProducao.Attributes.Add("onkeypress", "return SomenteNumero(event)");
            txtTurnos.Attributes.Add("onkeypress", "return SomenteNumero(event)");
            txtTotalKits.Attributes.Add("onkeypress", "return SomenteNumero(event)");

            txtMaodeObra.Attributes.Add("onkeypress", "return SomenteNumero(event)");
            txtQuantidadeEfetuada.Attributes.Add("onkeypress", "return SomenteNumero(event)");
            txtTurno.Attributes.Add("onkeypress", "return SomenteNumero(event)");



            if (Request.QueryString["id"] == null)
                 lblId.Text = "0";
            else
            {
                lblId.Text = Request.QueryString["id"].ToString();
                carregarTela();                
            }

            carregarItensProducao();
            carregarGridItens();
            carregarArquivos();
            CarregarDadosFilial("RECEBIMENTO", lstFilialRecebimento);
            CarregarDadosFilial("ENTREGA", lstFilialEntrega);
        }
    }

    private void CarregarDadosFilial(string tipo, ListBox list)
    {
        string strsql = "SELECT FL.NOME, FL.IDFILIAL FROM PROJETOFILIAL PF INNER JOIN FILIAL FL ON FL.IDFILIAL = PF.IDFILIAL WHERE TIPO='"+tipo+"' AND IDPROJETO=" + lblId.Text ;
        DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        list.DataSource = dt;
        list.DataTextField = "NOME";
        list.DataValueField = "IDFILIAL";
        list.DataBind();
    }

    private void carregarArquivos()
    {
        string strsql = "SELECT * FROM PROJETOARQUIVO WHERE IDPROJETO=" + lblId.Text;
        DataTable dtImages = Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        Session["dtImages"] = dtImages;
        grdArquivos.DataSource = dtImages;
        grdArquivos.DataBind();
    }

    private void carregarItensProducao()
    {
        string strsql = "SELECT  ";
        strsql += " IDPROJETOPRODUCAO, ";
        strsql += " IDPROJETO, ";
        strsql += " LANCAMENTO, ";
        strsql += " TURNO, ";
        strsql += " PROJETOPRODUCAO.IDUSUARIO, ";
        strsql += " USUARIO.NOME NOMEUSUARIO, ";
        strsql += " HORAINICIAL, ";
        strsql += " HORAFINAL, ";
        strsql += " MAODEOBRA, ";
        strsql += " QUANTIDADEEFETUADA ";
        strsql += " FROM PROJETOPRODUCAO ";
        strsql += " LEFT JOIN USUARIO ON USUARIO.IDUSUARIO = PROJETOPRODUCAO.IDUSUARIO ";
        strsql += " WHERE IDPROJETO= " + (lblId.Text == "" ? "0" : lblId.Text);
        strsql += " ORDER BY LANCAMENTO DESC ";

        DataTable dtItensProducao = Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        Session["dtItensProducao"] = dtItensProducao;
        grdProducao.DataSource = dtItensProducao;
        grdProducao.DataBind();
    }

    private void carregarGridItens()
    {
        string strsql = "SELECT ";
        strsql += " IDPROJETOITEM,  ";
        strsql += " IDPROJETO,  ";
        strsql += " CODIGO,  ";
        strsql += " DESCRICAO, ";
        strsql += " QUANTIDADE, ";
        strsql += " QUANTIDADERECEBIDA, ";
        strsql += " ULTIMORECEBIMENTO ";
        strsql += " FROM PROJETOITEM WHERE IDPROJETO = " + (lblId.Text == "" ? "0" : lblId.Text);
        DataTable dtItens = Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        Session["dtItens"] = dtItens;

        grdItens.DataSource = dtItens;
        grdItens.DataBind();
    }

    private void carregarTela()
    {
        DataTable dt = new Projeto().Filtrar(int.Parse(Request.QueryString["id"].ToString()));

        if (dt.Rows.Count > 0)
        {
            cboFilial.SelectedValue = dt.Rows[0]["IDFILIAL"].ToString();
            cboAreaClimatizada.SelectedValue = dt.Rows[0]["UTILIZAAREACLIMATIZADA"].ToString();
            txtContatoCliente.Text = dt.Rows[0]["CONTATOCLIENTE"].ToString();
            txtContatoContratado.Text = dt.Rows[0]["CONTATOCONTRATADO"].ToString();
            txtFatorCaixa.Text = dt.Rows[0]["FATORPORCAIXA"].ToString();
            txtNome.Text = dt.Rows[0]["NOME"].ToString();
            txtInicioProducao.Text = dt.Rows[0]["INICIODAPRODUCAO"].ToString();
            txtFinalProducao.Text = dt.Rows[0]["FINALDAPRODUCAO"].ToString();
            txtInicioEntrega.Text = dt.Rows[0]["INICIODAENTREGA"].ToString();
            txtFimEntrega.Text = dt.Rows[0]["FINALDAENTREGA"].ToString();
            txtTotalKits.Text = dt.Rows[0]["TOTALDEKITS"].ToString();
            txtFatorPallet.Text = dt.Rows[0]["FATORPORPALLET"].ToString();
            txtPesoKit.Text = decimal.Parse((dt.Rows[0]["PESOPORKIT"] == DBNull.Value ? "0" : dt.Rows[0]["PESOPORKIT"].ToString())).ToString("#0.00");
            txtFeteKit.Text = dt.Rows[0]["FRETEPORKIT"].ToString();
            txtTempoProducao.Text = dt.Rows[0]["TEMPODEPRODUCAO"].ToString();
            txtTurnos.Text = dt.Rows[0]["TURNOS"].ToString();
            txtPessoasTurno.Text = dt.Rows[0]["PESSOASPORTURNO"].ToString();
            txtMaodeObra.Text = dt.Rows[0]["MAODEOBRA"].ToString();
            txtStatus.Text = dt.Rows[0]["STATUS"].ToString();

            txtVencimentoFaturamento.Text = dt.Rows[0]["Vencimento"].ToString();
            txtOrdemColeta.Text = dt.Rows[0]["OrigemDaColeta"].ToString();
            txtValorColeta.Text = dt.Rows[0]["ValorPorColeta"].ToString();
            txtPlanejamentoTransferencia.Text = dt.Rows[0]["PlanejamentoDeTransferencia"].ToString();
            txtObservacao.Text = dt.Rows[0]["Observacao"].ToString();

            cboEdi.SelectedValue = dt.Rows[0]["Edi"].ToString();
            cboComprovantes.SelectedValue = dt.Rows[0]["DigitalizarComprovante"].ToString();
            cboCTRC.SelectedValue = dt.Rows[0]["EmitirCTRC"].ToString();
            cboEmitirNota.SelectedValue = dt.Rows[0]["EmitirNotaFiscalServico"].ToString();
            txtFormaDeFaturamento.Text = dt.Rows[0]["FormaFaturamento"].ToString();


        }
    }

    private void CarregarCboFilial(DropDownList cbo)
    {
        cbo.DataSource = new SistranDAO.Filial().ListarTodasFiliais(Session["Conn"].ToString());
        cbo.DataValueField = "VALOR";
        cbo.DataTextField = "NOME";
        cbo.DataBind();
        cbo.Items.Insert(0, new ListItem("SELECIONE", ""));
    }

    protected void txtNome5_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            Session["filialRecebimento"] = lstFilialRecebimento;
            Session["filialEntrega"] = lstFilialEntrega;

            if (cboFilial.SelectedValue == "" || cboFilial.SelectedValue == "0")
                throw new Exception("Informe a filial."); ;


            if (DateTime.Parse(txtFinalProducao.Text) < DateTime.Parse(txtInicioProducao.Text))
            {
                txtInicioProducao.Focus();
                throw new Exception("Data Inicial Produção não pode ser maior que a final."); ;
            }

            if (DateTime.Parse(txtFimEntrega.Text) < DateTime.Parse(txtInicioEntrega.Text))
            {
                txtInicioEntrega.Focus();
                throw new Exception("Data Inicial de Entrega não pode ser maior que a final."); ;
            }

            SistranBLL.Projeto prj = new SistranBLL.Projeto();
            lblId.Text = prj.InserirAlterar(
                (
                        lblId.Text == "" ? 0 : int.Parse(lblId.Text)),
                        int.Parse(cboFilial.SelectedValue),
                        txtNome.Text.Trim().ToUpper(),
                        txtContatoCliente.Text.Trim().ToUpper(),
                        txtContatoContratado.Text.ToUpper().Trim(),
                        cboAreaClimatizada.SelectedItem.Text,
                        DateTime.Parse(txtInicioProducao.Text),
                        DateTime.Parse(txtFinalProducao.Text),
                        DateTime.Parse(txtInicioEntrega.Text),
                        DateTime.Parse(txtFimEntrega.Text),
                        int.Parse(txtTotalKits.Text),
                        int.Parse(txtFatorCaixa.Text),
                        int.Parse(txtFatorPallet.Text),
                        decimal.Parse(txtPesoKit.Text),
                        decimal.Parse(txtFeteKit.Text),
                        decimal.Parse(txtTempoProducao.Text),
                        int.Parse(txtTurnos.Text),
                        int.Parse(txtPessoasTurno.Text),
                        int.Parse(txtMaodeObra.Text),
                        txtStatus.Text.Trim().ToUpper(),
                        (DataTable)Session["dtItens"], (DataTable)Session["dtItensProducao"],
                        cboEdi.SelectedItem.Text.ToUpper(),
                        cboComprovantes.SelectedItem.Text.ToUpper(),
                        cboCTRC.SelectedItem.Text.ToUpper(),
                        cboEmitirNota.SelectedItem.Text.ToUpper(),
                        txtFormaDeFaturamento.Text.ToUpper().Trim(),
                        txtVencimentoFaturamento.Text.ToUpper().Trim(),
                        txtOrdemColeta.Text.ToUpper().Trim(),
                        txtValorColeta.Text.ToUpper().Trim(),
                        txtPlanejamentoTransferencia.Text.ToUpper().Trim(),
                        txtObservacao.Text.ToUpper().Trim()
                ).ToString();


            DataTable dtImagens = (DataTable)Session["dtImages"];
            Sistran.Library.GetDataTables.ExecutarSemRetorno("DELETE FROM PROJETOARQUIVO WHERE IDPROJETO=" + lblId.Text, "");

            if (dtImagens.Rows.Count > 0)
            {
                for (int i = 0; i < dtImagens.Rows.Count; i++)
                {
                    string[] ar = dtImagens.Rows[i]["Nome"].ToString().Split('.');
                    Sistran.Library.GetDataTables.InserirArquivosProjeto(lblId.Text, (byte[])dtImagens.Rows[i]["Arquivo"], dtImagens.Rows[i]["Descricao"].ToString(), dtImagens.Rows[i]["Nome"].ToString(), ar[ar.Length - 1], dtImagens.Rows[i]["IDPROJETOARQUIVO"].ToString());
                }
            }

            Response.Redirect("frmProjeto.aspx");
        }
        catch (Exception ex)
        {
           // ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmProjeto.aspx");

    }

    protected void btnNovoItem_Click(object sender, EventArgs e)
    {
        pnlNovoItem.Visible = true;
        btnNovoItem.Visible = false;
        grdItens.Visible = false;
        txtItemCodigo.Focus();

    }

    protected void btnCancelarrItem_Click(object sender, EventArgs e)
    {
        pnlNovoItem.Visible = false;
        btnNovoItem.Visible = true;
        grdItens.Visible = true;
    }

    protected void btnConfirmarItem_Click(object sender, EventArgs e)
    {
        if (txtItemCodigo.Text == "" || txtItemDescricao.Text.ToUpper().Trim() == "" || txtItemQuantidade.Text == "" || txtItemQuantidadeRecebida.Text == "" || txtItemUltimoRecebimento.Text == "")
        {
            lblIdProjetoItem.Text = "Imforme os campos corretamente.";
            return;
        }

        DataTable dtItens = (DataTable)Session["dtItens"];
        DataRow rw = dtItens.NewRow();
        rw[0] = 0;
        rw[1] = lblId.Text.ToUpper().Trim();
        rw[2] = txtItemCodigo.Text;
        rw[3] = txtItemDescricao.Text.ToUpper().Trim();
        rw[4] = int.Parse(txtItemQuantidade.Text);
        rw[5] = int.Parse(txtItemQuantidadeRecebida.Text);
        rw[6] = DateTime.Parse(txtItemUltimoRecebimento.Text);
        dtItens.Rows.Add(rw);

        Session["dtItens"] = dtItens;
        grdItens.DataSource = dtItens;
        grdItens.DataBind();

        pnlNovoItem.Visible = false;
        btnNovoItem.Visible = true;
        grdItens.Visible = true;
    }

    protected void grdItens_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtItens = (DataTable)Session["dtItens"];
        for (int i = 0; i < dtItens.Rows.Count; i++)
        {
            if (dtItens.Rows[i]["CODIGO"].ToString() == e.CommandName.ToString())
            {
                dtItens.Rows[i].Delete();
                dtItens.AcceptChanges();
                Session["dtItens"] = dtItens;
                grdItens.DataSource = dtItens;
                grdItens.DataBind();
                return;
            }
        }


    }

    protected void btnConfirmarProducao_Click(object sender, EventArgs e)
    {
        try
        {


            if (txtTurno.Text.Trim().Length == 0 || txtHoraIni.Text.Trim().Length == 0 || txtHoraFim.Text.Trim().Length == 0 || txtQuantidadeEfetuada.Text.Trim().Length == 0)
            {
                lblIdProjetoItem0.Text = "Informe os campos corretamente.";
            }


            string[] hi = txtHoraIni.Text.Split(':');
            string[] hf = txtHoraFim.Text.Split(':');

            DateTime horai = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(hi[0]), int.Parse(hi[1]), 0);
            DateTime horaf = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(hf[0]), int.Parse(hf[1]), 0);



            DataTable dtItensProducao = (DataTable)Session["dtItensProducao"];
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];

            DataRow r = dtItensProducao.NewRow();

            r[0] = "0";
            r[1] = lblId.Text;
            r[2] = DateTime.Now;
            r[3] = txtTurno.Text;
            r[4] = ILusuario[0].UsuarioId;
            r[5] = ILusuario[0].UsuarioNome;


            r[6] = horai;
            r[7] = horaf;
            r[8] = txtMaodeObra.Text;
            r[9] = txtQuantidadeEfetuada.Text;

            dtItensProducao.Rows.Add(r);
            Session["dtItensProducao"] = dtItensProducao;
            grdProducao.DataSource = dtItensProducao;
            grdProducao.DataBind();

            pnlNovaProducao.Visible = false;
            btnNovaProducao.Visible = true;
            grdProducao.Visible = true;
        }
        catch (Exception)
        {

            lblIdProjetoItem0.Text = "Informe os campos corretamente.";
        }
    }

    protected void btnCancelarProducao_Click(object sender, EventArgs e)
    {
        pnlNovaProducao.Visible = false;
        btnNovaProducao.Visible = true;
        grdProducao.Visible = true;
    }

    protected void btnNovaProducao_Click(object sender, EventArgs e)
    {
        pnlNovaProducao.Visible = true;
        btnNovaProducao.Visible = false;
        grdProducao.Visible = false;
    }

    protected void btnNovoArquivo_Click(object sender, EventArgs e)
    {
        //btnNovoArquivo.Visible = false;
        //pnlNovoArquivo.Visible = true;
    }

    protected void btnConfirmarArquivo_Click(object sender, EventArgs e)
    {

        if (FileUpload1.HasFile && txtDescricao.Text.Trim() != "")
        {
            int intTamanho = System.Convert.ToInt32(FileUpload1.PostedFile.InputStream.Length);

            byte[] imageBytes = new byte[intTamanho];
            FileUpload1.PostedFile.InputStream.Read(imageBytes, 0, intTamanho);

            DataTable dtImages = (DataTable)Session["dtImages"];

            DataRow rw = dtImages.NewRow();

            rw[0] = Sistran.Library.GetDataTables.RetornarIdTabela("PROJETOARQUIVO");
            rw[1] = lblId.Text;
            rw[2] = txtDescricao.Text.Trim().ToUpper();
            rw[3] = DateTime.Now;
            rw[4] = imageBytes;
            rw[5] = FileUpload1.FileName;

            dtImages.Rows.Add(rw);


            grdArquivos.DataSource = dtImages;
            grdArquivos.DataBind();
            Session["dtImages"] = dtImages;
            txtDescricao.Text = "";

        }
    }

    protected void grdArquivos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() == "ExcluirArquivo")
        {
            DataTable dti = (DataTable)Session["dtImages"];

            for (int i = 0; i < dti.Rows.Count; i++)
            {
                if (e.CommandName.ToString() == dti.Rows[i]["IDPROJETOARQUIVO"].ToString())
                {
                    dti.Rows[i].Delete();
                    dti.AcceptChanges();
                    Session["dtImages"] = dti;

                    grdArquivos.DataSource = dti;
                    grdArquivos.DataBind();

                    //carregarArquivos();
                    return;

                }
            }

        }

    }

    protected void grdArquivos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton btnVerArquivo = (ImageButton)e.Row.FindControl("btnVerArquivo");

        if (btnVerArquivo != null)
        {
            btnVerArquivo.Attributes.Add("onclick", "javascript:window.open('GerarProjetoArquivo.aspx?i=" + btnVerArquivo.CommandName.ToString() + "')");

        }
    }
 
    protected void btnAdicionarFilial_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstFilialRecebimento.Items.Count; i++)
        {
            if (lstFilialRecebimento.Items[i].Value == cboFilialRecebimento.SelectedValue)
                return;
        }
        lstFilialRecebimento.Items.Add(new ListItem(cboFilialRecebimento.SelectedItem.Text, cboFilialRecebimento.SelectedValue));
        lstFilialRecebimento.Visible = true;

        
    }

    protected void btnRemoverFilial_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstFilialRecebimento.Items.Count; i++)
        {
            if (lstFilialRecebimento.Items[i].Value == lstFilialRecebimento.SelectedValue)
            {
                lstFilialRecebimento.Items.RemoveAt(i);
                return;
            }
        }
    }

    protected void btnAdicionarFilialEntrega_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstFilialEntrega.Items.Count; i++)
        {
            if (lstFilialEntrega.Items[i].Value == cboFilialEntrega.SelectedValue)
                return;
        }
        lstFilialEntrega.Items.Add(new ListItem(cboFilialEntrega.SelectedItem.Text, cboFilialEntrega.SelectedValue));
        lstFilialEntrega.Visible = true;
    }

    protected void btnRemoverFilialEntrega_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstFilialEntrega.Items.Count; i++)
        {
            if (lstFilialEntrega.Items[i].Value == lstFilialEntrega.SelectedValue)
            {
                lstFilialEntrega.Items.RemoveAt(i);
                return;
            }
        }
    }
}