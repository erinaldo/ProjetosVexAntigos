using System;
using Sistran.Library;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Sistran.Logins
{
    public class Acesso
    {
        public string ConexaoPorNomeBase(string Nome)
        {
            Settings1 x = new Settings1();
            string strConnection = "";
            string strConnectionRet = "";
            for (int i = 0; i < x.Properties.Count; i++)
            {
                //pega a conexao
                strConnection = x.Properties["Conn" + (i + 1).ToString()].DefaultValue.ToString();
                strConnectionRet = x.Properties["Conn" + (i + 1).ToString()].DefaultValue.ToString();
                if (strConnection.ToLower().Contains(("Initial Catalog=" + Nome.ToLower() + ";").ToLower()) == true)
                {
                    HttpContext.Current.Session["ConnLogin"] = strConnection;
                    HttpContext.Current.Session["ConexaoCliete"] = "Conn" + (i + 1).ToString();
                    strConnection = strConnectionRet;
                    return strConnection;
                }
            }
            strConnection = strConnectionRet;
            return strConnection;
        }

        public string ConexaoPorUsuario(string usuario, string senha)
        {

            Settings1 x = new Settings1();
            bool achou = false;
            string strConnection = "";

            for (int i = 0; i < x.Properties.Count; i++)
            {
                if (achou == true)
                    return strConnection;

                //pega a conexao
                strConnection = x.Properties["Conn" + (i + 1).ToString()].DefaultValue.ToString();

                //verifica se a conexao esta ativa 
                //Caso ocorra erro vai para a proxima select
                try
                {
                    SqlConnection cnn = new SqlConnection(strConnection);
                    cnn.Open();
                    //Seleciona o usuario e verifica se ele tem acesso ao .net
                    string strsql = "Select Login from Usuario where UPPER(Login)='" + usuario.ToUpper() + "' and  UPPER(Senha) ='" + senha.ToUpper() + "' AND SITE='ASP'";

                    SqlCommand cmm = new SqlCommand(strsql, cnn);
                    cmm.CommandType = CommandType.Text;

                    SqlDataReader dr = cmm.ExecuteReader();

                    while (dr.Read())
                    {
                        achou = true;
                        HttpContext.Current.Session["ConexaoCliete"] = "Conn" + (i + 1).ToString();
                    }
                    dr.Close();
                    cnn.Close();
                    dr.Dispose();
                    cnn.Dispose();
                }
                catch (Exception)
                {

                }
            }

            if (achou == true)
            {
                return strConnection;
            }
            else
            {
                return "";
            }
        }

        public string ConexaoPorUsuarioPromocional(string usuario, string senha)
        {

            Settings1 x = new Settings1();
            bool achou = false;
            string strConnection = "";

            for (int i = 0; i < x.Properties.Count; i++)
            {
                if (achou == true)
                    return strConnection;

                //pega a conexao
                strConnection = x.Properties["Conn" + (i + 1).ToString()].DefaultValue.ToString();

                //verifica se a conexao esta ativa 
                //Caso ocorra erro vai para a proxima select
                try
                {
                    SqlConnection cnn = new SqlConnection(strConnection);
                    cnn.Open();
                    //Seleciona o usuario e verifica se ele tem acesso ao .net
                    string strsql = "Select Login from Usuario where UPPER(Login)='" + usuario.ToUpper() + "' and  UPPER(Senha) ='" + senha.ToUpper() + "' AND SITE='PHP'";

                    SqlCommand cmm = new SqlCommand(strsql, cnn);
                    cmm.CommandType = CommandType.Text;

                    SqlDataReader dr = cmm.ExecuteReader();

                    while (dr.Read())
                    {
                        achou = true;
                        HttpContext.Current.Session["ConexaoCliete"] = "Conn" + (i + 1).ToString();
                    }
                    dr.Close();
                    cnn.Close();
                    dr.Dispose();
                    cnn.Dispose();
                }
                catch (Exception)
                {
                }
            }

            if (achou == true)
            {
                return strConnection;
            }
            else
            {
                return "";
            }
        }

        public class Menu
        {
            public DataTable RetornarAcessos(int idusuario)
            {
                string ssql = " SELECT  ModMen.IDModuloMenu ";
                ssql += " FROM ModuloMenu ModMen ";
                ssql += " LEFT JOIN ModuloOpcao ModOpc ";
                ssql += " ON(ModOpc.IDModuloOpcao = ModMen.IDModuloOpcao) ";
                ssql += " WHERE ModMen.IDModuloOpcao in ( ";
                ssql += " SELECT ";
                ssql += " UsuOpc.IDModuloOpcao ";
                ssql += " FROM UsuarioOpcao UsuOpc ";
                ssql += " WHERE UsuOpc.IDUsuario = " + idusuario;
                ssql += " AND UsuOpc.Permissao = 'SIM' ";
                ssql += " ) ";
                ssql += " OR ModMen.IDModuloOpcao in ( ";
                ssql += " SELECT ";
                ssql += " UsuOpc.IDModuloOpcao ";
                ssql += " FROM Usuario Usu ";
                ssql += " LEFT JOIN UsuarioOpcao UsuOpc ";
                ssql += " on(UsuOpc.IDUsuario = Usu.IDPerfil) ";
                ssql += " WHERE Usu.IDUsuario = " + idusuario;
                ssql += " AND UsuOpc.Permissao = 'SIM') ";
                ssql += " order by 1 ";

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(ssql);

                string s = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    s += dt.Rows[i][0].ToString() + ",";
                }

                s = s.Substring(0, s.Length - 1);

                ssql = " SELECT ";
                ssql += " coalesce(ModOpc.Descricao, Mod.Nome) as Menu, ";
                ssql += " coalesce(ModMen.IDParente,0) IDParente, ";
                ssql += " ModOpc.Programa, ";
                ssql += " ModOpc.Pacote, ";
                ssql += " coalesce(ModOpc.Tipo, coalesce(Mod.tipo, 'WIN')) Tipo, ";
                ssql += " ModMen.IDModuloMenu, ";
                ssql += " ModOpc.IDModuloOpcao, ";
                ssql += " Mod.Ordem ";
                ssql += " FROM ModuloMenu ModMen ";
                ssql += " LEFT JOIN ModuloOpcao ModOpc ";
                ssql += " ON(ModOpc.IDModuloOpcao = ModMen.IDModuloOpcao) ";
                ssql += " LEFT JOIN Modulo Mod ";
                ssql += " ON(Mod.IDModulo = ModMen.IDModulo)		 ";
                ssql += " WHERE ModMen.IDModuloMenu in ("+s+")  ";
                ssql += " AND (coalesce(ModMen.Ativo,'NAO') = 'SIM') and (coalesce(ModOpc.tipo, coalesce(Mod.tipo, 'WIN')) = 'WEB')			 ";
                ssql += " AND (ModOpc.Link IS NULL OR RTRIM(LTRIM(LINK)) ='') ";
                ssql += " ORDER BY ModOpc.Descricao, Mod.Nome, ModMen.idparente , ModMen.ordem ASC ";

                return GetDataTables.RetornarDataTable(ssql);
            }
        }

    }
}