using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using System.Configuration;


public partial class frmFecharPedido : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CarregarCombo();
            //HtmlTableCell tr0 = (HtmlTableCell)Master.FindControl("tr0");
            //tr0.Style.Add("display", "none");

            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            txtDestinatario.Focus();
            btnVoltarCarrinho.Visible = false;
        }
    }

    private void CarregarCombo(DataTable dt)
    {
        cboDestinatario.Items.Clear();
        cboDestinatario.DataSource = dt; //new SistranBLL.Destinatario().ListarDestinatario(txtDestinatario.Text);
        cboDestinatario.DataTextField = "Nome";
        cboDestinatario.DataValueField = "IDCADASTRO";
        cboDestinatario.DataBind();
        cboDestinatario.Items.Insert(0, new ListItem("Selecione","0"));
        cboDestinatario.Visible = true;
        cboDestinatario.Width = 0;
    }

    decimal tot = 0;
    protected void btnConfirma_Click(object sender, EventArgs e)
    {
       
        int idDocumento = 0;
        SistranBLL.Pedido oPed = new SistranBLL.Pedido();

        try
        {
            List<SistranMODEL.Carrinho> Lcarrinho = (List<SistranMODEL.Carrinho>)Session["Carrinho"];

            

            foreach (var item in Lcarrinho)
            {
                item.Disponivel = Convert.ToInt32(new SistranBLL.Produto().RetornarSaldoDisponivelPorIdProdutoClienteDivisao(int.Parse(item.IdClienteDivisao), item.IdProdutoCliente, int.Parse( item.lblIDUnidadeDeArmazenagemLote)));

                if (item.Disponivel < item.Quantidade)
                {
                    btnVoltarCarrinho.Visible = true;
                    btnConfirma.Visible = false;
                    throw new Exception("A quantidade de algum item esta maior que o saldo disponivel. Favor Verificar o Carrinho");                    
                }

            }



            if (cboDestinatario.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Escolha um Destinatário.')", true);
                return;
            }

            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];


            foreach (var item in Lcarrinho)
            {
                tot += (item.Quantidade * item.ValorUnitario);
            }

            //gravar na documento
            DataTable dtCliente = SistranBLL.Cliente.Read(Convert.ToInt32(HttpContext.Current.Session["IDEmpresa"].ToString()));
            int numerador = oPed.Numerador(Convert.ToInt32(dtCliente.Rows[0]["IDFilialPadraoInternet"]), "PEDIDO");
            
            idDocumento = oPed.InserirTabDocumentoPedido(dtCliente.Rows[0]["IDFilialPadraoInternet"].ToString(), dtCliente.Rows[0]["IDFilialPadraoInternet"].ToString(), ILusuario[0].EmpresaId.ToString(), Session["IDEmpresa"].ToString(), cboDestinatario.SelectedValue, "PED", numerador.ToString(), "0", "0", Lcarrinho.Count.ToString(), tot.ToString());

            //gravar na documentoItem
            int idDocumentoItem = 0;
            if (idDocumento > 0)
            {
                foreach (var item in Lcarrinho)
                {

                    int idProdutoEmbalagem = oPed.RetornarIdProdutoEmbalagem(item.idProduto, item.IdProdutoCliente);

                    if (idProdutoEmbalagem > 0)
                    {
                        idDocumentoItem = oPed.InserirTabDocumentoItemPedido(idDocumento, idProdutoEmbalagem, ILusuario[0].UsuarioId, Convert.ToInt32(item.IdClienteDivisao), Convert.ToInt32(item.Quantidade), item.ValorUnitario,int.Parse( item.lblIDUnidadeDeArmazenagemLote), item.Lote);
                    }
                }
            }


            //gravar na documentoFilial
            oPed.InserirTabDocumentoFilial(idDocumento, Convert.ToInt32(dtCliente.Rows[0]["IDFilialPadraoInternet"]), 1, FuncoesGerais.LoadDataSetConstantes("ConstDcoFilSitLiberadoSeparacao"));


            pnlConfirmacao.Visible = true;
            btnConfirma.Visible = false;
            cboDestinatario.Enabled = false;
            //carregarDadosDestinatario();
            //carregar();
            lblNumero.Text = "Número do Pedido: " + numerador.ToString();
            lblNumero.ForeColor = System.Drawing.Color.Red;
            lblNumero.Visible = false;

            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "CONFIRMOU DEFINITIVAMENTE UM PEDIDO", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Pedido Realizado com Sucesso.')", true);

            DataTable dtSolicitante = new SistranBLL.Pedido().AprovarPedidoPrepararDadosEmail(idDocumento.ToString());

            string eMailSolcitante = "";

            try
            {
                string carta = MontarCarta(idDocumento.ToString(), eMailSolcitante, eMailSolcitante, true, true);


                carregar(carta);

                if (dtSolicitante.Rows.Count > 0)
                {
                    eMailSolcitante = dtSolicitante.Rows[0]["endereco"].ToString();
                    carta = MontarCarta(idDocumento.ToString(), eMailSolcitante, eMailSolcitante, true, false);
                    Sistran.Library.EnviarEmails.EnviarEmail(eMailSolcitante, ConfigurationSettings.AppSettings["emailPedido"].ToString(), "AVISO: NOVO PEDIDO DE NUMERO (CÓPIA): " + numerador.ToString(), carta, ConfigurationSettings.AppSettings["smtp"].ToString(), ConfigurationSettings.AppSettings["senhasmtp"].ToString(), "moises@sistecno.com.br");
                }


                DataTable dtAprovadores = new SistranBLL.Cadastro.CadastroContato().ListarEmailsAprovadoresPedidos();
                foreach (DataRow item in dtAprovadores.Rows)
                {
                    carta = MontarCarta(idDocumento.ToString(), item["ENDERECO"].ToString(), eMailSolcitante, false, false);
                    Sistran.Library.EnviarEmails.EnviarEmail(item["ENDERECO"].ToString(), ConfigurationSettings.AppSettings["emailPedido"].ToString(), "AVISO: NOVO PEDIDO DE NUMERO " + numerador.ToString(), carta, ConfigurationSettings.AppSettings["smtp"].ToString(), ConfigurationSettings.AppSettings["senhasmtp"].ToString(), "moises@sistecno.com.br");
                }
            }
            catch (Exception)
            {
            }

            Session["Carrinho"] = null;
        }
        catch (Exception ex)
        {
            oPed.CancelarPedidoByErro(idDocumento.ToString());
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);

        }
    }


    private string MontarCarta(string idDocumento, string emailAprovador, string emailSolicitante, bool Solicitante, bool tela)
    {
        List<SistranMODEL.Carrinho> Lcarrinho = (List<SistranMODEL.Carrinho>)Session["Carrinho"];
        List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)System.Web.HttpContext.Current.Session["USUARIO"];

        DataTable dtDest = new SistranBLL.Destinatario().ConsultarDadosDestinatario(cboDestinatario.SelectedValue);
        DataTable dtDoc = new SistranBLL.Pedido().ConsultarByDocumento(Convert.ToInt32(idDocumento));

        string caminhoUrl = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        if (HttpContext.Current.Request.ServerVariables["SERVER_PORT"] != "" && HttpContext.Current.Request.ServerVariables["SERVER_PORT"] != "80")
        {
            caminhoUrl += ":" + HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
        }

        string logAutorizador = "";
        if (Solicitante == true)
        {
            caminhoUrl = "#";
            logAutorizador = "Não Autorizado.";
        }
        else
        {
            logAutorizador = new SistranBLL.Usuario().ConsultarLoginByEmail(emailAprovador);
        }


        caminhoUrl += HttpContext.Current.Request.ServerVariables["URL"].Substring(0, HttpContext.Current.Request.ServerVariables["URL"].LastIndexOf("/"));
        caminhoUrl += @"/loginPedido.aspx?l=" + logAutorizador + "&e=" + Server.UrlEncode(emailAprovador).Replace("@", "|||").Replace(".", "***") + "&id_documento=" + idDocumento;

        string m = " <html>  <head></head> <body >  ";

        m += " <table border='0' cellpadding='2' cellspacing='2' width='95%'>  ";

        if (tela == false)
        {
            m += "   <tr>   ";
            m += " <td colspan='2' valign='middle'> <div align='center'><font size='3' face='Verdana'><a href='" + caminhoUrl + "&tipo=1" + "'><strong>Aprovar   ";
            m += " Pedido</strong></a></font></div></td>";
            m += " <td width='1%'><font size='3' face='Verdana'>&nbsp;</font></td>";
            m += " <td valign='middle'><div align='center'><font size='3' face='Verdana'><a href='" + caminhoUrl + "&tipo=0" + "'><strong>Cancelar   ";
            m += " Pedido</strong></a></font></div></td>  ";
            m += " </tr>  ";
        }

        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='4'> <p align='center'><font size='4' face='Verdana'><strong>";
        
        if (tela == false)
        {
            m += " AVISO DE ";
        }

        m += " NOVO PEDIDO: "+lblNumero.Text+"</strong></font></p></td>  ";
        m += " </tr>  ";
        m += " <tr>   ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>Efetuado Por:</strong></font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>E-Mail:</strong></font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + ILusuario[0].UsuarioNome + "</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'>"+emailSolicitante+"</font></td>  ";
        m += " </tr>  ";
        m += " <tr>   ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += "  <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='4'><font size='2' face='Verdana'><strong>Remetente:</strong></font><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='4'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["RAZAONOMEREMETENTE"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr>   ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>Destinatário:</strong></font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>Fantasia:</strong></font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["RAZAONOMEDESTINATARIO"].ToString() + "</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["FANTASIAAPELIDODESTINATARIO"].ToString() + "</font></td>  ";
        m += " </tr>  ";
        m += " <tr>   ";
        m += " <td width='1%'><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>CNPJ:</strong></font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>I.E:</strong></font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["CNPJCPFDESTINATARIO"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["IEDEST"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr>   ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>Endereço:</strong></font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'><strong>Cidade</strong>:</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["ENDERECODESTINATARIO"].ToString() + "," + dtDoc.Rows[0]["NUMERODESTINATARIO"].ToString() + "," + dtDoc.Rows[0]["COMPLEMENTODESTINATARIO"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td colspan='2'><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>" + dtDoc.Rows[0]["CIDADEDEST"].ToString() + "-" +  dtDoc.Rows[0]["ESTADODEST"].ToString() + "</font></td>  ";
        m += " </tr>  ";
        m += " <tr bgcolor='#CCCCCC'>   ";
        m += " <td><font size='2' face='Verdana'><strong>Cep:</strong></font></td>  ";
        m += " <td colspan='3'><font size='2' face='Verdana'>"+ dtDoc.Rows[0]["CEPDEST"].ToString() +"</font><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " </tr>  ";
        m += " </table>  ";
        m += " <p><font face='Verdana'><strong>Itens do Pedido:</strong></font></p>  ";
        m += " <table border=0 cellpadding='1' cellspacing='1' width='95%'>  ";
        m += " <tr bgcolor='#999999' >   ";
        m += " <td><font size='2' face='Verdana'><strong>Código</strong></font></td>  ";
        m += " <td><font size='2' face='Verdana'><strong>Descrição</strong></font></td>  ";
        m += " <td><font size='2' face='Verdana'><strong>Lote</strong></font></td>  ";
        //m += " <td>   ";

        m += " <td><font size='2' face='Verdana'><strong>UA</strong></font></td>  ";
        m += " <td>   ";

        m += " <div align='right'><font size='2' face='Verdana'><strong>Quantidade</strong></font></div></td>  ";
        m += " <td>   ";
        m += " <div align='right'><font size='2' face='Verdana'><strong>Valor Unitario</strong></font></div></td>  ";
        m += " <td>   ";
        m += " <div align='right'><font size='2' face='Verdana'><strong>Valor Total</strong></font></div></td>  ";
        m += " </tr>  ";

          decimal x = Convert.ToDecimal(0);
          foreach (var item in Lcarrinho)
          {
              m += " <tr>   ";
              m += " <td><font size='2' face='Verdana'>" + item.CodigoProdutoCliente + "</font></td>  ";
              m += " <td><font size='2' face='Verdana'>" + item.Descricao + "</font></td>  ";
              m += " <td><font size='2' face='Verdana'>" + item.Lote + "</font></td>  ";
              m += " <td><font size='2' face='Verdana'>" + item.ua + "</font></td>  ";
              m += " <td><div align='right'><font size='2' face='Verdana'>" + item.Quantidade.ToString("#0.00") + "</font></div></td>  ";
              m += " <td><div align='right'><font size='2' face='Verdana'>" + item.ValorUnitario.ToString("#0.00") + "</font></div></td>  ";
              m += " <td><div align='right'><font size='2' face='Verdana'>" + (item.Quantidade * item.ValorUnitario).ToString() + "</font></div></td>  ";
              m += " </tr>  ";
              x += (item.Quantidade * item.ValorUnitario);
          }

        m += "  <tr>   ";
        m += "  <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += "  <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += "   <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
        m += " <td><div align='right'><font size='2' face='Verdana'><strong>Total:</strong></font></div></td>  ";
        m += " <td > <div align='right'><font size='2' face='Verdana'><strong>"+x.ToString("#0.00")+"</strong></font></div></td>  ";
        m += " </tr>  ";
        m += " </table>  ";
        m += " </body> </html>   ";

     
        return m;
    }

    //private void carregarDadosDestinatario()
    //{
    //   //DataTable dtDest = new SistranBLL.Destinatario().ConsultarDadosDestinatario(cboDestinatario.SelectedValue);

    //   // PlaceHolder2.Controls.Clear();

    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"<table class='table'  cellspacing=0 celpanding=1 widht='100%' border=0>"));
    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));


    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left'  style='font-size:7pt;'>RAZÃO SOCIAL"));
    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left'  style='font-size:7pt;'>FANTASIA"));
    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left'  style='font-size:7pt;'>ENDEREÇO"));
    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left'  style='font-size:7pt;'>CIDADE"));
    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left'  style='font-size:7pt;'>CEP"));
    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));


    //   // decimal toro = Convert.ToDecimal(0);
    //   // foreach (DataRow item in dtDest.Rows)
    //   // {

    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"<tr>"));

    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"<td class='tdp' align='left' nowrap=nowrap >" + item["RazaoSocialNome"].ToString() + "&nbsp;&nbsp;&nbsp;"));
    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"<td  class='tdp' align='left' nowrap=nowrap >" + item["FantasiaApelido"].ToString() + "&nbsp;&nbsp;&nbsp;"));
    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));


    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"<td  class='tdp' align='right' nowrap=nowrap >" + item["Endereco"].ToString() + ", " + item["Numero"].ToString() + "&nbsp;&nbsp;&nbsp;"));
    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"<td  class='tdp' align='left' nowrap=nowrap >" + item["Nome"].ToString() + "&nbsp;&nbsp;&nbsp;"));
    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));

    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"<td  class='tdp' align='right' nowrap=nowrap >" + item["CEP"].ToString() + "&nbsp;&nbsp;&nbsp;"));
    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"</td>"));
    //   //     PlaceHolder2.Controls.Add(new LiteralControl(@"</tr>"));


    //   // }
    //   // PlaceHolder2.Controls.Add(new LiteralControl(@"</table>"));
    //   // * /
    //}

    private void carregar(string carta)
    {
        PlaceHolder1.Controls.Clear();
        PlaceHolder1.Controls.Add(new LiteralControl(carta));

        /*List<SistranMODEL.Carrinho> Lcarrinho = (List<SistranMODEL.Carrinho>)Session["Carrinho"];
        PlaceHolder1.Controls.Clear();     
     
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

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>DIVISAO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;QUANTIDADE"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;VALOR UNITÁRIO"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap class='tdpCabecalho' align='left' nowrap=nowrap style='font-size:7pt;'>&nbsp;&nbsp;&nbsp;VALOR TOTAL"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));


        decimal toro = Convert.ToDecimal(0);
        foreach (var item in Lcarrinho)
        {

            PlaceHolder1.Controls.Add(new LiteralControl(@"<tr>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>"));
            PlaceHolder1.Controls.Add(GerarFoto(item.idProduto.ToString()));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.CodigoProdutoCliente));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.Descricao));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.Disponivel.ToString("#0")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='left' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.Divisao));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'> " + item.Quantidade));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + item.ValorUnitario.ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap style='font-size:10px; font-name:Arial; border: 1px solid #dddddd;'>" + (item.Quantidade * item.ValorUnitario).ToString("#0.00")));
            PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

            PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));

            toro += (item.Quantidade * item.ValorUnitario);


        }
        PlaceHolder1.Controls.Add(new LiteralControl(@"<tr style='background-image:url(Images/skins/primeiro/img/menu_3_2.jpg); height:20px'>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"<td colspan=7 align='right' nowrap=nowrap  class='tdpCabecalho' style='font-size:7pt;' >TOTAL"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

        PlaceHolder1.Controls.Add(new LiteralControl(@"<td  align='right' nowrap=nowrap class='tdpCabecalho' style='font-size:7pt;'>" + toro.ToString("#0.00")));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</td>"));

       PlaceHolder1.Controls.Add(new LiteralControl(@"</tr>"));
        PlaceHolder1.Controls.Add(new LiteralControl(@"</table>"));
        */
        
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
                Image1.ImageUrl = "~/Images/naoDisponivel.jpg";
            }
        }

        Image1.Height = 60;
        Image1.Attributes.Add("OnClick", "NewWindow('frmPopUpProduto.aspx?cod=" + x + "', 'pg', '350', '400', 'no')");
        Image1.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");

        return Image1;
    }

    protected void RadGrid16_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        LinkButton lnkCodigo = (LinkButton)e.Item.FindControl("lnkCodigo");
        Label lblNomes = (Label)e.Item.FindControl("lblNomes");
        if (lnkCodigo != null)
        {
            cboDestinatario.SelectedValue = lnkCodigo.CommandName;
            txtDestinatario.Text = lblNomes.Text.ToUpper();
            RadGrid16.DataSource = null;
            RadGrid16.DataBind();
            dvPesquisa.Visible = false;
            btnConfirma.Visible = true;
            btnConfirma.Text = "Confirmar Pedido";

            DataTable dtDest = new SistranBLL.Destinatario().ConsultarDadosDestinatario(cboDestinatario.SelectedValue);
            List<SistranMODEL.Carrinho> Lcarrinho = (List<SistranMODEL.Carrinho>)Session["Carrinho"];
            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)System.Web.HttpContext.Current.Session["USUARIO"];

            if (dtDest.Rows.Count > 0)
            {
                pnlConfirmacao.Visible = true;
                bool tela = true;
                string m = " <html>  <head></head> <body >  ";

                m += " <table border='0' cellpadding='1' cellspacing='0' width='95%'>  ";

                if (tela == false)
                {
                    m += "   <tr>   ";
                    m += " <td colspan='2' valign='middle'> <div align='center'><font size='3' face='Verdana'><a href='#'><strong>Aprovar   ";
                    m += " Pedido</strong></a></font></div></td>";
                    m += " <td width='1%'><font size='3' face='Verdana'>&nbsp;</font></td>";
                    m += " <td valign='middle'><div align='center'><font size='3' face='Verdana'><a href='#'><strong>Cancelar   ";
                    m += " Pedido</strong></a></font></div></td>  ";
                    m += " </tr>  ";
                }

                m += " <tr bgcolor='#CCCCCC'>   ";
                m += " <td colspan='4'> <p align='center'><font size='4' face='Verdana'><strong>";

                if (tela == false)
                {
                    m += " AVISO DE ";
                }

                m += " CONFIRME OS DADOS DO PEDIDO: " + lblNumero.Text + "</strong></font></p></td>  ";
                m += " </tr>  ";
                m += " <tr>   ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " </tr>  ";
                m += " <tr bgcolor='#CCCCCC'>   ";
                m += " <td colspan='2'><font size='2' face='Verdana'><strong>Efetuado Por:</strong></font></td>  ";
                m += " <td colspan='2'><font size='2' face='Verdana'><strong>E-Mail:</strong></font></td>  ";
                m += " </tr>  ";
                m += " <tr bgcolor='#CCCCCC'>   ";
                m += " <td colspan='2'><font size='2' face='Verdana'>" + ILusuario[0].UsuarioNome + "</font></td>  ";
                m += " <td colspan='2'><font size='2' face='Verdana'>----</font></td>  ";
                m += " </tr>  ";
                m += " <tr>   ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += "  <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " </tr>  ";
                //m += " <tr bgcolor='#CCCCCC'>   ";
                //m += " <td colspan='4'><font size='2' face='Verdana'><strong>Remetente:</strong></font><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                //m += " </tr>  ";
                //m += " <tr bgcolor='#CCCCCC'>   ";
                //m += " <td colspan='4'><font size='2' face='Verdana'>" + dtDoc.Rows[0]["RAZAONOMEREMETENTE"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                //m += " </tr>  ";
                m += " <tr>   ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " </tr>  ";
                m += " <tr bgcolor='#CCCCCC'>   ";
                m += " <td colspan='2'><font size='2' face='Verdana'><strong>Destinatário:</strong></font></td>  ";
                m += " <td colspan='2'><font size='2' face='Verdana'><strong>Fantasia:</strong></font></td>  ";
                m += " </tr>  ";
                m += " <tr bgcolor='#CCCCCC'>   ";
                m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDest.Rows[0]["RazaoSocialNome"].ToString() + "</font></td>  ";
                m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDest.Rows[0]["FantasiaApelido"].ToString() + "</font></td>  ";
                m += " </tr>  ";
                m += " <tr>   ";
                m += " <td width='1%'><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " </tr>  ";
                m += " <tr bgcolor='#CCCCCC'>   ";
                m += " <td colspan='2'><font size='2' face='Verdana'><strong>CNPJ:</strong></font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td colspan='2'><font size='2' face='Verdana'><strong>I.E:</strong></font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " </tr>  ";
                m += " <tr bgcolor='#CCCCCC'>   ";
                m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDest.Rows[0]["CNPJCPF"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDest.Rows[0]["InscricaoRG"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " </tr>  ";
                m += " <tr>   ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " </tr>  ";
                m += " <tr bgcolor='#CCCCCC'>   ";
                m += " <td colspan='2'><font size='2' face='Verdana'><strong>Endereço:</strong></font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td colspan='2'><font size='2' face='Verdana'><strong>Cidade</strong>:</font></td>  ";
                m += " </tr>  ";
                m += " <tr bgcolor='#CCCCCC'>   ";
                m += " <td colspan='2'><font size='2' face='Verdana'>" + dtDest.Rows[0]["Endereco"].ToString() + "," + dtDest.Rows[0]["Numero"].ToString() + "," + dtDest.Rows[0]["Complemento"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " <td colspan='2'><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>" + dtDest.Rows[0]["NOME"].ToString() + "-" + dtDest.Rows[0]["UF"].ToString() + "</font></td>  ";
                m += " </tr>  ";
                m += " <tr bgcolor='#CCCCCC'>   ";
                m += " <td><font size='2' face='Verdana'><strong>Cep:</strong></font></td>  ";
                m += " <td colspan='3'><font size='2' face='Verdana'>" + dtDest.Rows[0]["CEP"].ToString() + "</font><font size='2' face='Verdana'>&nbsp;</font><font size='2' face='Verdana'>&nbsp;</font></td>  ";
                m += " </tr>  ";
                m += " </table>  ";
                m += " </body> </html>   ";

                PlaceHolder1.Controls.Clear();
                PlaceHolder1.Controls.Add(new LiteralControl(m));
                PlaceHolder1.Visible = true;
                
            }


            
        }

    }

    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        try
        {
            List<SistranMODEL.Carrinho> Lcarrinho = (List<SistranMODEL.Carrinho>)Session["Carrinho"];

            foreach (var item in Lcarrinho)
            {
                item.Disponivel = Convert.ToInt32(new SistranBLL.Produto().RetornarSaldoDisponivelPorIdProdutoClienteDivisao(int.Parse(item.IdClienteDivisao), item.IdProdutoCliente, int.Parse(item.lblIDUnidadeDeArmazenagemLote)));

                if (item.Disponivel < item.Quantidade)
                {
                    btnVoltarCarrinho.Visible = true;
                    btnConfirma.Visible = false;
                    throw new Exception("A quantidade de algum item esta maior que o saldo disponivel. Favor Verificar o Carrinho");
                }

            }


            if (txtDestinatario.Text.Length == 0 || txtDestinatario.Text == "")
            {
                txtDestinatario.Focus();
                //throw new Exception("Digite ao menos as iniciais.");
                return;
            }
            txtDestinatario.Text.Replace(".", "").Replace("/", "").Replace("\\", "").Replace("-", "");

            if (txtDestinatario.Text.Length == 11 || txtDestinatario.Text.Length == 14)
            {
                try
                {
                    //int m = int.Parse(txtDestinatario.Text);
                    bool tudoNumero = true;
                    for (int i = 0; i < txtDestinatario.Text.Length; i++)
                    {
                        string m = txtDestinatario.Text.Substring(i, 1);

                        try
                        {

                            int f = int.Parse(m);

                        }
                        catch (Exception)
                        {
                            tudoNumero = false;
                            break;
                        }                    

                    }

                    if(tudoNumero)
                        txtDestinatario.Text = FuncoesGerais.FormatarCnpj(txtDestinatario.Text);
                }
                catch (Exception)
                {}
            }

            DataTable d = new SistranBLL.Destinatario().ListarDestinatario(txtDestinatario.Text);

            if (d.Rows.Count == 0)
            {
                txtDestinatario.Text = "";
                txtDestinatario.Focus();
                cboDestinatario.SelectedIndex = 0;
                throw new Exception("Nenhum Destinatário Encontrado.");
            }

            dvPesquisa.Visible = true;
            RadGrid16.DataSource = d;
            RadGrid16.DataBind();
            CarregarCombo(d);

            if (d.Rows.Count == 0)
            {
                txtDestinatario.Text = "";
                txtDestinatario.Focus();
                cboDestinatario.SelectedIndex = 0;
                throw new Exception("Nenhum Destinatário Encontrado.");
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }


    protected void btnFecharDiv_Click(object sender, EventArgs e)
    {
        RadGrid16.DataSource = null;
        RadGrid16.DataBind();
        dvPesquisa.Visible = false;
        txtDestinatario.Focus();
    }

    
    protected void btnVoltarCarrinho_Click(object sender, EventArgs e)
    {

    }
}