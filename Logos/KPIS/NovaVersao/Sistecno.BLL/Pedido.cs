using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistecno.DAL;
using Sistecno.DAL.BD;


namespace Sistecno.BLL
{
    public class Pedido
    {

        public DataTable RetornarProdutosPrimeiraVez(int idUsuario, int IdCliente, string cnx)
        {
            return new Sistecno.DAL.Pedido().RetornarProdutosPrimeiraVez(idUsuario, IdCliente, cnx);
        }


        public DataTable RetornarProdutosDivisoesGrupodeProduto(int idUsuario, int IdCliente, bool considerarDivisoes, string idClienteDivisao, string cnx)
        {
            return new Sistecno.DAL.Pedido().RetornarProdutos(idUsuario, IdCliente, considerarDivisoes, idClienteDivisao, cnx);
        }

        public decimal RetornarSaldoDaDivisao(string idProdutoCliente, string idClienteDivisao, string cnx)
        {
            return new Sistecno.DAL.Pedido().RetornarSaldoDaDivisao(idProdutoCliente, idClienteDivisao, cnx);
        }

        public DataTable RetornarProdutos(int idUsuario, int IdCliente, bool ConsiderarDivisao, string IdClienteDivisao, string cnx)
        {
            return new Sistecno.DAL.Pedido().RetornarProdutos(idUsuario, IdCliente, ConsiderarDivisao, IdClienteDivisao, cnx);
        }

        public static string RetornarNumerador(int idfilial, string ChaveNome, string serie, string cnx)
        {
            string sqlstr = "SELECT * FROM NUMERADOR WHERE IDFILIAL=" + idfilial + " AND NOME='" + ChaveNome + "'";
            DataTable dt = DAL.BD.cDb.RetornarDataTable(sqlstr, cnx);

            string id = "";
            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0]["IDNUMERADOR"].ToString();
            }

            if (id == "")
            {
                id = DAL.BD.cDb.RetornarIDTabela(cnx, "NUMERADOR").ToString();
                sqlstr = "INSERT INTO NUMERADOR VALUES (" + id + ",NULL," + idfilial + ",'" + ChaveNome + "', '" + serie + "', 1); SELECT PROXIMONUMERO FROM NUMERADOR WHERE IDNUMERADOR =  " + id;

            }
            else
            {
                sqlstr = "UPDATE NUMERADOR SET PROXIMONUMERO=PROXIMONUMERO+1 WHERE IDNUMERADOR =  " + id + "; SELECT PROXIMONUMERO FROM NUMERADOR WHERE IDNUMERADOR =  " + id;

            }

            return DAL.BD.cDb.ExecutarRetornoIDs(sqlstr, cnx);

        }



        public int GravarPedido(int idfilial, int idCliente, DataTable Destinatario, int idRemetente, List<Carrinho> lProd, int idUsuario, string cnx)
        {
             SqlConnection conn = new SqlConnection(cnx);
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

            try
            {

                string numeroPedido = RetornarNumerador(idfilial, "PEDIDO", "PED", cnx);
                
                #region Documento
                int IDDoc = cDb.RetornarIDTabela(cnx, "Documento");

                System.Text.StringBuilder s = new System.Text.StringBuilder();
                s.Append(" INSERT INTO DOCUMENTO (IDDocumento, ");
                s.Append(" IDFilial, ");
                s.Append(" IDFilialAtual, ");
                s.Append(" TipoDeDocumento, ");
                s.Append(" Serie, ");
                s.Append(" Numero, ");
                s.Append(" IDCliente, ");
                s.Append(" IDRemetente, ");
                s.Append(" IDDestinatario, ");
                s.Append(" PesoBruto, ");
                s.Append(" MetragemCubica, ");
                s.Append(" Volumes, ");
                s.Append(" AnoMes, ");
                s.Append(" ValorDaNota,");
                s.Append(" tipodeservico, ");
                s.Append(" numerooriginal, ");
                s.Append(" origem, ");
                s.Append(" entradaSaida, ");
                s.Append(" datadeemissao, ");
                s.Append(" datadeentrada, ");
                s.Append(" valordasmercadorias, ");
                s.Append(" endereco, ");
                s.Append(" endereconumero, ");
                s.Append(" enderecocomplemento, ");
                s.Append(" idenderecobairro, ");
                s.Append(" idenderecocidade, ");
                s.Append(" enderecoCep, IDUSUARIO )");
                s.Append(" VALUES ( ");
                s.Append(IDDoc + ",");
                s.Append(idfilial + " , ");
                s.Append(idfilial + ", ");
                s.Append(" 'PEDIDO', ");
                s.Append("'PED' , ");
                s.Append(numeroPedido.ToString() + " , ");
                s.Append(idCliente + ", ");

                s.Append(idRemetente + ", ");
                s.Append(Destinatario.Rows[0]["IDCADASTRO"].ToString() + ", ");

                decimal tot = 0;
                decimal tpeso = 0;
                decimal tm3 = 0;
                for (int i = 0; i < lProd.Count; i++)
                {
                    tot += (lProd[i].Quantidade * lProd[i].ValorUnitario);
                    tpeso += lProd[i].Peso * lProd[i].Quantidade;
                    tm3 += lProd[i].M3 * lProd[i].Quantidade;
                }


                s.Append(tpeso.ToString().Replace(",", ".") + ", ");
                s.Append(tm3.ToString().Replace(",", ".") + ", ");
                s.Append("0 , ");
                s.Append("'" + DateTime.Now.Year.ToString() + "/" + (DateTime.Now.Month < 10? "0"+DateTime.Now.Month.ToString():DateTime.Now.Month.ToString()) + "',");

               

                s.Append(tot.ToString().Replace(",", ".") + ",");

                s.Append(" 'NORMAL', ");
                s.Append(numeroPedido + " , ");
                s.Append("'INTERNET', ");
                s.Append(" 'SAIDA', ");
                s.Append(" GETDATE(), ");
                s.Append(" GETDATE(), ");
                s.Append(Convert.ToDouble(tot).ToString().Replace(",", ".") + " , ");

                s.Append("'" + Destinatario.Rows[0]["Endereco"].ToString() + "', ");
                s.Append("'" + Destinatario.Rows[0]["Numero"].ToString() + "', ");
                s.Append("'" + Destinatario.Rows[0]["Complemento"].ToString() + "', ");
                s.Append(" NULL , ");
                s.Append((Destinatario.Rows[0]["IDCIDADE"].ToString() == "" ? "0" : Destinatario.Rows[0]["IDCIDADE"].ToString()) + ", ");
                s.Append("'" + Destinatario.Rows[0]["CEP"].ToString() + "', " + idUsuario + " )  ");

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = s.ToString();
                cmd.ExecuteNonQuery();

                #endregion

                #region DocumentoItem

                foreach (var item in lProd)
                {
                    int ID = cDb.RetornarIDTabela(cnx, "DOCUMENTOITEM");
                     s = new System.Text.StringBuilder();
                    s.Append(" INSERT INTO  DOCUMENTOITEM (IDDOCUMENTOITEM, IDDOCUMENTO, IDPRODUTOEMBALAGEM,IDUSUARIO, IDCLIENTEDIVISAO, QUANTIDADE, VALORUNITARIO,  ");
                    s.Append(" QUANTIDADEUNIDADEESTOQUE, SALDO, VALORTOTALDOITEM, IdUnidadeDeArmazenagemLote, REFERENCIA, IDPRODUTOCLIENTE) VALUES(");
                    s.Append(ID + " , ");
                    s.Append(IDDoc.ToString() + ",");
                    s.Append(item.IdProdutoEmbalagem.ToString() + ", ");
                    s.Append(idUsuario.ToString() + ", ");
                    s.Append((item.IdClienteDivisao.ToString() == "0" ? "NULL" : item.IdClienteDivisao.ToString()) + ",  ");
                    s.Append(item.Quantidade.ToString() + ", ");
                    s.Append(item.ValorUnitario.ToString().ToString().Replace(",", ".") + ",  ");
                    s.Append(item.Quantidade.ToString() + ", ");
                    s.Append(item.Quantidade.ToString() + ", ");
                    s.Append((item.Quantidade * item.ValorUnitario).ToString().Replace(",", "."));
                    s.Append(", " + (item.IdUnidadeDeArmazenagemLote.ToString() == "" ? "NULL" : item.IdUnidadeDeArmazenagemLote.ToString()));
                    s.Append(", NULL, "+item.IdProdutoCliente+" )");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Transaction = trans;
                    cmd.CommandText = s.ToString();
                    cmd.ExecuteNonQuery();
                }


                #endregion

                #region Documento Filial
                int IDS = DAL.BD.cDb.RetornarIDTabela(cnx, "DOCUMENTOFILIAL");
                s = new System.Text.StringBuilder();

                s.Append(" INSERT INTO DOCUMENTOFILIAL");
                s.Append(" (IDDOCUMENTOFILIAL, ");
                s.Append(" IDDOCUMENTO, ");
                s.Append(" IDFILIAL,");
                s.Append(" IDREGIAOITEM,");
                s.Append(" SITUACAO,");
                s.Append(" DATA)");
                s.Append(" VALUES(");
                s.Append(IDS + " , ");
                s.Append(IDDoc + ", ");
                s.Append(idfilial + ",");
                s.Append("0,");
                s.Append("'LIBERADO PARA SEPARACAO',");
                s.Append(" GETDATE())");
                
                cmd.CommandType = CommandType.Text;                
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandText = s.ToString();
                cmd.ExecuteNonQuery();

                #endregion

                trans.Commit();
                return int.Parse(numeroPedido);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return 0;
            }
        }

        public DataTable ConsultarProdutoComFoto(string idClienteDivisao, string idCliente, string desc, string cnx)
        {
            return new Sistecno.DAL.Pedido().ConsultarProdutoComFoto(idClienteDivisao,idCliente, desc,  cnx);
        }

        public DataTable RetonarRelatorioEstoque(int idCliente, string pesq, string cnx)
        {
            return new Sistecno.DAL.Pedido().RetonarRelatorioEstoque(idCliente, pesq, cnx);
        }
    }
}