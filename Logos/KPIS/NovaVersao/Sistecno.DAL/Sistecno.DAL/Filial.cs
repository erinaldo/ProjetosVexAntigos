using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistecno.DAL.Models;

namespace Sistecno.DAL
{
    public class Filial
    {

        public DataSet RetornarTodosCampos(int idfilial, string cnx)
        {
            string ssql = "";
            ssql += " SELECT  *, C.IDCIDADE, CID.IDESTADO, F.IDEMPRESA  ";
            ssql += " FROM CADASTRO C WITH (NOLOCK)  ";
            ssql += " LEFT JOIN CIDADE CID ON CID.IDCIDADE = C.IDCIDADE  ";
            ssql += " INNER JOIN FILIAL F ON F.IDCADASTRO = C.IDCADASTRO ";
            ssql += " WHERE F.IDFILIAL =" + idfilial;

            ssql += " SELECT CTC.NOME TIPODECONTATO,  CCE.ENDERECO ENDCONTADO, *  ";
            ssql += " FROM CADASTROCONTATOENDERECO CCE  ";
            ssql += " LEFT JOIN CADASTROTIPODECONTATO CTC ON CTC.IDCADASTROTIPODECONTATO = CCE.IDCADASTROTIPODECONTATO    ";
            ssql += " INNER JOIN FILIAL F ON F.IDCADASTRO = CCE.IDCADASTRO ";
            ssql += " WHERE F.IDFILIAL=" + idfilial;
            return DAL.BD.cDb.RetornarDataSet(ssql, cnx);
        }


        public int Inserir(DAL.Models.Filial obj, string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            try
            {
                context.Database.Connection.Open();         
                obj.IDFilial = DAL.BD.cDb.RetornarIDTabela(cnx, "FILIAL");
                context.Filial.Add(obj);
                context.SaveChanges();
                return obj.IDFilial;
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

        public void Alterar(DAL.Models.Filial ff, string cnx)
        {
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;


            try
            {
                context.Database.Connection.Open();
                var o = context.Filial.First(i => i.IDFilial == ff.IDFilial);
                o.Ativo = ff.Ativo;
                o.Nome = ff.Nome;
                o.Ativo  = ff.Ativo;
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
    }
}