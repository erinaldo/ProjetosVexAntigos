using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SistranBLL;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.IO;

namespace Sistram.Web.Captacao
{
    public partial class frmConcluir : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            enviarEmail(false);
        }


        private void enviarEmail(bool debugs)
        {
            //create the mail message
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            //set the addresses
            mail.From = new System.Net.Mail.MailAddress("sistema@grupologos.com.br");
            mail.Bcc.Add("moises@sistecno.com.br");

            if (!debugs)
                mail.To.Add("jjunior@grupologos.com.br");
            else
                mail.To.Add("moises@sistecno.com.com.br");


            //set the content
            mail.Subject = "AVISO DE NOVO CADASTRO CAPTAÇÃO";

            string bodyy = "Prezado, <br> Acesse o intranet e entre na tela de cadastro de motorista/proprietario e veja os dados desta nova ocorrência.";
            bodyy+= " Lembrando que este cadastro esta bloqueado para a validação. <br> CPF: " +Session["cpf"]+ " Nome: " + Session["nome"] ;  
             bodyy+= "<br> Placa: "+ Session["Placa"];
             bodyy += "<br> Proprietario: " + Session["Proprietario"];
            bodyy+= "     <br> Atenciosamente, <br> GrupoLogos Intranet"; 
            mail.Priority = System.Net.Mail.MailPriority.High;

            mail.Body = bodyy;
            mail.IsBodyHtml = true;


            //send the message
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.grupologos.com.br");
            smtp.EnableSsl = false;

            System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential("sistema@grupologos.com.br", "logos0902");
            smtp.Credentials = credenciais;
            smtp.Send(mail);
        }
    }
}