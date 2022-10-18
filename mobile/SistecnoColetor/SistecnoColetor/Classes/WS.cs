using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SistecnoColetor.Classes
{
    public class WS
    {
        public string StringConexao(string idEmpresa)
        {
            string senhaws = DateTime.Now.Year.ToString() +
                                DateTime.Now.Month.ToString() +
                                DateTime.Now.Day.ToString() +
                                DateTime.Now.Hour.ToString();


            wsLogin.Login w = new SistecnoColetor.wsLogin.Login();
            return w.RetornaConexao(idEmpresa, senhaws);
        }

        public string InternetAtiva()
        {
            wsLogin.Login w = new SistecnoColetor.wsLogin.Login();
            return w.EstaAtivo();
        }

        public DataSet ResgatarDocumento(string chave)
        {
            try
            {
                br.com.grupologos.www.ReposcicaoRoge w = new SistecnoColetor.br.com.grupologos.www.ReposcicaoRoge();
                DataSet ds = w.ResgatarDocumentoPeloColetor(chave, VarGlobal.Usuario.Login);
                
                //insere no banco
                string sql = "";

                List<string> l = new List<string>();
                foreach (DataRow item in ds.Tables[0].Rows) //Tabela de ReposicaoRoge
                {
                    sql += " INSERT INTO REPOSICAOROGE(IdReposicaoRoge, Chave, CodigoRegiao, NomeRegiao) ";
                    sql += " VALUES (" + item["IDREPOSICAOROGE"].ToString() + ", '" + item["CHAVE"].ToString() + "', " + item["CODIGOREGIAO"].ToString() + ", '" + item["NOMEREGIAO"].ToString() + "') ; ";

                    l.Add(sql);

                }


                    foreach (DataRow itemVol in ds.Tables[1].Rows) //Tabela de ReposicaoVolume
                    {
                        sql = "INSERT INTO REPOSICAOROGEVOLUME (IDREPOSICAOROGEVOLUME,  IdReposicaoRoge, CODIGODEBARRAS, CONFERIDO) ";
                        sql += "VALUES (" + itemVol["IDREPOSICAOROGEVOLUME"].ToString() + ",  " + itemVol["IdResposicaoRoge"].ToString() + ",'" + itemVol["codigodebarras"].ToString() + "', 'NAO')";
                        l.Add(sql);
                    }

                    foreach (DataRow itemItem in ds.Tables[2].Rows) //Tabela de ReposicaoItem
                    {
                        sql = "INSERT INTO ReposicaoRogeItem (IdReposicaoRogeItem,  IdReposicaoRoge, CodigoRoge,PertenceANota ) ";
                        sql += "VALUES (" + itemItem["IdReposicaoRogeItem"].ToString() + ",  " + itemItem["IDREPOSICAOROGE"].ToString() + ", " + itemItem["CodigoRoge"].ToString() + ", 'SIM')";
                        l.Add(sql);

                       

                    }

                    foreach (DataRow itemCB in ds.Tables[3].Rows) //Tabela de ReposicaoCB
                    {
                        sql = "INSERT INTO ReposicaoRogeCB (IDReposicaoRogeCB,  CodigoDeBarras, IdReposicaoRogeItem, Embalagem, QuantidadeEmbalagem) ";
                        sql += " VALUES(" + itemCB["IDReposicaoRogeCB"].ToString() + ",  '" + itemCB["CodigoDeBarras"].ToString() + "', " + itemCB["IdReposicaoRogeItem"].ToString() + ", '" + itemCB["Embalagem"].ToString() + "', " + int.Parse(itemCB["QUANTIDADE"].ToString()).ToString() + ")";
                        l.Add(sql);

                    }
                

                if (l.Count == 0)
                    throw new Exception("Documento Não Encontrado");

                Classes.BbColetor.excSql_trans(l);


                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}