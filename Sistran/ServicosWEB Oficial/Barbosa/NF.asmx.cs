using Sistecno;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de OL005
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class NF : System.Web.Services.WebService
    {
        [WebMethod]
        [SoapLoggerExtensionAttribute(Filename = "C:\\temp\\ol014\\nf.log")]
        public Retorno Cadastrar(Autentica Credenciais, List<Nf> nfs)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
          
            //if (Credenciais.login != "wsbarbosa_prod")
            //    cnx = "Data Source=192.168.10.10;Initial Catalog=vex;User ID=sa;Password=WERasd27;  ";
            

            try
            {
                if(Credenciais == null ||
                    Credenciais.login== null || Credenciais.login =="")
                    throw new Exception("Nao Autorizado");


                if (!Autentica.Autenticar(Credenciais.login, Credenciais.senha))
                    throw new Exception("Nao Autorizado");

                string s = "";
                for (int i = 0; i < nfs.Count; i++)
                {
                    s = "Insert into BarbosaNF(Chave, Xml,Pdf, Processado, DataHoraRecbimento, Origem, Destino) ";
                    s += "values ('"+nfs[i].Chave + "', '" + nfs[i].XML + "','" + nfs[i].PDF + "', Null, getdate(), '"+ nfs[i].Origem + "', '" + nfs[i].Destino + "'); ";

                }

                if (s.Length < 30)
                    throw new Exception("Dados Inválidos");

                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(s, cnx);

                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }
        }


        public class Nf
        {
            public string Chave { get; set; }
            public string XML { get; set; }
            public string PDF { get; set; }            
            public string Origem { get; set; }            
            public string Destino { get; set; }
        }     
    }
}