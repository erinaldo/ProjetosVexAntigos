using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.DAL
{
    public class Menu
    {
        public DataTable RetornarMenusPlanoEmissorCTE(string cnx)
        {
            string ss = "Select distinct  mo.IdModuloOpcao,    mo.link programa ,  isnull(mo.DescricaoWeb, mo.Descricao) Descricao,    mm.parametro  From PlanoModuloOpcao PMO ";
            ss += " Inner Join ModuloOpcao MO on MO.IdModuloOpcao = PMO.IdModuloOpcao ";
            ss += " Inner Join ModuloMenu MM on MM.IdModuloOpcao = MO.IdModuloOpcao ";
            ss += " where PMO.IdPlano = 1  ";
            ss += " Order By Parametro  ";

            return DAL.BD.cDb.RetornarDataTable(ss, cnx);
        }

        public DataTable RetornarMenusPlanoPedidos(string cnx)
        {
            string ss = "Select distinct  mo.IdModuloOpcao,    mo.link programa ,  isnull(mo.DescricaoWeb, mo.Descricao) Descricao,    mm.parametro  From PlanoModuloOpcao PMO ";
            ss += " Inner Join ModuloOpcao MO on MO.IdModuloOpcao = PMO.IdModuloOpcao ";
            ss += " Inner Join ModuloMenu MM on MM.IdModuloOpcao = MO.IdModuloOpcao ";
            ss += " where PMO.IdPlano = 5  ";
            ss += " Order By Parametro  ";

            return DAL.BD.cDb.RetornarDataTable(ss, cnx);
        }


        public DataTable RetornarMenuBase(string cnx, int plano)
        {
            string ssql = "SELECT ";
            ssql += " MO.IDMODULOOPCAO,    MM.IDMODULOMENU,  ISNULL(MM.IDPARENTE, 0) IDPARENTE,   ";
            ssql += " MO.PROGRAMA,   isnull(mo.DescricaoWeb, mo.Descricao) Descricao,    MM.PARAMETRO, LOWER( MO.LINK) LINK,  MM.ORDEM, ISNULL(IDPLANO, 0) IDPLANO ";
            ssql += " FROM  MODULOOPCAO MO   ";
            ssql += " INNER JOIN MODULOMENU MM ON MM.IDMODULOOPCAO = MO.IDMODULOOPCAO   ";
            ssql += " LEFT JOIN PLANOMODULOOPCAO PMO ON PMO.IDMODULOOPCAO = MO.IDMODULOOPCAO";
            ssql += " WHERE MM.ATIVO='SIM' AND MO.ATIVO='SIM'  ";
            ssql += " AND (IDPLANO IS NULL   OR IDPLANO=" + plano + ")";

            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);
        }

        public DataTable RetornaPermissoes(string cnx, int idUsuario)
        {
            return DAL.BD.cDb.RetornarDataTable("SELECT IDMODULOOPCAO FROM USUARIOOPCAO WHERE IDUSUARIO="+idUsuario+"  AND PERMISSAO='SIM'", cnx);            
        }

        public DataTable RetornarMenuBaseColetor(string cnx)
        {
            string sql = "SELECT MO.IDMODULOOPCAO, MO.DESCRICAO, MO.PROGRAMA, MO.IDOPERACAOCOLETOR, OC.NOME OPERACAO FROM MODULOOPCAO MO LEFT JOIN OPERACAOCOLETOR OC ON OC.IDOPERACAOCOLETOR = MO.IDOPERACAOCOLETOR WHERE MO.TIPO='CLW'";
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }


        public DataTable RetornarMenuDoUsuario(int idUsuario, string visao ,string cnx)
        {
            string ssql = "SELECT ";
            ssql += " MS.IDMODULOOPCAO, isnull(IDPARENTE,0) IDPARENTE, DESCRICAO, ISNULL(LINK, '') LINK, ICONE ";
            ssql += " FROM MENUSITE MS ";
            ssql += " INNER JOIN MODULOOPCAO MO ON MO.IDMODULOOPCAO = MS.IDMODULOOPCAO ";
            ssql += " LEFT JOIN USUARIOOPCAO UO ON UO.IDMODULOOPCAO = MS.IDMODULOOPCAO ";
            //ssql += " AND MS.VISAO ='"+visao+"' ";
            ssql += " AND UO.IDUSUARIOOPCAO = "+idUsuario;
            ssql += " AND UO.PERMISSAO='SIM'"; 
            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);

        }
    }
}
