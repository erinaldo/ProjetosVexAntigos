using System.Text;
using System.Data;
using System.Web;
using System;

namespace SistranDAO
{
    public sealed class Projeto
    {

        public DataTable Filtrar(int? IDPROJETO)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" SELECT P.IDPROJETO , P.NOME PROJETO , FL.NOME FILIAL, * FROM PROJETO P ");
            //strsql.Append(" LEFT JOIN PROJETOITEM PIT ON PIT.IDPROJETO = P.IDPROJETO ");
            //strsql.Append(" LEFT JOIN PROJETOPRODUCAO PP ON PP.IDPROJETO = P.IDPROJETO ");
            strsql.Append(" INNER JOIN FILIAL FL ON FL.IDFILIAL = P.IDFILIAL ");
            strsql.Append((IDPROJETO == null ? "" : " AND P.IDPROJETO=" + IDPROJETO.ToString()));
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }
        
        public int InserirAlterar(
                                    int IDPROJETO,
                                    int IDFILIAL,
                                    string NOME,
                                    string CONTATOCLIENTE,
                                    string CONTATOCONTRATADO,
                                    string UTILIZAAREACLIMATIZADA,
                                    DateTime INICIODAPRODUCAO,
                                    DateTime FINALDAPRODUCAO,
                                    DateTime INICIODAENTREGA,
                                    DateTime FINALDAENTREGA,
                                    int TOTALDEKITS,
                                    int FATORPORCAIXA,
                                    int FATORPORPALLET,
                                    decimal PESOPORKIT,
                                    decimal FRETEPORKIT,
                                    decimal TEMPODEPRODUCAO,
                                    int TURNOS,
                                    int PESSOASPORTURNO,
                                    int MAODEOBRA,
                                    string STATUS,
                                    DataTable Itens, DataTable Producao
                            )
        {
            try
            {
                string ID = "0";
                string strsql = "";

                if (IDPROJETO == 0)
                {
                    ID = Sistran.Library.GetDataTables.RetornarIdTabela("PROJETO");
                    strsql += " INSERT INTO PROJETO ";
                    strsql += " ( ";
                    strsql += " IDPROJETO, ";
                    strsql += " IDFILIAL, ";
                    strsql += " NOME, ";
                    strsql += " CONTATOCLIENTE, ";
                    strsql += " CONTATOCONTRATADO, ";
                    strsql += " UTILIZAAREACLIMATIZADA, ";
                    strsql += " INICIODAPRODUCAO, ";
                    strsql += " FINALDAPRODUCAO, ";
                    strsql += " INICIODAENTREGA, ";
                    strsql += " FINALDAENTREGA, ";
                    strsql += " TOTALDEKITS, ";
                    strsql += " FATORPORCAIXA, ";
                    strsql += " FATORPORPALLET, ";
                    strsql += " PESOPORKIT, ";
                    strsql += " FRETEPORKIT, ";
                    strsql += " TEMPODEPRODUCAO, ";
                    strsql += " TURNOS, ";
                    strsql += " PESSOASPORTURNO, ";
                    strsql += " MAODEOBRA, ";
                    strsql += " STATUS ";
                    strsql += " ) ";
                    strsql += " VALUES ";
                    strsql += " ( ";
                    strsql += ID + " , ";
                    strsql += IDFILIAL + ", ";
                    strsql += " '" + NOME + "', ";
                    strsql += " '" + CONTATOCLIENTE + "', ";
                    strsql += " '" + CONTATOCONTRATADO + "', ";
                    strsql += " '" + UTILIZAAREACLIMATIZADA + "', ";
                    strsql += " CONVERT(DATETIME,'" + INICIODAPRODUCAO + "',103), ";
                    strsql += " CONVERT(DATETIME,'" + FINALDAPRODUCAO + "',103), ";
                    strsql += " CONVERT(DATETIME,'" + INICIODAENTREGA + "',103), ";
                    strsql += " CONVERT(DATETIME,'" + FINALDAENTREGA + "',103), ";
                    strsql += TOTALDEKITS + ", ";
                    strsql += FATORPORCAIXA.ToString().Replace(",", ".") + ", ";
                    strsql += FATORPORPALLET.ToString().Replace(",", ".") + ", ";
                    strsql += PESOPORKIT.ToString().Replace(",", ".") + ", ";
                    strsql += FRETEPORKIT.ToString().Replace(",", ".") + ", ";
                    strsql += TEMPODEPRODUCAO.ToString().Replace(",", ".") + ", ";
                    strsql += TURNOS + ", ";
                    strsql += PESSOASPORTURNO.ToString().Replace(",", ".") + ", ";
                    strsql += MAODEOBRA + ", ";
                    strsql += " '" + STATUS + "' ";
                    strsql += " ) ";
                }
                else
                {
                    ID = IDPROJETO.ToString();
                    strsql += "UPDATE PROJETO SET ";
                    strsql += " IDFILIAL= "+  IDFILIAL + ", ";
                    strsql += " NOME= '" + NOME + "', ";
                    strsql += " CONTATOCLIENTE= '" + CONTATOCLIENTE + "', ";
                    strsql += " CONTATOCONTRATADO= '" + CONTATOCONTRATADO + "', ";
                    strsql += " UTILIZAAREACLIMATIZADA='" + UTILIZAAREACLIMATIZADA + "' , ";
                    strsql += " INICIODAPRODUCAO= CONVERT(DATETIME,'" + INICIODAPRODUCAO + "',103), ";
                    strsql += " FINALDAPRODUCAO= CONVERT(DATETIME,'" + FINALDAPRODUCAO + "',103), ";
                    strsql += " INICIODAENTREGA= CONVERT(DATETIME,'" + INICIODAENTREGA + "',103), ";
                    strsql += " FINALDAENTREGA= CONVERT(DATETIME,'" + FINALDAENTREGA + "',103), ";
                    strsql += " TOTALDEKITS=" + TOTALDEKITS + " , ";
                    strsql += " FATORPORCAIXA="+  FATORPORCAIXA.ToString().Replace(",", ".") +", ";
                    strsql += " FATORPORPALLET="+ FATORPORPALLET.ToString().Replace(",", ".") + " , ";
                    strsql += " PESOPORKIT="+ PESOPORKIT.ToString().Replace(",", ".") + ", ";
                    strsql += " FRETEPORKIT="+FRETEPORKIT.ToString().Replace(",", ".") + " , ";
                    strsql += " TEMPODEPRODUCAO="+  TEMPODEPRODUCAO.ToString().Replace(",", ".") + ", ";
                    strsql += " TURNOS="+ TURNOS + " , ";
                    strsql += " PESSOASPORTURNO="+PESSOASPORTURNO.ToString().Replace(",", ".") + " , ";
                    strsql += " MAODEOBRA="+ MAODEOBRA + ", ";
                    strsql += " STATUS='"+STATUS+"' ";
                    strsql += " WHERE IDPROJETO = " + ID;
                }
                Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");

                //itens
                Sistran.Library.GetDataTables.ExecutarSemRetorno("DELETE FROM PROJETOITEM WHERE IDPROJETO=" + ID, "");

                for (int i = 0; i < Itens.Rows.Count; i++)
                {
                   string  IDitem = Sistran.Library.GetDataTables.RetornarIdTabela("PROJETOITEM");
                    strsql = "";
                    strsql += " INSERT INTO PROJETOITEM ";
                    strsql += " ( ";
                    strsql += " IDPROJETOITEM,  ";
                    strsql += " IDPROJETO,  ";
                    strsql += " CODIGO,  ";
                    strsql += " DESCRICAO, ";
                    strsql += " QUANTIDADE, ";
                    strsql += " QUANTIDADERECEBIDA, ";
                    strsql += " ULTIMORECEBIMENTO ";
                    strsql += " ) ";
                    strsql += " VALUES ";
                    strsql += " ( ";
                    strsql += IDitem + " ,  ";
                    strsql += ID+" ,  ";
                    strsql += " '" + Itens.Rows[i]["CODIGO"].ToString() + "',  ";
                    strsql += " '" + Itens.Rows[i]["DESCRICAO"].ToString() + "' , ";
                    strsql += Itens.Rows[i]["QUANTIDADE"].ToString().Replace(",", ".") + " , ";
                    strsql += Itens.Rows[i]["QUANTIDADERECEBIDA"].ToString().Replace(",",".") + " , ";
                    strsql += "CONVERT(DATETIME,'" + Itens.Rows[i]["ULTIMORECEBIMENTO"].ToString() + "',103)  ";
                    strsql += " ) ";
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");

                }

                //Sistran.Library.GetDataTables.ExecutarSemRetorno("DELETE FROM PROJETOPRODUCAO WHERE IDPROJETO=" + ID, "");

                for (int i = 0; i < Producao.Rows.Count; i++)
                {
                    if (Producao.Rows[i]["IDPROJETOPRODUCAO"].ToString() == "0")
                    {

                        string IDitem = Sistran.Library.GetDataTables.RetornarIdTabela("PROJETOPRODUCAO");

                        strsql = "";
                        strsql += "  INSERT INTO ProjetoProducao ";
                        strsql += " VALUES ";
                        strsql += " ( ";
                        strsql +=  IDitem + " , ";
                        strsql += ID+ " , ";
                        strsql += " getdate(), ";
                        strsql += Producao.Rows[i]["Turno"].ToString() + " , ";
                        strsql += Producao.Rows[i]["IDUSUARIO"].ToString() + ", ";
                        strsql += "CONVERT(datetime, '" + Producao.Rows[i]["HoraInicial"].ToString() + "',103) ,";
                        strsql += "CONVERT(datetime, '" + Producao.Rows[i]["HoraFinal"].ToString() + "',103) ,";
                        strsql += Producao.Rows[i]["MaoDeObra"].ToString() + ", ";
                        strsql += Producao.Rows[i]["QuantidadeEfetuada"].ToString();
                        strsql += " ) ";
                        Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");


                    }
                }
                


                return int.Parse(ID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}