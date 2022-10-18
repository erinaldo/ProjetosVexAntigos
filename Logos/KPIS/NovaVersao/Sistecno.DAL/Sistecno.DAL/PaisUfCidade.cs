using System.Data;
using System.Linq;
using Sistecno.DAL.Models;
namespace Sistecno.DAL
{
    public class PaisUfCidade
    {
        #region Estados

        public string[] RetornarCodigoIBGE_UF_Cidade(int idCidade, string  cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var o = context.Cidade.FirstOrDefault(i => i.IDCidade == idCidade);
            context.Database.Connection.Close();
            string[] ret = new string[2];
            Estado est = o.Estado;
            ret[0] = est.CodigoDoIbge.ToString();
            ret[1] = o.CodigoDoIBGE.ToString();
            return ret;
        }

        public DataTable RetornarUf(string cnx)
        {
            string sqlstr = "SELECT top 100 IDESTADO [CODIGO], * FROM ESTADO ORDER BY NOME";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }

        public int RetornarIdEstadoPorCidade(string cnx, int idCidade)
        {
            Sistecno_Entities context = new Sistecno_Entities() ;

            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();

            var o = context.Cidade.FirstOrDefault(i => i.IDCidade == idCidade);

            context.Database.Connection.Close();
            return o.IDEstado;
        }

        public string  RetornarUFPorIdCidade(string cnx, int idCidade)
        {
            Sistecno_Entities context = new Sistecno_Entities();

            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var o = context.Cidade.FirstOrDefault(i => i.IDCidade == idCidade);
            context.Database.Connection.Close();
            return o.Nome;
        }

        public int RetornarIdEstadoPorUF(string cnx, string Uf)
        {
            Sistecno_Entities context = new Sistecno_Entities();

            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();

            var o = context.Estado.FirstOrDefault(i => i.Uf == Uf);

            context.Database.Connection.Close();
            return o.IDEstado;
        }
       

        public DAL.Models.Cidade RetornarByCodigoIBGE(string cnx, string CodigoIBGE)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var o = context.Cidade.FirstOrDefault(i => i.CodigoDoIBGE == CodigoIBGE);
            context.Database.Connection.Close();
            return o;
        }



        //public void inicializar(string cnx)
        //{
        //    var context = new SistecnoContext();

        //    context.Database.Connection.ConnectionString = cnx;
        //    context.Database.Connection.Open();
        //    var occ = context.Estado.FirstOrDefault(ii => ii.IDEstado == 26);
        //    context.Database.Connection.Close();
        //}


        #endregion

        #region Cidades


        public DataTable RetornarCidade(string cnx, int iIdEstado, string sCidade)
        {

            string where = "  ";

            if (iIdEstado > 0 || sCidade.Length > 0)
            {

                switch (iIdEstado)
                {
                    case 0:
                        where += " WHERE NOME='" + sCidade + "'";
                        break;

                    default:
                        where += " WHERE IDESTADO=" + iIdEstado;
                        break;

                }
            }


            string sqlstr = "SELECT IDCIDADE , NOME FROM CIDADE " + where + " ORDER BY NOME";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);


        }

        public DataTable RetornarCidadePesquisa(string cnx, string sCidade, int? iCodigoDaCidade, string sEstado)
        {

            string sql = "SELECT TOP 50 C.IDCIDADE  ID, C.NOME CIDADE,E.NOME ESTADO, E.UF, P.SIGLA [SIGLA PAIS] ";
            sql += " FROM CIDADE C ";
            sql += " LEFT JOIN ESTADO E ON C.IDESTADO = E.IDESTADO ";
            sql += " LEFT JOIN PAIS P ON P.IDPAIS = E.IDPAIS ";
            sql += " WHERE 0=0 ";

            if (sCidade.Length > 0)
                sql += " AND C.NOME LIKE '" + sCidade + "%' ";

            if (sEstado.Length > 0)
                sql += " AND E.NOME LIKE '" + sEstado + "%' ";

            if (iCodigoDaCidade != null)
            {
                sql += " AND IDCIDADE=" + iCodigoDaCidade.ToString();
            }

            sql += " ORDER BY C.NOME, E.NOME";
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }


        public DAL.Models.Cidade RetornarCidadePorUfNome(string cnx, int idestado, string nome)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var o = context.Cidade.FirstOrDefault(i => i.IDEstado == idestado & i.Nome == nome);
            context.Database.Connection.Close();
            return o;
        }

        #endregion

        #region Pais

        public DataTable RetornarPais(string cnx)
        {

            string sqlstr = "SELECT *  FROM PAIS ORDER BY NOME";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }
        #endregion


        public DataTable RetornarBairro(string cnx, int iIdCidade, string sBairro)
        {
            string where = "  ";

            if (iIdCidade > 0 || sBairro.Length > 0)
            {

                switch (iIdCidade)
                {
                    case 0:
                        where += " WHERE NOME='" + sBairro + "'";
                        break;

                    default:
                        where += " WHERE IDCIDADE=" + iIdCidade;
                        break;

                }
            }

            string sqlstr = "SELECT *  FROM BAIRRO " + where + " AND NOME <> '' ORDER BY NOME";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }

        public int RetornarBairroPorCidadeNome(string cnx, int idCidade, string nome)
        {
            Sistecno_Entities context = new Sistecno_Entities() ;
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
            var o = context.Bairro.FirstOrDefault(i => i.IDCidade == idCidade & i.Nome == nome.Trim());

            if (o== null)
            {
                o = new DAL.Models.Bairro();
                o.IDBairro = DAL.BD.cDb.RetornarIDTabela(cnx, "Bairro");
                o.Nome = nome.ToUpper().Trim();
                o.IDCidade = idCidade;

                context.Bairro.Add(o);
                context.SaveChanges();
            }
            context.Database.Connection.Close();
            return o.IDBairro;
        }
    }
}
