using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.Data;
using System.Globalization;
using System.Threading;

public partial class frmDivisoesCliente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

           

            if (!IsPostBack)
            {
                DataTable cli = SistranBLL.Cliente.ReadCadastro(Convert.ToInt32(Session["IDEmpresa"]));

              
                if (cli.Rows.Count > 0)
                {
                    lblCnpj.Text = cli.Rows[0]["CNPJCPF"].ToString();
                    lblNome.Text = cli.Rows[0]["RAZAOSOCIALNOME"].ToString();
                }

                CarregarMenuDivisao();


                tblDetalhes.Visible = true;
                btnAdicionar.Visible = true;
                btnExcluir.Visible = false;
                lblCodDiviSel.Text = "";
                lblCodDiviSel.Visible = false;
                btnOK.Visible = false;
                lblNomeAdcItem.Text = " Nenhum item selecionado ";
                lblNomeAdcItem.Visible = true;
                

            }

            
            if(Request.QueryString["opc"] != null)
                  lblTitulo.Text = Server.UrlDecode(Request.QueryString["opc"].ToString().ToUpper());

            List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
            SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ACESSOU " + lblTitulo.Text.ToUpper(), System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }

    DataTable dtxx;
    string n = "";
    DataRow pp;
    private void CarregarMenuDivisao()
    {
        DataTable DivisoesCompleta = SistranDAO.Cliente.DivisoesCompleta(Session["IDEmpresa"].ToString()) ;        
        Session["DivisoesCompleta"] = DivisoesCompleta;

        dtxx = new DataTable();
        dtxx.Columns.Add("IDCLIENTEDIVISAO");
        dtxx.Columns.Add("NOME");

        DataRow[] xx = DivisoesCompleta.Select("IDPARENTE IS NULL ", "");

        if (xx.Length == 0)
        {
            return;
        }


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
            if(lblNome0 !=null)
            {
                tblDetalhes.Visible = true;
                btnAdicionar.Visible = true;
                btnExcluir.Visible = true;
                lblCodDiviSel.Text = e.CommandArgument.ToString();
                lblCodDiviSel.Visible = false;
                txtTextSelecionado.Text = lblNome0.Text.Replace("&nbsp;", "").Replace(";", "");
                btnOK.Visible = true;
                lblNomeAdcItem.Text = "Item Selecionado: " + lblNome0.Text.Trim().Replace("&nbsp;", "").Replace(";", "");
                lblNomeAdcItem.Visible = true;
                txtTextSelecionado.Visible = true;
                btnAdicionar.Text = "Adcionar Sub-Item";

            }
        }

        if (e.CommandName == "del")
        {
            LinkButton excluir = (LinkButton)e.Item.FindControl("excluir");

            if (excluir != null)
            {
                try
                {

                }
                catch (Exception )
                {
                    ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Não é possível excluir esta divião. Para excluí-la mova todos os seus itens para uma outra divisão.')", true);

                    return;
                }
            }


        }

    }
   
    protected void btnAdicionar_Click(object sender, EventArgs e)
    {
        lblNomeAdcItem.Text ="Item Selecionado: "+ txtTextSelecionado.Text;
        lblNomeAdcItem.Visible = true;
        
        txtTextSelecionado.Visible = true;
        txtTextSelecionado.Text = "";
        txtTextSelecionado.Focus();
        btnOK.Visible = true;
        btnAdicionar.Visible = false;
        btnExcluir.Visible = false;
        
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            SistranBLL.Cliente.Divisao clidiv = new Cliente.Divisao();
            clidiv.DeletarClienteDivisao(lblCodDiviSel.Text);

            CarregarMenuDivisao();
            //btnAdicionar.Visible = false;
            //btnExcluir.Visible = false;
            //txtTextSelecionado.Text = "";
            //lblCodDiviSel.Text = "";
            //lblNomeAdcItem.Text = "";
            //lblNomeAdcItem.Visible = false;
            //btnOK.Visible = false;
            //tblDetalhes.Visible = false;

            txtTextSelecionado.Visible = false;
            btnAdicionar.Visible = true;
            btnExcluir.Visible = false;
            btnAdicionar.Text = "Novo";
            btnOK.Visible = false;
            txtTextSelecionado.Text = "";
            lblCodDiviSel.Text = "";
            lblNomeAdcItem.Text = "Nenhum Item Selecionado ";

            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Operação efetuada com sucesso.')", true);
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Não é possível excluir esta divião. Para excluí-la mova todos os seus itens para uma outra divisão.')", true);
        }

    }   

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTextSelecionado.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Digite um Texto.')", true);
                txtTextSelecionado.Focus();
                return;
            }

            SistranBLL.Cliente.Divisao clidiv = new Cliente.Divisao();

            if (btnAdicionar.Visible == false || lblCodDiviSel.Text=="")
            {
                //novo item
                clidiv.InserirClienteDivisao(Session["IDEmpresa"].ToString(), txtTextSelecionado.Text, lblCodDiviSel.Text);
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "CRIOU UMA NOVA DIVISÃO", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            }
            else
            {
                clidiv.AlterarNomeClienteDivisao(lblCodDiviSel.Text, txtTextSelecionado.Text);

                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)Session["USUARIO"];
                SistranBLL.Usuario.LogBDBLL.GravarLog(ILusuario[0].UsuarioId, ILusuario[0].Login, "ALTEROU O NOME DE UMA DIVISAO", System.IO.Path.GetFileName(HttpContext.Current.Request.FilePath), Session["Conn"].ToString());
            }

            CarregarMenuDivisao();
            /*//btnAdicionar.Visible = false;
            txtTextSelecionado.Text = "";
            lblCodDiviSel.Text = "";
            lblNomeAdcItem.Text = "";
            //lblNomeAdcItem.Visible = false;
            btnOK.Visible = false;
            //tblDetalhes.Visible = false;*/

            txtTextSelecionado.Visible = false;
            btnAdicionar.Visible = true;
            btnExcluir.Visible = false;
            btnAdicionar.Text = "Novo";
            btnOK.Visible = false;
            txtTextSelecionado.Text = "";
            lblCodDiviSel.Text = "";
            lblNomeAdcItem.Text = "Nenhum Item Selecionado ";

            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('Operação efetuada com sucesso.')", true);

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterClientScriptBlock(Master.FindControl("Up"), this.GetType(), "Alert", "alert('" + ex.Message.Replace("'", "´") + "')", true);
        }

    }
}