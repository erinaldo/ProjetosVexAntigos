using System.Text;
using System.Data;


namespace SistranDAO
{
    public class Destinatario
    {
        public DataTable ListarDestinatario()
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("  SELECT IDCADASTRO, RAZAOSOCIALNOME + ' - ' + CNPJCPF 'NOME', ");
            strsql.Append("  CNPJCPF, ");
            strsql.Append("  INSCRICAORG, ");
            strsql.Append("  RAZAOSOCIALNOME, ");
            strsql.Append("  FANTASIAAPELIDO ");
            strsql.Append("  FROM CADASTRO ");
            strsql.Append("  ORDER BY RAZAOSOCIALNOME ");            
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(),"");            
        }
        
        public DataTable ListarDestinatario(string filtro)
        {

            StringBuilder strsql = new StringBuilder();
            strsql.Append("  SELECT TOP 20 IDCADASTRO, RAZAOSOCIALNOME + ' - ' + CNPJCPF 'NOME', ");
            strsql.Append("  CNPJCPF, ");
            strsql.Append("  INSCRICAORG, ");
            strsql.Append("  RAZAOSOCIALNOME, ");
            strsql.Append("  FANTASIAAPELIDO ");
            strsql.Append("  FROM CADASTRO ");
            strsql.Append("  WHERE RAZAOSOCIALNOME LIKE '"+filtro+"%' or cnpjcpf like '%"+ filtro +"%' ");
            strsql.Append("  ORDER BY RAZAOSOCIALNOME ");
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public DataTable ConsultarDadosDestinatario(string idCadastro)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("  SELECT ");
            strsql.Append("  RazaoSocialNome, CNPJCPF, InscricaoRG, UF, ");
            strsql.Append("  FantasiaApelido,");
            strsql.Append("  Endereco,");
            strsql.Append("  Numero,");
            strsql.Append("  Complemento,");
            strsql.Append("  IDBairro,");
            strsql.Append("  CAD.CEP , CID.NOME, CID.IDCIDADE");
            strsql.Append("  FROM CADASTRO CAD");
            strsql.Append("  LEFT JOIN Cidade CID ON  CID.IDCidade = CAD.IDCidade  left JOIN Estado ON Estado.IDEstado = CID.IDCidade");
            strsql.Append("  WHERE CAD.IDCadastro = " + idCadastro.ToString());
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");            

        }
    }
}