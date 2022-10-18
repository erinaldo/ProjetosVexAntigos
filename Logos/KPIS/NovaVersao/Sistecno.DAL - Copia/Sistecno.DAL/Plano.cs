using System.Data;
namespace Sistecno.DAL
{
    public class Plano
    {
        public int IdPlano { get; set; }
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Ativo { get; set; }
        public int NumeroDocumentos { get; set; }
        public string CnpjCliente { get; set; }
        public string NomeCliente { get; set; }

        public Plano()
        {
        }

        public Plano(int idplano, string nome, string ativo, int numeroDocumentos, int idCliente, string cnpj, string nomeCliente)
        {
            IdPlano = idplano;
            Nome = nome;
            Ativo = ativo;
            NumeroDocumentos = numeroDocumentos;
            IdCliente = idCliente;
            NomeCliente = nomeCliente;
            CnpjCliente = cnpj;
        }
        
        public Plano Retornar(int idcliente, string cnx)
        {
            string strsql = " SELECT * FROM PLANO P LEFT JOIN CLIENTE PC ON PC.IDPLANO = P.IDPLANO LEFT JOIN CADASTRO CDC ON CDC.IDCADASTRO = PC.IDCLIENTE WHERE PC.IDCLIENTE =  " + idcliente;
            DataTable dt = DAL.BD.cDb.RetornarDataTable(strsql, cnx);
            Plano p = null;



            if (dt.Rows.Count > 0)
                p = new Plano(int.Parse(dt.Rows[0]["IDPLANO"].ToString()), dt.Rows[0]["NOME"].ToString(), dt.Rows[0]["ATIVO"].ToString(), int.Parse(dt.Rows[0]["NumeroDocumentos"].ToString()), int.Parse(dt.Rows[0]["IDCLIENTE"].ToString()), dt.Rows[0]["CNPJCPF"].ToString(), dt.Rows[0]["RazaoSocialNome"].ToString());

            return p;
        }

        public Plano RetornarById(int idPlano, string cnx)
        {
            string strsql = " SELECT * FROM PLANO P WHERE P.IDPLANO =  " + idPlano;
            DataTable dt = DAL.BD.cDb.RetornarDataTable(strsql, cnx);
            Plano p = null;



            if (dt.Rows.Count > 0)
                p = new Plano(int.Parse(dt.Rows[0]["IDPLANO"].ToString()), dt.Rows[0]["NOME"].ToString(), dt.Rows[0]["ATIVO"].ToString(), int.Parse(dt.Rows[0]["NumeroDocumentos"].ToString()), 0, "", "");

            return p;
        }

    }
}
