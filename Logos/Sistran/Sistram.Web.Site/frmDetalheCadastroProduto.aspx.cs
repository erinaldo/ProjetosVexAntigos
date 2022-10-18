using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Data;
using System.IO;

public partial class frmDetalheCadastroProduto :  System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt;
            if (Request.QueryString["tipo"] != null)
                dt = (DataTable)Session["GRID"];
            else
            {
                dt = new SistranBLL.Produto().ListarProdutoByDivisaoCliente(Convert.ToInt32(Session["click"].ToString()), false);
                dt.Columns.Add("numero");
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["numero"] = i.ToString();
            }

            Session["dt_click"] = dt;
            criarRegistro(Request.QueryString["Codigo"]);

            CarregarListbox();
            CarregarMenuDivisao();

            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
        }
    }

    private void CarregarListbox()
    {
        ListBox1.DataSource = new SistranBLL.Cliente.Divisao().RetornarListaDivisoesProduto(Request.QueryString["Codigo"]);
        ListBox1.DataTextField = "Nome";
        ListBox1.DataValueField = "IDClienteDivisao";
        ListBox1.DataBind();
    }

    private void criarRegistro(string Codigo)
    {
        DataTable dt = (DataTable)Session["dt_click"];

        DataRow[] ow = dt.Select("CODIGO='" + Codigo + "'", "");

       // lblReg.Text = ow[0]["numero"].ToString();
        lblCodigo.Text = ow[0]["CODIGO"].ToString();
        lblCodCliente.Text = ow[0]["CODIGODOCLIENTE"].ToString();
        lblDescricao.Text = ow[0]["DESCRICAO"].ToString();
        lblSaldo.Text = Convert.ToDecimal(ow[0]["SALDO"]).ToString("#0");
        lblDataCadastro.Text = ow[0]["DATADECADASTRO"].ToString();
        lblDataLimite.Text = ow[0]["DATALIMITEDEUSO"].ToString();
        lblAtivo.Text = ow[0]["ATIVO"].ToString();
        lblConsumoMensal.Text = ow[0]["CONSUMOMENSAL"].ToString();
        lblSaldoMinimo.Text = ow[0]["SALDOMINIMO"].ToString();

        GerarImagem(ow[0]["IDPRODUTO"].ToString(), ow[0]["IdProdutoCliente"].ToString());    
        
    }

    private void GerarImagem(string IdProduto, string IdProdutoCliente)
    {
        string x = IdProduto.ToString();
        if (File.Exists("~/imgReport/" + x + ".jpg"))
        {
            Image1.ImageUrl = "imgReport/" + x + ".jpg";
        }
        else
        {

            DataTable dImagem = new SistranBLL.Produto().RetornarImagemProduto(Convert.ToInt32(IdProduto));
            if (dImagem.Rows.Count > 0)
            {
                byte[] imagem = (byte[])dImagem.Rows[0]["FOTO"];
                //string x = IdProduto.ToString();
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
        RadGrid1.DataSource = new SistranBLL.Produto().ListarProdutosByIdProdutoCliente(IdProdutoCliente);
        RadGrid1.DataBind();

    }

    //protected void btnAnterior_Click(object sender, ImageClickEventArgs e)
    //{
    //    DataTable dt = (DataTable)Session["dt_click"];
    //    DataRow[] ow = dt.Select("numero='" + (Convert.ToInt32(lblReg.Text) - 1).ToString() + "'", "");

    //    if (ow.Length > 0)
    //    {
    //        //lblReg.Text = ow[0]["numero"].ToString();
    //        lblCodigo.Text = ow[0]["CODIGO"].ToString();
    //        lblCodCliente.Text = ow[0]["CODIGODOCLIENTE"].ToString();
    //        lblDescricao.Text = ow[0]["DESCRICAO"].ToString();
    //        lblSaldo.Text = Convert.ToDecimal(ow[0]["SALDO"]).ToString("#0");
    //        lblDataCadastro.Text = ow[0]["DATADECADASTRO"].ToString();
    //        lblDataLimite.Text = ow[0]["DATALIMITEDEUSO"].ToString();
    //        lblAtivo.Text = ow[0]["ATIVO"].ToString();
    //        lblConsumoMensal.Text = ow[0]["CONSUMOMENSAL"].ToString();
    //        lblSaldoMinimo.Text = ow[0]["SALDOMINIMO"].ToString();
    //        GerarImagem(ow[0]["IDPRODUTO"].ToString(), ow[0]["IdProdutoCliente"].ToString());    
    //    }

    //}

    //protected void btnPosterior_Click(object sender, ImageClickEventArgs e)
    //{
    //    DataTable dt = (DataTable)Session["dt_click"];
    //    DataRow[] ow = dt.Select("numero='" + (Convert.ToInt32(lblReg.Text) + 1).ToString() + "'", "");

    //    if (ow.Length > 0)
    //    {
    //        //lblReg.Text = ow[0]["numero"].ToString();
    //        lblCodigo.Text = ow[0]["CODIGO"].ToString();
    //        lblCodCliente.Text = ow[0]["CODIGODOCLIENTE"].ToString();
    //        lblDescricao.Text = ow[0]["DESCRICAO"].ToString();
    //        lblSaldo.Text = Convert.ToDecimal(ow[0]["SALDO"]).ToString("#0");
    //        lblDataCadastro.Text = ow[0]["DATADECADASTRO"].ToString();
    //        lblDataLimite.Text = ow[0]["DATALIMITEDEUSO"].ToString();
    //        lblAtivo.Text = ow[0]["ATIVO"].ToString();
    //        lblConsumoMensal.Text = ow[0]["CONSUMOMENSAL"].ToString();
    //        lblSaldoMinimo.Text = ow[0]["SALDOMINIMO"].ToString();
    //        GerarImagem(ow[0]["IDPRODUTO"].ToString(), ow[0]["IdProdutoCliente"].ToString());    

    //    }
    //}

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

        ////n = "&nbsp;&nbsp;&nbsp;&nbsp;";
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
                lblSelecionado.Text = "Item Selecionado: "+ lblNome0.Text.Trim().Replace("&nbsp;","");
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

            //incluir
            SistranBLL.Cliente.Divisao o = new Cliente.Divisao();
            o.InserirEstoqueDivisao(Request.QueryString["Codigo"], hdCodigoDivisaoCliente.Value);          

            CarregarListbox();
            CarregarMenuDivisao();

            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "INCLUIU UMA DIVISÃO AO PRODUTO", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
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
                    SistranBLL.Cliente.Divisao o = new Cliente.Divisao();
                    o.ApagarEstoqueDivisaoByCodigoProdutoAndIDClienteDivisao(Request.QueryString["Codigo"], ListBox1.Items[i].Value);
                    ListBox1.Items.RemoveAt(i);

                    List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                    SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "REMOVEU A DIVISÃO DE UM PRODUTO", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
                    throw new Exception("Item excluído com sucesso.");
                }
            }

            ListBox1.Items.Add(new ListItem(hdCodigoDivisaoCliente.Value, hdCodigoDivisaoCliente.Value));
        }
        catch (Exception ex)
        {
            if (ex.Message != "Item excluído com sucesso.")
                  ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Não é possível excluir esta divisão. Exitem movimentações para esta Divisão. Descrição do Erro:" + ex.Message.Replace("'", "´") + "')", true);            
            else
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);            
        }
    }
}