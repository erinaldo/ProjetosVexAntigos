using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Sistecno.UI.Web.SpeechAppWs
{
    /// <summary>
    /// Summary description for App
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class App : System.Web.Services.WebService
    {

        [WebMethod]
        public string Logar(string usuario, string senha)
        {
            try
            {

                Sistecno.DAL.BD.ConexaoPrincipal cx = new  Sistecno.DAL.BD.ConexaoPrincipal("");

                string idCliente = "";
                string cnx = cx.RetornarCxCliente(usuario, senha, ref idCliente);


                if (cnx == "")
                    throw new Exception("ERR^Empresa não encontrada");


                Sistecno.DAL.Models.Usuario usu = new Sistecno.DAL.Models.Usuario();
                usu.Login = usuario;
                usu.Senha = senha;

                Sistecno.DAL.Models.Usuario ret = new Sistecno.BLL.Usuario().Logar(usu, cnx);


                if (ret == null)
                {
                    if (ret == null)
                        throw new Exception("ERR^Usuário ou senha inválido");
                }


                return cnx + "|" + usuario + "|"+senha;

            }
            catch (Exception EX)
            {               
                    return EX.Message;
            }
        }



        //[WebMethod]
        //public string ZerarTaefas()
        //{
        //    try
        //    {
        //        frwSistecno.DataBase.DB.Executar("Exec PRC_ZERAR_TAREFAS null", new frwSistecno.ConexaoPrincipal("").ConexaoApp());
        //        return "OPERACAO EFETUADA COM SUCESSO";
        //    }
        //    catch (Exception ex)
        //    {
        //        return "ERR^" + ex.Message;
        //    }
        //}



        //[WebMethod]
        //public DataTable  Logar(string login, string Senha)
        //{
        //    try
        //    {
        //        DataTable dt = frwSistecno.DataBase.DB.RetornarDataTable("Exec PRC_LOGAR '"+login+"', '"+Senha+"'", new frwSistecno.ConexaoPrincipal("").ConexaoApp());

        //        if (dt.Rows.Count == 0)
        //            throw new Exception("USUARIO NAO ENCONTRADO.");

        //        return dt;

        //    }
        //    catch (Exception ex)
        //    {
        //        DataTable err = new DataTable("erro");
        //        err.Columns.Add("err");

        //        DataRow er = err.NewRow();
        //        er[0] ="ERR^" + ex.Message; 
        //        err.Rows.Add(er);
        //        return err;
        //    }
        //}

        //[WebMethod]
        //public DataTable ReceberTarefas(int IdUsuario)
        //{
        //    try
        //    {
        //        DataTable dt = frwSistecno.DataBase.DB.RetornarDataTable("Exec  PRC_GET_TAREFAS "+ IdUsuario, new frwSistecno.ConexaoPrincipal("").ConexaoApp());

        //        if (dt.Rows.Count == 0)
        //            throw new Exception("NAO EXITE TAREFAS PARA ESTE USUARIO.");

        //        return dt;

        //    }
        //    catch (Exception ex)
        //    {

        //        DataTable err = new DataTable("erro");
        //        err.Columns.Add("err");

        //        DataRow er = err.NewRow();
        //        er[0] ="ERR^" + ex.Message; 
        //        err.Rows.Add(er);
        //        return err;
        //    }
        //}
    }
}