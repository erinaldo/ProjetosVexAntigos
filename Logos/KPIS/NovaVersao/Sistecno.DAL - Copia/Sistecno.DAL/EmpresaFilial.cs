using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistecno.DAL.Models;

namespace Sistecno.DAL
{
    public class EmpresaFilial
    {
        public void VerificaGrupo(string cnx)
        {
            string sql = " SELECT * FROM grupo ";
            DataTable d = DAL.BD.cDb.RetornarDataTable(sql, cnx);

            if (d.Rows.Count == 0)
            {
                sql = "INSERT INTO GRUPO VALUES (1,'EXPRESS','SIM')";
                DAL.BD.cDb.ExecutarSemRetorno(sql, cnx);
            }
        }

        public DataTable RetornarEmpresa(int? idEmpresa, string cnx)
        {
            string sql = "";
            if (idEmpresa == null)
                sql = "SELECT IDEMPRESA ID, * FROM EMPRESA WHERE ATIVO='SIM'  ORDER BY 4";
            else
                sql = " SELECT * FROM EMPRESA EMP INNER  JOIN CADASTRO CAD ON CAD.IDCADASTRO = EMP.IDEMPRESA  WHERE EMP.IDEMPRESA=" + idEmpresa;
;

            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }

        public int InserirEmpresa(DAL.Models.Empresa obj, string cnx)
        {

            VerificaGrupo(cnx);

            var context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            try
            {
                context.Database.Connection.Open();
                obj.IDEmpresa = DAL.BD.cDb.RetornarIDTabela(cnx, "EMPRESA");
                context.Empresas.Add(obj);
                context.SaveChanges();
                return obj.IDEmpresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();
            }
        }


        public void AlterarEmpresa(DAL.Models.Empresa obj, string cnx)
        {
            var context = new SistecnoContext();
            context.Database.Connection.ConnectionString = cnx;
            try
            {
                context.Database.Connection.Open();

                var o = context.Empresas.First(i => i.IDEmpresa == obj.IDEmpresa);
                o.AliquotaSimples = obj.AliquotaSimples;
                o.Ativo = obj.Ativo;
                o.IDEmpresa = obj.IDEmpresa;
                o.IDGrupo = obj.IDGrupo;
                o.Nome = obj.Nome;
                o.OptanteSimples = obj.OptanteSimples;

                context.SaveChanges();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (context.Database.Connection.State == ConnectionState.Open)
                    context.Database.Connection.Close();
            }
        }

        public DataTable RetornarFilial(int idempresa, int? idFilial, string cnx)
        {
            string sql = "";

            if (idFilial == null)
                sql = "SELECT IDFILIAL ID, NOME FILIAL, * FROM FILIAL WHERE ATIVO='SIM' AND IDEMPRESA = " + idempresa + " ORDER BY NOME";
            else
                sql = " SELECT TOP 1 EST.NOME ESTADO, CID.NOME CIDADE, BAR.NOME BAIRRO, CCE.ENDERECO EMAIL,* FROM FILIAL FIL  INNER JOIN CADASTRO CAD ON CAD.IDCADASTRO = FIL.IDCADASTRO  LEFT JOIN CIDADE CID ON CID.IDCIDADE = CAD.IDCIDADE LEFT JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO LEFT JOIN BAIRRO BAR ON BAR.IDBAIRRO = CAD.IDBAIRRO LEFT JOIN CADASTROCONTATOENDERECO CCE ON CCE.IDCADASTRO = CAD.IDCADASTRO AND IDCADASTROTIPODECONTATO=1  WHERE FIL.IDFILIAL=" + idFilial;
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }


        public DataTable RetornarFilial(string cnx)
        {
            string sqlstr = "SELECT IDFILIAL ID , NOME FILIAL, * FROM FILIAL WHERE ATIVO='SIM' ORDER BY NOME";
            return DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);
        }


        public DataTable Retornar(string where, string cnx)
        {
            //string ssql = " SELECT E.IDFILIAL ID, CNPJCPF [CNPJ/CPF],RAZAOSOCIALNOME [RAZAO SOCIAL], E.NOME FILIAL, E.ATIVO ";
            //ssql += " FROM FILIAL E INNER JOIN CADASTRO C ON E.IDEMPRESA =C.IDCADASTRO  ";
            //ssql += " INNER JOIN EMPRESA EMP ON EMP.IDEMPRESA = E.IDEMPRESA ";
            //ssql += where;
            //ssql += " ORDER BY 2,3";



            string ssql = "SELECT E.IDFILIAL ID, CCF.CNPJCPF [CNPJ/CPF], C.RAZAOSOCIALNOME [RAZAO SOCIAL], E.NOME FILIAL, E.ATIVO  ";
            ssql += " FROM FILIAL E  ";
            ssql += " INNER JOIN CADASTRO C ON E.IDEMPRESA =C.IDCADASTRO    ";
            ssql += " INNER JOIN EMPRESA EMP ON EMP.IDEMPRESA = E.IDEMPRESA   ";
            ssql += " INNER JOIN CADASTRO CCF ON E.IDCADASTRO =CCF.IDCADASTRO    ";
            ssql += where;
            ssql += " ORDER BY 1 DESC ";
            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);
        }

        public DataTable RetonarFilialDaEmpresa(int idempresa, string cnx)
        {
            string ssql = " SELECT f.nome filial, f.idfilial from filial f where idempresa= " + idempresa;
            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);
        }

        public string RetonarCnpjEmpresaByIdFilial(int idFilial, string cnx)
        {
            string ssql = "SELECT  C.CNPJCPF FROM EMPRESA  E LEFT JOIN FILIAL F ON F.IDEMPRESA = E.IDEMPRESA LEFT JOIN CADASTRO C ON C.IDCADASTRO = E.IDEMPRESA WHERE F.IDFILIAL=" + idFilial;
            return DAL.BD.cDb.ExecutarRetornoIDs(ssql, cnx).ToString();
        }

        

        public DataTable  RetornarEmpresa(int IdEmpresa, string cnx)
        {
            string ssql = " SELECT *  FROM EMPRESA WHERE IDEMPRESA= " + IdEmpresa;
            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);
   
        }
    }
}
