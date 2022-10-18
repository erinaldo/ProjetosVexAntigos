using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;

public partial class frmCarrinho : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {
            //HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
            //tr0.Style.Add("display", "none");

            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
        //    carregar();
        }
        carregar();
       // btnFecharPedido.Visible = false;
    }

    private void carregar()
    {
        List<SistranMODEL.Carrinho> Lcarrinho = (List<SistranMODEL.Carrinho>)Session["Carrinho"];
        PlaceHolder1.Controls.Clear();

        //if (Lcarrinho == null)
        //    return;

        if (Lcarrinho == null || Lcarrinho.Count == 0)
        {
            PlaceHolder1.Controls.Add(new LiteralControl(@"<table  cellspacing=0 celpanding=1 widht='100%' border=0>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>Carrinho Vazio."));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
            btnFecharPedido.Visible = false;
            return;
        }

        btnFecharPedido.Visible = true;
        PlaceHolder1.Controls.Add(new LiteralControl(@"<table  cellspacing=0 celpanding=1 widht='100%' border=0>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  class='tdpCabecalho' align='center' nowrap=nowrap style='font-size:7pt;'>FOTO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrapclass='tdpCabecalho' align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;CÓDIGO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>DESCRIÇÃO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;DISPONÍVEL"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        //PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>DIVISAO"));
        //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;Lote"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;ENDEREÇO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;QUANTIDADE"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;VALOR UNITÁRIO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;VALOR TOTAL"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;EXCLUIR"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        decimal toro = Convert.ToDecimal(0);
        foreach (var item in Lcarrinho)
        {
            if (item.lblIDUnidadeDeArmazenagemLote.ToString() == "")
                item.lblIDUnidadeDeArmazenagemLote = "0";

            item.Disponivel = Convert.ToInt32(new SistranBLL.Produto().RetornarSaldoDisponivelPorIdProdutoClienteDivisao(int.Parse(item.IdClienteDivisao), item.IdProdutoCliente, int.Parse( item.lblIDUnidadeDeArmazenagemLote)));

            if (item.Disponivel < item.Quantidade)
            {
                item.Quantidade = item.Disponivel;
            }

            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;' align='center'>"));

            PlaceHolder1.Controls.Add(GerarFoto(item.idProduto.ToString()));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.CodigoProdutoCliente));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.Descricao));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.lblSaldoEndereco));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.Lote));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.Endereco));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            //PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.Divisao));
            
            //PlaceHolder1.Controls.Add(CriarTXTInvisivel(item.IdClienteDivisao));
            //PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>"));
            PlaceHolder1.Controls.Add(CriarTXT(item.chavedoItem , item.Quantidade));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.ValorUnitario.ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + (item.Quantidade * item.ValorUnitario).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));


            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='center' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" ));
            PlaceHolder1.Controls.Add(criarBotaoExcluir(item.chavedoItem, item.lblIDUnidadeDeArmazenagemLote));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            toro += (item.Quantidade * item.ValorUnitario);


        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan=8 align='right' nowrap=nowrap  class='tdpCabecalho' style='font-size:7pt;' >TOTAL"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap class='tdpCabecalho' style='font-size:7pt;'>" + toro.ToString("#0.00")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));
        
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
    }

    private Button criarBotaoExcluir(string IdProduto, string IdClienteDivisao)
    {
        Button bot = new Button();        
        bot.BorderStyle = BorderStyle.None;
        bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
        bot.Click += new EventHandler(btnExcluir_Click);
        bot.ID = IdProduto + "x" + IdClienteDivisao;
        bot.CssClass = "botaoExcluir";
        return bot;
    }

    private void btnExcluir_Click(object sender, System.EventArgs e)
    {
        List<SistranMODEL.Carrinho> Lcarrinho = (List<SistranMODEL.Carrinho>)Session["Carrinho"];
        
        Button bot = (Button)sender;
        int count = 0;
        bool removel = false;

        string[] codigos =  bot.ID.ToString().Split(new char[]{'x'});


        foreach (SistranMODEL.Carrinho item in Lcarrinho)
        {
            if (Lcarrinho[count].idProduto.ToString() == codigos[0] && Lcarrinho[count].IdClienteDivisao.ToString() == codigos[1])
            {
                Lcarrinho.RemoveAt(count);
                removel = true;
            }

            if (removel == true)
            {
                Session["Carrinho"] = Lcarrinho;
                carregar();
                return;
            }
                count +=1;
        }

        carregar();
    }

    public TextBox CriarTXTInvisivel(string Nome)
    {
        TextBox txt = new TextBox();
        txt.ID = Guid.NewGuid().ToString() +  "txtI" + Nome;
        txt.Width = 50;
        txt.CssClass = "txtValor";
        txt.Text = Nome;
        txt.Visible = false;
        //txt.TextChanged += txt_TextChanged;
        return txt;
    }

   
    public TextBox CriarTXT(string idProduto, decimal quantidade)
    {
        TextBox txt = new TextBox();
        txt.ID = "txt" + idProduto.ToString();
        txt.Width = 50;
        txt.CssClass = "txtValor";
        txt.Text = quantidade.ToString();
        txt.Attributes.Add("onkeypress", "return SomenteNumero(event)");
        txt.AutoPostBack = true;
        txt.TextChanged +=txt_TextChanged;

        return txt;

    }

    private void txt_TextChanged(object sender, System.EventArgs e)
    {
        List<SistranMODEL.Carrinho> Lcarrinho = (List<SistranMODEL.Carrinho>)Session["Carrinho"];
        int cont = 0;
        foreach (var item in Lcarrinho)
        {
            TextBox txt = (TextBox)sender;

            if (item.chavedoItem  == txt.ID.Replace("txt", ""))
            {
                Lcarrinho[cont].Quantidade = Convert.ToDecimal(txt.Text);
            }
            cont += 1;
        }
        Session["Carrinho"] = Lcarrinho;               
        carregar();
    }

    public Image GerarFoto(string idProduto)
    {
        Image Image1 = new System.Web.UI.WebControls.Image();

        string x = idProduto;
        if (File.Exists("~/imgReport/" + x + ".jpg"))
        {
            Image1.ImageUrl = "imgReport/" + x + ".jpg";
        }
        else
        {

            DataTable dImagem = new SistranBLL.Produto().RetornarImagemProduto(Convert.ToInt32(idProduto));
            if (dImagem.Rows.Count > 0)
            {
                byte[] imagem = (byte[])dImagem.Rows[0]["FOTO"];
                MemoryStream ms = new MemoryStream(imagem);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                returnImage.Save(Server.MapPath(@"imgReport/" + x + ".jpg"));
                Image1.ImageUrl = "imgReport/" + x + ".jpg";
            }
            else
            {
                Image1.ImageUrl = "~/Images/Maquina.jpg";
                Image1.Height = 15;
            }
        }

        Image1.Height = 25;
        Image1.Attributes.Add("OnClick", "NewWindow('frmPopUpProduto.aspx?cod=" + x + "', 'pg', '350', '400', 'no')");
        Image1.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");

        return Image1;
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["tipo"] == null)
        {
            Response.Redirect("frmpedido.aspx");
        }
        else
        {
            Response.Redirect("frmpedidoLista.aspx");
        }
    }

    protected void btnFecharPedido_Click(object sender, EventArgs e)
    {
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
        SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "FECHOU PEDIDO", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
        Response.Redirect("frmFecharPedido.aspx");
    }
}