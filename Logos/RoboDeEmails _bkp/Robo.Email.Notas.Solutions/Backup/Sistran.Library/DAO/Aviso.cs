using System.Text;
using System.Data;
using System.Web;

namespace SistranDAO
{
    public sealed class Aviso
    {
        public void ApagarAviso(string IdAviso)
        {
            Sistran.Library.GetDataTables.ExecutarSemRetorno("DELETE FROM AVISO WHERE IDAVISO="+ IdAviso,"");            
        }

        public DataTable Listar(string Nome, string Login, string Operacao, string IdAviso)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT A.IdAviso, U.IDUSUARIO, U.NOME, U.LOGIN, A.OPERACAO, CD.NOME DIVISAO, CD.IDClienteDivisao ,UCV.Nome  'REPRESENTANTE', UCV.IDUsuario IDREPRESENTANTE FROM USUARIO U ");
            strsql.Append(" INNER JOIN AVISO A ON A.IDUSUARIO = U.IDUSUARIO ");
            strsql.Append(" LEFT JOIN CLIENTEDIVISAO CD ON CD.IDCLIENTEDIVISAO = A.IDCLIENTEDIVISAO   ");
            strsql.Append(" LEFT JOIN CANALDEVENDA CV  ON CV.IDCANALDEVENDA = A.IDCANALDEVENDA ");
            strsql.Append(" LEFT JOIN USUARIO UCV ON UCV.IDUSUARIO = CV.IDREPRESENTANTE ");
            strsql.Append(" WHERE U.ATIVO ='SIM' ");
            strsql.Append(" AND CD.IDCLIENTE=" + HttpContext.Current.Session["IDEmpresa"]);

            if (Nome.Trim() != "")
            {
                strsql.Append(" AND U.NOME LIKE '" + Nome + "%' ");
            }

            if (Login.Trim() != "")
            {
                strsql.Append(" AND U.LOGIN LIKE '" + Login + "%' ");
            }

            if (Operacao.Trim() != "")
            {
                strsql.Append(" AND A.OPERACAO LIKE '" + Operacao + "%' ");
            }

            if (IdAviso.Trim() != "")
            {
                strsql.Append(" AND A.IdAviso= " + IdAviso);
            }



            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public int Inserir(string Operacao, string IdClienteDivisao, string IdCanalDeVenda, string IdUsuario)
        {
            string m = " INSERT INTO Aviso ( ";
            m += " IdAviso, ";
            m += " IdUsuario, ";
            m += "IdCliente, ";


            if (IdClienteDivisao != "" && IdClienteDivisao != "0")
                m += "IdClienteDivisao, ";
            
            m += "Operacao ";

            if (IdCanalDeVenda != "" && IdCanalDeVenda != "0")
                m += ", IdCanalDeVenda ";
            m += ") VALUES ( ";
//            m += "(SELECT ISNULL(MAX(IdAviso),0) + 1 FROM Aviso), ";
            m += Sistran.Library.GetDataTables.RetornarIdTabela("AVISO").ToString() + ", ";
            m += IdUsuario + ", ";
            m += HttpContext.Current.Session["IDEmpresa"].ToString() + ", ";

            if (IdClienteDivisao != "" && IdClienteDivisao != "0")
                m += IdClienteDivisao + ", ";
            
            
            m += "'" + Operacao.ToUpper() + "' ";

            if (IdCanalDeVenda != "" && IdCanalDeVenda != "0")
                m += "," + IdCanalDeVenda;
            
            m += ") ";
            return Sistran.Library.GetDataTables.ExecutarRetornoID(m, "");

        }

        public void Alterar(string Operacao, string IdClienteDivisao, string IdCanalDeVenda, string IdUsuario, string idAviso)
        {
            string m = " UPDATE AVISO SET  ";
            m += "IDUSUARIO=" + IdUsuario + ",";
            m += "IDCLIENTE=" + HttpContext.Current.Session["IDEmpresa"].ToString() + ",";

            if (IdClienteDivisao != "" && IdClienteDivisao != "0")
                m += "IDCLIENTEDIVISAO=" + IdClienteDivisao + ",";

            m += "OPERACAO= '" + Operacao.ToUpper() + "'";


            if (IdCanalDeVenda != "" && IdCanalDeVenda != "0")
                   m += ", IDCANALDEVENDA=" + IdCanalDeVenda;


            m += "WHERE IDAVISO=" + idAviso;
            Sistran.Library.GetDataTables.ExecutarSemRetorno(m, "");
        }

        public int VerificarDuplicidade(string idUsuario, string idClienteDivisao, string Operacao, string IdAviso)
        {
            if (IdAviso == "")
                IdAviso = "0";

            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT COUNT(*) FROM AVISO WHERE  IDUSUARIO=" + idUsuario);
            
            if (idClienteDivisao != "" && idClienteDivisao != "0")
            {
                strsql.Append(" AND IDCLIENTEDIVISAO=" + idClienteDivisao);
            }

            strsql.Append(" AND OPERACAO='" + Operacao + "'");
            strsql.Append(" AND IDAVISO<>" + IdAviso);
            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql.ToString(), "");
        }

        public sealed class CanalDeVenda
        {
            public DataTable Listar()
            {
                StringBuilder strsql = new StringBuilder();
                strsql.Append(" SELECT USUARIO.NOME, USUARIO.IDUSUARIO  FROM CANALDEVENDA ");
                strsql.Append(" INNER JOIN USUARIO ON USUARIO.IDUSUARIO = CANALDEVENDA.IDREPRESENTANTE ");
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
            }
        }
    }
}