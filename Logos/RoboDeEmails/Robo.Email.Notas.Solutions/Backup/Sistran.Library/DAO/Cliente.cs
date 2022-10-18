using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using System.Web;

namespace SistranDAO
{
    public sealed class Cliente
    {
        public static SistranMODEL.Cliente RetornaCliente(int clienteIdCadastro, string Conn)
        {
            SistranMODEL.Cliente Cliente = new SistranMODEL.Cliente();
            Encoding oEnc = Encoding.ASCII;

            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT C.IDCADASTRO, C.FANTASIAAPELIDO, CI.IMAGEM 	FROM CADASTRO C INNER JOIN  CADASTROIMAGEM CI ON C.IDCADASTRO = CI.IDCADASTRO 	WHERE C.IDCADASTRO = " + clienteIdCadastro + " ");

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
            IDataReader drCliente = cd.ExecuteReader();
            if (drCliente.Read())
            {
                Cliente = new SistranMODEL.Cliente(Convert.ToInt32(drCliente["IDCadastro"]), drCliente["FantasiaApelido"].ToString(), ((byte[])drCliente["Imagem"]));
            }

            drCliente.Close();
            cn.Close();
            return Cliente;
        }

        public static string RetornaDivisoesClientes(string idUsuario, string idCliente)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT CliDiv.IDClienteDivisao     ");
            txt.Append(" FROM   UsuarioCliente UsuCli    ");
            txt.Append(" INNER JOIN UsuarioClienteDivisao UsuCliDiv on(UsuCliDiv.IDUsuarioCliente = UsuCli.IDUsuarioCliente)");
            txt.Append(" INNER JOIN ClienteDivisao CliDiv on(CliDiv.IDClienteDivisao = UsuCliDiv.IDClienteDivisao)");
            txt.Append(" WHERE CliDiv.IDCliente = " + idCliente);
            txt.Append(" AND UsuCli.IDUsuario =" + idUsuario);

            string m = "";
            DataTable t = Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");

            foreach (DataRow item in t.Rows)
            {
                m += item["IDClienteDivisao"].ToString() + ",";
                m += RetornaDivisoesFilhos(item["IDClienteDivisao"].ToString());
            }

            if (m.Length > 0)
                m = m.Substring(0, m.Length - 1);
            return m;
        }

        public static DataTable DivisoesCompleta(string idCliente)
        {
            StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT * FROM CLIENTEDIVISAO WHERE  Nome <> 'CIELO' AND IDCLIENTE='" + idCliente + "'");
            return Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");
        }

        public static string RetornaDivisoesFilhos(string IDClienteDivisao)
        {
            string mm = "";
            StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT  IDClienteDivisao FROM  ClienteDivisao WHERE  IDParente = " + IDClienteDivisao);

            DataTable t = Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");

            foreach (DataRow item in t.Rows)
            {
                mm += item["IDClienteDivisao"].ToString() + ",";
                mm += RetornaDivisoesNetos(item["IDClienteDivisao"].ToString());

            }

            return mm;
        }

        public static string RetornaDivisoesNetos(string IDClienteDivisao)
        {
            string mm = "";
            StringBuilder txt = new StringBuilder();
            txt.Append(" SELECT  IDClienteDivisao FROM  ClienteDivisao WHERE  IDParente = " + IDClienteDivisao);

            DataTable t = Sistran.Library.GetDataTables.RetornarDataTable(txt.ToString(), "");

            foreach (DataRow item in t.Rows)
            {
                mm += item["IDClienteDivisao"].ToString() + ",";
            }

            return mm;
        }

        public static DataTable Read(int idCliente)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" SELECT * FROM CLIENTE WHERE  IDCLIENTE=" + idCliente.ToString());
            return Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), "");
        }

        public static DataTable ReadCadastro(int idCliente)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" SELECT CNPJCPF, RAZAOSOCIALNOME FROM CLIENTE  INNER JOIN CADASTRO ON CLIENTE.IDCLIENTE = CADASTRO.IDCADASTRO  WHERE CLIENTE.IDCLIENTE =" + idCliente.ToString());
            return Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), "");
        }

        public static string ReadCNPJByIdCliente(int IdCliente)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" SELECT CnpjCpf FROM Cadastro WHERE  IDCADASTRO=" + IdCliente.ToString());
            return Sistran.Library.GetDataTables.ExecutarRetornoIDs(s.ToString(), "").ToString();
        }

        public DataTable RetornarClientesIntranet(string iniciais, string codigos)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" SELECT CLI.IDCLIENTE IDCLIENTE, CAD.CNPJCPF, ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) RAZAOSOCIALNOME, CODIGODOCLIENTE FROM CADASTRO CAD ");
            s.Append(" INNER JOIN CLIENTE CLI ON CLI.IDCLIENTE = CAD.IDCADASTRO ");
            s.Append(" WHERE (CAD.RAZAOSOCIALNOME LIKE '" + iniciais + "%' OR FantasiaApelido LIKE '" + iniciais + "%' )" + (codigos != "" ? " AND CLI.IDCLIENTE in(" + codigos + ")" : ""));
            s.Append(" ORDER BY ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) ");
            return Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), "");
        }

        public DataTable RetornarClientesRelacionados(string CodigoDoCliente)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" SELECT CLI.IDCLIENTE, CODIGODOCLIENTE, CAD.CNPJCPF, ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) RAZAOSOCIALNOME FROM CADASTRO CAD ");
            s.Append(" INNER JOIN CLIENTE CLI ON CLI.IDCLIENTE = CAD.IDCADASTRO ");
            s.Append(" WHERE CLI.IDCLIENTE = " + CodigoDoCliente + " OR CODIGODOCLIENTE =" + CodigoDoCliente);
            s.Append("  OR CODIGODOCLIENTE = (SELECT CODIGODOCLIENTE FROM CLIENTE WHERE IDCLIENTE =" + CodigoDoCliente + ")");
            s.Append(" ORDER BY ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) ");
            return Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), "");
        }

        public DataTable RetornarClientesUsuariosPelasIniciais(string Iniciais)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("  SELECT TOP 10  * FROM CADASTRO CAD ");
            s.Append(" LEFT JOIN USUARIO USU ON USU.IDCADASTRO = CAD.IDCADASTRO ");
            s.Append(" WHERE RAZAOSOCIALNOME LIKE '"+Iniciais+"%' ");
            return Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), "");
        }

        public sealed class Divisao
        {
            public DataTable RetornarPais(int idCliente)
            {
                //string strsql = "SELECT IDCLIENTEDIVISAO, IDCLIENTE, NOME, IDPARENTE FROM CLIENTEDIVISAO WHERE IDCLIENTE="+ idCliente.ToString() +" AND  IDPARENTE IS NULL ";

                List<SistranMODEL.Usuario> us = (List<SistranMODEL.Usuario>)HttpContext.Current.Session["USUARIO"];

                StringBuilder strsql = new StringBuilder();
                strsql.Append(" SELECT  CliDiv.IDClienteDivisao, CliDiv.Nome  FROM   UsuarioClienteDivisao UsuCliDiv     ");
                strsql.Append(" INNER JOIN ClienteDivisao CliDiv  ON (CliDiv.IDClienteDivisao = UsuCliDiv.IDClienteDivisao)     ");
                strsql.Append(" INNER JOIN UsuarioCliente UsuCli  ON (UsuCli.IDUsuarioCliente = UsuCliDiv.IDUsuarioCliente)     ");
                strsql.Append(" WHERE UsuCli.IDUsuario=" + us[0].UsuarioId);
                strsql.Append(" AND CliDiv.IDCliente =" + idCliente.ToString());
                strsql.Append(" AND CliDiv.Ativo = 'SIM'     ");
                strsql.Append(" ORDER BY CliDiv.IDParente ASC      ");
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
            }

            public DataTable RetornarFlihos(int idCliente, int IdParente)
            {
                string strsql = "SELECT IDCLIENTEDIVISAO, IDCLIENTE, NOME, IDPARENTE FROM CLIENTEDIVISAO WHERE IDClienteDivisao <> " + IdParente + " AND IDCLIENTE=" + idCliente.ToString() + " AND  IDPARENTE=" + IdParente;
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
            }

            public void InserirClienteDivisao(string idCliente, string nome, string IdParente)
            {
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CLIENTEDIVISAO");
                
                string strsql = " INSERT INTO CLIENTEDIVISAO ";
                strsql += " ( ";
                strsql += " IDCLIENTEDIVISAO, ";
                strsql += " IDCLIENTE, ";
                strsql += " NOME, ";
                strsql += " IDPARENTE, ";
                strsql += " BASEEXTERNA, ";
                strsql += " SISTEMA, ";
                strsql += " DATADECADASTRO, ";
                strsql += " ATIVO) ";
                strsql += " VALUES( ";
                strsql += ID+ " , ";
                strsql += idCliente + " , ";
                strsql += "'" + nome.ToUpper() + "', ";
                strsql += (IdParente==""?"null":IdParente) + ", ";
                strsql += " 'NAO', ";
                strsql += " 'NAO', ";
                strsql += " GETDATE(), ";
                strsql += " 'SIM') ";

                Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql.ToString(), "");
            }

            public void AlterarNomeClienteDivisao(string IDCLIENTEDIVISAO, string nome)
            {
                string strsql = " UPDATE CLIENTEDIVISAO SET NOME='" + nome.ToUpper() + "' WHERE IDCLIENTEDIVISAO=" + IDCLIENTEDIVISAO;
                Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql.ToString(), "");
            }

            public void DeletarClienteDivisao(string IDCLIENTEDIVISAO)
            {
                string strsql = " DELETE FROM CLIENTEDIVISAO WHERE IDCLIENTEDIVISAO=" + IDCLIENTEDIVISAO;
                Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql.ToString(), "");
            }

            public DataTable RetornarListaDivisoesProduto(string CodigoProduto)
            {
                string strsql = " SELECT E.IDEstoque, Nome, CD.IDClienteDivisao, PC.IDProdutoCliente FROM PRODUTOCLIENTE PC ";
                strsql += " INNER JOIN Estoque  E ON E.IDProdutoCliente = PC.IDProdutoCliente ";
                strsql += " INNER JOIN EstoqueDivisao ED ON ED.IDEstoque = E.IDEstoque  ";
                strsql += " INNER JOIN ClienteDivisao CD ON CD.IDClienteDivisao = ED.IDClienteDivisao ";
                strsql += " WHERE CODIGO='" + CodigoProduto + "' AND (ED.ATIVO ='SIM' OR ED.ATIVO IS NULL)";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");

            }

            public void ApagarEstoqueDivisaoByCodigoProdutoAndIDClienteDivisao(string CdProduto, string IDClienteDivisao)
            {
                string strsql = " DELETE FROM EstoqueDivisao WHERE IDEstoqueDivisao IN(";
                strsql += " SELECT ED.IDEstoqueDivisao ";
                strsql += " FROM PRODUTOCLIENTE PC";
                strsql += " INNER JOIN Estoque  E ON E.IDProdutoCliente = PC.IDProdutoCliente";
                strsql += " INNER JOIN EstoqueDivisao ED ON ED.IDEstoque = E.IDEstoque ";
                strsql += " INNER JOIN ClienteDivisao CD ON CD.IDClienteDivisao = ED.IDClienteDivisao";
                strsql += " WHERE CODIGO='" + CdProduto + "'";
                strsql += " and ed.IDClienteDivisao = " + IDClienteDivisao + ")";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql.ToString(), "");
            }

            public void InserirEstoqueDivisao(string CdProduto, string IDClienteDivisao)
            {

                string strsql = " SELECT top 1 E.*  ";
                strsql += " FROM PRODUTOCLIENTE PC";
                strsql += " INNER JOIN Estoque  E ON E.IDProdutoCliente = PC.IDProdutoCliente";
                strsql += " INNER JOIN EstoqueDivisao ED ON ED.IDEstoque = E.IDEstoque ";
                strsql += " INNER JOIN ClienteDivisao CD ON CD.IDClienteDivisao = ED.IDClienteDivisao";
                strsql += " WHERE CODIGO='" + CdProduto + "'";
                DataTable dtEstoque = Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");


                if (dtEstoque.Rows.Count == 0)
                {
                    //GravarEstoque
                    string IDESTOQUE = Sistran.Library.GetDataTables.RetornarIdTabela("ESTOQUE");
                    try
                    {
                        strsql = "INSERT INTO ESTOQUE (";
                        strsql += " IDEstoque,";
                        strsql += " IDProdutoCliente,";
                        strsql += " IDFilial,";
                        strsql += " Saldo) VALUES(";
                        strsql += IDESTOQUE + " ,";
                        strsql += " (SELECT IDPRODUTOCLIENTE FROM PRODUTOCLIENTE WHERE CODIGO='" + CdProduto + "' OR CODIGODOCLIENTE='" + CdProduto + "'),";
                        strsql += " 1,";
                        strsql += " 0.0)";// SELECT ISNULL(MAX(IDEstoque),0) FROM ESTOQUE";
                        Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");


                    }
                    catch (Exception)
                    {
                       // throw;
                    }
                    string IDESTOQUEDIVISAO = Sistran.Library.GetDataTables.RetornarIdTabela("ESTOQUEDIVISAO");

                    strsql = "INSERT INTO ESTOQUEDIVISAO(IDEstoqueDivisao, IDEstoque, IDClienteDivisao, Saldo, SaldoBaseExterna, ATIVO) VALUES (" + IDESTOQUEDIVISAO + " , " + IDESTOQUE + ", " + IDClienteDivisao + ", 0.00, 0.00, 'SIM')";
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");

                }
                else
                {
                    int IDEstoqueDivisao = Sistran.Library.GetDataTables.ExecutarRetornoID("select idestoquedivisao from ESTOQUEDIVISAO where idestoque=" + dtEstoque.Rows[0]["IDESTOQUE"] + " and IDClienteDivisao=" + IDClienteDivisao, "");

                    if (IDEstoqueDivisao == 0)
                    {
                        string IDESTOQUEDIVISAO = Sistran.Library.GetDataTables.RetornarIdTabela("ESTOQUEDIVISAO");
                        strsql = "INSERT INTO ESTOQUEDIVISAO(IDEstoqueDivisao, IDEstoque, IDClienteDivisao, Saldo, SaldoBaseExterna, ATIVO) VALUES (" + IDESTOQUEDIVISAO + " , " + dtEstoque.Rows[0]["IDESTOQUE"] + ", " + IDClienteDivisao + ", 0.00, 0.00, 'SIM')";
                        Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
                    }
                    else
                    {
                        strsql = "UPDATE ESTOQUEDIVISAO SET ATIVO='SIM' WHERE IDEstoqueDivisao=" + IDEstoqueDivisao;
                        Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
                    }
                }

            }

            public void DesabilitarEstoqueDivisao(string CdProduto)
            {

                string strsql = " UPDATE ESTOQUEDIVISAO  SET ATIVO='NAO' ";
                strsql += " WHERE IDESTOQUE IN  ";
                strsql += " ( ";
                strsql += " SELECT E.IDESTOQUE FROM PRODUTOCLIENTE PC  ";
                strsql += " INNER JOIN ESTOQUE  E ON E.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE  ";
                strsql += " INNER JOIN ESTOQUEDIVISAO ED ON ED.IDESTOQUE = E.IDESTOQUE   ";
                strsql += " INNER JOIN CLIENTEDIVISAO CD ON CD.IDCLIENTEDIVISAO = ED.IDCLIENTEDIVISAO  ";
                strsql += " WHERE CODIGO='" + CdProduto + "' ";
                strsql += " ) AND Saldo=0";
                Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");

            }

            public DataTable ListarDivisoesCadastradasUser(string IdUsuario, string IdCliente)
            {
                string strsql = " SELECT CD.IDCLIENTEDIVISAO, CD.NOME  FROM CLIENTEDIVISAO CD ";
                strsql += " INNER JOIN USUARIOCLIENTEDIVISAO UCD ON CD.IDCLIENTEDIVISAO = UCD.IDClienteDivisao  INNER JOIN USUARIOCLIENTE UC ON UC.IDUsuarioCliente = UCD.IDUsuarioCliente  INNER JOIN USUARIO U ON U.IDUSUARIO = UC.IDUSUARIO   ";
                strsql += " WHERE CD.IDCLIENTE=" + IdCliente + " AND U.IDUSUARIO=" + IdUsuario;
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");

            }

        }
    }
}