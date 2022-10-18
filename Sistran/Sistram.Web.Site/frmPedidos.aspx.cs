using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;

public partial class frmPedidos : System.Web.UI.Page
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            CarregarMenuDivisao();        
            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                SiteUtils.CollectionPager CollectionPager1 = new SiteUtils.CollectionPager();

                HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
                tr0.Style.Add("display", "none");

                if (Session["pesq"] == null)
                {
                    if (DataList1.Items.Count == 0)
                    {
                        string[] x = Session["PrimeiraDivisao"].ToString().Split(new char[] { '|' });

                        DataTable dt = new SistranBLL.Produto().ListarProdutoByDivisaoCliente(Convert.ToInt32(x[0]), Convert.ToInt32(Session["IDEmpresa"]), true);
                        lblMensagem0.Text = x[1];
                        carregerDataList(dt);
                        Session["PrimeiraDivisao"] = null;

                        if (DataList1.Items.Count == 0)
                        {
                            FiltarPorNome();
                        }
                    }
                }
                else
                {
                    txtBuscar.Text = Session["pesq"].ToString();
                    FiltarPorNome();
                }
            }
           

            if (Session["Carrinho"] != null)
            {

                if (((List<SistranMODEL.Carrinho>)Session["Carrinho"]).Count > 0)
                {
                    pnlCar.Visible = true;
                    lblcar.Text = ((List<SistranMODEL.Carrinho>)Session["Carrinho"]).Count.ToString() + " Itens";
                    // tdCarrinho.Visible = true;
                }
                else
                {
                    lblcar.Text = "Nenhum Item.";
                }
            }
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('"+ ex.Message.Replace("'","´") +"')", true);
        }
    }
         
    private void carregerDataList(DataTable dt)
    {
        Panel3.Controls.Clear();
        SiteUtils.CollectionPager CollectionPager1 = new SiteUtils.CollectionPager();
        
        CollectionPager1.DataSource = dt.Rows;
        CollectionPager1.BindToControl = DataList1;
        DataList1.DataSource = CollectionPager1.DataSourcePaged;
        DataList1.DataBind();
        
        CollectionPager1.PageSize = 12;
        CollectionPager1.PagingMode = SiteUtils.PagingModeType.QueryString;

        CollectionPager1.MaxPages = 200;
        CollectionPager1.ShowFirstLast = true;
        CollectionPager1.ShowLabel = true;
                
        Session["dtsTemp"] = dt;
      
        CollectionPager1.PageSize = 12;
        CollectionPager1.BackText = "« Anterior";
        CollectionPager1.NextText = "Próximo »";
        CollectionPager1.ResultsFormat = "Exibindo {0}-{1} (de {2})";
        CollectionPager1.LabelText = "Página:  &nbsp;&nbsp;";
        CollectionPager1.LastText = "Última Página";
        CollectionPager1.FirstText = "  &nbsp;&nbsp; Primeira Página";
        Panel3.Controls.Add(CollectionPager1);
    }
    
    private void CarregarMenuGrupo()
    {
        PlaceHolderMenuGrupo.Controls.Clear();
        //List<SistranMODEL.Usuario> LUser = (Session["USUARIO"] as List<SistranMODEL.Usuario>);

        SistranBLL.Produto.GrupoProduto gpBLL = new SistranBLL.Produto.GrupoProduto();
        DataTable dtGrupo = gpBLL.ListarMenu(Convert.ToInt32(Session["IDEmpresa"]));

        if (dtGrupo.Rows.Count ==1 && dtGrupo.Rows[0]["Codigo"].ToString() == "")
        {
            return;
        }
        else
        {

            PlaceHolderMenuGrupo.Controls.Add(new LiteralControl(@"<table class='tableMenu' cellspacing=0 celpanding=0 widht=200px border=0>"));

            PlaceHolderMenuGrupo.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolderMenuGrupo.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap style='font-size:10px; font-name:Arial; border-bottom: 1px solid #dddddd;'>POR GRUPO"));
            PlaceHolderMenuGrupo.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolderMenuGrupo.Controls.Add(new LiteralControl(@"</tr>"));

            foreach (DataRow item in dtGrupo.Rows)
            {
                PlaceHolderMenuGrupo.Controls.Add(new LiteralControl(@"<tr>"));
                PlaceHolderMenuGrupo.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>"));
                PlaceHolderMenuGrupo.Controls.Add(criarBotaoGrupo(item["NOMEGRUPO"].ToString(), false, item["CODIGO"].ToString()));
                PlaceHolderMenuGrupo.Controls.Add(new LiteralControl(@"</td>"));
                PlaceHolderMenuGrupo.Controls.Add(new LiteralControl(@"</tr>"));
            }
            PlaceHolderMenuGrupo.Controls.Add(new LiteralControl(@"</table>"));
        }

    }

    private void CarregarMenuDivisao()
    {       

        PlaceHolderMenuDivisao.Controls.Clear();
        
        SistranBLL.Cliente.Divisao pdBLL = new SistranBLL.Cliente.Divisao();
        DataTable dtPai = pdBLL.RetornarPais(Convert.ToInt32(Session["IDEmpresa"]));

        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<table  cellspacing=0 celpanding=0 widht=200px >"));

        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr >"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td align='left' nowrap=nowrap style='font-size:10px; border-bottom: 1px solid #dddddd'><B>ESCOLHA UMA DIVISÃO</B>"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));

        int c = 0;
        foreach (DataRow item in dtPai.Rows)
        {
            if (c == 0 && Session["PrimeiraDivisao"]==null)
                Session["PrimeiraDivisao"] = item["IDClienteDivisao"].ToString() + "|" + item["NOME"].ToString();

            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>"));
            PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(item["NOME"].ToString(), true, item["IDClienteDivisao"].ToString()));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));

            DataTable dtFilho = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(item["IDClienteDivisao"]));

            foreach (DataRow itemFilho in dtFilho.Rows)
            {

                if (ProcurarBotaoMenuDivisao(itemFilho["IDClienteDivisao"].ToString()) == false)
                {
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;"));
                    PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemFilho["NOME"].ToString(), false, itemFilho["IDClienteDivisao"].ToString()));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                    PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));
                }

                DataTable dtFilhoFilho = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemFilho["IDClienteDivisao"]));

                foreach (DataRow itemff in dtFilhoFilho.Rows)
                {
                    if (ProcurarBotaoMenuDivisao(itemff["IDClienteDivisao"].ToString()) == false)
                    {

                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;"));
                        PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemff["NOME"].ToString(), false, itemff["IDClienteDivisao"].ToString()));
                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));


                        DataTable dtFilhoNivel3 = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemff["IDClienteDivisao"]));
                        foreach (DataRow itemNivel3 in dtFilhoNivel3.Rows)
                        {
                            //nivel 3
                            if (ProcurarBotaoMenuDivisao(itemNivel3["IDClienteDivisao"].ToString()) == false)
                            {

                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                                PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemNivel3["NOME"].ToString(), false, itemNivel3["IDClienteDivisao"].ToString()));
                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));


                                DataTable dtFilhoNivel4 = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemNivel3["IDClienteDivisao"]));
                                foreach (DataRow itemNivel4 in dtFilhoNivel4.Rows)
                                {
                                    //nivel 4
                                    if (ProcurarBotaoMenuDivisao(itemNivel4["IDClienteDivisao"].ToString()) == false)
                                    {

                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                                        PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemNivel4["NOME"].ToString(), false, itemNivel4["IDClienteDivisao"].ToString()));
                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                                        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));


                                        DataTable dtFilhoNivel5 = pdBLL.RetornarFlihos(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(itemNivel4["IDClienteDivisao"]));
                                        foreach (DataRow itemNivel5 in dtFilhoNivel5.Rows)
                                        {
                                            //nivel 4
                                            if (ProcurarBotaoMenuDivisao(itemNivel5["IDClienteDivisao"].ToString()) == false)
                                            {

                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<tr>"));
                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"<td nowrap=nowrap left='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"));
                                                PlaceHolderMenuDivisao.Controls.Add(criarBotaoDivisao(itemNivel5["NOME"].ToString(), false, itemNivel5["IDClienteDivisao"].ToString()));
                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</td>"));
                                                PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</tr>"));
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
        PlaceHolderMenuDivisao.Controls.Add(new LiteralControl(@"</table>"));

    }

    private bool ProcurarBotaoMenuDivisao(string id)
    {
        foreach (Control cc in PlaceHolderMenuDivisao.Controls)
        {
            Button tb = cc as Button;

            if (tb != null)
            {
                if (tb.ID == id)
                {
                    return true;
                }

            }
        }
        return false;
    }

    private Button criarBotaoDivisao(string p, bool bold, string Codigo)
    {
        Button bot = new Button();
        bot.Text = p;
        bot.BorderStyle = BorderStyle.None;
        bot.Font.Name = "Verdana";
        bot.Style.Add("font-size", "7pt");  
        bot.Font.Bold = bold;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        bot.Click += new EventHandler(Button_ClickDivisao);
        bot.ID = Codigo;
        bot.BackColor = System.Drawing.Color.Transparent ;
        return bot;
    }

    private Button criarBotaoGrupo(string p, bool bold, string Codigo)
    {
        Button bot = new Button();
        bot.Text = p;
        bot.BorderStyle = BorderStyle.None;
        bot.Font.Name = "Arial";
        bot.Style.Add("font-size", "7pt");        
        bot.Font.Bold = bold;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        bot.Click += new EventHandler(Button_ClickGrupo);
        if (Codigo != "" && Codigo != "0")
        {
            bot.ID = Codigo;
        }
        else
        {
            bot.ID = "0";
        }
        bot.BackColor = System.Drawing.Color.Transparent;
        return bot;
    }   

    private void Button_ClickGrupo(object sender, System.EventArgs e)
    {
        Session["pesq"] = null;
        txtBuscar.Text = "";
        Button b = (Button)sender;
        DataTable dt = new SistranBLL.Produto().ListarProdutosByGrupo(Convert.ToInt32(Session["IDEmpresa"]), b.ID.ToString());
        Session["dtsTemp"] = "";
        lblMensagem.Text = "VOCÊ FILTROU POR: GRUPO =>> " + b.Text.ToUpper();
        lblMensagem0.Text = b.Text.ToUpper();
        carregerDataList(dt);        
    }

    private void Button_ClickDivisao(object sender, System.EventArgs e)
    {
        Session["pesq"] = null;
        txtBuscar.Text = "";
        Button b = (Button)sender;
        DataTable dt = new SistranBLL.Produto().ListarProdutoByDivisaoCliente(Convert.ToInt32(b.ID), Convert.ToInt32(Session["IDEmpresa"]), true);
        lblMensagem.Text = "VOCÊ FILTROU POR: DIVISÃO =>> " + b.Text.ToUpper();
        lblMensagem0.Text = b.Text.ToUpper();
        carregerDataList(dt);
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        try
        {
            DataRow dv = (DataRow)e.Item.DataItem;
            Image Image1 = (Image)e.Item.FindControl("Image1");
            Label lblCodigo = (Label)e.Item.FindControl("lblCodigo");
            Label lblTitulo = (Label)e.Item.FindControl("lblTitulo");
            Label lblPreco = (Label)e.Item.FindControl("lblPreco");
            Label lblDescricao = (Label)e.Item.FindControl("lblDescricao");
            Label lblCodigoProdutoCliente = (Label)e.Item.FindControl("lblCodigoProdutoCliente");
            Label lblDivisao = (Label)e.Item.FindControl("lblDivisao");
            ImageButton btnADDCart = (ImageButton)e.Item.FindControl("btnADDCart");
            Label lblIdClienteDivisao = (Label)e.Item.FindControl("lblIdClienteDivisao");

            if (Image1 != null)
            {
                lblCodigo.Text = dv["IDPRODUTO"].ToString();
                lblCodigoProdutoCliente.Text = dv["CODIGO"].ToString();
                lblTitulo.Text = dv["DESCRICAO"].ToString();
                lblIdClienteDivisao.Text = dv["idclientedivisao"].ToString();
                lblDivisao.Text = lblMensagem0.Text;

                btnADDCart.CommandArgument = dv["IDPRODUTO"].ToString();
                btnADDCart.CommandName = dv["idclientedivisao"].ToString();

                lblPreco.Text = Convert.ToDecimal(dv["ValorUnitario"]).ToString("N2");
                lblDescricao.Text = "Saldo Disponível: " + dv["SaldoDivisaoDisponivel"].ToString();
                Image1.ToolTip = lblTitulo.Text;
                lblTitulo.ToolTip = lblTitulo.Text;

                if (lblTitulo.Text.Length > 20)
                {
                    lblTitulo.Text = lblTitulo.Text.Substring(0, 19) + "...";
                }

                Image1.Attributes.Add("OnClick", "NewWindow('frmPopUpProduto.aspx?cod=" + lblCodigo.Text + "', 'pg', '350', '400', 'no')");

                if (lblCodigo.Text.Trim() != "")
                {
                    string x = lblCodigo.Text;
                    if (System.IO.File.Exists(Server.MapPath(@"imgReport/" + x + ".jpg")))
                    {
                        Image1.ImageUrl = "imgReport/" + x + ".jpg";
                    }
                    else
                    {

                        DataTable dImagem = new SistranBLL.Produto().RetornarImagemProduto(Convert.ToInt32(lblCodigo.Text));

                        if (dImagem.Rows.Count > 0)
                        {
                            byte[] imagem = (byte[])dImagem.Rows[0]["FOTO"];
                            MemoryStream ms = new MemoryStream(imagem);
                            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                            returnImage.Save(Server.MapPath(@"imgReport/" + x + ".jpg"));
                            Image1.ImageUrl = "imgReport/" + x + ".jpg";
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandArgument.ToString() != "" && e.CommandName != "")
        {
            ModalPopupExtender1.Show();
            imgPop.ImageUrl = "imgReport/" + e.CommandArgument.ToString() + ".jpg";
            DataTable dt = (DataTable)Session["dtsTemp"];
            DataRow[] ow = dt.Select("IDPRODUTO=" + e.CommandArgument.ToString() + " AND IDCLIENTEDIVISAO='" + e.CommandName + "'", "");
            
            if (ow.Length > 0)
            {
                lblCodigo.Text = ow[0]["IDPRODUTO"].ToString();
                lblTitulo.Text = ow[0]["DESCRICAO"].ToString();
                lblPreco.Text = Convert.ToDecimal(ow[0]["ValorUnitario"]).ToString("N2");
                lblDescricao.Text = "Saldo Disponível: ";
                lblDisponivel.Text = ow[0]["SaldoDivisaoDisponivel"].ToString();
                lblCodigoProdutoCliente.Text = ow[0]["CODIGO"].ToString().Trim();
                lblIdCliDivPop.Text = ow[0]["IdCLienteDivisao"].ToString().Trim();
                lblIDProdutoClientePop.Text = ow[0]["IDProdutoCliente"].ToString().Trim();

                if (lblMensagem.Text.StartsWith("VOCÊ FILTROU POR: GRUPO =>> ") || lblMensagem.Text == "")
                {
                    lblDivisaoPop.Text = ow[0]["Nome"].ToString().Trim();
                }
                else
                {
                    lblDivisaoPop.Text = lblMensagem0.Text.Trim();
                }

                DataTable dtProd = new SistranBLL.Produto().ListarProdutosByIdProdutoCliente(ow[0]["IDProdutoCliente"].ToString());

                if (dt.Rows.Count > 0)
                {
                    lblMetro0.Text = dtProd.Rows[0]["CODIGODEBARRAS"].ToString();
                    lblMetro.Text = Convert.ToDecimal(dtProd.Rows[0]["Rodoviario"]).ToString("#0.000");
                }
            }
        }
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        if (txtQuantidade.Text != "")
        {
            if (Convert.ToDecimal(lblDisponivel.Text) >= Convert.ToDecimal(txtQuantidade.Text))
            {
                List<SistranMODEL.Carrinho> LCarrinho = new List<SistranMODEL.Carrinho>();
                if (Session["Carrinho"] != null)
                {
                    LCarrinho = (List<SistranMODEL.Carrinho>)Session["Carrinho"];
                }

                int count = 0;
                bool mesmoProd = false;
                foreach (var item in LCarrinho)
                {
                    if (lblCodigo.Text == item.idProduto.ToString() && item.IdClienteDivisao == lblIdCliDivPop.Text )
                    {
                        LCarrinho[count].Quantidade +=Convert.ToDecimal(txtQuantidade.Text);
                        mesmoProd = true;
                    }

                    count += 1;
                }


                if (mesmoProd == false)
                {
                    SistranMODEL.Carrinho car = new SistranMODEL.Carrinho();
                    car.idProduto = Convert.ToInt32(lblCodigo.Text);
                    car.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
                    car.Descricao = lblTitulo.Text;
                    car.Disponivel = Convert.ToInt32(lblDisponivel.Text);
                    car.ValorUnitario = Convert.ToDecimal(lblPreco.Text);
                    car.idProduto = Convert.ToInt32(lblCodigo.Text);
                    car.CodigoProdutoCliente = lblCodigoProdutoCliente.Text;
                    car.Divisao = lblDivisaoPop.Text;
                    car.IdClienteDivisao = lblIdCliDivPop.Text;
                    car.IdProdutoCliente = Convert.ToInt32(lblIDProdutoClientePop.Text);
                    LCarrinho.Add(car);
                }
                Session["Carrinho"] = LCarrinho;

                ModalPopupExtender1.Hide();

                if (((List<SistranMODEL.Carrinho>)Session["Carrinho"]).Count > 0)
                {
                    pnlCar.Visible = true;
                    lblcar.Text = ((List<SistranMODEL.Carrinho>)Session["Carrinho"]).Count.ToString() + " Itens";                    
                }
                else
                {
                    lblcar.Text = "Nenhum Item.";
                }

                txtQuantidade.Text = "1";
                pnlMensagem.Width = new Unit("100%");
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ADICIONOU UM ITEM AO CARRINHO", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath),  Session["Conn"].ToString());
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Quantidade maior que a quantidade disponível.')", true);
                ModalPopupExtender1.Show();
            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }

    protected void btnCancelar_Click1(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();

    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        FiltarPorNome();    
    }

    private void FiltarPorNome()
    {
        DataTable ds = new SistranBLL.Produto().Pesquisar(txtBuscar.Text, Convert.ToInt32(Session["IDEmpresa"]));
        carregerDataList(ds);
        Session["pesq"] = txtBuscar.Text;
    }
}
