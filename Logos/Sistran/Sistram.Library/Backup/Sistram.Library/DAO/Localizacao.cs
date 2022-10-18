using System.Data;

namespace SistranDAO
{
    public class Localizacao
    {
        public class Cidade
        {
            public DataTable Read(int IdCidade)
            {
                string strsql = " SELECT IDCIDADE, IDESTADO, NOME, CEP, TIPO, CODIFICARPOR,REGIAO, PRAZODEENTREGAPADRAO, CODIGODOIBGE, CODIGODIPAM FROM CIDADE WHERE IDCIDADE =" + IdCidade.ToString();
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
            }

            public DataTable ReadbyIdEstado(int IdEstado)
            {
                string strsql = " SELECT IDCIDADE, IDESTADO, NOME, CEP, TIPO, CODIFICARPOR,REGIAO, PRAZODEENTREGAPADRAO, CODIGODOIBGE, CODIGODIPAM FROM CIDADE WHERE IdEstado =" + IdEstado.ToString();
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
            }
        }

        public class Estado
        {
            public DataTable Read(int IdEstado)
            {
                string strsql = " SELECT IDESTADO, IDPAIS, UF, NOME, CODIGODOIBGE FROM Estado WHERE IDESTADO =" + IdEstado.ToString();
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
            }

            public DataTable Listar()
            {
                string strsql = " SELECT IDESTADO, IDPAIS, UF, NOME, CODIGODOIBGE FROM Estado ORDER BY NOME ";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql);

            }
           
        }

        public class Bairro
        {
            public DataTable Read(int IdCidade)
            {
                string strsql = " SELECT IDBAIRRO, NOME FROM BAIRRO WHERE IDCIDADE =" + IdCidade.ToString();
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
            }

        }
    }
}