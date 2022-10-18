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

        #region Painel de Faturamento

        public static DataTable RetornarEmAbertoAnteriores(string con)
        {
            string data =  DateTime.Now.AddMonths(-12).Year.ToString() + "-" +  DateTime.Now.AddMonths(-12).Month.ToString() + "-1";
            string strsql = " SELECT  ";
            strsql += " TI.IDCLIENTE, ";
            strsql += " SUM(ISNULL(TDI.SALDO,0)) EMABERTO   ";
            strsql += " FROM TITULO TI      ";
            strsql += " INNER JOIN TITULODUPLICATA TDI ON TDI.IDTITULO = TI.IDTITULO    ";
            strsql += " WHERE PAGARRECEBER='RECEBER'     ";
            strsql += " AND TDI.DATADEVENCIMENTO < '"+data+"'";
            strsql += " AND TI.ATIVO='SIM'     ";
            strsql += " AND TDI.ATIVO='SIM'   ";
            strsql += " GROUP  BY TI.IDCLIENTE ";

            if(con=="")
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
            else
                return Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, con);


        }

        public static DataTable RetornarListaClienteFaturamento(bool considerarFilial)
        {
            //monta os clientes
            string strsql = " SELECT DISTINCT IDCLIENTE, CADCLI.FantasiaApelido RAZAOSOCIALNOME, CNPJCPF";

            if (considerarFilial)
                strsql += " , T.IDFilial , FL.Nome FILIAL ";

            strsql += " FROM TITULO T  ";
            strsql += " INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = T.IDCLIENTE ";
         
            if (considerarFilial)
                strsql += "INNER JOIN Filial FL ON FL.IDFilial = T.IDFilial ";

            strsql += " WHERE PAGARRECEBER='RECEBER' ";

            if (considerarFilial)
                strsql += " AND CONVERT(DATETIME, DATADEEMISSAO, 120) >=CONVERT(DATETIME,CAST(YEAR(DATEADD(MONTH, -3, GETDATE())) AS NVARCHAR(4)) + '-' + CAST(MONTH(DATEADD(MONTH, -3, GETDATE())) AS NVARCHAR(2)) + '-01', 120) ";
            else
                strsql += " AND CONVERT(DATETIME, DATADEEMISSAO, 120) >=CONVERT(DATETIME,CAST(YEAR(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(4)) + '-' + CAST(MONTH(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(2)) + '-01', 120) ";

            strsql += " AND IDCLIENTE IS NOT NULL ";

            if(considerarFilial==false)
                 strsql += " AND DATADEEMISSAO <'" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01'";

            strsql += " ORDER BY CADCLI.FantasiaApelido ";

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }

        public static DataTable RetornarListaClienteFaturamento()
        {
            //monta os clientes
            string strsql = " SELECT DISTINCT IDCLIENTE, CADCLI.FantasiaApelido RAZAOSOCIALNOME, CNPJCPF";            
            strsql += " FROM TITULO T  ";
            strsql += " INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = T.IDCLIENTE ";            
            strsql += " WHERE PAGARRECEBER='RECEBER' ";           
            strsql += " AND CONVERT(DATETIME, DATADEEMISSAO, 120) >=CONVERT(DATETIME,CAST(YEAR(DATEADD(MONTH, -3, GETDATE())) AS NVARCHAR(4)) + '-' + CAST(MONTH(DATEADD(MONTH, -3, GETDATE())) AS NVARCHAR(2)) + '-01', 120) ";            
            strsql += " AND IDCLIENTE IS NOT NULL ";            
            strsql += " ORDER BY CADCLI.FantasiaApelido ";

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }

        public static DataTable GerarMesesClienteFaturamento()
        {
            string strsql = " SELECT  ";
            strsql += " T.IDCLIENTE, ";
            strsql += " MONTH(T.DATADEEMISSAO) MES , YEAR(T.DATADEEMISSAO) ANO, ";
            //strsql += " T.IDFilial , FL.Nome FILIAL, ";
            strsql += " SUM(ISNULL(TD.VALOR,0)) -  SUM(ISNULL(TD.Desconto,0)) FATURADO,  ";
            //strsql += " SUM(ISNULL(TD.SALDO,0)) EMABERTO ";
            strsql += " ISNULL(( ";
            strsql += " SELECT       ";
            strsql += " SUM(ISNULL(TDI.SALDO,0))   ";
            strsql += " FROM TITULO TI    ";
            strsql += " INNER JOIN TITULODUPLICATA TDI ON TDI.IDTITULO = TI.IDTITULO    ";
            strsql += " WHERE PAGARRECEBER='RECEBER'   ";
            strsql += " AND CONVERT(DATETIME, TDI.DATADEVENCIMENTO, 120) >= CONVERT(DATETIME,CAST(YEAR(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(4)) + '-' + CAST(MONTH(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(2)) + '-01', 120)   ";
            strsql += " AND TDI.DATADEVENCIMENTO <'" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01' ";
            strsql += " AND TI.IDCLIENTE = T.IDCLIENTE  ";
            strsql += " AND TI.ATIVO='SIM'   ";
            strsql += " AND TDI.ATIVO='SIM'   ";
            strsql += " AND MONTH(TDI.DATADEVENCIMENTO) = MONTH(T.DATADEEMISSAO) ";
            strsql += " AND YEAR(TDI.DATADEVENCIMENTO) = YEAR(T.DATADEEMISSAO) ";
            strsql += " ),0) EMABERTO ";
            //            strsql += " SUM(ISNULL(TD.SALDO,0)) -  SUM(ISNULL(TD.Desconto,0)) EMABERTO ";
            strsql += " FROM TITULO T  ";
            strsql += " INNER JOIN TITULODUPLICATA TD ON TD.IDTITULO = T.IDTITULO  ";
            //strsql += "INNER JOIN Filial FL ON FL.IDFilial = T.IDFilial";
            strsql += " WHERE PAGARRECEBER='RECEBER' ";
            strsql += " AND CONVERT(DATETIME, T.DATADEEMISSAO, 120) >= CONVERT(DATETIME,CAST(YEAR(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(4)) + '-' + CAST(MONTH(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(2)) + '-01', 120) ";
            strsql += " AND T.DATADEEMISSAO <'" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01' ";
            strsql += " AND IDCLIENTE IS NOT NULL ";
            strsql += " AND T.Ativo='SIM' ";
            strsql += " AND TD.Ativo='SIM' ";
            strsql += " GROUP BY  ";
            strsql += " T.IDCLIENTE, MONTH(T.DATADEEMISSAO)  ,  YEAR(T.DATADEEMISSAO)  ";
            //strsql += " T.IDFilial , FL.Nome  ";
            strsql += " ORDER BY IDCLIENTE,  YEAR(T.DATADEEMISSAO) ";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }

        public static DataTable GerarDiasClienteFaturamento()
        {
            string strsql = " SELECT  ";
            strsql += " T.IDCLIENTE, ";
            strsql += " day(T.DATADEEMISSAO) dia , ";
            strsql += " T.IDFilial , FL.Nome FILIAL,";
            strsql += " SUM(ISNULL(TD.VALOR,0)) -  SUM(ISNULL(TD.Desconto,0)) FATURADO ";
            strsql += " FROM TITULO T  ";
            strsql += " INNER JOIN TITULODUPLICATA TD ON TD.IDTITULO = T.IDTITULO  ";
            strsql += "INNER JOIN Filial FL ON FL.IDFilial = T.IDFilial";

            strsql += " WHERE PAGARRECEBER='RECEBER' ";
            strsql += " AND T.DATADEEMISSAO >= '" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01' ";
            strsql += " and T.DATADEEMISSAO <= '" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "' ";
            strsql += " AND IDCLIENTE IS NOT NULL ";
            strsql += " AND T.ATIVO='SIM' ";
            strsql += " AND TD.ATIVO='SIM' ";
            strsql += " GROUP BY T.IDCLIENTE, day(T.DATADEEMISSAO) , T.IDFilial , FL.Nome ";
            strsql += " ORDER BY IDCLIENTE ";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }
                
        public static DataTable RetornarListaClienteFaturamento(bool considerarFilial, string con)
        {
            //monta os clientes
            string strsql = " SELECT DISTINCT IDCLIENTE, CADCLI.FantasiaApelido RAZAOSOCIALNOME, CNPJCPF";

            if (considerarFilial)
                strsql += " , T.IDFilial , FL.Nome FILIAL ";

            strsql += " FROM TITULO T  ";
            strsql += " INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO = T.IDCLIENTE ";

            if (considerarFilial)
                strsql += "INNER JOIN Filial FL ON FL.IDFilial = T.IDFilial ";

            strsql += " WHERE PAGARRECEBER='RECEBER' ";

            if (considerarFilial)
                strsql += " AND CONVERT(DATETIME, DATADEEMISSAO, 120) >=CONVERT(DATETIME,CAST(YEAR(DATEADD(MONTH, -3, GETDATE())) AS NVARCHAR(4)) + '-' + CAST(MONTH(DATEADD(MONTH, -3, GETDATE())) AS NVARCHAR(2)) + '-01', 120) ";
            else
                strsql += " AND CONVERT(DATETIME, DATADEEMISSAO, 120) >=CONVERT(DATETIME,CAST(YEAR(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(4)) + '-' + CAST(MONTH(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(2)) + '-01', 120) ";

            strsql += " AND IDCLIENTE IS NOT NULL ";

            if (considerarFilial == false)
                strsql += " AND DATADEEMISSAO <'" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01'";

            strsql += " ORDER BY CADCLI.FantasiaApelido ";

            return Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, con);
        }
        
        public static DataTable GerarMesesClienteFaturamento(string con)
        {
            string strsql = " SELECT  ";
            strsql += " T.IDCLIENTE, ";
            strsql += " MONTH(T.DATADEEMISSAO) MES , YEAR(T.DATADEEMISSAO) ANO, ";
            //strsql += " T.IDFilial , FL.Nome FILIAL, ";
            strsql += " SUM(ISNULL(TD.VALOR,0)) -  SUM(ISNULL(TD.Desconto,0)) FATURADO,  ";
            //strsql += " SUM(ISNULL(TD.SALDO,0)) - SUM(ISNULL(TD.Desconto,0)) EMABERTO ";
            //strsql += " SUM(ISNULL(TD.SALDO,0) ) EMABERTO ";
            strsql += " ISNULL(( ";
            strsql += " SELECT       ";
            strsql += " SUM(ISNULL(TDI.SALDO,0))   ";
            strsql += " FROM TITULO TI    ";
            strsql += " INNER JOIN TITULODUPLICATA TDI ON TDI.IDTITULO = TI.IDTITULO    ";
            strsql += " WHERE PAGARRECEBER='RECEBER'   ";
            strsql += " AND CONVERT(DATETIME, TDI.DATADEVENCIMENTO, 120) >= CONVERT(DATETIME,CAST(YEAR(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(4)) + '-' + CAST(MONTH(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(2)) + '-01', 120)   ";
            strsql += " AND TDI.DATADEVENCIMENTO <'" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01' ";
            strsql += " AND TI.IDCLIENTE = T.IDCLIENTE  ";
            strsql += " AND TI.ATIVO='SIM'   ";
            strsql += " AND TDI.ATIVO='SIM'   ";
            strsql += " AND MONTH(TDI.DATADEVENCIMENTO) = MONTH(T.DATADEEMISSAO) ";
            strsql += " AND YEAR(TDI.DATADEVENCIMENTO) = YEAR(T.DATADEEMISSAO) ";
            strsql += " ),0) EMABERTO ";
            strsql += " FROM TITULO T  ";
            strsql += " INNER JOIN TITULODUPLICATA TD ON TD.IDTITULO = T.IDTITULO  ";
            //strsql += "INNER JOIN Filial FL ON FL.IDFilial = T.IDFilial";
            strsql += " WHERE PAGARRECEBER='RECEBER' ";
            strsql += " AND CONVERT(DATETIME, T.DATADEEMISSAO, 120) >= CONVERT(DATETIME,CAST(YEAR(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(4)) + '-' + CAST(MONTH(DATEADD(MONTH, -12, GETDATE())) AS NVARCHAR(2)) + '-01', 120) ";
            strsql += " AND T.DATADEEMISSAO <'" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01' ";
            strsql += " AND IDCLIENTE IS NOT NULL ";
            strsql += " AND T.Ativo='SIM' ";
            strsql += " AND TD.Ativo='SIM' ";
            strsql += " GROUP BY  ";
            strsql += " T.IDCLIENTE, MONTH(T.DATADEEMISSAO)  ,  YEAR(T.DATADEEMISSAO)  ";
            //strsql += " T.IDFilial , FL.Nome  ";
            strsql += " ORDER BY IDCLIENTE,  YEAR(T.DATADEEMISSAO) ";
            return Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, con);
        }

        public static DataTable GerarDiasClienteFaturamento(string con)
        {
            string strsql = " SELECT  ";
            strsql += " T.IDCLIENTE, ";
            strsql += " day(T.DATADEEMISSAO) dia , ";
            strsql += " T.IDFilial , FL.Nome FILIAL,";
            strsql += " SUM(ISNULL(TD.VALOR,0)) -  SUM(ISNULL(TD.Desconto,0)) FATURADO ";
            strsql += " FROM TITULO T  ";
            strsql += " INNER JOIN TITULODUPLICATA TD ON TD.IDTITULO = T.IDTITULO  ";
            strsql += "INNER JOIN Filial FL ON FL.IDFilial = T.IDFilial";

            strsql += " WHERE PAGARRECEBER='RECEBER' ";
            strsql += " AND T.DATADEEMISSAO >= '" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-01' ";
            strsql += " and T.DATADEEMISSAO <= '" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "' ";
            strsql += " AND IDCLIENTE IS NOT NULL ";
            strsql += " AND T.ATIVO='SIM' ";
            strsql += " AND TD.ATIVO='SIM' ";
            strsql += " GROUP BY T.IDCLIENTE, day(T.DATADEEMISSAO) , T.IDFilial , FL.Nome ";
            strsql += " ORDER BY IDCLIENTE ";
            return Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, con);
        }


        #endregion




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
            return Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), HttpContext.Current.Session["Conn"].ToString());
        }

        public static string ReadCNPJByIdCliente(int IdCliente)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" SELECT CnpjCpf FROM Cadastro WHERE  IDCADASTRO=" + IdCliente.ToString());
            return Sistran.Library.GetDataTables.ExecutarRetornoIDs(s.ToString(), HttpContext.Current.Session["Conn"].ToString()).ToString();
        }

        public DataTable RetornarClientesIntranet(string iniciais, string codigos)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            //s.Append(" SELECT CLI.IDCLIENTE IDCLIENTE, CAD.CNPJCPF, ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) RAZAOSOCIALNOME, CODIGODOCLIENTE FROM CADASTRO CAD, CID.NOME + ' / ' + EST.NOME CIDADE ");
            //s.Append(" INNER JOIN CLIENTE CLI ON CLI.IDCLIENTE = CAD.IDCADASTRO ");
            //s.Append(" INNER JOIN DOCUMENTO DOC ON DOC.IDCLIENTE = CLI.IDCLIENTE ");
            //s.Append(" INNER JOIN CIDADE CID  ON CID.IDCIDADE = C.IDCIDADE ");
            //s.Append(" INNER JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO ");
            //s.Append(" WHERE (CAD.RAZAOSOCIALNOME LIKE '" + iniciais + "%' OR FantasiaApelido LIKE '" + iniciais + "%' )" + (codigos != "" ? " AND CLI.IDCLIENTE in(" + codigos + ")" : ""));
            //s.Append(" ORDER BY ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) ");

            s.Append("  SELECT DISTINCT ");
            s.Append(" CLI.IDCLIENTE IDCLIENTE, CAD.CNPJCPF,  ");
            s.Append(" ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) RAZAOSOCIALNOME, ");
            s.Append(" CID.NOME + ' / ' + EST.UF CIDADE ,  ");
            s.Append(" CODIGODOCLIENTE  ");
            s.Append(" FROM CADASTRO CAD  ");
            s.Append(" INNER JOIN CLIENTE CLI ON CLI.IDCLIENTE = CAD.IDCADASTRO   ");
            s.Append(" INNER JOIN DOCUMENTO DOC ON DOC.IDCLIENTE = CLI.IDCLIENTE   ");
            s.Append(" INNER JOIN CIDADE CID  ON CID.IDCIDADE = CAD.IDCIDADE   ");
            s.Append(" INNER JOIN ESTADO EST ON EST.IDESTADO = CID.IDESTADO   ");
            s.Append(" WHERE 0=0 ");
            
            if(iniciais!="")
                s.Append(" and (CAD.RAZAOSOCIALNOME LIKE '" + iniciais + "%' OR FANTASIAAPELIDO LIKE '" + iniciais + "%' )  ");

            if (codigos != "")
                s.Append("AND CLI.IDCLIENTE in(" + codigos + ")");

            s.Append(" AND CLI.ATIVO='SIM' ");
            s.Append(" AND DOC.TIPODEDOCUMENTO='NOTA FISCAL' ");
            s.Append(" AND DOC.TIPODESERVICO='TRANSPORTE' ");
            s.Append(" AND DOC.DATADEEMISSAO>='2012-01-01' ");
            s.Append(" ORDER BY ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) ");

            return Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), HttpContext.Current.Session["Conn"].ToString());
        }

        public DataTable RetornarClientesRelacionados(string CodigoDoCliente)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" SELECT CLI.IDCLIENTE, CODIGODOCLIENTE, CAD.CNPJCPF, ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) + ' - ' + ISNULL(CID.NOME,'') + '/' + ISNULL(EST.UF,'') RAZAOSOCIALNOME FROM CADASTRO CAD ");
            s.Append(" INNER JOIN CLIENTE CLI ON CLI.IDCLIENTE = CAD.IDCADASTRO ");
            s.Append(" LEFT JOIN CIDADE CID ON CID.IDCidade = CAD.IDCidade ");
            s.Append(" LEFT JOIN Estado EST ON EST.IDEstado = CID.IDEstado ");
            s.Append(" WHERE CLI.IDCLIENTE = " + CodigoDoCliente + " OR CODIGODOCLIENTE =" + CodigoDoCliente);
            s.Append("  OR CODIGODOCLIENTE = (SELECT CODIGODOCLIENTE FROM CLIENTE WHERE IDCLIENTE =" + CodigoDoCliente + ")");
            s.Append(" ORDER BY ISNULL(CAD.FANTASIAAPELIDO, CAD.RAZAOSOCIALNOME) ");
            return Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), HttpContext.Current.Session["Conn"].ToString());
        }

        public DataTable RetornarClientesUsuariosPelasIniciais(string Iniciais)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("  SELECT TOP 15  * FROM CADASTRO CAD ");
            s.Append(" LEFT JOIN USUARIO USU ON USU.IDCADASTRO = CAD.IDCADASTRO ");
            s.Append(" WHERE RAZAOSOCIALNOME LIKE '" + Iniciais + "%' AND FANTASIAAPELIDO IS NOT NULL  ORDER BY FANTASIAAPELIDO");
            return Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), HttpContext.Current.Session["Conn"].ToString());
        }

        public sealed class Divisao
        {
            public DataTable RetornarPais(int idCliente)
            {                

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