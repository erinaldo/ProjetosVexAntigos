using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistecno.DAL.Models;
using System.Data;

namespace Sistecno.DAL
{
    public class ConexaoSistecno
    {
        public DataTable RetornarGrid(int? idConexao, string cnx)
        {
            string ssql = "";


            ssql += " Select Top 50 Cnx.IdConexao, Cnx.IdCliente, Cli.RazaoSocialNome Cliente, Cnx.IP, Cnx.BaseDeDados, Cnx.UsuarioBD, Cnx.SenhaBD, Cnx.Porta   ";
            ssql += " From Conexao Cnx   ";
            ssql += " Inner Join Cadastro Cli on Cli.IdCadastro = Cnx.IdCliente  ";
            if (idConexao != null)
            {
                ssql += " where Cnx.IdConexao = " + idConexao.ToString();
            }
            ssql += " Order By 1 desc ";

            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oC"></param>
        /// <param name="cnx"></param>
        public void Gravar(Conexao oC, string cnx)
        {
            string ssql = "";

            ssql += " Update Conexao Set IP = '" + oC.IP + "',";
            ssql += " BaseDeDados = '" + oC.BaseDeDados + "',";
            ssql += " UsuarioBD = '" + oC.UsuarioBD + "',";
            ssql += " SenhaBD = '" + oC.SenhaBD + "',";
            ssql += " Porta = '" + oC.Porta + "'";
            ssql += " where IdConexao = " + oC.IdConexao.ToString();
            DAL.BD.cDb.ExecutarSemRetorno(ssql, cnx);

            
            #region Usando Entities Framework
            Sistecno_Entities context = new Sistecno_Entities();
            context.Database.Connection.ConnectionString = cnx;
            context.Database.Connection.Open();
           
            var o = context.Conexao.FirstOrDefault(i => i.IdConexao == oC.IdConexao);
            o.IP = oC.IP;
            o.BaseDeDados = oC.BaseDeDados;
            o.SenhaBD = oC.SenhaBD;
            o.Porta = oC.Porta;
            context.SaveChanges();

            if(context.Database.Connection.State== ConnectionState.Open)
            {
                context.Database.Connection.Close();
            }

            #endregion

        }

        public DataTable Pesquisar(Models.Conexao obj, string cnx)
        {
            string ssql = "";
            Models.Cliente ocliente = obj.Cliente;

            ssql += " Select Top 50 Cnx.IdConexao, Cnx.IdCliente, Cli.RazaoSocialNome Cliente, Cnx.IP, Cnx.BaseDeDados, Cnx.UsuarioBD, Cnx.SenhaBD, Cnx.Porta   ";
            ssql += " From Conexao Cnx   ";
            ssql += " Inner Join Cadastro Cli on Cli.IdCadastro = Cnx.IdCliente  ";
            ssql += " where 0=0 ";
            
            if (obj.IdConexao != 0)
            {
                ssql += " and Cnx.IdConexao = " + obj.IdConexao.ToString();
            }

            if (obj.IdCliente != 0)
            {
                ssql += " and Cnx.IdCliente = " + obj.IdCliente.ToString();
            }

            if (obj.IP != null && obj.IP != "")
            {
                ssql += " and Cnx.IP = " + obj.IP.ToString();
            }

            if (obj.BaseDeDados != null && obj.BaseDeDados != "")
            {
                ssql += " and Cnx.BaseDeDados = " + obj.BaseDeDados.ToString();
            }

            if (ocliente != null)
            {
                Models.Cadastro ocad = ocliente.Cadastro;
                if (ocad != null && ocad.RazaoSocialNome !="")
                {
                    ssql += " and Cli.RazaoSocialNome = " + ocad.RazaoSocialNome;
                }
            }

            ssql += " Order By 1 desc ";

            return DAL.BD.cDb.RetornarDataTable(ssql, cnx);

        }

    }
}

