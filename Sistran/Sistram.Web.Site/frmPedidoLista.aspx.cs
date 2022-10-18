using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class frmPedidoLista : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /// CarregarMenuDivisao();
            if (!IsPostBack)
            {
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + "PEDIDO LISTA", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

                //HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
                //tr0.Style.Add("display", "none");

                //string[] x = Session["PrimeiraDivisao"].ToString().Split(new char[] { '|' });
                //string[] DivTemp = Session["Divisoes"].ToString().Split(',');

                //for (int i = 0; i < DivTemp.Length; i++)
                //{
                DataTable dt = new SistranBLL.Produto().Pesquisar("", Convert.ToInt32(Session["IDEmpresa"]));
                CarregarRepeater(dt);

                ////    if (rpt.Items.Count > 0)
                ////    {
                ////        break;
                ////    }
                ////}      
                Button2.Attributes.Add("onClick", "window.open('kpi/gerarexcelInic.aspx', 'NovaJanela2', 'yes'); return false;");
                

            }
            //pnlCar.Visible = true;

            if (Session["Carrinho"] != null)
            {

                if (((List<SistranMODEL.Carrinho>)Session["Carrinho"]).Count > 0)
                {
                    //pnlCar.Visible = true;
                    lblcar.Text = ((List<SistranMODEL.Carrinho>)Session["Carrinho"]).Count.ToString() + " Itens";
                    tdTable.Width = "86%";
                }
            }
            else
            {
                tdTable.Width = "86%";
                lblcar.Text = "Vazio";

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    private void CarregarRepeater(DataTable dt)
    {
        rpt.DataSource = dt;
        rpt.DataBind();
        Session["dtsTemp"] = dt;
        Session["dt"] = dt;

    }

    public void AddCart()
    {
        try
        {
            bool erro = false;
            for (int ii = 0; ii < rpt.Items.Count; ii++)
            {

                TextBox txtQuantidade = (TextBox)rpt.Items[ii].FindControl("txtQuantidade");
                Label lblIdClienteDivisao = (Label)rpt.Items[ii].FindControl("lblIdClienteDivisao");
                Label lblIdProduro = (Label)rpt.Items[ii].FindControl("lblIdProduro");
                Label lblDescricao = (Label)rpt.Items[ii].FindControl("lblDescricao");
                Label lblSaldo = (Label)rpt.Items[ii].FindControl("lblSaldo");
                Label lblValorUnitario = (Label)rpt.Items[ii].FindControl("lblValorUnitario");
                Label lblIdProdutoCliente = (Label)rpt.Items[ii].FindControl("lblIdProdutoCliente");
                Label lblDivisao = (Label)rpt.Items[ii].FindControl("lblDivisao");
                Label lblIDUnidadeDeArmazenagemLote = (Label)rpt.Items[ii].FindControl("lblIDUnidadeDeArmazenagemLote");
                Label lblSaldoEndereco = (Label)rpt.Items[ii].FindControl("lblSaldoEndereco");
                Label lblLote = (Label)rpt.Items[ii].FindControl("lblLote");
                Label lblCodigoProduto = (Label)rpt.Items[ii].FindControl("lblCodigoProduto");
                Label lblChaveDoProduto = (Label)rpt.Items[ii].FindControl("lblChaveDoProduto");




                txtQuantidade.ForeColor = System.Drawing.Color.Black;
                txtQuantidade.BackColor = System.Drawing.Color.White;

                try
                {
                    txtQuantidade.Text = Convert.ToInt32(txtQuantidade.Text).ToString();

                }
                catch (Exception)
                {
                    txtQuantidade.Text = "0";
                }

                if (Convert.ToInt32(txtQuantidade.Text) > 0)
                {
                    if (Convert.ToInt32(lblSaldoEndereco.Text) < Convert.ToInt32(txtQuantidade.Text))
                    {
                        txtQuantidade.ForeColor = System.Drawing.Color.Red;
                        txtQuantidade.BackColor = System.Drawing.Color.Yellow;
                        txtQuantidade.Focus();
                        erro = true;
                    }

                }


            }

            if (erro)
            {
                throw new Exception("Verifique as quantidades. Quantidade informada maior que o Saldo Disponível.");

            }


            for (int i = 0; i < rpt.Items.Count; i++)
            {
                TextBox txtQuantidade = (TextBox)rpt.Items[i].FindControl("txtQuantidade");
                Label lblIdClienteDivisao = (Label)rpt.Items[i].FindControl("lblIdClienteDivisao");
                Label lblIdProduro = (Label)rpt.Items[i].FindControl("lblIdProduro");
                Label lblDescricao = (Label)rpt.Items[i].FindControl("lblDescricao");
                Label lblSaldo = (Label)rpt.Items[i].FindControl("lblSaldo");
                Label lblValorUnitario = (Label)rpt.Items[i].FindControl("lblValorUnitario");
                Label lblIdProdutoCliente = (Label)rpt.Items[i].FindControl("lblIdProdutoCliente");
                Label lblDivisao = (Label)rpt.Items[i].FindControl("lblDivisao");
                Label lblIDUnidadeDeArmazenagemLote = (Label)rpt.Items[i].FindControl("lblIDUnidadeDeArmazenagemLote");
                Label lblSaldoEndereco = (Label)rpt.Items[i].FindControl("lblSaldoEndereco");
                Label lblLote = (Label)rpt.Items[i].FindControl("lblLote");
                Label lblCodigoProduto = (Label)rpt.Items[i].FindControl("lblCodigoProduto");
                Label lblUA = (Label)rpt.Items[i].FindControl("lblUA");
                Label lblEndereco = (Label)rpt.Items[i].FindControl("lblEndereco");
                Label lblChaveDoProduto = (Label)rpt.Items[i].FindControl("lblChaveDoProduto");

                try
                {
                    txtQuantidade.Text = Convert.ToInt32(txtQuantidade.Text).ToString();

                }
                catch (Exception)
                {
                    txtQuantidade.Text = "0";
                }


                if (Convert.ToInt32(txtQuantidade.Text) > 0)
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
                        if (lblChaveDoProduto.Text == item.chavedoItem)
                        {
                            LCarrinho[count].Quantidade += Convert.ToDecimal(txtQuantidade.Text);
                            mesmoProd = true;
                        }

                        count += 1;
                    }


                    if (mesmoProd == false)
                    {
                        SistranMODEL.Carrinho car = new SistranMODEL.Carrinho();

                        car.idProduto = Convert.ToInt32(lblIdProduro.Text);
                        car.Quantidade = Convert.ToDecimal(txtQuantidade.Text);
                        car.Descricao = lblDescricao.Text;
                        car.Disponivel = Convert.ToInt32(lblSaldo.Text);
                        car.ValorUnitario = Convert.ToDecimal(lblValorUnitario.Text);
                        car.CodigoProdutoCliente = lblCodigoProduto.Text;
                        car.Divisao = lblDivisao.Text;
                        car.IdClienteDivisao = lblIdClienteDivisao.Text;
                        car.IdProdutoCliente = Convert.ToInt32(lblIdProdutoCliente.Text);
                        car.Lote = lblLote.Text;
                        car.lblSaldoEndereco = int.Parse(lblSaldoEndereco.Text);
                        car.lblIDUnidadeDeArmazenagemLote = lblIDUnidadeDeArmazenagemLote.Text;
                        car.ua = lblUA.Text;
                        car.Endereco = lblEndereco.Text;
                        car.chavedoItem = lblIdProdutoCliente.Text + "_" + lblUA.Text + "_" + lblEndereco.Text + "_" + lblLote.Text;
                        LCarrinho.Add(car);
                    }
                    Session["Carrinho"] = LCarrinho;


                    if (((List<SistranMODEL.Carrinho>)Session["Carrinho"]).Count > 0)
                    {
                        //pnlCar.Visible = true;
                        lblcar.Text = ((List<SistranMODEL.Carrinho>)Session["Carrinho"]).Count.ToString() + " Itens";
                    }


                }
            }

            //Response.Redirect("frmPedidoLista.aspx");
            Response.Redirect("frmCarrinho.aspx?tipo=lista");


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);

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
            if (c == 0 && Session["PrimeiraDivisao"] == null)
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
        bot.BackColor = System.Drawing.Color.Transparent;
        return bot;
    }

    private void Button_ClickDivisao(object sender, System.EventArgs e)
    {
        Session["pesq"] = null;
        Button b = (Button)sender;
        DataTable dt = new SistranBLL.Produto().ListarProdutoByDivisaoCliente(Convert.ToInt32(b.ID), Convert.ToInt32(Session["IDEmpresa"]), true);
        CarregarRepeater(dt);
    }

    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblChaveDoProduto = (Label)e.Item.FindControl("lblChaveDoProduto");

        if (lblChaveDoProduto != null)
        {
            DataRowView dataItem = (DataRowView)e.Item.DataItem;
            lblChaveDoProduto.Text = dataItem["IDPRODUTOCLIENTE"].ToString() + "_" + dataItem["IDUNIDADEDEARMAZENAGEM"].ToString() + "_" + dataItem["endereco"].ToString() + "_" + dataItem["loteReferencia"].ToString();
        }

        // lblIdProdutoCliente.Text + "_" + lblUA.Text + "_" + lblEndereco.Text + "_"+lblLote.Text


        ImageButton Image1 = (ImageButton)e.Item.FindControl("img");
        // ImageButton img1 = (ImageButton)e.Item.FindControl("img1");

        Label lblIdProduro = (Label)e.Item.FindControl("lblIdProduro");

        if (lblIdProduro == null || lblIdProduro.Text == "")
            return;

        Image1.Attributes.Add("OnClick", "NewWindow('frmPopUpProduto.aspx?cod=" + lblIdProduro.Text + "', 'pg', '350', '400', 'no')");
        criarImagem(lblIdProduro.Text);

        if (File.Exists(MapPath("imgReport") + "\\p\\" + lblIdProduro.Text + ".jpg"))
            Image1.ImageUrl = "imgReport/p" + lblIdProduro.Text + ".jpg";
        else
            Image1.Enabled = false;
        // img1.ImageUrl = "imgReport/" + lblIdProduro.Text + ".jpg";

    }

    private void criarImagem(string IdProduto)
    {
        DataTable dImagem = new SistranBLL.Produto().RetornarImagemProduto(Convert.ToInt32(IdProduto));
        if (dImagem.Rows.Count > 0)
        {
            byte[] imagem = (byte[])dImagem.Rows[0]["FOTO"];
            MemoryStream ms = new MemoryStream(imagem);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            returnImage.Save(Server.MapPath(@"imgReport/" + IdProduto + ".jpg"));
            redimensionarImagem(returnImage, new Size(20, 20), IdProduto);
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        AddCart();
    }

    protected void ButtonOrdenar_Click(object sender, ImageClickEventArgs e)
    {

    }

    public void redimensionarImagem(System.Drawing.Image imagem, Size tamanho, string nome)
    {
        int larguraOrigem = imagem.Width;
        int alturaOrigem = imagem.Height;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)tamanho.Width / (float)larguraOrigem);
        nPercentH = ((float)tamanho.Height / (float)alturaOrigem);

        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;

        int larguraDestino = (int)(larguraOrigem * nPercent);
        int alturaDestino = (int)(alturaOrigem * nPercent);

        Bitmap b = new Bitmap(larguraDestino, alturaDestino);
        Graphics g = Graphics.FromImage((System.Drawing.Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        g.DrawImage(imagem, 0, 0, larguraDestino, alturaDestino);
        g.Dispose();

        b.Save(Server.MapPath(@"imgReport/p" + nome + ".jpg"));
        //return (System.Drawing.Image)b;
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtsTemp"];
        DataView dv = dt.DefaultView;
        Label lblOrdenaDescricao = (Label)e.Item.FindControl("lblOrdenaDescricao");
        ImageButton btnOrdenarDescricao = (ImageButton)e.Item.FindControl("btnOrdenarDescricao");

        if (e.CommandArgument.ToString() == "Descricao")
        {
            if (lblOrdenaDescricao.Text == "ASC")
            {
                dv.Sort = "Descricao desc";
                lblOrdenaDescricao.Text = "DESC";

                btnOrdenarDescricao.ImageUrl = "images/setaPraCima.jpg";
            }
            else
            {
                dv.Sort = "Descricao asc";
                lblOrdenaDescricao.Text = "ASC";
                btnOrdenarDescricao.ImageUrl = "images/seta.jpg";

            }
        }
        rpt.DataSource = dv;
        rpt.DataBind();

    }

    protected void ImageButton2_Click1(object sender, ImageClickEventArgs e)
    {
        AddCart();
    }

    protected void lnkDescricao_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtsTemp"];
        DataView dv = dt.DefaultView;

        dv.Sort = "Descricao, loteReferencia, Endereco asc";
        rpt.DataSource = dv;
        rpt.DataBind();
    }

    protected void lnkCodigo_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtsTemp"];
        DataView dv = dt.DefaultView;

        dv.Sort = "Codigo, loteReferencia, Endereco asc";
        rpt.DataSource = dv;
        rpt.DataBind();
    }
    protected void btnPesquisar_Click(object sender, EventArgs e)
    {
        DataTable dt = new SistranBLL.Produto().Pesquisar(txtPesq.Text, Convert.ToInt32(Session["IDEmpresa"]));
        CarregarRepeater(dt);
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        //DataTable dt = new SistranBLL.Produto().Pesquisar("", Convert.ToInt32(Session["IDEmpresa"]));
        //CarregarRepeater(dt);
    }
}
