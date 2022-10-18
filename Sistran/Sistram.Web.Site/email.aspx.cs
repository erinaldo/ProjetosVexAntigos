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
using System.Net.Mail;
using System.Net.Mime;
// using System.Net.Mail;



public partial class email : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string deEmail = "moises@sistecno.com.br";
        string deNome = "Moises Rovani de Andrade";
        string paraEmail = "moises@mrandrade.com";
        string Assunto = "Teste";
        string Mensagem = "<b>Conteúdo do e-mail " + DateTime.Now.ToString() + "</b>";

        MailMessage Email = new MailMessage();
        SmtpClient SMTP = new SmtpClient();       

        SMTP.Credentials = new System.Net.NetworkCredential("moises@sistecno.com.br", "mo2404");
        SMTP.Host = "mail.sistecno.com.br";
        
        Email.From = new MailAddress(deEmail, deNome);
        Email.To.Clear();
        Email.To.Add(paraEmail);
        Email.Subject = Assunto;
        Email.IsBodyHtml = true;
        Email.Body = Mensagem;
        SMTP.Send(Email);
    }
}
