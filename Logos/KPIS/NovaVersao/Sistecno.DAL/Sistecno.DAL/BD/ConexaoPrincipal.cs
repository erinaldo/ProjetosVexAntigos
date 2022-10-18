using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Threading;
using System.Data.SqlClient;
///using Microsoft.SqlServer.Management.Smo;
///using Microsoft.SqlServer.Management.Common;
using Sistecno.DAL.BD;

namespace Sistecno.DAL.BD
{
    public class ConexaoPrincipal
    {
        public string CxPrincipal;
      
        /// <summary>
        /// Retorna a conexao inicial ou do cliente
        /// </summary>
        /// <param name="conexao">Vazio = Conexao Inical</param>
        /// Cnn().Conn == conexao da sistecno
        public ConexaoPrincipal(string conexao)
        {            
            if(conexao=="")
                CxPrincipal = new Cnn().Conn;
            else
                CxPrincipal = conexao;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ConexaoLogos()
        {           
                return new Cnn().ConnLogos;
        }

        public string ConexaoApp()
        {
            return new Cnn().ConnApp;
        }

        public string RetornarCxCliente(int idCliente, string LinkExterno)
        {

            if (LinkExterno.Length > 0)
            {
                return LinkExterno;
            }

            DataTable dt = cDb.RetornarDataTable("Select * from Cliente where IdCliente =" + idCliente, this.CxPrincipal);
            StringBuilder con = new StringBuilder();
            con.Append("Data Source=@IP,@PORTA;Initial Catalog=@DataBase;User ID=@Usuario;Password=@senha");

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BLOQUEADO"].ToString() == "SIM")
                    throw new Exception("Empresa Bloqueada");

                con.Replace("@IP", dt.Rows[0]["IP"].ToString());
                con.Replace("@DataBase", dt.Rows[0]["BASEDEDADOS"].ToString());
                con.Replace("@Usuario", dt.Rows[0]["USUARIOBD"].ToString());
                con.Replace("@senha", dt.Rows[0]["SENHABD"].ToString());
                con.Replace("@PORTA", dt.Rows[0]["PORTA"].ToString());

            }
            return con.ToString();
        }

        /// <summary>
        /// Retorna a string de conexao conforme a empresa
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns>Texto</returns>
        public string RetornarCxCliente(string email, string senha, ref string idCliente)
        {

            string sql = "Select Cd.RazaoSocialNome Cliente, Cx.Ip,Cx.BaseDeDados,Cx.UsuarioBd, Cx.SenhaBD, Cx.Porta, cl.Bloqueado, cl.idcliente ";
            sql += " From Usuario U ";
            sql += "    Inner Join Conexao Cx on (Cx.IdConexao = U.IdConexao)";
            sql += "    Inner Join Cliente Cl on (Cl.IdCliente = Cx.IdCliente)";
            sql += "    Inner Join Cadastro Cd on Cd.IdCadastro = CL.IdCliente";
            sql += " where U.Login ='" + email + "' and U.Senha ='" + senha + "'";


            DataTable dt = cDb.RetornarDataTable(sql, this.CxPrincipal);
            StringBuilder con = new StringBuilder();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BLOQUEADO"].ToString() == "SIM")
                    throw new Exception("Empresa Bloqueada");

                con.Append("Data Source=@IP,@porta;Initial Catalog=@DataBase;User ID=@Usuario;Password=@senha");

                con.Replace("@IP", dt.Rows[0]["IP"].ToString());
                con.Replace("@DataBase", dt.Rows[0]["BASEDEDADOS"].ToString());
                con.Replace("@Usuario", dt.Rows[0]["USUARIOBD"].ToString());
                con.Replace("@senha", dt.Rows[0]["SENHABD"].ToString());
                con.Replace("@porta", (dt.Rows[0]["PORTA"].ToString() == "" ? "1433" : dt.Rows[0]["PORTA"].ToString()));


                idCliente = dt.Rows[0]["idcliente"].ToString();
            }


            return con.ToString();
        }


        public string RetornarCxClienteEmail(string idUsuario , ref string idCliente)
        {

            string sql = "Select Cd.RazaoSocialNome Cliente, Cx.Ip,Cx.BaseDeDados,Cx.UsuarioBd, Cx.SenhaBD, Cx.Porta, cl.Bloqueado, cl.idcliente ";
            sql += " From Usuario U ";
            sql += " Inner Join Conexao Cx on (Cx.IdConexao = U.IdConexao)";
            sql += " Inner Join Cliente Cl on (Cl.IdCliente = Cx.IdCliente)";
            sql += " Inner Join Cadastro Cd on Cd.IdCadastro = CL.IdCliente";
            sql += " where U.IdUsuario ='" + idUsuario.ToString() + "' ";


            DataTable dt = cDb.RetornarDataTable(sql, this.CxPrincipal);
            StringBuilder con = new StringBuilder();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["BLOQUEADO"].ToString() == "SIM")
                    throw new Exception("Empresa Bloqueada");

                con.Append("Data Source=@IP,@porta;Initial Catalog=@DataBase;User ID=@Usuario;Password=@senha");

                con.Replace("@IP", dt.Rows[0]["IP"].ToString());
                con.Replace("@DataBase", dt.Rows[0]["BASEDEDADOS"].ToString());
                con.Replace("@Usuario", dt.Rows[0]["USUARIOBD"].ToString());
                con.Replace("@senha", dt.Rows[0]["SENHABD"].ToString());
                con.Replace("@porta", (dt.Rows[0]["PORTA"].ToString() == "" ? "1433" : dt.Rows[0]["PORTA"].ToString()));


                idCliente = dt.Rows[0]["idcliente"].ToString();
            }


            return con.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NomeBD"></param>
        /// <param name="cnx">Conexao Destino</param>
        /// <returns></returns>
        public bool ExisteBase(string NomeBD, string cnx)
        {
            DataTable dt = cDb.RetornarDataTable("SELECT * FROM sys.databases WHERE name = '" + NomeBD + "'", cnx.Replace(NomeBD, "master"));
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Cria o Database
        /// </summary>
        /// <param name="NomeBD"></param>
        /// <param name="caminho"></param>
        public void CriarDatabase(string NomeBD, int idEmpresa, string nomeArqXml, string CaminhoENomeSriptGeral)
        {
            string cnxDestino = RetornarCxCliente(idEmpresa,"");
            if (ExisteBase(NomeBD, cnxDestino))
                throw new Exception("DataBase já existe.");
            else
            {
                //ExecutarScriptCriacaoBanco(NomeBD, cnxDestino,  nomeArqXml, CaminhoENomeSriptGeral);
            }
        }

        /// <summary>
        /// Executa o script de criação de db
        /// </summary>
        /// <param name="NomeBD"></param>
        /// <param name="caminho"></param>
        //public void ExecutarScriptCriacaoBanco(string NomeBD, string cnxDestino, string nomeArqXml, string CaminhoENomeSriptGeral)
        //{         

        //    DataSet conf = new DataSet();
        //    conf.ReadXml(nomeArqXml);

        //    for (int i = 0; i < conf.Tables["tbDataBaseOrdemTable"].Rows.Count; i++)
        //    {

        //        if (i == 0)
        //        {
        //           DAL.BD.cDb.CriarDatabase("CREATE DATABASE " + NomeBD.ToUpper(), cnxDestino, NomeBD);
        //            ExcutarCriacaoObjetosSql(cnxDestino, CaminhoENomeSriptGeral);
        //        }
        //        this.ExecuatarProcCriacaoRegistorTabela(
        //                                    conf.Tables["tbCaminhoScript"].Rows[0]["CAMINHO"].ToString(),
        //                                    conf.Tables["tbDataBaseOrdemTable"].Rows[i]["NOMETABLE"].ToString(), cnxDestino
        //                                    );

        //    }         
           
        //}

        //private void ExcutarCriacaoObjetosSql(string cnx, string CaminhoENomeSriptGeral)
        //{
        //    try
        //    {

        //        SqlConnection con = new SqlConnection();
        //        FileInfo file = new FileInfo(CaminhoENomeSriptGeral);
        //        string script = file.OpenText().ReadToEnd();
                
        //        SqlConnection conn = new SqlConnection(cnx);
        //        Server server = new Server(new ServerConnection(conn));

        //        try
        //        {
        //            server.ConnectionContext.ExecuteNonQuery(script);
        //        }
        //        catch (Exception ex)
        //        {
        //            //frwSistecno.OperacoesEmail.EnviarInfoErros(ex);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        

        public void ExecuatarProcCriacaoRegistorTabela(string Caminho, string NomeDaTabela, string cnx)
        {
            try
            {
                string idEmpresa = "";
                List<ParametrosProcedures> list = new List<ParametrosProcedures>();
                ParametrosProcedures p = new ParametrosProcedures();
                
                p.nomePar = "ARQUIVO";
                p.valorPar = Caminho.Replace("\\", @"\")+ NomeDaTabela+ ".txt";
                p.tipoDeDados = "string";
                idEmpresa = p.valorPar;
                list.Add(p);

                p = new ParametrosProcedures();
                p.nomePar = "TABELA";
                p.valorPar = NomeDaTabela;
                p.tipoDeDados = "string";
                list.Add(p);
                Sistecno.DAL.BD.cDb.ExecutarProcedure("SP_CARGAINICIAL", list, cnx);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
