using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SistranDAO
{
    public class Deposito
    {
        public DataTable CarregarCboDeposiito(int idFilial)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT DISTINCT ");
            strsql.Append(" D.IDDEPOSITO, ");
            strsql.Append(" D.DESCRICAO DEPOSITO   ");
            strsql.Append(" FROM DEPOSITO D ");
            strsql.Append(" INNER JOIN DEPOSITOPLANTA DP ON (DP.IDDEPOSITO = D.IDDEPOSITO) ");
            strsql.Append(" INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON (DPL.IDDEPOSITOPLANTA = DP.IDDEPOSITOPLANTA) WHERE D.IDFILIAL =" + idFilial);
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString());
        }

        //public DataTable CarregarCboDeposiitobyFilialInternet()
        //{
        //    StringBuilder strsql = new StringBuilder();
        //    strsql.Append(" SELECT DISTINCT ");
        //    strsql.Append(" D.IDDEPOSITO, ");
        //    strsql.Append(" D.DESCRICAO DEPOSITO   ");
        //    strsql.Append(" FROM DEPOSITO D ");
        //    strsql.Append(" INNER JOIN DEPOSITOPLANTA DP ON (DP.IDDEPOSITO = D.IDDEPOSITO) ");
        //    strsql.Append(" INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON (DPL.IDDEPOSITOPLANTA = DP.IDDEPOSITOPLANTA) WHERE D.IDFILIAL =" + idFilial);
        //    return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString());
        //}

        public DataTable CarregarCboPlanta(int idDeposito)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT DISTINCT ");
            strsql.Append(" DP.IDDEPOSITOPLANTA, ");
            strsql.Append(" DP.DESCRICAO PLANTA   ");
            strsql.Append(" FROM DEPOSITO D ");
            strsql.Append(" INNER JOIN DEPOSITOPLANTA DP ON (DP.IDDEPOSITO = D.IDDEPOSITO) ");
            strsql.Append(" INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON (DPL.IDDEPOSITOPLANTA = DP.IDDEPOSITOPLANTA) WHERE DP.DESCRICAO LIKE '%PALLE%' AND D.IDDeposito=" + idDeposito);
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString());
        }

        public DataTable CarregarCboRua(int idDepositoPlanta)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT DISTINCT ");
            strsql.Append(" SUBSTRING(DPL.CODIGO, 1,2) RUA  ");
            strsql.Append(" FROM DEPOSITOPLANTALOCALIZACAO DPL WHERE IDDEPOSITOPLANTA=" + idDepositoPlanta);
            strsql.Append(" ORDER BY SUBSTRING(DPL.CODIGO, 1,2)");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString());
        }

        public DataTable CarregarCboIventario(string IdFilial, string IDCLIENTE)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT IDINVENTARIO, DESCRICAO  + '( '+ FANTASIAAPELIDO +' - ' + CNPJCPF +')' INVENTARIO   FROM INVENTARIO I   INNER JOIN CADASTRO CADCLI ON CADCLI.IDCADASTRO= I.IDCLIENTE WHERE IdFilial =" + IdFilial + " AND CADCLI.IDCADASTRO="+ IDCLIENTE +" AND I.SITUACAO = 'EM ANDAMENTO'");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString());
        }

        public DataTable CarregarCboIventarioContagem(string IdFilial, string clientes, string idinventario)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT CAST(IC.IDINVENTARIOCONTAGEM AS NVARCHAR(50)) + '-' + IC.Descricao + '-' + STATUS VALOR, IC.DESCRICAO FROM INVENTARIOCONTAGEM IC INNER JOIN INVENTARIO I ON I.IDINVENTARIO = IC.IDINVENTARIO WHERE I.IDFILIAL=" + IdFilial + " AND I.Situacao LIKE '%ANDAMENTO%' AND IDCLIENTE in(" + clientes + ") AND I.IDINVENTARIO=" + idinventario);
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString());
        }

        public DataTable RetornarMaxMinRuas(string IDINVENTARIO)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT     ");
            strsql.Append(" MIN(SUBSTRING(DPL.CODIGO, 1,2)) RUA_MIN , MAX(SUBSTRING(DPL.CODIGO, 1,2)) RUA_MAX ");
            strsql.Append(" FROM DEPOSITO D    ");
            strsql.Append(" inner JOIN DEPOSITOPLANTA DP ON (DP.IDDEPOSITO = D.IDDEPOSITO)      ");
            strsql.Append(" inner JOIN DEPOSITOPLANTALOCALIZACAO DPL ON (DPL.IDDEPOSITOPLANTA = DP.IDDEPOSITOPLANTA)       ");
            strsql.Append(" inner JOIN INVENTARIOCONTAGEMPRODUTO ICP ON  ICP.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO       ");
            strsql.Append(" inner JOIN INVENTARIOCONTAGEM IC ON  IC.IDINVENTARIOCONTAGEM = ICP.IDINVENTARIOCONTAGEM       ");
            strsql.Append(" inner JOIN INVENTARIO I ON I.IDINVENTARIO = IC.IDINVENTARIO      ");
            strsql.Append(" inner JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM=ICP.IDPRODUTOEMBALAGEM)    ");
            strsql.Append(" inner JOIN PRODUTO P ON (P.IDPRODUTO=PE.IDPRODUTO)    ");
            strsql.Append(" inner JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE=PE.IDPRODUTOCLIENTE)     ");
            strsql.Append(" WHERE 0=0 ");
            strsql.Append(" AND I.SITUACAO LIKE '%ANDAMENTO%' AND ( (IC.DESCRICAO LIKE '%CONTAGEM%' AND  IC.DESCRICAO LIKE '%SISTEMA%') OR IC.STATUS='POSICAO DO SISTEMA')  ");
            strsql.Append(" AND COALESCE(ICP.SITUACAO,'') <> 'CANCELADO'   AND I.IDINVENTARIO="+ IDINVENTARIO);
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString());
        }

        public DataTable CarregarIventario(int IdInventarioContagem, bool ConsiderarRuas, string RuaIni, string RuaFinal, string colunas,  string andades, string idInventario)
        {
            return Sistran.Library.GetDataTables.RetornarDataTable("SELECT * FROM RETORNARPOSICOESCOMPLETAS(" + andades + ", " + RuaIni + "," + RuaFinal + ", " + IdInventarioContagem.ToString() + ", " + colunas + "," + idInventario + ")");
        }

        public DataTable CarregarIventario(int IdInventarioContagem, bool ConsiderarRuas, string RuaIni, string RuaFinal, string idinventario)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT   DPL.IDDEPOSITOPLANTA,");
            strsql.Append(" DPL.CODIGO, ");
            strsql.Append(" DPL.ATIVO,   ");
            strsql.Append(" SUBSTRING(DPL.CODIGO, 1,2) RUA,  ");
            strsql.Append(" SUBSTRING(DPL.CODIGO, 3,2) COLUNA,   ");
            strsql.Append(" SUBSTRING(DPL.CODIGO, 5,2) PREDIO,  I.IDINVENTARIO,  IC.DESCRICAO,  ");
            strsql.Append(" SUM(CAST(ISNULL(ICP.QUANTIDADE,0) AS INT) * CAST(ISNULL(PE.UNIDADEDOCLIENTE,0) AS INT)) QUANTIDADE, ");
            strsql.Append(" I.IDINVENTARIO   ");
            strsql.Append(" FROM DEPOSITO D  ");
            strsql.Append(" LEFT JOIN DEPOSITOPLANTA DP ON (DP.IDDEPOSITO = D.IDDEPOSITO)    ");
            strsql.Append(" LEFT JOIN DEPOSITOPLANTALOCALIZACAO DPL ON (DPL.IDDEPOSITOPLANTA = DP.IDDEPOSITOPLANTA)     ");
            strsql.Append(" LEFT JOIN INVENTARIOCONTAGEMPRODUTO ICP ON  ICP.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO     ");
            strsql.Append(" LEFT JOIN INVENTARIOCONTAGEM IC ON  IC.IDINVENTARIOCONTAGEM = ICP.IDINVENTARIOCONTAGEM     ");
            strsql.Append(" LEFT JOIN INVENTARIO I ON I.IDINVENTARIO = IC.IDINVENTARIO    ");
            strsql.Append(" LEFT JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM=ICP.IDPRODUTOEMBALAGEM)  ");
            strsql.Append(" LEFT JOIN PRODUTO P ON (P.IDPRODUTO=PE.IDPRODUTO)  ");
            strsql.Append(" LEFT JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE=PE.IDPRODUTOCLIENTE)   ");
            strsql.Append(" WHERE  IC.IDINVENTARIOCONTAGEM=" + IdInventarioContagem);
            strsql.Append(" AND COALESCE(ICP.SITUACAO,'') <> 'CANCELADO' ");
            strsql.Append(" AND I.IDINVENTARIO="+ idinventario);

            if (ConsiderarRuas)
            {
                strsql.Append(" AND  SUBSTRING(DPL.CODIGO, 1,2)>='"+ RuaIni + "'");
                strsql.Append(" AND  SUBSTRING(DPL.CODIGO, 1,2)<='" + RuaFinal + "'");
            }

            strsql.Append(" GROUP BY  DPL.IDDEPOSITOPLANTA, DPL.CODIGO, DPL.ATIVO,  SUBSTRING(DPL.CODIGO, 1,2) , SUBSTRING(DPL.CODIGO, 3,2) ,   SUBSTRING(DPL.CODIGO, 5,2) ,  I.IDINVENTARIO,  IC.DESCRICAO,  I.IDINVENTARIO   ");
            strsql.Append(" ORDER BY DPL.CODIGO  ");


            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString());
        }

        public DataTable RakingUsers(int IdInventarioContagem)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("  SELECT  ICP.IDUSUARIO,U.NOME, COUNT(DPL.CODIGO) QUANTIDADE ");
            strsql.Append(" FROM DEPOSITO D    ");
            strsql.Append(" LEFT JOIN DEPOSITOPLANTA DP ON (DP.IDDEPOSITO = D.IDDEPOSITO)      ");
            strsql.Append(" LEFT JOIN  DEPOSITOPLANTALOCALIZACAO DPL ON (DPL.IDDEPOSITOPLANTA = DP.IDDEPOSITOPLANTA)      ");
            strsql.Append(" LEFT JOIN  INVENTARIOCONTAGEMPRODUTO ICP ON  ICP.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO       ");
            strsql.Append(" LEFT JOIN  INVENTARIOCONTAGEM IC ON  IC.IDINVENTARIOCONTAGEM = ICP.IDINVENTARIOCONTAGEM       ");
            strsql.Append(" LEFT JOIN  INVENTARIO I ON I.IDINVENTARIO = IC.IDINVENTARIO      ");
            strsql.Append(" LEFT JOIN  PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM=ICP.IDPRODUTOEMBALAGEM)    ");
            strsql.Append(" LEFT JOIN  PRODUTO P ON (P.IDPRODUTO=PE.IDPRODUTO)    ");
            strsql.Append(" LEFT JOIN  PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE=PE.IDPRODUTOCLIENTE) ");
            strsql.Append(" LEFT JOIN  USUARIO U ON U.IDUSUARIO = ICP.IDUSUARIO   ");
            strsql.Append(" WHERE  IC.IDINVENTARIOCONTAGEM="+IdInventarioContagem+" AND COALESCE(ICP.SITUACAO,'') <> 'CANCELADO' ");
            strsql.Append(" GROUP BY ICP.IDUSUARIO, U.NOME      ");
            strsql.Append(" ORDER BY COUNT(DPL.CODIGO) DESC ");

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString());
        }

        public DataTable NaoContados(string IDDEPOSITOPLANTA, string ends  )
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT DPL.CODIGO, DPL.ATIVO  FROM DEPOSITOPLANTA DP INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DP.IDDEPOSITOPLANTA = DPL.IDDEPOSITOPLANTA AND CODIGO in (" + ends + ") AND DP.IDDEPOSITOPLANTA = " + IDDEPOSITOPLANTA + " AND DPL.ATIVO='SIM'");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(),"");
           
        }

        public DataTable Layout(string IdDepositoPlanta, string Rua)
        {
            StringBuilder strsql = new StringBuilder();
            //strsql.Append(" SELECT ");
            //strsql.Append(" DPL.IDDEPOSITOPLANTA,");
            //strsql.Append(" DPL.CODIGO,");
            //strsql.Append(" SUBSTRING(DPL.CODIGO, 1,2) RUA,");
            //strsql.Append(" SUBSTRING(DPL.CODIGO, 3,2) COLUNA,");
            //strsql.Append(" SUBSTRING(DPL.CODIGO, 5,2) PREDIO,");
            //strsql.Append(" DPL.ATIVO,");
            //strsql.Append(" DPL.IDDEPOSITOPLANTALOCALIZACAO");
            //strsql.Append(" FROM DEPOSITOPLANTALOCALIZACAO DPL ");
            //strsql.Append(" WHERE DPL.IDDEPOSITOPLANTA =" + IdDepositoPlanta);


            strsql.Append(" SELECT   DPL.IDDEPOSITOPLANTA,  ");
            strsql.Append(" DPL.CODIGO, SUBSTRING(DPL.CODIGO, 1,2) RUA,  ");
            strsql.Append(" SUBSTRING(DPL.CODIGO, 3,2) COLUNA,  ");
            strsql.Append(" SUBSTRING(DPL.CODIGO, 5,2) PREDIO,  ");
            strsql.Append(" DPL.ATIVO,  ");
            strsql.Append(" DPL.IDDEPOSITOPLANTALOCALIZACAO,  ");
            strsql.Append(" CASE WHEN SUM(UAL.SALDO) >0 THEN 'SIM' ELSE 'NAO' END PRODUTO ");
            strsql.Append(" FROM DEPOSITOPLANTALOCALIZACAO DPL  ");
            strsql.Append(" INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO   ");
            strsql.Append(" INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON (UAL.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM)  ");
            strsql.Append(" WHERE DPL.IDDEPOSITOPLANTA =" + IdDepositoPlanta);
            strsql.Append(" AND UAL.SALDO>0 ");

            if (Rua != "TODOS" && Rua != "")
            {
                strsql.Append(" AND SUBSTRING(DPL.CODIGO, 1,2)='" + Rua + "'");
            }

            strsql.Append(" GROUP BY  ");
            strsql.Append(" DPL.IDDEPOSITOPLANTA,  ");
            strsql.Append(" DPL.CODIGO, SUBSTRING(DPL.CODIGO, 1,2) ,  ");
            strsql.Append(" SUBSTRING(DPL.CODIGO, 3,2) ,  ");
            strsql.Append(" SUBSTRING(DPL.CODIGO, 5,2) ,  ");
            strsql.Append(" DPL.ATIVO,  ");
            strsql.Append(" DPL.IDDEPOSITOPLANTALOCALIZACAO ");
            strsql.Append(" ORDER BY CODIGO ");


            
            //strsql.Append(" AND DPL.CODIGO BETWEEN '060101' and '100505' ");
            //strsql.Append(" ORDER BY CODIGO");

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }


        public DataTable RetornarProdutosEndereco(string endereco)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT DISTINCT PC.CODIGO, PC.IDProdutoCliente, ISNULL(PC.CODIGODOCLIENTE, PC.CODIGO) CODIGODOCLIENTE,  PC.DESCRICAO,   DPL.IDDEPOSITOPLANTALOCALIZACAO ,   PC.IDPRODUTOCLIENTE ,  ISNULL(EST.SALDO, 0.00) SALDO ");
            strsql.Append(" FROM DEPOSITOPLANTALOCALIZACAO DPL  ");
            strsql.Append(" INNER JOIN UNIDADEDEARMAZENAGEM UA ON UA.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO   ");
            strsql.Append(" INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON (UAL.IDUNIDADEDEARMAZENAGEM = UA.IDUNIDADEDEARMAZENAGEM)  ");
            strsql.Append(" INNER JOIN LOTE LT ON LT.IDLOTE = UAL.IDLOTE  INNER JOIN PRODUTOCLIENTE  PC ON PC.IDPRODUTOCLIENTE = LT.IDPRODUTOCLIENTE ");
            strsql.Append(" INNER JOIN ProdutoEmbalagem PE ON PE.IDProdutoCliente = PC.IDProdutoCliente ");
            strsql.Append(" INNER JOIN Produto P ON P.IDProduto = PE.IDProduto LEFT JOIN ESTOQUE EST ON EST.IDPRODUTOCLIENTE = PC.IDPRODUTOCLIENTE");
            strsql.Append(" WHERE  PC.IDCliente in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ");
            strsql.Append(" AND DPL.CODIGO='" + endereco + "'  ");

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }
    }
}