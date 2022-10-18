using System.Data;


namespace SistranDAO
{
    public  class Filial
    {
        public DataTable ListarFiliais(string Conn)
        {
            string strsql = "SELECT CAST(CF.IDFILIAL AS NVARCHAR(20))  VALOR , NOME + ' Unid.:' +  CAST(ISNULL(UNIDADE, '') AS NVARCHAR(20)) NOME FROM FILIAL INNER JOIN CLIENTEFILIAL CF ON CF.IDFILIAL = FILIAL.IDFILIAL WHERE FILIAL.ATIVO='SIM' AND IDCLIENTE in (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ORDER BY 2";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn); //return SistranDb.ExecuteDataSet(CommandType.Text, strsql).Tables[0];
        }

        public DataTable ListarTodasFiliais(string Conn)
        {
            string strsql = "SELECT CAST(IDFILIAL AS NVARCHAR(20))  VALOR , NOME FROM FILIAL WHERE ATIVO='SIM' ORDER BY 2";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn); //return SistranDb.ExecuteDataSet(CommandType.Text, strsql).Tables[0];
        }
        
        public DataTable ListarFilialPadraoInternet( string clientes)
        {
            string strsql = " SELECT FIL.NOME, FIL.IDFILIAL ";
            strsql += " FROM CLIENTE CLI ";
            strsql += " INNER JOIN FILIAL FIL ON FIL.IDFILIAL = CLI.IDFILIALPADRAOINTERNET ";
            strsql += " WHERE IDCLIENTE in("+clientes+")  ";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), ""); 
        }

        public DataTable ListarSelecionadosByIDMotorista(string Conn, int idMotorista)
        {
            string strsql = "SELECT MF.IDFILIAL, NOME  FROM MOTORISTAFILIAL MF INNER JOIN FILIAL F ON F.IDFILIAL = MF.IDFILIAL  WHERE IDMOTORISTA = " + idMotorista.ToString();
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public DataTable ListarDisponiveisByIDMotorista(string Conn, int idMotorista, bool captacao)
        {
            if (captacao == false)
            {
                string strsql = "SELECT IDFILIAL,  NOME FROM FILIAL WHERE IDFILIAL NOT IN (SELECT IDFILIAL FROM MOTORISTAFILIAL WHERE IDMOTORISTA=" + idMotorista + ") ORDER BY NOME";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
            }
            else
            {
                string strsql = "SELECT F.IDFILIAL, APELIDOFILIAL NOME FROM FILIAL F INNER JOIN HABILITAFILIALMOTORISTA HFM ON HFM.IDFILIAL = F.IDFILIAL ORDER BY NOME ";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);

            }
        }       
    }
}
