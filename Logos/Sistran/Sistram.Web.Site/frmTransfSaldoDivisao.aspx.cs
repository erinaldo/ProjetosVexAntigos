using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class frmTransfSaldoDivisao :System.Web.UI.Page
{
    string p = "";
    DataRow temp;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if ((txtCodigo.Text.Trim().Length > 0 || txtDescricao.Text.Trim().Length > 0) && Label1.Text !="0")
                {
                    disparar(txtDescricao.Text, txtCodigo.Text);
                }          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        pnlDestino.Visible = false;
        Panel4.Visible = false;
        tblConfirmar.Visible = false;
        lblOrigemDivisaoClienteSelecionado.Text = "";
        lblOrigemDivisaoClienteSelecionado0.Text = "";

        lblOrigemDivisaoOrigemSelecionatoTexto.Text = "";
        lblOrigemDivisaoOrigemSelecionatoTexto0.Text = "";
        Pesquisar();
    }

    private void Pesquisar()
    {
        if (txtCodigo.Text.Trim().Length > 0 || txtDescricao.Text.Trim().Length > 0)
        {
            DataTable dt = new SistranBLL.Produto().ListarProdutoIniciais(txtCodigo.Text, txtDescricao.Text, Session["IDEmpresa"].ToString());

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    Label1.Text = dt.Rows[0]["IDProdutoCliente"].ToString();
                    disparar(dt.Rows[0]["Descricao"].ToString(), dt.Rows[0]["Codigo"].ToString());
                }
                else
                {
                    ModalPopupExtender1.Show();
                    RadGrid16.DataSource = dt;
                    RadGrid16.DataBind();
                }
            }
        }
        else
        {
            ModalPopupExtender1.Hide();
            //ModalPopupExtender1.Dispose();
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Digite algum filtro.')", true);
            return;
        }
    }    

    protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {

    }

    DataTable dt;
    protected void RadGrid16_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        Label1.Text = e.CommandName.ToString();
        LinkButton lnl = (LinkButton)e.Item.FindControl("lnkCodigo");
        LinkButton lnkDescricao = (LinkButton)e.Item.FindControl("lnkDescricao");

        if (lnl != null)
        {
            disparar(lnkDescricao.Text, lnl.Text);
        }
    }

    private void disparar(string descricao, string Codigo)
    {
        dt = new DataTable("dts");
        txtCodigo.Text = Codigo;
        txtDescricao.Text = descricao;

        if (Label1.Text != "0")
        {
            DataTable dtEncontrados = new SistranBLL.Produto().ListarSaldoOrigem(Label1.Text, Session["IDEmpresa"].ToString());
            Panel4.Visible = true;

            dt.Columns.Add("CODIGO");
            dt.Columns.Add("NOME");
            dt.Columns.Add("IDPARENTE");
            dt.Columns.Add("SALDO");
            dt.Columns.Add("SALDODIVISAO");
            dt.Columns.Add("SALDODISPONIVEL");
            dt.Columns.Add("IDESTOQUEDIVISAO");
            dt.Columns.Add("IDCLIENTEDIVISAO");
            MontarDT(dtEncontrados);
        }

        DataView dv = dt.DefaultView;
        dv.Sort = "IDCLIENTEDIVISAO Asc";
        GridView1.DataSource = dv;
        GridView1.DataBind();
        GridView1.Visible = false;

        carregarPlaceHolderOrigem(dt);

        if (dt.Rows.Count == 0)
        {            
                lblOrigemDivisaoOrigemSelecionatoTexto.Text = "Saldo em estoque não foi encontrado.";
                return;           
        }

    }

    private void carregarPlaceHolderOrigem(DataTable dt)
    {
        PHOrigem.Controls.Clear();
        PHOrigem.Controls.Add(new LiteralControl(@"<table cellspacing=1 celpanding=1>"));

        //define o primeiro nivel
        DataRow[] r = dt.Select("IDPARENTE IS NULL OR IDPARENTE=''");

        PHOrigem.Controls.Add(new LiteralControl(@"<tr>"));
        PHOrigem.Controls.Add(new LiteralControl(@"<td>"));
        PHOrigem.Controls.Add(new LiteralControl(@"</td>"));
        PHOrigem.Controls.Add(new LiteralControl(@"<td align='rigth' style='font-weight: bold; font-family: Verdana' > Disponível"));
        PHOrigem.Controls.Add(new LiteralControl(@"</td>"));
        PHOrigem.Controls.Add(new LiteralControl(@"</tr>"));

        bool encontrou = false;

        foreach (DataRow item in r)
        {
            encontrou = true;
            string txt = item["SALDODISPONIVEL"].ToString();

            if (txt != "")
            {
                txt = Convert.ToDouble(txt).ToString();
            }

            PHOrigem.Controls.Add(new LiteralControl(@"<tr>"));
            PHOrigem.Controls.Add(new LiteralControl(@"<td>"));
            string chave = item["IDCLIENTEDIVISAO"].ToString() + "|" + txt + "|" + item["nome"].ToString() + "|" + item["IDESTOQUEDIVISAO"].ToString();
            PHOrigem.Controls.Add(criarBotaoDivisao(chave, item["nome"].ToString(), txt));
            PHOrigem.Controls.Add(new LiteralControl(@"</td>"));
            PHOrigem.Controls.Add(new LiteralControl(@"<td align='right'>"));
            PHOrigem.Controls.Add(txt_ReadOnly(txt));
            PHOrigem.Controls.Add(new LiteralControl(@"</td>"));
            PHOrigem.Controls.Add(new LiteralControl(@"</tr>"));


            p = "&nbsp;&nbsp;";
            ProdurarFilhosOrigem(dt, item["IDCLIENTEDIVISAO"].ToString(), PHOrigem);
        }
        PHOrigem.Controls.Add(new LiteralControl(@"</table>"));


        if (encontrou == true)
        {
            pnlAjax.Visible = true;
            DataTable dtProd = new SistranBLL.Produto().ListarProdutosByIdProdutoCliente(Label1.Text);

            lblCodigoProdutoCliente.Text = txtCodigo.Text;
            
            if (dt.Rows.Count > 0)
            {
                lblMetro0.Text = dtProd.Rows[0]["CODIGODEBARRAS"].ToString();
                lblMetro.Text = Convert.ToDecimal(dtProd.Rows[0]["Rodoviario"]).ToString("#0.000");

                if (!File.Exists(Server.MapPath(@"imgReport/" + dtProd.Rows[0]["IDPRODUTO"].ToString() + ".jpg")))
                {
                    DataTable dImagem = new SistranBLL.Produto().RetornarImagemProduto(Convert.ToInt32(dtProd.Rows[0]["IDPRODUTO"].ToString()));

                    if (dImagem.Rows.Count > 0)
                    {
                        byte[] imagem = (byte[])dImagem.Rows[0]["FOTO"];
                        string x = dtProd.Rows[0]["IDPRODUTO"].ToString();
                        MemoryStream ms = new MemoryStream(imagem);
                        System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                        returnImage.Save(Server.MapPath(@"imgReport/" + x + ".jpg"));
                        imgPop.ImageUrl = "imgReport/" + x + ".jpg";
                    }
                   
                }
                else
                {
                    imgPop.ImageUrl = "imgReport/" + dtProd.Rows[0]["IDPRODUTO"].ToString() + ".jpg";

                }
            }
        }
    }
    private TextBox txt_ReadOnly(string valor)
    {
        TextBox txt = new TextBox();
        txt.ReadOnly = true;
        txt.CssClass = "txtValor";
        txt.Width = new Unit("70px");
        txt.Text = valor;

        return txt;
    }

    private Button criarBotaoDivisao(string chave, string texto, string valor)
    {
        Button bot = new Button();
        bot.Text = texto;
        bot.BorderStyle = BorderStyle.None;
        bot.Font.Name = "Verdana";
        bot.Style.Add("font-size", "7pt");
        bot.CommandArgument = chave;
        bot.Enabled = false;
        if (valor != "" && valor != "0")
        {
            bot.Attributes.Add("onmouseover", "javascript: this.style.cursor = 'hand'");
            bot.Enabled = true;

            bot.Click += new EventHandler(Button_Click);
        }

        bot.BackColor = System.Drawing.Color.Transparent;
        bot.ForeColor = System.Drawing.Color.Black;
        return bot;
    }

    private void Button_Click(object sender, System.EventArgs e)
    {
        Button b = (Button)sender;        
        string[] chave = b.CommandArgument.Split('|');
        lblOrigemDivisaoClienteSelecionado.Text = chave[0];
        lblOrigemDivisaoOrigemSelecionatoTexto.Text = chave[2];
        lblIdEstoqueDivisaoOrigem.Text = chave[3];
        lblDivDisponivel.Text = chave[1];
        txtDivDisponivel.Text = chave[1];


        CarregarMenuDivisao();
        pnlDestino.Visible = true;
        tblConfirmar.Visible = true;

    } 

    private void ProdurarFilhosOrigem(DataTable dt, string valor, PlaceHolder PHOrigem)
    {      

        DataRow[] r = dt.Select("IDPARENTE='"+ valor +"'");

        foreach (DataRow item in r)
        {
           string txt = item["SALDODISPONIVEL"].ToString();

            if(txt!="")
            {
                txt = Convert.ToDouble(txt).ToString();
            }


            PHOrigem.Controls.Add(new LiteralControl(@"<tr>"));   
            PHOrigem.Controls.Add(new LiteralControl(@"<td>" + p ));
            string chave = item["IDCLIENTEDIVISAO"].ToString() + "|" + txt + "|" + item["nome"].ToString() + "|" + item["IDESTOQUEDIVISAO"].ToString();
            PHOrigem.Controls.Add(criarBotaoDivisao(chave, item["nome"].ToString(), txt));

            PHOrigem.Controls.Add(new LiteralControl(@"</td>"));
            PHOrigem.Controls.Add(new LiteralControl(@"<td align='right'>"));
            PHOrigem.Controls.Add(txt_ReadOnly(txt));
            PHOrigem.Controls.Add(new LiteralControl(@"</td>" ));
            PHOrigem.Controls.Add(new LiteralControl(@"</tr>"));

            r = dt.Select("IDPARENTE='" + item["IDCLIENTEDIVISAO"].ToString() + "'");
            if (r.Length > 0)
            {
                p += "&nbsp;&nbsp;";
                ProdurarFilhosOrigem(dt, item["IDCLIENTEDIVISAO"].ToString(), PHOrigem);
            }
        }


    }

    protected void MontarDT(DataTable dtEncontrados)
    {

        int count = 0;
        foreach (DataRow item in dtEncontrados.Rows)
        {
            temp = dt.NewRow();
            temp[0] = txtCodigo.Text;
            temp[1] = item["NOME"].ToString();
            temp[2] = item["IDPARENTE"].ToString();
            temp[3] = item["SALDO"].ToString();
            temp[4] = item["SALDODIVISAO"].ToString();
            temp[5] = item["SALDODISPONIVEL"].ToString();
            temp[6] = item["IDESTOQUEDIVISAO"].ToString();
            temp[7] = item["IDCLIENTEDIVISAO"].ToString();
            dt.Rows.Add(temp);

            if (count == 0 && item["IDPARENTE"].ToString()!="")
            {             
                PaisRec(item["IDPARENTE"].ToString());
            }
            else
            {
                //if (item["IDPARENTE"].ToString() != "")
                //{
                    DataRow[] d = dt.Select("IDCLIENTEDIVISAO='" + item["IDPARENTE"].ToString() + "'", "");
                    //ainda nao tem o pai
                   if (d.Length == 0)
                   {
                       PaisRec(item["IDPARENTE"].ToString());
                   }
                //}
            }


            count += 1;
        }
    }

    protected void PaisRec(string idParente)
    {
        if (idParente == "")
            return;

        DataTable DivisoesCompleta = (DataTable)Session["DivisoesCompleta"];

        DataRow[] dr = DivisoesCompleta.Select("IDClienteDivisao='" + idParente + "'", "");

        DataRow[] d = dt.Select("IDCLIENTEDIVISAO='" + dr[0]["IDCLIENTEDIVISAO"].ToString() + "'", "");

        if (d.Length == 0)
        {
            if (dr.Length > 0)
            {
                temp = dt.NewRow();
                temp[0] = txtCodigo.Text;
                temp[1] = dr[0]["NOME"].ToString();
                temp[2] = dr[0]["IDPARENTE"];
                temp[3] = "";
                temp[4] = "";
                temp[5] = "";
                temp[6] = "";
                temp[7] = dr[0]["IDClienteDivisao"].ToString();
                dt.Rows.Add(temp);

                if (dr[0]["IDPARENTE"].ToString() != "")
                {
                    PaisRec(dr[0]["IDPARENTE"].ToString());
                }

            }
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        txtCodigo.Text = "";
        txtDescricao.Text ="";
        Label1.Text = "0";
        ModalPopupExtender1.Hide();
        ModalPopupExtender1.Dispose();
    }

    DataTable dtxx;
    string n = "";
    DataRow pp;

    private void CarregarMenuDivisao()
    {
        DataTable DivisoesCompleta = (DataTable)Session["DivisoesCompleta"];

        dtxx = new DataTable();
        dtxx.Columns.Add("IDCLIENTEDIVISAO");
        dtxx.Columns.Add("NOME");

        DataRow[] xx = DivisoesCompleta.Select("IDPARENTE IS NULL ", "");

        if (xx.Length == 0)
        {
            return;
        }

        //n += "&nbsp;&nbsp;";
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

        Repeater1.DataSource = dtxx;
        Repeater1.DataBind();

    }

    private void procurarFilhosDestino(DataTable DivisoesCompleta, string IdClienteDivisao)
    {
        //DataRow[] cc = DivisoesCompleta.Select("IDPARENTE='" + IdClienteDivisao + "'", "");

        //foreach (DataRow item in cc)
        //{
        //    pp = dtxx.NewRow();
        //    pp[0] = item["IDCLIENTEDIVISAO"].ToString();
        //    pp[1] = n +  item["NOME"].ToString();
        //    dtxx.Rows.Add(pp);


        //    DataRow[] ccx = DivisoesCompleta.Select("IDPARENTE='" + item["IDCLIENTEDIVISAO"].ToString() + "'", "");

        //    if (ccx.Length > 0)
        //    {
        //        n += "&nbsp;&nbsp;";
        //        foreach (DataRow itemff in ccx)
        //        {
        //            pp = dtxx.NewRow();
        //            pp[0] = itemff["IDCLIENTEDIVISAO"].ToString();
        //            pp[1] = n + itemff["NOME"].ToString();
        //            dtxx.Rows.Add(pp);


        //            DataRow[] ccz = DivisoesCompleta.Select("IDPARENTE='" + itemff["IDCLIENTEDIVISAO"].ToString() + "'", "");
        //            if (ccz.Length > 0)
        //            {
        //                foreach (DataRow itemfff in ccz)
        //                {
        //                    n += "&nbsp;&nbsp;";
        //                    pp = dtxx.NewRow();
        //                    pp[0] = itemfff["IDCLIENTEDIVISAO"].ToString();
        //                    pp[1] = n + itemfff["NOME"].ToString();
        //                    dtxx.Rows.Add(pp);

        //                }
        //            }

        //        }
        //    }
          
        //}

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

    protected void btnDivConfirmar_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lblDivDisponivel.Text) < Convert.ToInt32(txtDivDisponivel.Text))
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Verifique as Quantidades.')", true);
            return;
        }


        divqtd.Visible = false;
        lblDivDestino.Text = "";
        lblDivOrigem.Text = "";
        txtDivDisponivel.Text = "";
    }

    protected void btnConfirmarTudo_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender1.Hide();
        //ModalPopupExtender1.Dispose();
        int calc = 0;
        for (int i = 0; i < Repeater1.Items.Count; i++)
        {
            TextBox txt = (TextBox)Repeater1.Items[i].FindControl("txtValor");
            Label lblIDClienteDivisao = (Label)Repeater1.Items[i].FindControl("lblIDClienteDivisao");

            try
            {
                calc += Convert.ToInt32(txt.Text);

            }
            catch (Exception)
            {
                txt.Text = "0";

            }

        }

        if (calc > Convert.ToInt32(lblDivDisponivel.Text))
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('A soma das quantidades digitadas excede o Saldo Disponível.')", true);
            return;
        }

        for (int i = 0; i < Repeater1.Items.Count; i++)
        {

            TextBox txt = (TextBox)Repeater1.Items[i].FindControl("txtValor");

            if (txt.Text != "0")
            {
                Label lblIDClienteDivisao = (Label)Repeater1.Items[i].FindControl("lblIDClienteDivisao");
                string idProdutoCliente = Label1.Text;

                #region Origem

                string idEstoqueOrigem = new SistranBLL.Produto.Estoque().ConsultarEstoqueByIDProdutoCliente(Convert.ToInt32(idProdutoCliente)).ToString();

                string IdEstoqueDivisaoOrigem = new SistranBLL.Produto.Estoque().ConsultarEstoqueDivisao(Convert.ToInt32(idEstoqueOrigem), Convert.ToInt32(lblOrigemDivisaoClienteSelecionado.Text)).ToString();
                // inserir na estique divisao ORIGEM
                if (IdEstoqueDivisaoOrigem == "0")
                {
                    IdEstoqueDivisaoOrigem = new SistranBLL.Produto.Estoque().InserirEstoqueDivisao(Convert.ToInt32(IdEstoqueDivisaoOrigem), Convert.ToInt32(lblOrigemDivisaoClienteSelecionado.Text), Convert.ToInt32(txt.Text)).ToString();
                }
                else
                {
                    SistranBLL.Produto.Estoque o = new SistranBLL.Produto.Estoque();
                    o.AtualizarEstoqueDivisao(Convert.ToInt32(IdEstoqueDivisaoOrigem), Convert.ToInt32(txt.Text), "-");

                }

                

                #endregion
                
                #region Destino
                string idEstoqueDestino = new SistranBLL.Produto.Estoque().ConsultarEstoqueByIDProdutoCliente(Convert.ToInt32(idProdutoCliente)).ToString();
                string IdEstoqueDivisaoDestino = new SistranBLL.Produto.Estoque().ConsultarEstoqueDivisao(Convert.ToInt32(idEstoqueDestino), Convert.ToInt32(lblIDClienteDivisao.Text)).ToString();
                // inserir na estique divisao DESTINO
                if (IdEstoqueDivisaoDestino == "0")
                {
                    IdEstoqueDivisaoDestino = new SistranBLL.Produto.Estoque().InserirEstoqueDivisao(Convert.ToInt32(idEstoqueDestino), Convert.ToInt32(lblIDClienteDivisao.Text), Convert.ToInt32(txt.Text)).ToString();
                }
                else
                {
                    SistranBLL.Produto.Estoque o = new SistranBLL.Produto.Estoque();
                    o.AtualizarEstoqueDivisao(Convert.ToInt32(IdEstoqueDivisaoDestino), Convert.ToInt32(txt.Text), "+");

                }
                #endregion

                List<SistranMODEL.Usuario> luser = (List<SistranMODEL.Usuario>) Session["USUARIO"];
                                
                #region Movimentação Saida
                string EstoqueDivisaoMovSaida = new SistranBLL.Produto.Estoque().InserirMovimentacaoSaida(IdEstoqueDivisaoDestino.ToString(), IdEstoqueDivisaoOrigem.ToString(), luser[0].UsuarioId.ToString(), txt.Text).ToString();
                #endregion

                #region Movimentação Entrada
                string EstoqueDivisaoMovEntrada= new SistranBLL.Produto.Estoque().InserirMovimentacaoEntrada(IdEstoqueDivisaoDestino.ToString(), IdEstoqueDivisaoOrigem.ToString(), luser[0].UsuarioId.ToString(), txt.Text).ToString();
                #endregion


            }
        }
        disparar(txtDescricao.Text, txtCodigo.Text);
        pnlDestino.Visible = false;
        Repeater1.DataSource = null;
        Repeater1.DataBind();

        txtCodigo.Text = "";
       // Label1.Text = "";
        txtDescricao.Text = "";
        PHOrigem.Controls.Clear();
        Panel4.Visible = false;
        pnlAjax.Visible = false;



        ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Transferência Efetuada com sucesso.')", true);
        
    }

    protected void txtreoeater_TextChanged(object sender, EventArgs e)
    {

        //int calc = 0;
        //foreach (Control cc in Repeater1.Controls)
        //{
        //    TextBox tb = cc as TextBox;

        //    if (tb != null && tb.Text != "")
        //    {
        //        calc += Convert.ToInt32(tb.Text);
        //    }
        //}

        //if (calc > Convert.ToInt32(lblDivDisponivel.Text))
        //{
        //    ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('A soma das quantidades digitadas excede o Saldo Disponível.')", true);
        //    return;
        //}
    }

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        TextBox txt = (TextBox)e.Item.FindControl("txtValor");
        Label lblIDClienteDivisao = (Label)e.Item.FindControl("lblIDClienteDivisao");


        if (txt != null)
        {
            txt.Text = "0";

            if (lblIDClienteDivisao.Text == lblOrigemDivisaoClienteSelecionado.Text)
            {
                txt.Visible = false;
            }

            txt.Attributes.Add("onkeypress", " return SomenteNumero(event);");
        }

    }
    protected void btnLimparFiltro_Click(object sender, EventArgs e)
    {
        pnlDestino.Visible = false;
        Repeater1.DataSource = null;
        Repeater1.DataBind();

        txtCodigo.Text = "";
        // Label1.Text = "";
        txtDescricao.Text = "";
        PHOrigem.Controls.Clear();
        Panel4.Visible = false;
        pnlAjax.Visible = false;
    }
}

    