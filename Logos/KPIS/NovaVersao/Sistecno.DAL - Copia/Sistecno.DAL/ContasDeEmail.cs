using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistecno.DAL.Models;


namespace Sistecno.DAL
{
    public class ContaDeEmail
    {
        public DAL.Models.ContaDeEmail Retornar(string operacao, string cnx)
        {
            SistecnoContext context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var query = context.ContaDeEmails.FirstOrDefault(p => p.Operacao== operacao);
            context.Database.Connection.Close();
            return query;
        }


        public void Gravar(DAL.Models.ContaDeEmail objemail, string cnx)
        {
            SistecnoContext context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            objemail.IdContaDeEmail = DAL.BD.cDb.RetornarIDTabela(cnx, "ContaDeEmail");
            context.ContaDeEmails.Add(objemail);
            context.SaveChanges();
            context.Database.Connection.Close();            
        }

        public void Alterar(DAL.Models.ContaDeEmail objemail, string cnx)
        {
            SistecnoContext context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();

            var o = context.ContaDeEmails.First(i => i.IdContaDeEmail == objemail.IdContaDeEmail);

            o.Operacao = objemail.Operacao;
            o.Para = objemail.Para;
            o.Porta = objemail.Porta;
            o.Senha = objemail.Senha;
            o.SMTP = objemail.SMTP;
            
            context.SaveChanges();
            context.Database.Connection.Close();
        }



    }
}
