using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistecno.DAL;
using Sistecno.DAL.Models;

namespace Sistecno.BLL
{
    public class EmpresaFilial
    {
        public DataTable RetornarEmpresa(int? idEmpresa, string cnx)
        {
            return new Sistecno.DAL.EmpresaFilial().RetornarEmpresa(idEmpresa, cnx);
        }

        public DataTable RetornarFilial(int idempresa, int? idFilial, string cnx)
        {
            return new Sistecno.DAL.EmpresaFilial().RetornarFilial(idempresa, idFilial, cnx);
        }

        public int InserirEmpresa(Sistecno.DAL.Models.Empresa obj, string cnx)
        {
            return new Sistecno.DAL.EmpresaFilial().InserirEmpresa(obj, cnx);
        }

        public DataTable RetornarFilial(string cnx)
        {
            return new Sistecno.DAL.EmpresaFilial().RetornarFilial(cnx);
        }


        public DataTable Retornar(List<ParametrosPesquisa> swhere, int idEmpresa, string cnx)
        {
            try
            {
                string where = " WHERE 0=0 AND c.RAZAOSOCIALNOME <>'' AND E.IDEMPRESA = " + idEmpresa + " ";

                if (swhere != null)
                {
                    for (int i = 0; i < swhere.Count; i++)
                    {
                        if (swhere[i].Valor.Length > 0)
                        {
                            switch (swhere[i].Tipo)
                            {
                                case "int":
                                    where += " AND " + swhere[i].Campo + "=" + swhere[i].Valor;
                                    break;

                                case "string":
                                    where += " AND " + swhere[i].Campo + " like '" + swhere[i].Valor + "%'";
                                    break;
                            }
                        }

                    }
                }
                return new Sistecno.DAL.EmpresaFilial().Retornar(where, cnx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RetonarCnpjEmpresaByIdFilial(int idempresa, string cnx)
        {
            return new Sistecno.DAL.EmpresaFilial().RetonarCnpjEmpresaByIdFilial(idempresa, cnx);
        }

        public DataTable RetonarFilialDaEmpresa(int idempresa, string cnx)
        {
            return new Sistecno.DAL.EmpresaFilial().RetonarFilialDaEmpresa(idempresa, cnx);
        }

        public DataTable RetornarEmpresa(int IdEmpresa, string cnx)
        {
            return new Sistecno.DAL.EmpresaFilial().RetornarEmpresa(IdEmpresa, cnx);
        }

        public void AlteraEmpresa(Empresa objEmpresa, string cnx)
        {
            new Sistecno.DAL.EmpresaFilial().AlterarEmpresa(objEmpresa, cnx);
        }
    }
}