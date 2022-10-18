using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Web;

namespace SistranDAO
{
    public sealed class Menu
    {
        public static List<SistranMODEL.Menu> RetornaMenuParent(int user, string Conn)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT  coalesce(ModOpc.Descricao, Mod.Nome) as Menu, ");
            strsql.Append(" coalesce(ModMen.IDParente,0) IDParente,  ");
            strsql.Append(" ModOpc.Programa,  ");
            strsql.Append(" ModOpc.Pacote,  ");
            strsql.Append(" coalesce(ModOpc.Tipo, coalesce(Mod.tipo, 'WIN')) Tipo,  ");
            strsql.Append(" ModMen.IDModuloMenu,  ");
            strsql.Append(" ModOpc.IDModuloOpcao,  ");
            strsql.Append(" Mod.Ordem, ");
            strsql.Append(" ModMen.Ordem ");
            strsql.Append(" FROM ModuloMenu ModMen ");
            strsql.Append(" LEFT JOIN ModuloOpcao ModOpc ");
            strsql.Append(" ON(ModOpc.IDModuloOpcao = ModMen.IDModuloOpcao) ");
            strsql.Append(" LEFT JOIN Modulo Mod ");
            strsql.Append(" ON(Mod.IDModulo = ModMen.IDModulo) ");
            strsql.Append(" WHERE (coalesce(ModMen.Ativo,'NAO') = 'SIM') ");
            strsql.Append(" and (coalesce(ModOpc.tipo, coalesce(Mod.tipo, 'WIN')) = 'WEB') ");
            strsql.Append(" and coalesce(ModMen.IDParente,0) = 0 ");
            strsql.Append(" ORDER BY IDParente ASC,  ModMen.Ordem asc ");

            List<SistranMODEL.Menu> ILMenu = new List<SistranMODEL.Menu>();

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            IDbConnection cn = factory.CreateConnection();
            IDbCommand cd = factory.CreateCommand();


            if (HttpContext.Current.Session["Conn"] == null || HttpContext.Current.Session["Conn"].ToString() == "")
            {
                cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            }
            else
            {
                Database SistranDb = DatabaseFactory.CreateDatabase(Conn);
                cn.ConnectionString = SistranDb.ConnectionString;
            }

            cd.Connection = cn;
            cd.CommandText = strsql.ToString();


            cn.Open();
            IDataReader drProfile = cd.ExecuteReader();

            ILMenu.Add(new SistranMODEL.Menu(0, "Home", "Inicial.aspx"));

            while (drProfile.Read())
            {
                ILMenu.Add(new SistranMODEL.Menu(Convert.ToInt32(drProfile["IDModuloMenu"]),
                    drProfile["Menu"].ToString(),
                    drProfile["Menu"].ToString()));
            }
            drProfile.Close();
            cn.Close();
            return ILMenu;
        }



        public static DataTable MontarMenu(int IDUsuario)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT    coalesce(ModOpc.Descricao, Mod.Nome) as Menu,  ");
            strsql.Append(" coalesce(ModMen.IDParente,0) IDParente,   ");
            strsql.Append(" ModOpc.Programa,     ");
            strsql.Append(" ModOpc.Pacote,     ");
            strsql.Append(" coalesce(ModOpc.Tipo, coalesce(Mod.tipo, 'WIN')) Tipo,     ");
            strsql.Append(" ModMen.IDModuloMenu,     ");
            strsql.Append(" ModOpc.IDModuloOpcao,      ");
            strsql.Append(" Mod.Ordem,     ");
            strsql.Append(" ModMen.Ordem,   ");
            strsql.Append(" Link     ");
            strsql.Append(" FROM ModuloMenu ModMen    LEFT JOIN ModuloOpcao ModOpc  ON(ModOpc.IDModuloOpcao = ModMen.IDModuloOpcao)     ");
            strsql.Append(" LEFT JOIN Modulo Mod    ON(Mod.IDModulo = ModMen.IDModulo)     ");
            strsql.Append(" WHERE (coalesce(ModMen.Ativo,'NAO') = 'SIM')     ");
            strsql.Append(" AND (coalesce(ModOpc.tipo, coalesce(Mod.tipo, 'WIN')) = 'WEB')     ");
            strsql.Append(" AND ModMen.IDModuloMenu in     ( SELECT IDParente from ModuloMenu ModMen2  INNER JOIN UsuarioOpcao UsuOpc    ON(UsuOpc.IDModuloOpcao = ModMen2.IDModuloOpcao)     ");
            strsql.Append(" LEFT JOIN ModuloOpcao ModOpc    ON(ModOpc.IDModuloOpcao = ModMen2.IDModuloOpcao)     ");
            strsql.Append(" WHERE UsuOpc.IDUsuario =" + IDUsuario.ToString());
            strsql.Append(" AND UsuOpc.Permissao = 'SIM' )   ");
            strsql.Append(" UNION ALL     ");
            strsql.Append(" SELECT     coalesce(ModOpc.Descricao, Mod.Nome) as Menu,    coalesce(ModMen.IDParente,0) IDParente,    ModOpc.Programa,    ModOpc.Pacote,     ");
            strsql.Append(" coalesce(ModOpc.Tipo, coalesce(Mod.tipo, 'WIN')) Tipo,    ModMen.IDModuloMenu,    ModOpc.IDModuloOpcao,    Mod.Ordem,    ModMen.Ordem,  Link     ");
            strsql.Append(" FROM ModuloMenu ModMen    LEFT JOIN ModuloOpcao ModOpc  ON(ModOpc.IDModuloOpcao = ModMen.IDModuloOpcao)     ");
            strsql.Append(" LEFT JOIN Modulo Mod    ON(Mod.IDModulo = ModMen.IDModulo)     ");
            strsql.Append(" INNER JOIN UsuarioOpcao UsuOpc    ON(UsuOpc.IDModuloOpcao = ModMen.IDModuloOpcao)     ");
            strsql.Append(" WHERE (coalesce(ModMen.Ativo,'NAO') = 'SIM')     ");
            strsql.Append(" AND (coalesce(ModOpc.tipo, coalesce(Mod.tipo, 'WIN')) = 'WEB')     ");
            strsql.Append(" AND  UsuOpc.IDUsuario =" + IDUsuario.ToString());
            strsql.Append(" AND UsuOpc.Permissao = 'SIM'       ");
            strsql.Append(" and ModOpc.Link is not null ");
            strsql.Append(" ORDER BY MENU --BY IDParente ASC,  ModMen.Ordem asc    ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), HttpContext.Current.Session["Conn"].ToString());
        }

        public static DataTable MenuOpcoes()
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT IDMODULOOPCAO, DESCRICAO FROM MODULOOPCAO WHERE TIPO ='WEB' ");
            strsql.Append(" AND ATIVO ='SIM'");
            strsql.Append(" AND LINK IS NOT NULL AND LINK <> ''");
            strsql.Append(" ORDER BY DESCRICAO ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public static DataTable MenuOpcoesGravados(string idUsuario)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT IDMODULOOPCAO, DESCRICAO FROM MODULOOPCAO WHERE TIPO ='WEB' ");
            strsql.Append(" AND ATIVO ='SIM'");
            strsql.Append(" AND LINK IS NOT NULL");
            strsql.Append(" AND IDMODULOOPCAO IN ");
            strsql.Append(" (");
            strsql.Append(" SELECT IDMODULOOPCAO FROM USUARIOOPCAO WHERE PERMISSAO ='SIM' AND IDUSUARIO =" + idUsuario);
            strsql.Append(" )");
            strsql.Append(" ORDER BY DESCRICAO ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public static void DesabilitarAcessoByIdUsuario(string idUsuario)
        {
            string str = "UPDATE USUARIOOPCAO SET PERMISSAO='NAO' WHERE IDUSUARIO = " + idUsuario;
            str += "  AND IDMODULOOPCAO IN ( ";
            str += "  SELECT IDMODULOOPCAO FROM MODULOOPCAO  ";
            str += "  WHERE MODULOOPCAO.LINK <> '' ";
            str += "  AND MODULOOPCAO.LINK IS NOT NULL ";
            str += "  AND TIPO = 'WEB' ";
            str += "  ) ";
            Sistran.Library.GetDataTables.ExecutarSemRetorno(str, "");
        }

        public static void InserirHabilitacoes(string idUsuario, string IDModuloOpcao, string Habilitado)
        {
            if (Habilitado == "SIM")
            {
                string strsql = "SELECT IDUsuarioOpcao FROM USUARIOOPCAO WHERE IDUSUARIO=" + idUsuario + " AND IDMODULOOPCAO =" + IDModuloOpcao;
                int IDUsuarioOpcao = Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");

                if (IDUsuarioOpcao > 0)
                {
                    strsql = "UPDATE USUARIOOPCAO SET PERMISSAO='SIM' WHERE IDUsuarioOpcao=" + IDUsuarioOpcao;
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
                }
                else
                {
                    string ids = Sistran.Library.GetDataTables.RetornarIdTabela("USUARIOOPCAO");

                    strsql = "INSERT INTO USUARIOOPCAO (IDUsuarioOpcao, IDUsuario, IDModuloOpcao,IDFilial,Permissao) VALUES (";
                    strsql += ids + " , " + idUsuario + ", " + IDModuloOpcao + ",1,'SIM'";
                    strsql += ")";
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");

                }
            }

        }

    }

    public class Modulo
    {
        /// <summary>
        /// Retorna datatable com os modulos web
        /// </summary>
        /// <returns></returns>
        public DataTable RetornarModulosWeb()
        {
            string ssql = "SELECT * FROM MODULO M inner JOIN MODULOMENU MU ON MU.IDMODULO=M.IDMODULO WHERE TIPO ='WEB' order by nome";
            return Sistran.Library.GetDataTables.RetornarDataTable(ssql);
        }
        /// <summary>
        /// Retorna datatable com todos os menus ativos
        /// </summary>
        /// <returns></returns>
        public DataTable RetornarTodosModulosMenus()
        {
            string ssql = "SELECT * FROM MODULOMENU MM LEFT JOIN MODULOOPCAO MP ON MP.IDMODULOOPCAO=MM.IDMODULOOPCAO WHERE MM.ATIVO <>'NAO' AND (TIPO IS NULL OR TIPO='WEB') ORDER BY DESCRICAO";
            return Sistran.Library.GetDataTables.RetornarDataTable(ssql);
        }

        public DataTable RetornarPermissoes(int idUsuario)
        {
            string ssql = "SELECT MODMEN.IDMODULOMENU, MODMEN.IDPARENTE, MODOPC.DESCRICAO, MODMEN.IDMODULO ";
            ssql += " FROM MODULOMENU MODMEN ";
            ssql += " LEFT JOIN MODULOOPCAO MODOPC ON(MODOPC.IDMODULOOPCAO = MODMEN.IDMODULOOPCAO) ";
            ssql += " WHERE MODMEN.IDMODULOOPCAO IN ( ";
            ssql += " SELECT  USUOPC.IDMODULOOPCAO ";
            ssql += " FROM USUARIOOPCAO USUOPC ";
            ssql += " WHERE USUOPC.IDUSUARIO = "+idUsuario+"	AND USUOPC.PERMISSAO = 'SIM' ";
            ssql += " ) ";
            ssql += " OR  ";
            ssql += " MODMEN.IDMODULOOPCAO IN ( ";
            ssql += " SELECT USUOPC.IDMODULOOPCAO ";
            ssql += " FROM USUARIO USU ";
            ssql += " LEFT JOIN USUARIOOPCAO USUOPC ON(USUOPC.IDUSUARIO = USU.IDPERFIL) ";
            ssql += " WHERE USU.IDUSUARIO = "+idUsuario+"	AND USUOPC.PERMISSAO = 'SIM' ";
            ssql += " ) ";          

            return Sistran.Library.GetDataTables.RetornarDataTable(ssql); 
        }
    }
}