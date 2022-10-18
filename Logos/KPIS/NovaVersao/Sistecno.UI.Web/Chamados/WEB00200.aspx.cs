using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using Util;

namespace Sistecno.UI.Web.Chamados
{
    public partial class WEB00200 : System.Web.UI.Page
    {
        DAL.Models.Usuario usuarioLogado = null;
        string cnx="";
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pt-BR");
            CultureInfo culture = new CultureInfo("pt-BR");

            if (Session["USUARIOLOGADO"] == null)
                Response.Redirect("../index.aspx", false);

            usuarioLogado = (Sistecno.DAL.Models.Usuario)Session["USUARIOLOGADO"];

            lblTitulo.Text = Request.QueryString["opc"].Replace("|", " ") + " (" + Request.Path.Substring(Request.Path.LastIndexOf("/") + 1).Replace(".ASPX", "") + ")";


            //somente conexao com o banco da sistecno
            cnx = new  Sistecno.DAL.BD.ConexaoPrincipal("").CxPrincipal;


            if (Session["IDCLIENTE"].ToString() == "1")
                btnAtribuirChamados.Visible = true;
            else
                btnAtribuirChamados.Visible = false;

            if (!IsPostBack)
            {
                carregarGrid("meus");
                btnMeusChamados.CssClass = "btn btn-w-m btn-info  btn-xs";

            }

        }

        private void carregarGrid(string tipo)
        {            

            DataTable dt = null;

            if(tipo=="meus")
                dt = new Sistecno.BLL.Ticket().RetornarChamadoCompleto(Convert.ToInt32(Session["IDCLIENTE"]), -1, usuarioLogado.IDUsuario,  false, cnx);

            if(tipo=="atribuidos")
                dt = new Sistecno.BLL.Ticket().RetornarChamadoCompleto(Convert.ToInt32(Session["IDCLIENTE"]), -1, usuarioLogado.IDUsuario, true, cnx);

            if(tipo=="atribuir")
                dt = new Sistecno.BLL.Ticket().RetornarChamadoCompletoNaoAtribuido((Convert.ToInt32(Session["IDCLIENTE"])), cnx);


            if (tipo == "pesquisa")
                dt = new Sistecno.BLL.Ticket().RetornarPesquisa(Convert.ToInt32(Session["IDCLIENTE"]), txtCodigo.Text, txtUsuario.Text, (txtPesauisaInicio.Text.Length > 0 ? DateTime.Parse(txtPesauisaInicio.Text) : (DateTime?)null), (txtPesauisaFim.Text.Length > 0 ? DateTime.Parse(txtPesauisaFim.Text) : (DateTime?)null), (cboDivisao.SelectedIndex>0?cboDivisao.SelectedItem.Text:""), usuarioLogado.IDUsuario.ToString() ,cnx );

           
            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "EMPRESA", "ASSUNTO", "ABERTURA", "PREVISAO", "INICIODATAREFA", "FINALDATAREFA", "CONCLUSAO", "NOMESOLICITANTE", "USUARIOATRIBUIDO", "STATUS", "IDTICKET");


            DataTable dtFinal = new DataTable();
            dtFinal.Columns.Add("ID", typeof(System.Int32)); //0
            dtFinal.Columns.Add("ABERTURA", typeof(System.DateTime));//1
            dtFinal.Columns.Add("CLI. SOLICITANTE", typeof(System.String));//2
            dtFinal.Columns.Add("CLI. USUÁRIO", typeof(System.String));//3
            dtFinal.Columns.Add("ASSUNTO", typeof(System.String));//4
            dtFinal.Columns.Add("INÍCIO TAREFA", typeof(System.DateTime));//5
            dtFinal.Columns.Add("FIM TAREFA", typeof(System.DateTime));//6
            dtFinal.Columns.Add("RESPONSÁVEL", typeof(System.String));//7
            dtFinal.Columns.Add("STATUS]", typeof(System.String));//8

            for (int i = 0; i < distinctValues.Rows.Count; i++)
            {
                if (distinctValues.Rows[i]["STATUS"].ToString() == "FINALIZADO" && tipo != "pesquisa")
                    continue;                

                DataRow dr = dtFinal.NewRow();
                dr[0] = int.Parse(distinctValues.Rows[i]["IDTICKET"].ToString());
                dr[1] = DateTime.Parse( distinctValues.Rows[i]["ABERTURA"].ToString() );
                dr[2] = (distinctValues.Rows[i]["EMPRESA"].ToString().Length>15? distinctValues.Rows[i]["EMPRESA"].ToString().Substring(0,14): distinctValues.Rows[i]["EMPRESA"].ToString());
                dr[3] = distinctValues.Rows[i]["NOMESOLICITANTE"].ToString();
                dr[4] = distinctValues.Rows[i]["ASSUNTO"].ToString();

                if (distinctValues.Rows[i]["FINALDATAREFA"].ToString() != "")
                    dr[5] = DateTime.Parse(distinctValues.Rows[i]["FINALDATAREFA"].ToString());

                if(distinctValues.Rows[i]["CONCLUSAO"].ToString()!="")
                    dr[6] = DateTime.Parse( distinctValues.Rows[i]["CONCLUSAO"].ToString());
                                
                dr[7] = distinctValues.Rows[i]["USUARIOATRIBUIDO"].ToString() ;
                dr[8] = distinctValues.Rows[i]["STATUS"].ToString();
                dtFinal.Rows.Add(dr);
            }
            ph.Controls.Clear();
            ph.Controls.Add(new LiteralControl(GradeDeDados.CriarGrid(dtFinal, "WEB00200a.ASPX", Request.QueryString["opc"])));
        }
                
        protected void btnHabPesquisa_Click(object sender, EventArgs e)
        {
            if (tblPesquisa.Visible == false)
            {
                tblPesquisa.Visible = true;
                btnHabPesquisa.CssClass = "btn btn-w-m btn-info  btn-xs";
                btnHabPesquisa.Text = "<< Pesquisa";
            }
            else
            {
                tblPesquisa.Visible = false;
                btnHabPesquisa.CssClass = "btn btn-primary btn-xs";
                btnHabPesquisa.Text = "Pesquisar >>";
                carregarGrid("meus");

            }

            btnMeusChamados.CssClass = "btn btn-primary btn-xs";
            btnAtribuirChamados.CssClass = "btn btn-primary btn-xs";
            btnSusChamados.CssClass = "btn btn-primary btn-xs";            
        }

        protected void btnMeusChamados_Click(object sender, EventArgs e)
        {
            tblPesquisa.Visible = false;            
            btnHabPesquisa.CssClass = "btn btn-primary btn-xs";
            btnAtribuirChamados.CssClass = "btn btn-primary btn-xs";
            btnSusChamados.CssClass = "btn btn-primary btn-xs";
            btnMeusChamados.CssClass = "btn btn-w-m btn-info  btn-xs";
            btnHabPesquisa.Text = "Pesquisar >>";

            carregarGrid("meus");
        }

        protected void btnSusChamados_Click(object sender, EventArgs e)
        {
            tblPesquisa.Visible = false;
            btnHabPesquisa.CssClass = "btn btn-primary btn-xs";
            btnAtribuirChamados.CssClass = "btn btn-primary btn-xs";
            btnSusChamados.CssClass = "btn btn-w-m btn-info  btn-xs";
            btnMeusChamados.CssClass = "btn btn-primary btn-xs";
            btnHabPesquisa.Text = "Pesquisar >>";

            carregarGrid("atribuidos");
        }

        protected void btnAtribuir_Click(object sender, EventArgs e)
        {
            tblPesquisa.Visible = false;
            btnHabPesquisa.CssClass = "btn btn-primary btn-xs";
            btnAtribuirChamados.CssClass = "btn btn-w-m btn-info  btn-xs";
            btnSusChamados.CssClass = "btn btn-primary btn-xs";
            btnMeusChamados.CssClass = "btn btn-primary btn-xs";
            btnHabPesquisa.Text = "Pesquisar >>";

            carregarGrid("atribuir");
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            carregarGrid("pesquisa");
        }

        
    }
}