using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Util;

namespace Sistecno.UI.Web.Chamados
{
    public partial class WEB00200a : System.Web.UI.Page
    {

        Sistecno.DAL.Models.Usuario usuarioLogado = null;
        string cnx = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["USUARIOLOGADO"] == null)
                    Label1.Text = "Usuario Null";

                //somente conexao com o banco da sistecno
                cnx = new Sistecno.DAL.BD.ConexaoPrincipal("").CxPrincipal;

                usuarioLogado = (Sistecno.DAL.Models.Usuario)Session["USUARIOLOGADO"];


                Label1.Text = "Inicio PostBack";
                Label1.Visible = false;
                if (!IsPostBack)
                {
                    lblTitulo.Text = Request.QueryString["opc"].Replace("|", " ");

                    Combo.CarregarCombo(new Sistecno.BLL.Ticket.Divisao().Retornar(cnx), ref cboDivisao, false, true, "IDTICKETDIVISAO", "DIVISAO");

                    // SE FOR SISTECNO LOGADO
                    if (Session["IDCLIENTE"].ToString() == "1")
                    {
                        DataTable dtc = new Sistecno.DAL.Cliente().RetornarTodosClientes(cnx);
                        Combo.CarregarCombo(dtc, ref cboCliente, true, true, "IDCLIENTE", "RAZAOSOCIALNOME");
                        cboUsuarioResponsavel.Items.Add("SELECIONE O CLIENTE");
                        Combo.CarregarCombo(new Sistecno.BLL.Usuario().RetornarUsuarioCliente(1, cnx), ref CboUsuarioAtribuir, false, true, "IDUSUARIO", "NOME");
                        CboUsuarioAtribuir.CssClass = "form-control";
                   
                    }
                    else
                    {
                        cboCliente.Items.Insert(0, new ListItem("SUPORTE", "1"));
                        cboCliente.Items.Insert(1, new ListItem("CHAMADO INTERNO", Session["IDCLIENTE"].ToString()));

                        cboCliente.AutoPostBack = true;
                        cboUsuarioResponsavel.SelectedIndex = 0;
                        
                        cboUsuarioResponsavel.Items.Insert(0, new ListItem("SISTECNO", "1"));
                        cboUsuarioResponsavel.Enabled = false;

                       // cboClientePor.Visible = false;
                        dvAtribuir.Visible = false;
                        dvUsuario.Attributes.Add("class", "col-sm-7");
                        lblStatus.Visible = false;
                    }

                    cboDivisao.CssClass = "form-control";
                    cboCliente.Focus();

                    if (Request.QueryString["id"] != null)
                    {
                        CarregarDadosDoChamado();
                        if (txtUserAtrib.Value == "S")
                        {
                            cboUsuarioResponsavel.Focus();
                        }
                    }
                }

                cboCliente.CssClass = "form-control";
                cboUsuarioResponsavel.CssClass = "form-control";
                txtTexto.config.toolbar = new object[]{new object[] { "Bold", "Italic", "-", "NumberedList", "BulletedList", "-", "Link", "Unlink", "-", "About" },};
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message + "-" + ex.InnerException + "-" + ex.StackTrace);
            }
        }

        private void CarregarDadosDoChamado()
        {
            DataTable dt = new Sistecno.BLL.Ticket().RetornarChamadoCompleto(Convert.ToInt32(Session["IDCLIENTE"]), int.Parse(Request.QueryString["id"]), usuarioLogado.IDUsuario, false,cnx);

            if (dt.Rows[0]["STATUS"].ToString() != "FINALIZADO")
            {

                //SE O usuario for o atribuido ele pode pegar a tarefa
                if (usuarioLogado.IDUsuario.ToString() == dt.Rows[0]["IDUSUARIOATRIBUIDO"].ToString() && dt.Rows[0]["IDUSUARIOATRIBUIDO"].ToString() != "")
                {
                    btnIniciarFecharTarefa.Visible = true;

                    if (dt.Rows[0]["InicioDaTarefa"].ToString() == "")
                        btnIniciarFecharTarefa.Text = "Iniciar Tarefa";
                    else
                        btnIniciarFecharTarefa.Text = "Finalizar Tarefa";

                    if (dt.Rows[0]["InicioDaTarefa"].ToString() != "" && dt.Rows[0]["FinalDaTarefa"].ToString() != "" && usuarioLogado.IDUsuario.ToString() == dt.Rows[0]["IDUSUARIO"].ToString())
                        btnIniciarFecharTarefa.Text = "Fechar Chamado";

                }
                else
                    //SE O USUARIO FOR O MESMO QUE ABRIU O CHAMADO ELE PODE FECHAR
                    if (usuarioLogado.IDUsuario.ToString() == dt.Rows[0]["IDUSUARIO"].ToString())
                    {
                        btnIniciarFecharTarefa.Visible = true;
                        btnIniciarFecharTarefa.Text = "Fechar Chamado";
                    }
            }
            else
            {
                btnConfirmar.Enabled = false;
                btnIniciarFecharTarefa.Text = "CHAMADO FINALIZADO";
                btnIniciarFecharTarefa.Enabled = false;
                txtTexto.Enabled = false;
                txtTexto.Height = new Unit(30);
                txtTexto.CssClass = "form-control";
            }

            txtCodigo.Text = dt.Rows[0]["idTicket"].ToString();
            txtAssunto.Text = dt.Rows[0]["Assunto"].ToString();
            txtAssunto.Enabled = false;
            txtAssunto.CssClass = "form-control";

            cboDivisao.SelectedValue = dt.Rows[0]["IdTicketDivisao"].ToString();
            cboDivisao.Enabled = false;

            Combo.CarregarCombo(new Sistecno.DAL.Cliente().RetornarTodosClientes(cnx), ref cboCliente, true, true, "IDCLIENTE", "RAZAOSOCIALNOME");

            cboCliente.SelectedValue = dt.Rows[0]["IdClienteAtribuido"].ToString();
            cboCliente.Enabled = false;

            Combo.CarregarCombo(new Sistecno.BLL.Usuario().RetornarUsuarioCliente(int.Parse(cboCliente.SelectedValue), cnx), ref cboUsuarioResponsavel, false, true, "IDUSUARIO", "NOME");

            //if (dt.Rows[0]["IdUsuarioSolicitante"].ToString() == "") 
            //{
            //    cboUsuarioResponsavel.Enabled = true;
            //    txtUserAtrib.Value = "S";
            //}
            //else
            //{
                Combo.CarregarCombo(new Sistecno.BLL.Usuario().RetornarUsuarioCliente(int.Parse(cboCliente.SelectedValue), cnx), ref cboUsuarioResponsavel, false, true, "IDUSUARIO", "NOME");
                cboUsuarioResponsavel.SelectedValue = dt.Rows[0]["IdUsuarioSolicitante"].ToString();
                cboUsuarioResponsavel.Enabled = false;

                CboUsuarioAtribuir.SelectedValue = dt.Rows[0]["IdUsuarioAtribuido"].ToString();
                CboUsuarioAtribuir.Enabled = false;
            //}
            
            lblStatus.Text = dt.Rows[0]["STATUS"].ToString();

            txtTexto.Text = "";
            txtTexto.Focus();
                      

            string htm = "";           
            htm += " <table class='nav-justified' style='width:100%'>";

            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "IDTICKETMOVIMENTO", "DATA", "USUARIO", "DESCRICAO", "PARAQUEM", "SOLICITANTE");

            for (int i = 0; i < distinctValues.Rows.Count; i++)
            {              

                DataRow[] l = dt.Select("IDTICKETMOVIMENTO=" + distinctValues.Rows[i]["IDTICKETMOVIMENTO"].ToString(), "IDTICKETMOVIMENTO DESC");
                
                string NomeArquivo = "";
                for (int ii = 0; ii < l.Length; ii++)
                {
                    if (l[ii]["NomeArquivo"].ToString() != "")
                    {
                        NomeArquivo += "<p style='font-size=8px'> <a href='baixaranexo.aspx?id=" + l[ii]["IDTICKETMOVIMENTOANEXO"].ToString() + "' target='_blank'>" + l[ii]["NomeArquivo"].ToString().ToLower() + "</a></p>";
                    }
                }

                #region Novo
                               
                string bot = "<a class='btn btn-success btn-rounded' href='#'>??</a>";
                if(distinctValues.Rows[i]["SOLICITANTE"].ToString() == distinctValues.Rows[i]["PARAQUEM"].ToString())
                    bot = "<a class='btn btn-success btn-rounded' href='#'>" + distinctValues.Rows[i]["Usuario"].ToString().Substring(0,1) + "</a>";               
                else
                    bot = "<a class='btn btn-info btn-rounded' href='#'>" + distinctValues.Rows[i]["Usuario"].ToString().Substring(0, 1) + "</a>";              

                htm += " <tr style='vertical-align:top'>";
                htm += " <td style='width:1%'>" + bot + "</td>";
                htm += " <td style='text-align:rigth;vertical-align:middle;'><strong>" + distinctValues.Rows[i]["Usuario"].ToString().ToUpper() + "</strong></td>";
                htm += " <td style='text-align:right;'><strong>" + distinctValues.Rows[i]["DATA"].ToString() + "</strong></td>";
                htm += " </tr>";
                htm += " <tr>";
                htm += " <td>";
                htm += " </td>";
                htm += " <td colspan='2'>" + "<span style='border: none;width:100%;'>" + distinctValues.Rows[i]["DESCRICAO"].ToString().Replace("&NBSP;", "") + "</span></td>";
                htm += " <td></td>";
                htm += " </tr>";
                htm += " <tr>";
                htm += " <td>";
                htm += " </td>"; 
                htm += " <td>" + NomeArquivo + "</td>";
                htm += " <td></td>";
                htm += " </tr>";
                htm += " <tr>";
                htm += " <td colspan='3'><hr /></td>";
                htm += " </tr>";
                #endregion
            }            
            htm += " </table>";

            phHistorico.Controls.Add(new LiteralControl(htm));            
        }


        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (cboDivisao.SelectedIndex == 0)
            {
                cboDivisao.Focus();
                return;
            }

            if (cboCliente.SelectedIndex == 0 && cboCliente.SelectedItem.Text != "SUPORTE")
            {
                cboCliente.Focus();
                return;
            }

            if (cboUsuarioResponsavel.SelectedItem.Text == "SELECIONE")
            {
                cboUsuarioResponsavel.Focus();
                return;
            }

            //if (cboClientePor.SelectedIndex == 0 && Session["IDCLIENTE"].ToString()=="1" && cboCliente.SelectedValue=="1")
            //{
            //    cboClientePor.Focus();
            //    return;
            //}

            //if (CboUsuarioAtribuir.SelectedIndex == 0 && Session["IDCLIENTE"].ToString() == "1" && cboCliente.SelectedValue == "1")
            //{
            //    CboUsuarioAtribuir.Focus();
            //    return;
            //}

            string idCriacao = "";
            try
            {
                HttpFileCollection uploadedFiles = Request.Files;
                Span1.Text = string.Empty;
                List<byte[]> imgs = new List<byte[]>(uploadedFiles.Count);
                List<string> nomes = new List<string>(uploadedFiles.Count);

                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile userPostedFile = uploadedFiles[i];

                    if (userPostedFile.ContentLength > 0)
                    {
                        int intTamanho = System.Convert.ToInt32(userPostedFile.ContentLength);
                        byte[] imageBytes = new byte[intTamanho];
                        userPostedFile.InputStream.Read(imageBytes, 0, intTamanho);
                        imgs.Add(imageBytes);
                        nomes.Add(userPostedFile.FileName);
                        Span1.Text += "File Name: " + userPostedFile.FileName + "<br>";

                    }
                }

                if (Request.QueryString["id"] == null)
                {
                    int? UserAtribui = null;

                    if (Session["IDCLIENTE"].ToString() != "1" && cboCliente.SelectedValue == "1") // SE FOR OUTRO CLIENTE ABRINDO PRA SISTECNO NAO ATRIBUI AO USUARIO
                        UserAtribui = null;
                    else
                        UserAtribui = int.Parse(cboUsuarioResponsavel.SelectedValue);

                    int? userPor = null;
                    userPor = (CboUsuarioAtribuir.SelectedIndex <= 0 ? (int?)null : int.Parse(CboUsuarioAtribuir.SelectedValue));

                    //criar este usuario 
                    Sistecno.DAL.Ticket.UserTicket usuSolicitante = new Sistecno.DAL.Ticket.UserTicket();
                    if (cboUsuarioResponsavel.SelectedValue == "-9999")
                    {
                        usuSolicitante.IdUsuario = int.Parse("-9999");
                        usuSolicitante.Nome = CboUsuarioAtribuir.SelectedItem.Text.ToUpper();
                        usuSolicitante.Login = hdDadosUsuarios.Value.Split('|')[1];
                        usuSolicitante.Senha = hdDadosUsuarios.Value.Split('|')[2];
                        usuSolicitante.IdConexao = new Sistecno.BLL.Usuario().RetornarIdConexao(int.Parse(cboCliente.SelectedValue), cnx);
                    }
                    else
                    {
                        if (cboCliente.SelectedValue.ToUpper() != "SELECIONE")
                            usuSolicitante.IdUsuario = int.Parse(cboUsuarioResponsavel.SelectedValue);
                    }


                    List<string> userAcompnha = new List<string>();
                    if (txtAcompanha.Value.Length > 0)
                    {
                        int c = txtAcompanha.Value.Split('|').Length;
                        for (int i = 0; i <c ; i++)
                        {
                            if (txtAcompanha.Value.Split('|')[i] != "")
                                userAcompnha.Add(txtAcompanha.Value.Split('|')[i]);
                        }

                    }
                    idCriacao = new Sistecno.BLL.Ticket().AbrirChamado(
                                                                                int.Parse(cboCliente.SelectedValue), 
                                                                                (CboUsuarioAtribuir.SelectedIndex<=0?(int?)null: int.Parse(CboUsuarioAtribuir.SelectedValue)), 
                                                                                usuarioLogado.IDUsuario,                         
                                                                                int.Parse(cboUsuarioResponsavel.SelectedValue),
                                                                                usuSolicitante,
                                                                                txtAssunto.Text.ToUpper().Trim().Replace("'", ""), 
                                                                                "ABERTO", txtTexto.Text.ToUpper().Trim().Replace("'", ""), 
                                                                                int.Parse(cboDivisao.SelectedValue), imgs, 
                                                                                nomes, 
                                                                                userAcompnha,
                                                                                cnx);
                }
                else
                {
                    new Sistecno.BLL.Ticket().AlterarChamado(
                        int.Parse(txtCodigo.Text), int.Parse(Session["IDCLIENTE"].ToString()), 
                        usuarioLogado.IDUsuario, txtAssunto.Text.ToUpper().Trim().Replace("'", ""), 
                        "ABERTO", 
                        txtTexto.Text.ToUpper().Trim().Replace("'", ""), 
                        int.Parse(cboDivisao.SelectedValue), imgs, nomes, (txtUserAtrib.Value == "S" ? true : false), (txtUserAtrib.Value == "S" ? int.Parse(cboUsuarioResponsavel.SelectedValue) : (int?)null), cnx);
                    idCriacao = txtCodigo.Text;
                }
                //Response.Redirect("web00200.aspx?titulo=SUPORTE", false);
                EnviarEmailDoChamado(idCriacao);
                ClientScript.RegisterClientScriptBlock(GetType(), "redirecionaPg", "<script> window.open('../Default.aspx?acao=chamados&plat=chamado', '_top') </script>");
            }
            catch (Exception ex)
            {
                Label1.Visible = true;
                Label1.Text = ex.Message;
            }
        }

        private void EnviarEmailDoChamado(string idCriacao)
        {            
            string line= "", htm="";
            System.IO.StreamReader file = new System.IO.StreamReader(MapPath("Html/StatusChamdo.html"));

            string carta = "";
            while ((line = file.ReadLine()) != null)
            {
                carta += line;                
            }
            file.Close();


            DataTable dt = new Sistecno.BLL.Ticket().RetornarChamadoCompleto(Convert.ToInt32(Session["IDCLIENTE"]), int.Parse(idCriacao), usuarioLogado.IDUsuario, false, cnx);

            DataView view = new DataView(dt);
            DataTable distinctValues = view.ToTable(true, "IDTICKET", "ASSUNTO","IDTICKETMOVIMENTO", "DATA", "USUARIO", "DESCRICAO", "PARAQUEM", "SOLICITANTE", "STATUS", "NOMESOLICITANTE");

            htm += " <table border='0' style='font-size: 9pt;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000066; width:100%'>";
            for (int i = 0; i < distinctValues.Rows.Count; i++)
            {
                
                DataRow[] l = dt.Select("IDTICKETMOVIMENTO=" + distinctValues.Rows[i]["IDTICKETMOVIMENTO"].ToString(), "IDTICKETMOVIMENTO DESC");

                htm += " <tr >";          
                htm += " <td><b>" + distinctValues.Rows[i]["Usuario"].ToString().ToUpper() + "</b></td>";
                htm += " <td style='text-align:right'> <b>" + distinctValues.Rows[i]["DATA"].ToString() + "</b></td>";
                htm += " </tr>";
                htm += " <tr>";
                htm += " <td colspan='2'>" + distinctValues.Rows[i]["DESCRICAO"].ToString() + "</td>";                
                htm += " </tr>";                
                htm += " <tr>";
                htm += " <td colspan='2'><hr /></td>";
                htm += " </tr>";
          
            }
            htm += " </table>";

            carta = carta.Replace("@@Interacoes@@", htm);
            carta = carta.Replace("@@STATUS@@", distinctValues.Rows[0]["STATUS"].ToString());
            carta = carta.Replace("@@TICKET@@", "Nº. " + distinctValues.Rows[0]["IDTICKET"].ToString());
            carta = carta.Replace("&NBSP;","");
            carta = carta.Replace("@@USUARIO@@", distinctValues.Rows[0]["NOMESOLICITANTE"].ToString());
            carta = carta.Replace("@@ASSUNTO@@", distinctValues.Rows[0]["ASSUNTO"].ToString());
            carta = carta.Replace("@@id@@", distinctValues.Rows[0]["IDTICKET"].ToString());
            carta = carta.Replace("@@usuario@@", usuarioLogado.IDUsuario.ToString());


            string sql = "select u.Login from ticket t inner join ticketUsuario tu on tu.idticket = t.idticket inner join Usuario u on u.idusuario = tu.idusuario where u.login like '%@%' and t.IdTicket =  "+idCriacao;
            DataTable dtemail = Sistecno.DAL.BD.cDb.RetornarDataTable(sql, cnx);

            string em = "";

            if(usuarioLogado.Login.Contains("@"))
                em= usuarioLogado.Login + ";";

            for (int i = 0; i < dtemail.Rows.Count; i++)
            {
                em+= dtemail.Rows[i][0].ToString() + ";";
            }

           
          EnviarEmail(em, "suporte@sistecno.com.br", "Acompanhamnto do Ticket #" + distinctValues.Rows[0]["IDTICKET"].ToString(), carta, "Sistema de Gestao de Tickects"); 
        }


        public  void EnviarEmail(string para, string de, string Assunto, string Mensagem, string NomeEmailFrom)
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                message.From = new MailAddress(de, NomeEmailFrom);

                message.SubjectEncoding = System.Text.Encoding.GetEncoding(1252);
                message.BodyEncoding = System.Text.Encoding.GetEncoding(1252);
                string[] destinatarios = para.Split(';');

                foreach (string dest in destinatarios)
                {
                    if (dest.Trim() != "")
                        message.To.Add(dest);
                }

                message.Bcc.Add("moises@sistecno.com.br");
                message.Subject = Assunto;
                string MsgTipo = MediaTypeNames.Text.Html;
                AlternateView alternate = AlternateView.CreateAlternateViewFromString(Mensagem, System.Text.Encoding.GetEncoding(1252), MsgTipo);
                message.AlternateViews.Add(alternate);
                client.Credentials = new System.Net.NetworkCredential(de, "@oncetsis14");
                client.Host = "mail.sistecno.com.br";             
                client.Send(message);
            }
            catch (Exception )
            {
             }
            finally
            {
                message.Dispose();
                message = null;
                client = null;
            }
        }


        protected void btnIniciarFecharTarefa_Click(object sender, EventArgs e)
        {
            switch (btnIniciarFecharTarefa.Text)
            {
                case "Fechar Chamado":
                    fecharChamado();
                    CarregarDadosDoChamado();
                    break;

                case "Iniciar Tarefa":
                    iniciarTarefa();
                    CarregarDadosDoChamado();
                    break;

                case "Finalizar Tarefa":
                    finalizarTarefa();
                    CarregarDadosDoChamado();
                    break;
            }
            EnviarEmailDoChamado(txtCodigo.Text);
        }

        private void finalizarTarefa()
        {
            try
            {
                HttpFileCollection uploadedFiles = Request.Files;
                Span1.Text = string.Empty;
                List<byte[]> imgs = new List<byte[]>(uploadedFiles.Count);
                List<string> nomes = new List<string>(uploadedFiles.Count);

                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile userPostedFile = uploadedFiles[i];

                    if (userPostedFile.ContentLength > 0)
                    {
                        int intTamanho = System.Convert.ToInt32(userPostedFile.ContentLength);
                        byte[] imageBytes = new byte[intTamanho];
                        userPostedFile.InputStream.Read(imageBytes, 0, intTamanho);
                        imgs.Add(imageBytes);
                        nomes.Add(userPostedFile.FileName);
                        Span1.Text += "File Name: " + userPostedFile.FileName + "<br>";

                    }
                }
                txtTexto.ForcePasteAsPlainText = true;
                

                new Sistecno.BLL.Ticket().FinalizarTarefa(int.Parse(txtCodigo.Text), usuarioLogado.IDUsuario, imgs, nomes, txtTexto.Text.Replace("'",""), cnx);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void iniciarTarefa()
        {
            try
            {
                HttpFileCollection uploadedFiles = Request.Files;
                Span1.Text = string.Empty;
                List<byte[]> imgs = new List<byte[]>(uploadedFiles.Count);
                List<string> nomes = new List<string>(uploadedFiles.Count);

                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile userPostedFile = uploadedFiles[i];

                    if (userPostedFile.ContentLength > 0)
                    {
                        int intTamanho = System.Convert.ToInt32(userPostedFile.ContentLength);
                        byte[] imageBytes = new byte[intTamanho];
                        userPostedFile.InputStream.Read(imageBytes, 0, intTamanho);
                        imgs.Add(imageBytes);
                        nomes.Add(userPostedFile.FileName);
                        Span1.Text += "File Name: " + userPostedFile.FileName + "<br>";

                    }
                }
                new Sistecno.BLL.Ticket().IniciarTarefa(int.Parse(txtCodigo.Text), usuarioLogado.IDUsuario, imgs, nomes, txtTexto.Text.Replace("'",""), cnx);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void fecharChamado()
        {
            try
            {
                HttpFileCollection uploadedFiles = Request.Files;
                Span1.Text = string.Empty;
                List<byte[]> imgs = new List<byte[]>(uploadedFiles.Count);
                List<string> nomes = new List<string>(uploadedFiles.Count);

                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile userPostedFile = uploadedFiles[i];

                    if (userPostedFile.ContentLength > 0)
                    {
                        int intTamanho = System.Convert.ToInt32(userPostedFile.ContentLength);
                        byte[] imageBytes = new byte[intTamanho];
                        userPostedFile.InputStream.Read(imageBytes, 0, intTamanho);
                        imgs.Add(imageBytes);
                        nomes.Add(userPostedFile.FileName);
                        Span1.Text += "File Name: " + userPostedFile.FileName + "<br>";

                    }
                }
                new Sistecno.BLL.Ticket().Finalizar(int.Parse(txtCodigo.Text), usuarioLogado.IDUsuario, imgs, nomes,txtTexto.Text.Replace("'",""), cnx);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboUsuarioResponsavel.Enabled = true;
            cboUsuarioResponsavel.Items.Clear();

            if (cboCliente.SelectedValue == "1")
            {
                if (Session["IDCLIENTE"].ToString() == "1")//SE FOR A PROPRIA SISTECNO
                {
                    //CARREGA OS USUARIOS DA SISTECNO
                    DataTable dt = new Sistecno.BLL.Usuario().RetornarUsuarioCliente(1, cnx);

                    Combo.CarregarCombo(dt, ref cboUsuarioResponsavel, false, true, "IDUSUARIO", "NOME");
                    
                    rbSelecionados.DataSource = dt;
                    rbSelecionados.DataTextField = "NOME";
                    rbSelecionados.DataValueField = "IDUSUARIO";
                    rbSelecionados.DataBind();



                    DataTable dtc = new Sistecno.BLL.Cliente().RetornarTodosClientes(cnx);

                    for (int i = 0; i < dtc.Rows.Count; i++)
                    {
                        if (dtc.Rows[i]["IDCLIENTE"].ToString() == Session["IDCLIENTE"].ToString())
                            dtc.Rows.RemoveAt(i);
                        continue;
                    }

                    //Combo.CarregarCombo(dtc, ref cboClientePor, true, true, "IDCLIENTE", "RAZAOSOCIALNOME");

                    //cboClientePor.Visible = true;
                    CboUsuarioAtribuir.Visible = true;
                    CboUsuarioAtribuir.Items.Add("SELECIONE O SOLICITANTE");
                   // linhaSolicitante.Visible = true;


                }
                else
                {
                    cboUsuarioResponsavel.Enabled = false;
                    cboUsuarioResponsavel.Items.Add("SISTECNO");
                }
            }
            else
            {
                DataTable dt = new Sistecno.DAL.Usuario().RetornarUsuarioCliente(int.Parse(cboCliente.SelectedValue), cnx);                              

                rbSelecionados.DataSource = dt;
                rbSelecionados.DataTextField = "NOME";
                rbSelecionados.DataValueField = "IDUSUARIO";
                rbSelecionados.DataBind();

                Combo.CarregarCombo(dt, ref cboUsuarioResponsavel, false, true, "IDUSUARIO", "NOME");
            }

            cboUsuarioResponsavel.CssClass = "form-control";
            //cboClientePor.CssClass = "form-control";

        }

        protected void cboClientePor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboClientePor.SelectedIndex > 0)
            //{
            //    Combo.CarregarCombo(new Sistecno.BLL.Usuario().RetornarUsuarioCliente(int.Parse(cboClientePor.SelectedValue), cnx), ref CboUsuarioAtribuir, false, true, "IDUSUARIO", "NOME");
            //    cboUsuarioResponsavel.CssClass = "form-control";

            //   // btnCadastrarUsuario.Visible = true;
            //}
            //else
            //{
            //    CboUsuarioAtribuir.Items.Clear();
            //    CboUsuarioAtribuir.Items.Add("SELECIONE O CLIENTE");
            //}

            //cboUsuarioResponsavel.CssClass = "form-control";
            //cboClientePor.CssClass = "form-control";

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ret = hdDadosUsuarios.Value;

            if (cboCliente.SelectedIndex > 0)
            {
                cboUsuarioResponsavel.Items.Insert(0, new ListItem(ret.Split('|')[0].ToString(), "-9999"));
                cboUsuarioResponsavel.SelectedIndex = 0;
                cboUsuarioResponsavel.CssClass = "form-control";
            }
        }

        protected void brnConfirmarAcompanhamento_Click(object sender, EventArgs e)
        {

        }

    }
}