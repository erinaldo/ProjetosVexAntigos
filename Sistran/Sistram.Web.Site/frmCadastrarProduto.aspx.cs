using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Data;
using System.IO;

public partial class frmCadastrarProduto : System.Web.UI.Page
{
    protected void txtCódigo_TextChanged(object sender, EventArgs e)
    {
        if (txtCodBarras.Text.Length > 3)
        {
            DataTable dtBarras = new Produto().ConsultarCodigoBarras(txtCodBarras.Text);

            if (dtBarras.Rows.Count > 0)
            {
                txtAltura.Text = Convert.ToDouble(dtBarras.Rows[0]["ALTURA"]).ToString("#0.000");
                txtLargura.Text = Convert.ToDouble(dtBarras.Rows[0]["LARGURA"]).ToString("#0.000");
                txtComprimento.Text = Convert.ToDouble(dtBarras.Rows[0]["COMPRIMENTO"]).ToString("#0.000");
                txtPesoBruto.Text = Convert.ToDouble(dtBarras.Rows[0]["PESOBRUTO"]).ToString("#0.000");
                txtPesoLiquido.Text = Convert.ToDouble(dtBarras.Rows[0]["PESOLIQUIDO"]).ToString("#0.000");

                txtAltura.Enabled = false;
                txtLargura.Enabled = false;
                txtComprimento.Enabled = false;
                txtPesoBruto.Enabled = false;
                txtPesoLiquido.Enabled = false;
                IdProduto = Convert.ToInt32(dtBarras.Rows[0]["IDPRODUTO"]);
                lblIdProduto.Text = dtBarras.Rows[0]["IDPRODUTO"].ToString();
                txtConteudo.Focus();

            }
            else
            {
                txtAltura.Enabled = true;
                txtLargura.Enabled = true;
                txtComprimento.Enabled = true;
                txtPesoBruto.Enabled = true;
                txtPesoLiquido.Enabled = true;
                IdProduto = 0;
                lblIdProduto.Text = "0";
                txtConteudo.Focus();

            }

        }
    }

    int IdProduto = 0;
    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        try
        {
            salvar();
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Operação efetuada com sucesso.')", true);
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Fechar", "window.close();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
          
        }
        

    }

    private void salvar()
    {
        int IdProdutoCliente = 0;
        int idProdutoEmbalagem = 0;
        Produto o = new Produto();

        try
        {

            if (txtCodBarras.Text == "")
                throw new Exception("Informe o Código de Barras");

            if (txtAltura.Text == "")
                throw new Exception("Informe a altura");

            if (txtLargura.Text == "")
                throw new Exception("Informe a largura");


            if (txtComprimento.Text == "")
                throw new Exception("Informe a comprimento");

            if (txtPesoLiquido.Text == "")
                throw new Exception("Informe o Peso Líquido");

            if (txtPesoBruto.Text == "")
                throw new Exception("Informe o Peso Bruto");

            if (txtDesc.Text == "")
                throw new Exception("Informe a descrição do produto");

            if (txtCodigo.Text == "")
                throw new Exception("Informe o Código do Produto");
                   
          
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() == "novo")
            {

                if (lblIdProduto.Text == "0" || lblIdProduto.Text == "" || IdProdutoCliente==0)
                {
                    IdProduto = o.Inserir(txtCodBarras.Text.ToUpper(), Convert.ToDouble(txtAltura.Text).ToString(), Convert.ToDouble(txtLargura.Text).ToString(), Convert.ToDouble(txtComprimento.Text).ToString(), Convert.ToDouble(txtPesoLiquido.Text).ToString(), Convert.ToDouble(txtPesoBruto.Text).ToString(), "UN");
                    lblIdProduto.Text = IdProduto.ToString();                    
                    
                    IdProdutoCliente = o.InserirProdutoCliente(Session["IDEmpresa"].ToString(), Convert.ToDouble(cboMedida.SelectedValue).ToString(), txtCodigo.Text.ToUpper(), txtDesc.Text.ToUpper(), cboMovimentacao.SelectedValue, cboAtivo.SelectedItem.Text);
                    idProdutoEmbalagem = o.InserirProdutoEmbalagem(IdProdutoCliente.ToString(), IdProduto.ToString(), Convert.ToDouble(txtUnidadeCliente.Text).ToString(), Convert.ToDouble(txtUnidadeLogistica.Text).ToString(), "UN", Convert.ToDouble(txtValorUnitario.Text).ToString(), txtConteudo.Text == "" ? "." : txtConteudo.Text.ToUpper());
                }                       
                               
            }

            if (lblIdProdutoCliente.Text != "0")
            {

                o.UpdateProdutoCliente(lblIdProdutoCliente.Text, Convert.ToDouble(cboMedida.SelectedValue).ToString(),  txtCodigo.Text.ToUpper(), txtDesc.Text.ToUpper(), cboMovimentacao.SelectedValue, cboAtivo.SelectedItem.Text, "0", txtValorUnitario.Text, txtUnidadeCliente.Text, txtUnidadeLogistica.Text);
            }

            
            if (ListBox1.Items.Count > 0)
            {
                SistranBLL.Cliente.Divisao oc = new Cliente.Divisao();
                oc.DesabilitarEstoqueDivisao(txtCodigo.Text);


                for (int i = 0; i < ListBox1.Items.Count; i++)
                {
                    try
                    {
                        oc.InserirEstoqueDivisao(txtCodigo.Text, ListBox1.Items[i].Value);
                    }
                    catch (Exception)
                    {                                              
                    }

                }
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CarregarMenuDivisao();

            txtValorUnitario.Attributes.Add("OnKeyDown", "this.value=limpa_string(this.value)");
            txtValorUnitario.Attributes.Add("OnKeyUp", "this.value=limpa_string(this.value)");

            txtUnidadeCliente.Attributes.Add("OnKeyDown", "this.value=limpa_string(this.value)");
            txtUnidadeCliente.Attributes.Add("OnKeyUp", "this.value=limpa_string(this.value)");

            txtUnidadeLogistica.Attributes.Add("OnKeyDown", "this.value=limpa_string(this.value)");
            txtUnidadeLogistica.Attributes.Add("OnKeyUp", "this.value=limpa_string(this.value)");

            txtPesoBruto.Attributes.Add("OnKeyDown", "this.value=limpa_string(this.value)");
            txtPesoBruto.Attributes.Add("OnKeyUp", "this.value=limpa_string(this.value)");

            txtPesoLiquido.Attributes.Add("OnKeyDown", "this.value=limpa_string(this.value)");
            txtPesoLiquido.Attributes.Add("OnKeyUp", "this.value=limpa_string(this.value)");

            txtLargura.Attributes.Add("OnKeyDown", "this.value=limpa_string(this.value)");
            txtLargura.Attributes.Add("OnKeyUp", "this.value=limpa_string(this.value)");

            txtAltura.Attributes.Add("OnKeyDown", "this.value=limpa_string(this.value)");
            txtAltura.Attributes.Add("OnKeyUp", "this.value=limpa_string(this.value)");


            txtComprimento.Attributes.Add("OnKeyDown", "this.value=limpa_string(this.value)");
            txtComprimento.Attributes.Add("OnKeyUp", "this.value=limpa_string(this.value)");

            
            if (Request.QueryString["id"] != "novo")
            {
                CarregarListbox();
                CarregarCampos();
            }



        }
    }

    private void CarregarCampos()
    {
        DataTable dtProd;

        if (Request.QueryString["Codigo"] == null)
        {
            dtProd = new Produto().ConsultarProdutoClienteCodigo(txtCodigo.Text);
        }
        else
        {
            dtProd = new Produto().ConsultarProdutoClienteCodigo(Request.QueryString["Codigo"]);
        }

        if (dtProd.Rows.Count > 0)
        {
            IdProduto = Convert.ToInt32(dtProd.Rows[0]["idproduto"]);
            lblIdProduto.Text = dtProd.Rows[0]["idproduto"].ToString();
            lblIdProdutoCliente.Text = dtProd.Rows[0]["IdprodutoCliente"].ToString();

            txtDesc.Text = dtProd.Rows[0]["DESCRICAO"].ToString().ToUpper();
            cboMovimentacao.SelectedValue = dtProd.Rows[0]["MetodoDeMovimentacao"].ToString().ToUpper();
            cboMedida.SelectedValue = dtProd.Rows[0]["IDUnidadeDeMedida"].ToString().ToUpper();
            cboAtivo.SelectedValue = dtProd.Rows[0]["ATIVO"].ToString().ToUpper();

            if (!IsPostBack)
            {
                txtCodigo.Text = dtProd.Rows[0]["CODIGO"].ToString().ToUpper();
            }
            txtCodBarras.Text = dtProd.Rows[0]["CodigoDeBarras"].ToString().ToUpper();


            txtAltura.Text = (dtProd.Rows[0]["ALTURA"]==DBNull.Value?"0": Convert.ToDouble(dtProd.Rows[0]["ALTURA"]).ToString("#0.000"));
            txtLargura.Text = dtProd.Rows[0]["LARGURA"]==DBNull.Value?"0": Convert.ToDouble(dtProd.Rows[0]["LARGURA"]).ToString("#0.000");
            txtComprimento.Text =dtProd.Rows[0]["COMPRIMENTO"]==DBNull.Value?"0": Convert.ToDouble(dtProd.Rows[0]["COMPRIMENTO"]).ToString("#0.000");
            txtPesoBruto.Text = dtProd.Rows[0]["PESOBRUTO"]==DBNull.Value?"0":Convert.ToDouble(dtProd.Rows[0]["PESOBRUTO"]).ToString("#0.000");
            txtPesoLiquido.Text = dtProd.Rows[0]["PESOLIQUIDO"] ==DBNull.Value?"0": Convert.ToDouble(dtProd.Rows[0]["PESOLIQUIDO"]).ToString("#0.000");

            txtConteudo.Text = dtProd.Rows[0]["CONTEUDO"].ToString();
            txtUnidadeCliente.Text = dtProd.Rows[0]["UnidadeDoCliente"].ToString();

            if (dtProd.Rows[0]["VALORUNITARIO"] != DBNull.Value)
            {
                txtValorUnitario.Text = Convert.ToDouble(dtProd.Rows[0]["VALORUNITARIO"]).ToString("#0.000");
            }
            else
            {
                txtValorUnitario.Text = "0";
            }

            txtUnidadeLogistica.Text = dtProd.Rows[0]["UnidadeLogistica"].ToString();
            //txtUnidadEMedida.Text = dtProd.Rows[0]["UnidadeLogistica"].ToString();


            DataTable dImagem = new SistranBLL.Produto().RetornarUltimaImagemProduto(Convert.ToInt32(dtProd.Rows[0]["IDPRODUTO"]));
            if (dImagem.Rows.Count > 0)
            {
                //if (Request.QueryString["Codigo"] != null || txtCodigo.Text !="")
                //{
                    try
                    {
                        byte[] imagem = (byte[])dImagem.Rows[0]["FOTO"];
                        //string x = IdProduto.ToString();
                        MemoryStream ms = new MemoryStream(imagem);
                        System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                        returnImage.Save(Server.MapPath(@"imgReport/" + dtProd.Rows[0]["IDPRODUTO"].ToString() + ".jpg"));
                        imgProd.ImageUrl = "imgReport/" + dtProd.Rows[0]["IDPRODUTO"].ToString() + ".jpg";
                    }
                    catch (Exception)
                    {              
                    imgProd.ImageUrl = "~/Images/naoDisponivel.jpg";
                        
                    }
                    
                //}
                //else
                //{
                //    imgProd.ImageUrl = "~/Images/naoDisponivel.jpg";
                //}
            }
            else
            {
                imgProd.ImageUrl = "~/Images/naoDisponivel.jpg";
            }

            txtAltura.Enabled = false;
            txtLargura.Enabled = false;
            txtComprimento.Enabled = false;
            txtPesoBruto.Enabled = false;
            txtPesoLiquido.Enabled = false;
            IdProduto = Convert.ToInt32(dtProd.Rows[0]["IDPRODUTO"]);
            lblIdProduto.Text = dtProd.Rows[0]["IDPRODUTO"].ToString();


        }
    }

    #region Divisoes
    private void CarregarListbox()
    {
        ListBox1.DataSource = new SistranBLL.Cliente.Divisao().RetornarListaDivisoesProduto(Request.QueryString["Codigo"]);
        ListBox1.DataTextField = "Nome";
        ListBox1.DataValueField = "IDClienteDivisao";
        ListBox1.DataBind();
    }

    DataTable dtxx;
    string n = "";
    DataRow pp;
    private void CarregarMenuDivisao()
    {
        DataTable DivisoesCompleta = SistranDAO.Cliente.DivisoesCompleta(Session["IDEmpresa"].ToString());
        Session["DivisoesCompleta"] = DivisoesCompleta;

        dtxx = new DataTable();
        dtxx.Columns.Add("IDCLIENTEDIVISAO");
        dtxx.Columns.Add("NOME");

        DataRow[] xx = DivisoesCompleta.Select("IDPARENTE IS NULL ", "");

        if (xx.Length == 0)
        {
            return;
        }

        
        //pp = dtxx.NewRow();
        //pp[0] = xx[0]["IDCLIENTEDIVISAO"].ToString();
        //pp[1] = xx[0]["NOME"].ToString();
        //dtxx.Rows.Add(pp);

        //procurarFilhosDestino(DivisoesCompleta, xx[0]["IDCLIENTEDIVISAO"].ToString());

        foreach (var item in xx)
        {
            //pp = dtxx.NewRow();
            //pp[0] = xx[0]["IDCLIENTEDIVISAO"].ToString();
            //pp[1] = xx[0]["NOME"].ToString();
            //dtxx.Rows.Add(pp);

            pp = dtxx.NewRow();
            pp[0] = item["IDCLIENTEDIVISAO"].ToString();
            pp[1] = item["NOME"].ToString();
            dtxx.Rows.Add(pp);


            procurarFilhosDestino(DivisoesCompleta, item["IDCLIENTEDIVISAO"].ToString());

        }

        ListBox1.Rows = (dtxx.Rows.Count + 1);
        Repeater1.DataSource = dtxx;
        Repeater1.DataBind();

    }

    private void procurarFilhosDestino(DataTable DivisoesCompleta, string IdClienteDivisao)
    {
        DataRow[] cc = DivisoesCompleta.Select("IDPARENTE='" + IdClienteDivisao + "'", "");

        foreach (DataRow item in cc)
        {
            //nivel 2
            n = "&nbsp;&nbsp;&nbsp;&nbsp;";
            pp = dtxx.NewRow();
            pp[0] = item["IDCLIENTEDIVISAO"].ToString();
            pp[1] = n + item["NOME"].ToString();
            dtxx.Rows.Add(pp);

            DataRow[] ccx = DivisoesCompleta.Select("IDPARENTE='" + item["IDCLIENTEDIVISAO"].ToString() + "'", "");



            if (ccx.Length > 0)
            {
                // nivel 3
                n = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                foreach (DataRow itemff in ccx)
                {
                    pp = dtxx.NewRow();
                    pp[0] = itemff["IDCLIENTEDIVISAO"].ToString();
                    pp[1] = n + itemff["NOME"].ToString();
                    dtxx.Rows.Add(pp);


                    DataRow[] ccz = DivisoesCompleta.Select("IDPARENTE='" + itemff["IDCLIENTEDIVISAO"].ToString() + "'", "");
                    if (ccz.Length > 0)
                    {
                        //nivel 4
                        foreach (DataRow itemfff in ccz)
                        {
                            n = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            pp = dtxx.NewRow();
                            pp[0] = itemfff["IDCLIENTEDIVISAO"].ToString();
                            pp[1] = n + itemfff["NOME"].ToString();
                            dtxx.Rows.Add(pp);

                            DataRow[] ccNivel4 = DivisoesCompleta.Select("IDPARENTE='" + itemfff["IDCLIENTEDIVISAO"].ToString() + "'", "");

                            if (ccNivel4.Length > 0)
                            {
                                //nivel 5
                                foreach (DataRow ItemNivel4 in ccNivel4)
                                {
                                    n = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                    pp = dtxx.NewRow();
                                    pp[0] = ItemNivel4["IDCLIENTEDIVISAO"].ToString();
                                    pp[1] = n + ItemNivel4["NOME"].ToString();
                                    dtxx.Rows.Add(pp);



                                    DataRow[] ccNivel5 = DivisoesCompleta.Select("IDPARENTE='" + ItemNivel4["IDCLIENTEDIVISAO"].ToString() + "'", "");
                                    if (ccNivel5.Length > 0)
                                    {
                                        //nivel 6
                                        foreach (DataRow ItemNivel5 in ccNivel5)
                                        {
                                            n = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                                            pp = dtxx.NewRow();
                                            pp[0] = ItemNivel5["IDCLIENTEDIVISAO"].ToString();
                                            pp[1] = n + ItemNivel5["NOME"].ToString();
                                            dtxx.Rows.Add(pp);
                                        }

                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblIDClienteDivisao = (Label)e.Item.FindControl("lblIDClienteDivisao");
        LinkButton lblNome0 = (LinkButton)e.Item.FindControl("lblNome0");

        if (lblNome0 == null)
            return;


        lblNome0.CommandArgument = lblIDClienteDivisao.Text;
        lblNome0.CommandName = "edit";
    }

    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "edit")
        {
            LinkButton lblNome0 = (LinkButton)e.Item.FindControl("lblNome0");
            if (lblNome0 != null)
            {
                hdCodigoDivisaoCliente.Value = e.CommandArgument.ToString();
                lblSelecionado.Text = "Item Selecionado: " + lblNome0.Text.Trim().Replace("&nbsp;", "");
                lblSelecionado.Visible = true;
                hdCodigoDivisaoCliente0.Value = lblNome0.Text.Trim().Replace("&nbsp;", "");
            }
        }

    }

    protected void btnPosterior_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                if (ListBox1.Items[i].Value == hdCodigoDivisaoCliente.Value)
                {
                    throw new Exception("Item já existente.");
                }
            }

            ////incluir
            //SistranBLL.Cliente.Divisao o = new Cliente.Divisao();
            //o.InserirEstoqueDivisao(Request.QueryString["Codigo"], hdCodigoDivisaoCliente.Value);
            ListBox1.Items.Add(new ListItem(hdCodigoDivisaoCliente0.Value, hdCodigoDivisaoCliente.Value));

            //CarregarListbox();
            //CarregarMenuDivisao();

            //List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "INCLUIU UMA DIVISÃO AO PRODUTO", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    protected void btnAnterior_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            for (int i = 0; i < ListBox1.Items.Count; i++)
            {
                if (ListBox1.Items[i].Selected == true)
                {
                    //DELETAR
                    //SistranBLL.Cliente.Divisao o = new Cliente.Divisao();
                    //o.ApagarEstoqueDivisaoByCodigoProdutoAndIDClienteDivisao(Request.QueryString["Codigo"], ListBox1.Items[i].Value);
                    ListBox1.Items.RemoveAt(i);

                    //List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                    //SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "REMOVEU A DIVISÃO DE UM PRODUTO", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath));
                    //throw new Exception("Item excluído com sucesso.");
                }
            }

            //ListBox1.Items.Add(new ListItem(hdCodigoDivisaoCliente.Value, hdCodigoDivisaoCliente.Value));
        }
        catch (Exception ex)
        {
            if (ex.Message != "Item excluído com sucesso.")
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Não é possível excluir esta divisão. Exitem movimentações para esta Divisão. Descrição do Erro:" + ex.Message.Replace("'", "´") + "')", true);
            else
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    #endregion
    protected void txtCodigo_TextChanged(object sender, EventArgs e)
    {
        CarregarCampos();
        txtDesc.Focus();
    }
    
    protected void brnCarregar_Click(object sender, EventArgs e)
    {
        salvar();
        SistranBLL.Produto o = new Produto();

        if (fileUploadArquivo.HasFile)
        {
            if(System.IO.File.Exists(Server.MapPath("imgReport") + "\\" + lblIdProduto.Text + ".jpg"))
            {
                System.IO.File.Delete(Server.MapPath("imgReport") + "\\" + lblIdProduto.Text + ".jpg");
            }
            
            fileUploadArquivo.SaveAs(Server.MapPath("imgReport") + "\\" + lblIdProduto.Text + ".jpg");
            imgProd.ImageUrl = null;
            imgProd.ImageUrl = "imgReport/" + lblIdProduto.Text + ".jpg";
            int intTamanho = System.Convert.ToInt32(fileUploadArquivo.PostedFile.InputStream.Length);

            byte[] imageBytes = new byte[intTamanho];
            fileUploadArquivo.PostedFile.InputStream.Read(imageBytes, 0, intTamanho);

            fileUploadArquivo.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
            o.InserirProdutoImagem(lblIdProduto.Text, imageBytes);           

        }
        CarregarCampos();

        Response.Redirect("frmCadastrarProduto.aspx?f="+Guid.NewGuid().ToString()+"&Codigo=" + txtCodigo.Text.ToUpper() + "&tipo=Mat&IdProduto=" + lblIdProduto.Text, true);
    }
}