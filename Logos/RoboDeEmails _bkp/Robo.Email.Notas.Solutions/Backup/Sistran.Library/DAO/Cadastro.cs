using System.Data;
using System.Web;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;



namespace SistranDAO
{
    public class Cadastro
    {
        public DataTable Read(string condicoes)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append(" SELECT IDCADASTRO,CNPJCPF,INSCRICAORG,RAZAOSOCIALNOME,FANTASIAAPELIDO,ENDERECO,NUMERO,COMPLEMENTO,IDCIDADE,IDBAIRRO,CEP,CNPJCPFERRADO,INSCRICAOERRADA,DATADECADASTRO,CEPVALIDO,ANIVERSARIO,SUFRAMA,SUFRAMAVALIDADE,ORGAOEMISSOR,TIPODECADASTRO,SITUACAOFISCAL FROM CADASTRO WHERE " + condicoes);
            return Sistran.Library.GetDataTables.RetornarDataTable(s.ToString(), "");
        }

        public int TransacaoInserirCadastroMotorista(string Conn, SistranMODEL.Cadastro oCad, SistranMODEL.Cadastro.Motorista oMot, SistranMODEL.Cadastro.Motorista.MotoristaHistorico oMotHst)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

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
            cn.Open();
            DbTransaction oTrans;
            oTrans = cn.BeginTransaction();

            try
            {
                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTRO");
                string sql = " INSERT INTO CADASTRO ";
                sql += " ( ";
                sql += " IDCADASTRO, ";
                sql += " CNPJCPF, ";
                sql += " INSCRICAORG, ";
                sql += " RAZAOSOCIALNOME, ";
                sql += " FANTASIAAPELIDO, ";
                sql += " ENDERECO, ";
                sql += " NUMERO, ";
                sql += " COMPLEMENTO, ";
                sql += " IDCIDADE, ";
                sql += " IDBAIRRO, ";
                sql += " CEP, ";
                sql += " DATADECADASTRO ";
                sql += " ) ";
                sql += " VALUES ";
                sql += " ( ";
                sql += ID + " , ";
                sql += " '" + oCad.CnpjCpf + "', ";
                sql += " '" + oCad.InscricaoRG + "', ";
                sql += " '" + oCad.RazaoSocialNome.ToUpper() + "', ";
                sql += " '" + oCad.RazaoSocialNome.ToUpper() + "', ";
                sql += " '" + oCad.Endereco.ToUpper() + "', ";
                sql += " '" + oCad.Numero.ToUpper() + "', ";
                sql += " '" + oCad.Complemento.ToUpper() + "', ";
                sql += oCad.IDCidade.ToString() + " , ";
                sql += " NULL , ";
                sql += " '" + oCad.Cep + "', ";
                sql += " GETDATE() ";
                sql += " ) ";

                cd.CommandText = sql.ToUpper();
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;
                cd.ExecuteNonQuery();
                int idCadastro = Convert.ToInt32(ID);


                sql = "INSERT INTO MOTORISTA ";
                sql += " ( ";
                sql += " IDMOTORISTA, ";
                sql += " CARTEIRADEHABILITACAO, ";
                sql += " VALIDADEDAHABILITACAO, ";
                sql += " DATADAPRIMEIRAHABILITACAO, ";
                sql += " CATEGORIA, ";
                sql += " DATADENASCIMENTO, ";
                sql += " IDCIDADENASCIMENTO, ";
                sql += " NOMEDOPAI, ";
                sql += " NOMEDAMAE, ";
                sql += " CONJUGE, ";
                sql += " VITIMADEROUBOQUANTIDADE, ";
                sql += " SOFREUACIDADEQUANTIDADE, ";
                sql += " ESTADOCIVIL, ";
                sql += " DATADECADASTRO, ";               

                if (oMot.VencimentoPancary != null)
                    sql += " VENCIMENTOPANCARY, ";

                if (oMot.VencimentoBrasilrisk != null)
                    sql += " VENCIMENTOBRASILRISK, ";

                if (oMot.VencimentoBuonny != null)
                    sql += " VENCIMENTOBUONNY, ";

                sql += " ALIQUOTASESTSENAT, ";
                sql += " ATIVO, ";
                sql += " LIBERADO, ";
                sql += " CARREGAMENTOAUTORIZADOATE, ";
                sql += " NUMEROPANCARD,  ";
                sql += " MOPP,  ";
                sql += " AGUARDANDOLIBERACAO  ";
                sql += " ) ";
                sql += " VALUES ";
                sql += " ( ";
                sql += idCadastro.ToString() + " , ";
                sql += " '" + oMot.CarteiraDeHabilitacao + "', ";
                sql += " @VALIDADEDAHABILITACAO, ";
                sql += " @DATADAPRIMEIRAHABILITACAO, ";
                sql += " '" + oMot.Categoria.ToUpper() + "', ";
                sql += " @DATADENASCIMENTO, ";
                sql += oMot.IDCidadeNascimento + " , ";
                sql += " '" + oMot.NomeDoPai.ToString() + "', ";
                sql += " '" + oMot.NomeDaMae.ToString() + "', ";
                sql += " '" + oMot.Conjuge.ToString() + "', ";
                sql += oMot.VitimaDeRouboQuantidade.ToString().Replace(",", ".") + " , ";
                sql += oMot.SofreuAcidadeQuantidade.ToString().Replace(",", ".") + " , ";
                sql += " '" + oMot.EstadoCivil.ToString().ToUpper() + "', ";
                sql += " GETDATE(), ";
                
                if (oMot.VencimentoPancary != null)
                    sql += " @VencimentoPancary, ";

                if (oMot.VencimentoBrasilrisk != null)
                    sql += " @VencimentoBrasilrisk, ";

                if (oMot.VencimentoBuonny != null)
                    sql += " @VencimentoBuonny, ";

                sql += oMot.AliquotaSestSenat.ToString().Replace(",", ".") + " , ";
                sql += " '" + oMot.Ativo + "', ";
                sql += " '" + oMot.Liberado + "', ";
                sql += oMot.CarregamentoAutorizadoAte.ToString().Replace(",", ".") + " , ";
                sql += " '" + oMot.NumeroPancard.ToUpper() + "',  ";
                sql += " '" + oMot.MOOP.ToUpper() + "',  ";
                sql += " getDate() ";
                sql += " ) ";

                //comandos
                cd.CommandText = sql.ToUpper();
                cd.Transaction = oTrans;

                
                DbParameter VALIDADEDAHABILITACAO = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateParameter();
                VALIDADEDAHABILITACAO.ParameterName = "@VALIDADEDAHABILITACAO";
                VALIDADEDAHABILITACAO.Value = oMot.ValidadeDaHabilitacao;
                VALIDADEDAHABILITACAO.DbType = DbType.DateTime;
                cd.Parameters.Add(VALIDADEDAHABILITACAO);

                DbParameter DATADAPRIMEIRAHABILITACAO = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateParameter();
                DATADAPRIMEIRAHABILITACAO.ParameterName = "@DATADAPRIMEIRAHABILITACAO";
                DATADAPRIMEIRAHABILITACAO.Value = oMot.DataDaPrimeiraHabilitacao;
                DATADAPRIMEIRAHABILITACAO.DbType = DbType.DateTime;
                cd.Parameters.Add(DATADAPRIMEIRAHABILITACAO);

                DbParameter DATADENASCIMENTO = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateParameter();
                DATADENASCIMENTO.ParameterName = "@DATADENASCIMENTO";
                DATADENASCIMENTO.Value = oMot.DataDaPrimeiraHabilitacao;
                DATADENASCIMENTO.DbType = DbType.DateTime;
                cd.Parameters.Add(DATADENASCIMENTO);

                if (oMot.VencimentoPancary != null)
                {
                    DbParameter VencimentoPancary = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateParameter();
                    VencimentoPancary.ParameterName = "@VencimentoPancary";
                    VencimentoPancary.Value = oMot.VencimentoPancary;
                    VencimentoPancary.DbType = DbType.DateTime;
                    cd.Parameters.Add(VencimentoPancary);
                }

                if (oMot.VencimentoBrasilrisk != null)
                {
                    DbParameter VencimentoBrasilrisk = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateParameter();
                    VencimentoBrasilrisk.ParameterName = "@VencimentoBrasilrisk";
                    VencimentoBrasilrisk.Value = oMot.VencimentoBrasilrisk;
                    VencimentoBrasilrisk.DbType = DbType.DateTime;
                    cd.Parameters.Add(VencimentoBrasilrisk);
                }

                if (oMot.VencimentoBuonny != null)
                {
                    DbParameter VencimentoBuonny = DbProviderFactories.GetFactory("System.Data.SqlClient").CreateParameter();
                    VencimentoBuonny.ParameterName = "@VencimentoBuonny";
                    VencimentoBuonny.Value = oMot.VencimentoBuonny;
                    VencimentoBuonny.DbType = DbType.DateTime;
                    cd.Parameters.Add(VencimentoBuonny);
                }
                cd.ExecuteNonQuery();

                
                sql = " INSERT INTO MOTORISTAHISTORICO ";
                sql += " ( ";
                sql += " IdMotoristaHistorico, ";
                sql += " IdMotorista, ";
                sql += " Historico, ";
                sql += " DataDeCadastro, ";
                sql += " IDUsuario ";
                sql += " ) ";
                sql += " VALUES ";
                sql += " ( ";
                sql += Sistran.Library.GetDataTables.RetornarIdTabela("MOTORISTAHISTORICO") + " , ";
                sql += ID + ", ";
                sql += " '" + oMotHst.Historico + "', ";
                sql += " getdate(), ";
                sql += oMotHst.IDUsuario;
                sql += " ) ";
                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;
                cd.ExecuteNonQuery();

                //TELEFONES
                int idfoneR = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO"));
                int idfoneRec = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO"));
                int idfoneCel = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO"));
                int idfoneNextel = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO"));

                if (oMot.TelefoneRes != "")
                {
                    sql = " INSERT INTO CADASTROCONTATOENDERECO ";
                    sql += " ( ";
                    sql += " IDCADASTROCONTATOENDERECO, ";
                    sql += " IDCADASTRO, ";
                    sql += " IDCADASTROTIPODECONTATO, ";
                    sql += " ENDERECO ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += idfoneR + " , ";
                    sql += idCadastro.ToString() + " , ";
                    sql += "(select IDCADASTROTIPODECONTATO from CADASTROTIPODECONTATO where nome='TELEFONE RESIDENCIAL'  ), ";
                    sql += " '" + oMot.TelefoneRes + "' ";
                    sql += " )";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();
                }


                if (oMot.TelefoneRecado != "")
                {
                    sql = " INSERT INTO CADASTROCONTATOENDERECO ";
                    sql += " ( ";
                    sql += " IDCADASTROCONTATOENDERECO, ";
                    sql += " IDCADASTRO, ";
                    sql += " IDCADASTROTIPODECONTATO, ";
                    sql += " ENDERECO ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += idfoneRec + " , ";
                    sql += idCadastro.ToString() + " , ";
                    sql += "(select IDCADASTROTIPODECONTATO from CADASTROTIPODECONTATO where nome='TELEFONE DE RECADO'  ), ";
                    sql += " '" + oMot.TelefoneRecado + "' ";
                    sql += " )";

                    cd.CommandText = sql.ToUpper();
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();
                }


                if (oMot.TelefoneCel != "")
                {
                    sql = " INSERT INTO CADASTROCONTATOENDERECO ";
                    sql += " ( ";
                    sql += " IDCADASTROCONTATOENDERECO, ";
                    sql += " IDCADASTRO, ";
                    sql += " IDCADASTROTIPODECONTATO, ";
                    sql += " ENDERECO ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += idfoneCel + " , ";
                    sql += idCadastro.ToString() + " , ";
                    sql += "(select IDCADASTROTIPODECONTATO from CADASTROTIPODECONTATO where nome='CELULAR'  ), ";
                    sql += " '" + oMot.TelefoneCel + "' ";
                    sql += " )";

                    cd.CommandText = sql.ToUpper();
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();
                }

                if (oMot.TelefoneNextel != "")
                {
                    sql = " INSERT INTO CADASTROCONTATOENDERECO ";
                    sql += " ( ";
                    sql += " IDCADASTROCONTATOENDERECO, ";
                    sql += " IDCADASTRO, ";
                    sql += " IDCADASTROTIPODECONTATO, ";
                    sql += " ENDERECO ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += idfoneNextel + " , ";
                    sql += idCadastro.ToString() + " , ";
                    sql += "(select IDCADASTROTIPODECONTATO from CADASTROTIPODECONTATO where nome='NEXTEL'  ), ";
                    sql += " '" + oMot.TelefoneCel + "' ";
                    sql += " )";

                    cd.CommandText = sql.ToUpper();
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();
                }

                GravarImagens(oTrans, cd, idCadastro.ToString());
                oTrans.Commit();
                return idCadastro;
               
            }
            catch (Exception exd)
            {
                oTrans.Rollback();
                throw exd;
            }
            finally
            {
                cn.Close();
            }
        }

        private void GravarImagens(DbTransaction oTrans, DbCommand cd, string idCadastro)
        {
            if (HttpContext.Current.Session["dtFoto"] != null)
            {
                DataTable dtFoto = (DataTable)HttpContext.Current.Session["dtFoto"];
                if (dtFoto.Rows.Count > 0)
                {
                    for (int i = 0; i < dtFoto.Rows.Count; i++)
                    {
                        bool DEL = false;
                        string m = "";
                        if (dtFoto.Rows[i]["id"].ToString() == "0")
                        {
                            m += " INSERT INTO CADASTROIMAGEM(";
                            m += " IDCADASTROIMAGEM,";
                            m += " IDCADASTRO,";
                            m += " IMAGEM,";
                            m += " NOME";
                            m += " ) VALUES";
                            m += " (";
                            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROIMAGEM");

                            m +=  ID+ " ,";
                            m +=  idCadastro + " ,";
                            m += " @IMAGEM" +i.ToString() + ",";
                            m += " '" + dtFoto.Rows[i]["texto"].ToString() + "'";
                            m += " )";
                        }
                        else if (dtFoto.Rows[i]["excluido"].ToString() == "SIM" && dtFoto.Rows[i]["id"].ToString() != "0")
                        {
                            m += " DELETE FROM CADASTROIMAGEM WHERE IDCADASTROIMAGEM = " + dtFoto.Rows[i]["id"].ToString() ;
                            DEL = true;
                        }
                        else
                        {
                            m += " UPDATE CADASTROIMAGEM SET IMAGEM=@IMAGEM" + i.ToString() + ", NOME='" + dtFoto.Rows[i]["texto"].ToString() + "'  WHERE IDCADASTROIMAGEM=" + dtFoto.Rows[i]["id"].ToString();
                        }

                        if(DEL==false)
                        {
                        cd.Parameters.Add(new SqlParameter("@IMAGEM" + i.ToString() , dtFoto.Rows[i]["conteudo"]));
                        }
                        
                        cd.CommandText = m;
                        cd.CommandType = CommandType.Text;
                        cd.Transaction = oTrans;
                        cd.ExecuteNonQuery();
                    }
                }
            }
        }

        public int TransacaoInserirCadastroProprietario(string Conn, SistranMODEL.Cadastro oCad, SistranMODEL.Cadastro.Proprietario oProp)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();

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
            cn.Open();
            DbTransaction oTrans;
            oTrans = cn.BeginTransaction();

            try
            {

                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTRO");

                string sql = " INSERT INTO CADASTRO ";
                sql += " ( ";
                sql += " IDCADASTRO, ";
                sql += " CNPJCPF, ";
                sql += " INSCRICAORG, ";
                sql += " RAZAOSOCIALNOME, ";
                sql += " FANTASIAAPELIDO, ";
                sql += " ENDERECO, ";
                sql += " NUMERO, ";
                sql += " COMPLEMENTO, ";
                sql += " IDCIDADE, ";
                sql += " IDBAIRRO, ";
                sql += " CEP, ";
                sql += " DATADECADASTRO ";
                sql += " ) ";
                sql += " VALUES ";
                sql += " ( ";
                sql += ID + " , ";
                sql += " '" + oCad.CnpjCpf + "', ";
                sql += " '" + oCad.InscricaoRG + "', ";
                sql += " '" + oCad.RazaoSocialNome.ToUpper() + "', ";
                sql += " '" + oCad.RazaoSocialNome.ToUpper() + "', ";
                sql += " '" + oCad.Endereco.ToUpper() + "', ";
                sql += " '" + oCad.Numero.ToUpper() + "', ";
                sql += " '" + oCad.Complemento.ToUpper() + "', ";
                sql += oCad.IDCidade.ToString() + " , ";
                sql += oCad.IDBairro.ToString() + ", ";
                sql += " '" + oCad.Cep + "', ";
                sql += " GETDATE() ";
                sql += " ) ";

                cd.CommandText = sql;
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;
                cd.ExecuteNonQuery();
                int idCadastro = Convert.ToInt32(ID);


                sql = "INSERT INTO PROPRIETARIO (IDPROPRIETARIO) VALUES (" + idCadastro + ") ";
                cd.CommandText = sql;
                cd.Transaction = oTrans;

                cd.ExecuteNonQuery();

                //TELEFONES
                int idfoneR = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO"));
                int idfoneRec = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO"));
                int idfoneCel = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO"));
                int idfoneNextel = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO"));

                if (oProp.TelefoneRes != "")
                {
                    sql = " INSERT INTO CADASTROCONTATOENDERECO ";
                    sql += " ( ";
                    sql += " IDCADASTROCONTATOENDERECO, ";
                    sql += " IDCADASTRO, ";
                    sql += " IDCADASTROTIPODECONTATO, ";
                    sql += " ENDERECO ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += idfoneR + " , ";
                    sql += idCadastro.ToString() + " , ";
                    sql += "(select IDCADASTROTIPODECONTATO from CADASTROTIPODECONTATO where nome='TELEFONE RESIDENCIAL'  ), ";
                    sql += " '" + oProp.TelefoneRes + "' ";
                    sql += " )";

                    cd.CommandText = sql;
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();
                }


                if (oProp.TelefoneRecado != "")
                {
                    sql = " INSERT INTO CADASTROCONTATOENDERECO ";
                    sql += " ( ";
                    sql += " IDCADASTROCONTATOENDERECO, ";
                    sql += " IDCADASTRO, ";
                    sql += " IDCADASTROTIPODECONTATO, ";
                    sql += " ENDERECO ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += idfoneRec + " , ";
                    sql += idCadastro.ToString() + " , ";
                    sql += "(select IDCADASTROTIPODECONTATO from CADASTROTIPODECONTATO where nome='TELEFONE DE RECADO'  ), ";
                    sql += " '" + oProp.TelefoneRecado + "' ";
                    sql += " )";

                    cd.CommandText = sql.ToUpper();
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();
                }


                if (oProp.TelefoneCel != "")
                {
                    sql = " INSERT INTO CADASTROCONTATOENDERECO ";
                    sql += " ( ";
                    sql += " IDCADASTROCONTATOENDERECO, ";
                    sql += " IDCADASTRO, ";
                    sql += " IDCADASTROTIPODECONTATO, ";
                    sql += " ENDERECO ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += idfoneCel + " , ";
                    sql += idCadastro.ToString() + " , ";
                    sql += "(select IDCADASTROTIPODECONTATO from CADASTROTIPODECONTATO where nome='CELULAR'  ), ";
                    sql += " '" + oProp.TelefoneCel + "' ";
                    sql += " )";

                    cd.CommandText = sql.ToUpper();
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();
                }

                if (oProp.TelefoneNextel != "")
                {
                    sql = " INSERT INTO CADASTROCONTATOENDERECO ";
                    sql += " ( ";
                    sql += " IDCADASTROCONTATOENDERECO, ";
                    sql += " IDCADASTRO, ";
                    sql += " IDCADASTROTIPODECONTATO, ";
                    sql += " ENDERECO ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += idfoneNextel + " , ";
                    sql += idCadastro.ToString() + " , ";
                    sql += "(select IDCADASTROTIPODECONTATO from CADASTROTIPODECONTATO where nome='NEXTEL'  ), ";
                    sql += " '" + oProp.TelefoneCel + "' ";
                    sql += " )";

                    cd.CommandText = sql.ToUpper();
                    cd.CommandType = CommandType.Text;
                    cd.Transaction = oTrans;
                    cd.ExecuteNonQuery();
                }

                GravarImagens(oTrans, cd, idCadastro.ToString());
                
                oTrans.Commit();
                return idCadastro;
            }
            catch (Exception)
            {
                oTrans.Rollback();
                throw;
            }
            finally
            {
                cn.Close();
            }
        }

        public int TransacaoInserirCadastroTrasnportadora(string Conn, SistranMODEL.Cadastro oCad, SistranMODEL.Cadastro.Tranportadora oTrans)
        {
            return Sistran.Library.GetDataTables.TransacaoInserirCadastroTrasnportadora(Conn, oCad, oTrans);
        }

        public int InserirCadastroUsuario(string CNPJ, string nome)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTRO");

            string strsql = "INSERT INTO CADASTRO (IDCadastro,";
            strsql += " CnpjCpf, ";
            strsql += " RazaoSocialNome, ";
            strsql += " IDCidade, ";
            strsql += " IDBairro, ";
            strsql += " DataDeCadastro) VALUES(" + ID + " , ";

            //if (CNPJ != "0")
            //    strsql += " '" + CNPJ + "', ";
            //else
            strsql += ID + " , ";

            strsql += " '" + nome + "', ";
            strsql += "0, ";
            strsql += "0, ";
            strsql += " GetDate())";// SELECT MAX(IDCADASTRO) FROM CADASTRO";

            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql.ToString(), "");
            return Convert.ToInt32(ID);

        }

        public int Inserir(string CNPJ, string InscricaoRG, string Razao, string FantasiaApelido, string Endereco, string Numero, string Complemento, string IDCidade, string IDBairro, string CEP)
        {
            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTRO");

            string strsql = "INSERT INTO CADASTRO (IDCadastro,";
            strsql += " CnpjCpf, ";
            strsql += " InscricaoRG, ";
            strsql += " RazaoSocialNome, ";
            strsql += " FantasiaApelido, ";
            strsql += " Endereco, ";
            strsql += " Numero, ";
            strsql += " Complemento, ";
            strsql += " IDCidade, ";
            strsql += " IDBairro, ";
            strsql += " Cep, ";
            strsql += " DataDeCadastro) VALUES(" + ID + ") , ";
            //            strsql += " DataDeCadastro) VALUES((SELECT ISNULL(MAX(IDCadastro),0) + 1 FROM CADASTRO) , ";
            strsql += " '" + CNPJ + "', ";
            strsql += " '" + InscricaoRG + "', ";
            strsql += " '" + Razao + "', ";
            strsql += " '" + FantasiaApelido + "', ";
            strsql += " '" + Endereco + "', ";
            strsql += " '" + Numero + "', ";
            strsql += " '" + Complemento + "', ";
            strsql += IDCidade + ", ";
            //strsql += IDBairro + ", ";

            if (IDBairro == "" || IDBairro == "0")
                strsql += " Null, ";
            else
                strsql += IDBairro + ", ";

            strsql += "'" + CEP + "', ";
            strsql += " GetDate())";// SELECT MAX(IDCADASTRO) FROM CADASTRO";

            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql.ToString(), "");
            return Convert.ToInt32(ID);

        }

        public void Alterar(string CNPJ, string InscricaoRG, string Razao, string FantasiaApelido, string Endereco, string Numero, string Complemento, string IDCidade, string IDBairro, string CEP, string IdCadastro)
        {

            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            DbConnection cn = factory.CreateConnection();
            DbCommand cd = factory.CreateCommand();            
            cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();
            
            cd.Connection = cn;
            cn.Open();
            DbTransaction oTrans;
            oTrans = cn.BeginTransaction();

            try
            {
                string strsql = "UPDATE CADASTRO SET ";
                strsql += " CnpjCpf= '" + CNPJ + "' , ";
                strsql += " InscricaoRG = '" + InscricaoRG + "', ";
                strsql += " RazaoSocialNome = '" + Razao + "', ";
                strsql += " FantasiaApelido = '" + FantasiaApelido + "', ";
                strsql += " Endereco='" + Endereco + "' , ";
                strsql += " Numero ='" + Numero + "' , ";
                strsql += " Complemento = '" + Complemento + "', ";
                strsql += " IDCidade=" + IDCidade + ", ";

                if (IDBairro == "" || IDBairro == "0")
                    strsql += " IDBairro=Null, ";
                else
                    strsql += " IDBairro=" + IDBairro + ", ";


                strsql += " Cep='" + CEP + "'";
                strsql += " WHERE IDCADASTRO=" + IdCadastro;
                cd.CommandText = strsql.ToUpper();
                cd.CommandType = CommandType.Text;
                cd.Transaction = oTrans;
                cd.ExecuteNonQuery();

               

                GravarImagens(oTrans, cd, IdCadastro.ToString());
                oTrans.Commit();
            }
            catch (Exception exd)
            {
                oTrans.Rollback();
                throw exd;
            }
            finally
            {
                cn.Close();
            }


            //Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql.ToString(), "");

        }

        public class CadastroComplemento
        {
            public int inserir(SistranMODEL.Cadastro.CadastroComplemento oCadCompl)
            {
                int IDCadastroComplemento = Sistran.Library.GetDataTables.ExecutarRetornoID("SELECT IDCadastroComplemento FROM CadastroComplemento WHERE IDCADASTRO=" + oCadCompl.IDCadastro, "");

                if (IDCadastroComplemento == 0)
                {
                    string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCOMPLEMENTO");

                    string strsql = " INSERT INTO CADASTROCOMPLEMENTO ";
                    strsql += " ( ";
                    strsql += " IDCadastroComplemento, ";
                    strsql += " IDCadastro, ";
                    strsql += " Aniversario, ";
                    strsql += " Dependentes, ";
                    strsql += " Banco, ";
                    strsql += " Agencia, ";
                    strsql += " AgenciaDigito, ";
                    strsql += " Conta, ";
                    strsql += " ContaDigito, ";
                    strsql += " TipoConta, ";
                    strsql += " CnpjCpfFavorecido, ";
                    strsql += " NomeFavorecido, ";
                    strsql += " InscricaoNoInss, ";
                    strsql += " InscricaoPIS, ";
                    strsql += " VinculoFavorecido, ";
                    strsql += " Aposentado, ";
                    strsql += " Contratado, ";
                    strsql += " DataExpedicaoRG, ";
                    strsql += " OrgaoExpedicaoRG, ";
                    strsql += " UltimaComprovacaoEndereco, ";

                    strsql += " ValorPensaoAlimenticia ";
                    strsql += " ) ";
                    strsql += " VALUES ";
                    strsql += " ( ";
                    strsql += ID + " , ";
                    strsql += oCadCompl.IDCadastro.ToString() + " , ";
                    strsql += " '" + oCadCompl.Aniversario + "', ";
                    strsql += oCadCompl.Dependentes + " , ";
                    strsql += " '" + oCadCompl.Banco + "', ";
                    strsql += " '" + oCadCompl.Agencia + "', ";
                    strsql += " '" + oCadCompl.AgenciaDigito + "', ";
                    strsql += " '" + oCadCompl.Conta + "', ";
                    strsql += " '" + oCadCompl.ContaDigito + "', ";
                    strsql += " '" + oCadCompl.TipoConta.ToUpper() + "', ";
                    strsql += " '" + oCadCompl.CnpjCpfFavorecido + "', ";
                    strsql += " '" + oCadCompl.NomeFavorecido + "', ";
                    strsql += " '" + oCadCompl.InscricaoNoInss + "', ";
                    strsql += " '" + oCadCompl.InscricaoPIS + "', ";
                    strsql += " '" + oCadCompl.VinculoFavorecido + "', ";
                    strsql += " '" + oCadCompl.Aposentado + "', ";
                    strsql += " '" + oCadCompl.Contratado + "', ";
                    strsql += "CONVERT(DATETIME,'" + oCadCompl.DataExpedicaoRG + "',103),";
                    strsql += " '" + oCadCompl.OrgaoExpedicaoRG + "', ";
                    strsql += " CONVERT(DATETIME,'" + oCadCompl.UltimaComprovacaoEndereco + "',103),";
                    strsql += oCadCompl.ValorPensaoAlimenticia.ToString().Replace(",", ".");
                    strsql += " ) ";//SELECT IDCadastroComplemento FROM CadastroComplemento";

                    Sistran.Library.GetDataTables.ExecutarRetornoID(strsql.ToString(), "");
                    return Convert.ToInt32(ID);

                }
                else
                {
                    oCadCompl.IDCadastroComplemento = IDCadastroComplemento;
                    string strsql = " UPDATE CadastroComplemento SET ";
                    strsql += " Aniversario = '" + oCadCompl.Aniversario + "', ";
                    strsql += " Dependentes =" + oCadCompl.Dependentes + " , ";
                    strsql += " Banco='" + oCadCompl.Banco + "', ";
                    strsql += " Agencia='" + oCadCompl.Agencia + "', ";
                    strsql += " AgenciaDigito='" + oCadCompl.AgenciaDigito + "', ";
                    strsql += " Conta='" + oCadCompl.Conta + "', ";
                    strsql += " ContaDigito='" + oCadCompl.ContaDigito + "', ";
                    strsql += " TipoConta='" + oCadCompl.TipoConta.ToUpper() + "', ";
                    strsql += " CnpjCpfFavorecido='" + oCadCompl.CnpjCpfFavorecido + "', ";
                    strsql += " NomeFavorecido='" + oCadCompl.NomeFavorecido + "', ";
                    strsql += " InscricaoNoInss='" + oCadCompl.InscricaoNoInss + "', ";
                    strsql += " InscricaoPIS='" + oCadCompl.InscricaoPIS + "', ";

                    strsql += " VinculoFavorecido='" + oCadCompl.VinculoFavorecido + "',";
                    strsql += " Aposentado='" + oCadCompl.Aposentado + "',";
                    strsql += " Contratado='" + oCadCompl.Contratado + "',";
                    strsql += " DataExpedicaoRG=CONVERT(DATETIME,'" + oCadCompl.DataExpedicaoRG + "',103),";
                    strsql += " OrgaoExpedicaoRG='" + oCadCompl.OrgaoExpedicaoRG + "',";
                    strsql += " UltimaComprovacaoEndereco=CONVERT(DATETIME,'" + oCadCompl.UltimaComprovacaoEndereco + "',103),";

                    strsql += " ValorPensaoAlimenticia=" + oCadCompl.ValorPensaoAlimenticia.ToString().Replace(",", ".");
                    strsql += " WHERE  IDCadastroComplemento =  " + oCadCompl.IDCadastroComplemento;
                    return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql.ToString(), "");
                }
            }

            public DataTable readByIdCadastro(int IdCadastro)
            {
                return Sistran.Library.GetDataTables.RetornarDataTable("SELECT * FROM CADASTROCOMPLEMENTO WHERE IDCADASTRO=" + IdCadastro.ToString());
            }
        }

        public class CadastroContato
        {
            public int Inserir(string IDCADASTRO, string IDCONTATO)
            {
                string str0 = "SELECT IDCADASTROCONTATO FROM CADASTROCONTATO WHERE IDCONTATO=" + IDCONTATO + " AND IDCADASTRO=" + IDCADASTRO;
                int m = Sistran.Library.GetDataTables.ExecutarRetornoID(str0.ToString(), "");

                if (m > 0)
                    return m;

                string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATO");
                string str = "INSERT INTO CADASTROCONTATO(IDCADASTROCONTATO,IDCADASTRO,IDCONTATO) VALUES(" + ID + " ," + IDCADASTRO + ", " + IDCONTATO + ") SELECT ISNULL(MAX(IDCADASTROCONTATO),0)  FROM CADASTROCONTATO";
                Sistran.Library.GetDataTables.ExecutarRetornoID(str.ToString(), "");
                return Convert.ToInt32(ID);
            }

            public DataTable ListarEmailsAprovadoresPedidos()
            {
                string STR = "SELECT CADCONEND.ENDERECO, U.NOME FROM AVISO A";
                STR += " INNER JOIN USUARIO U ON U.IDUSUARIO = A.IDUSUARIO ";
                STR += "INNER JOIN CADASTRO C ON C.IDCADASTRO = U.IDCADASTRO ";
                STR += "LEFT JOIN CADASTROCONTATOENDERECO CADCONEND  ";
                STR += "ON(CADCONEND.IDCADASTRO =C.IDCADASTRO)  ";
                STR += "LEFT JOIN CADASTROTIPODECONTATO CADTIPDECON  ";
                STR += "ON(CADTIPDECON.IDCADASTROTIPODECONTATO = CADCONEND.IDCADASTROTIPODECONTATO)      ";
                STR += "WHERE CADTIPDECON.NOME = 'E-MAIL'  ";
                STR += "AND A.OPERACAO='APROVAR PEDIDO' ";
                STR += "AND A.IDCLIENTE=" + HttpContext.Current.Session["IDEmpresa"].ToString();
                return Sistran.Library.GetDataTables.RetornarDataTable(STR);
            }

            public class CadastroContatoEndereco
            {
                public int Inserir(string IDCADASTRO, string email)
                {
                    string str0 = "SELECT IDCADASTROCONTATOENDERECO FROM CadastroContatoEndereco WHERE IDCadastroTipoDeContato=1 AND IDCADASTRO=" + IDCADASTRO;
                    int m = Sistran.Library.GetDataTables.ExecutarRetornoID(str0.ToString(), "");

                    if (m > 0)
                    {
                        str0 = "UPDATE CadastroContatoEndereco SET Endereco='" + email + "' WHERE IDCADASTROCONTATOENDERECO=" + m;
                        Sistran.Library.GetDataTables.ExecutarSemRetorno(str0.ToString(), "");
                        return m;
                    }
                    else
                    {                        
                        string str = "INSERT INTO CadastroContatoEndereco(IDCadastroContatoEndereco, IDCadastro,  IDCadastroTipoDeContato, Endereco) ";
                        str += " VALUES(" + Sistran.Library.GetDataTables.RetornarIdTabela("CadastroContatoEndereco") + "  ," + IDCADASTRO + ", 1 ,'" + email.ToLower() + "' ) SELECT ISNULL(MAX(IDCadastroContatoEndereco),0)  FROM CadastroContatoEndereco";
                        return Sistran.Library.GetDataTables.ExecutarRetornoID(str.ToString(), "");

                    }
                }

                public DataTable RetornarTelefone(int IdCadastro)
                {
                    string strsql = " SELECT DISTINCT * ";
                    strsql += " FROM CADASTROCONTATOENDERECO CCE ";
                    strsql += " INNER JOIN CADASTROTIPODECONTATO CTC ON CTC.IDCADASTROTIPODECONTATO = CCE.IDCADASTROTIPODECONTATO ";
                    strsql += " WHERE  ";
                    strsql += " IDCADASTRO = "+IdCadastro.ToString()+" AND  ";
                    strsql += " CTC.NOME IN ('TELEFONE RESIDENCIAL','CELULAR','TELEFONE de RECADO', 'TELEFONE COMERCIAL', 'NEXTEL') ";
                    strsql += " ORDER BY IDCADASTRO, CTC.NOME  ";
                    return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
                }

                public DataTable TipoDeTelefome()
                {
                    string strsql = "  SELECT IDCADASTROTIPODECONTATO, NOME   FROM CADASTROTIPODECONTATO WHERE NOME LIKE '%FONE%' OR NOME LIKE '%CELULAR%' OR NOME LIKE '%FAX%' OR NOME LIKE '%NEXTEL%'";
                    return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
                }

            }
        }

        public class Motorista
        {
            public DataTable Pesquisar(string nome, string cpf, string ativo, string inativo, int? IdMotorista, string liberado, string naoLiberado, bool vencida, 
                                       string campoGerenciadora, DateTime? gerInicio, DateTime? gerFim, string tipoFavorecido, string valorFavorecido, string contratado, int codFilial, DateTime? dataBloqeioIni, DateTime? dataBloqueioFim )
            {
                string strsql = " SELECT CASE WHEN M.LIBERADO<>'SIM' THEN 'NAO' ELSE 'SIM' END LIBERADO ,* FROM MOTORISTA  M INNER JOIN CADASTRO C ON C.IDCADASTRO = M.IDMOTORISTA ";
                strsql += " LEFT JOIN CADASTROCOMPLEMENTO CC ON CC.IDCADASTRO = C.IDCADASTRO ";

                if (codFilial > 0)
                {
                    strsql += " INNER JOIN MOTORISTAFILIAL MF ON MF.IDMOTORISTA=M.IDMOTORISTA AND MF.IDFILIAL =" + codFilial.ToString();
                }

                strsql += " WHERE  0=0";

                if (IdMotorista == null)
                {
                    strsql += " AND C.RAZAOSOCIALNOME LIKE '" + nome + "%' ";
                    strsql += " AND C.CNPJCPF LIKE '" + cpf + "%'";

                    if (ativo == "SIM" && inativo == "NAO")
                        strsql += " AND M.ATIVO = 'SIM'";

                    if (inativo == "SIM" && ativo == "NAO")
                        strsql += " AND M.ATIVO = 'NAO'";

                    if (liberado == "SIM" && naoLiberado == "NAO")
                        strsql += " AND M.LIBERADO = 'SIM'";

                    if (naoLiberado == "SIM" && liberado == "NAO")
                        strsql += " AND M.LIBERADO = 'NAO'";

                    if (campoGerenciadora != "Nenhum" && gerInicio != null && gerFim != null)
                    {
                        //if (gerInicio == null || gerFim == null)
                        //{
                        //    strsql += " and " + campoGerenciadora + " between cast('" + DateTime.Now.ToString("yyyy-MM-dd") + "' as date) and '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                        //}
                        //else
                        //{
                            strsql += " and " + campoGerenciadora + " between cast('" + Convert.ToDateTime(gerInicio).ToString("yyyy-MM-dd") + "' as date) and '" + Convert.ToDateTime(gerInicio).ToString("yyyy-MM-dd") + "' " ;
                        //}
                    }

                    if (tipoFavorecido != "Nenhum")
                    {
                        strsql += " and " + tipoFavorecido + " like '" + valorFavorecido + "%'";                        
                    }

                    if (contratado != "AMBOS")
                    {
                        strsql += " and Contratado='" + contratado.Replace("NÃO", "NAO") + "' ";                        
                    }

                    if (dataBloqeioIni != null && dataBloqueioFim != null)
                    {
                        strsql += " and DataDeBloqueio between cast('" + Convert.ToDateTime(dataBloqeioIni).ToString("yyyy-MM-dd") + "' as date) and '" + Convert.ToDateTime(dataBloqueioFim).ToString("yyyy-MM-dd") + "' ";
                    }

                    
                }
                else
                    strsql += " AND M.IDMOTORISTA =" + IdMotorista;

                if (vencida)
                    strsql += " AND M.VALIDADEDAHABILITACAO <=GetDate() ";



                strsql += " ORDER BY C.RAZAOSOCIALNOME";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");

                if (dt.Rows.Count == 0 && IdMotorista == null)
                {
                    strsql = " SELECT CASE WHEN M.LIBERADO<>'SIM' THEN 'NAO' ELSE 'SIM' END LIBERADO ,* FROM MOTORISTA  M RIGHT JOIN CADASTRO C ON C.IDCADASTRO = M.IDMOTORISTA ";
                    strsql += " WHERE  0=0";
                    strsql += " AND C.CNPJCPF ='" + cpf + "'";

                    dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
                }
                return dt;

            }

            public DataTable Pesquisar(string nome, string cpf, string ativo, string inativo, int? IdMotorista, string liberado, string naoLiberado, bool vencida)
            {
                string strsql = " SELECT CASE WHEN M.LIBERADO<>'SIM' THEN 'NAO' ELSE 'SIM' END LIBERADO ,* FROM MOTORISTA  M INNER JOIN CADASTRO C ON C.IDCADASTRO = M.IDMOTORISTA ";
                strsql += " LEFT JOIN CADASTROCOMPLEMENTO CC ON CC.IDCADASTRO = C.IDCADASTRO ";
                strsql += " WHERE  0=0";

                if (IdMotorista == null)
                {
                    strsql += " AND C.RAZAOSOCIALNOME LIKE '" + nome + "%' ";
                    strsql += " AND C.CNPJCPF LIKE '" + cpf + "%'";

                    if (ativo == "SIM" && inativo == "NAO")
                        strsql += " AND M.ATIVO = 'SIM'";

                    if (inativo == "SIM" && ativo == "NAO")
                        strsql += " AND M.ATIVO = 'NAO'";

                    if (liberado == "SIM" && naoLiberado == "NAO")
                        strsql += " AND M.LIBERADO = 'SIM'";

                    if (naoLiberado == "SIM" && liberado == "NAO")
                        strsql += " AND M.LIBERADO = 'NAO'";


                }
                else
                    strsql += " AND M.IDMOTORISTA =" + IdMotorista;

                if (vencida)
                    strsql += " AND M.VALIDADEDAHABILITACAO <=GetDate() ";



                strsql += " ORDER BY C.RAZAOSOCIALNOME";
                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");

                if (dt.Rows.Count == 0 && IdMotorista == null)
                {
                    strsql = " SELECT CASE WHEN M.LIBERADO<>'SIM' THEN 'NAO' ELSE 'SIM' END LIBERADO ,* FROM MOTORISTA  M RIGHT JOIN CADASTRO C ON C.IDCADASTRO = M.IDMOTORISTA ";
                    strsql += " WHERE  0=0";
                    strsql += " AND C.CNPJCPF ='" + cpf + "'";

                    dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
                }
                return dt;

            }

            public DataTable PesquisarLiberacao(string nome, string cpf, string ativo, string inativo, int? IdMotorista, string liberado, string naoLiberado, bool vencida, bool AguardandoLiberacao)
            {
                string strsql = " SELECT CASE WHEN M.LIBERADO<>'SIM' THEN 'NAO' ELSE 'SIM' END LIBERADO, CASE WHEN M.AGUARDANDOLIBERACAO IS NULL THEN 'NAO' ELSE 'SIM' END LIBERACAO ,* FROM MOTORISTA  M INNER JOIN CADASTRO C ON C.IDCADASTRO = M.IDMOTORISTA ";

                strsql += " WHERE C.RAZAOSOCIALNOME LIKE '" + nome + "%' ";
                strsql += " AND C.CNPJCPF LIKE '" + cpf + "%'";

                if (ativo == "SIM" && inativo == "NAO")
                    strsql += " AND M.ATIVO = 'SIM'";

                if (inativo == "SIM" && ativo == "NAO")
                    strsql += " AND M.ATIVO = 'NAO'";

                if (liberado == "SIM" && naoLiberado == "NAO")
                    strsql += " AND M.LIBERADO = 'SIM'";

                if (naoLiberado == "SIM" && liberado == "NAO")
                    strsql += " AND M.LIBERADO = 'NAO'";

                if (vencida)
                    strsql += " AND M.VALIDADEDAHABILITACAO <=GetDate() ";

                if (AguardandoLiberacao)
                    strsql += " AND M.AGUARDANDOLIBERACAO IS NOT NULL ";

                strsql += " ORDER BY C.RAZAOSOCIALNOME";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");

            }

            public DataTable CarregarReportMotorista(bool ativo, bool liberado, bool habilitacaoVencida)
            {
                string strsql = "  SELECT *  ";
                strsql += " FROM MOTORISTA MOT ";
                strsql += " INNER JOIN Cadastro cAD ON cAD.IDCadastro = MOT.IDMotorista ";
                strsql += " WHERE MOT.ATIVO = '" + (ativo == true ? "SIM" : "NAO") + "' ";
                strsql += " AND MOT.LIBERADO='" + (liberado == true ? "SIM" : "NAO") + "' ";
                strsql += (habilitacaoVencida == true ? " AND MOT.VALIDADEDAHABILITACAO <= GETDATE() " : "");
                strsql += " ORDER BY cAD.RAZAOSOCIALNOME ";

                return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
            }

            public DataTable Pesquisar(string cpf)
            {
                string strsql = " SELECT CASE WHEN M.LIBERADO<>'SIM' THEN 'NAO' ELSE 'SIM' END LIBERADO ,* FROM MOTORISTA  M INNER JOIN CADASTRO C ON C.IDCADASTRO = M.IDMOTORISTA ";
                strsql += " WHERE  0=0";
                strsql += " AND C.CNPJCPF ='" + cpf + "'";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
            }

            public void alterar(SistranMODEL.Cadastro.Motorista oMot, int IdCadastro)
            {
                string sql = " SELECT IDMOTORISTA FROM MOTORISTA WHERE IDMOTORISTA=" + oMot.IDMotorista;

                oMot.IDMotorista = Sistran.Library.GetDataTables.ExecutarRetornoID(sql, "");

                if (oMot.IDMotorista == 0)
                {
                    sql = "INSERT INTO MOTORISTA ";
                    sql += " ( ";
                    sql += " IDMOTORISTA, ";
                    sql += " CARTEIRADEHABILITACAO, ";
                    sql += " VALIDADEDAHABILITACAO, ";
                    sql += " DATADAPRIMEIRAHABILITACAO, ";
                    sql += " CATEGORIA, ";
                    sql += " DATADENASCIMENTO, ";
                    sql += " IDCIDADENASCIMENTO, ";
                    sql += " NOMEDOPAI, ";
                    sql += " NOMEDAMAE, ";
                    sql += " CONJUGE, ";
                    sql += " VITIMADEROUBOQUANTIDADE, ";
                    sql += " SOFREUACIDADEQUANTIDADE, ";
                    sql += " ESTADOCIVIL, ";
                    sql += " DATADECADASTRO, ";
                    sql += " VENCIMENTONOGERENCIADORDERISCO, ";
                    sql += " ALIQUOTASESTSENAT, ";
                    sql += " ATIVO, ";
                    sql += " LIBERADO, ";
                    sql += " CARREGAMENTOAUTORIZADOATE, ";
                    sql += " NUMEROPANCARD,  ";
                    sql += " MOPP,   ";
                    sql += " AGUARDANDOLIBERACAO ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += IdCadastro.ToString() + " , ";
                    sql += " '" + oMot.CarteiraDeHabilitacao + "', ";
                    sql += " @VALIDADEDAHABILITACAO, ";
                    sql += " @DATADAPRIMEIRAHABILITACAO, ";
                    sql += " '" + oMot.Categoria.ToUpper() + "', ";
                    sql += " @DATADENASCIMENTO, ";
                    sql += oMot.IDCidadeNascimento + " , ";
                    sql += " '" + oMot.NomeDoPai.ToString() + "', ";
                    sql += " '" + oMot.NomeDaMae.ToString() + "', ";
                    sql += " '" + oMot.Conjuge.ToString() + "', ";
                    sql += oMot.VitimaDeRouboQuantidade.ToString().Replace(",", ".") + " , ";
                    sql += oMot.SofreuAcidadeQuantidade.ToString().Replace(",", ".") + " , ";
                    sql += " '" + oMot.EstadoCivil.ToString().ToUpper() + "', ";
                    sql += " GETDATE(), ";
                    sql += " @VENCIMENTONOGERENCIADORDERISCO, ";
                    sql += oMot.AliquotaSestSenat.ToString().Replace(",", ".") + " , ";
                    sql += " '" + oMot.Ativo + "', ";
                    sql += " '" + oMot.Liberado + "', ";
                    sql += oMot.CarregamentoAutorizadoAte.ToString().Replace(",", ".") + " , ";
                    sql += " '" + oMot.NumeroPancard.ToUpper() + "',  ";
                    sql += " '" + oMot.MOOP.ToUpper() + "',  ";
                    sql += " getdate()) ";

                    sql = sql.Replace("@VALIDADEDAHABILITACAO", "CONVERT(DATETIME,'" + oMot.ValidadeDaHabilitacao + "' , 103)");
                    sql = sql.Replace("@DATADAPRIMEIRAHABILITACAO", "CONVERT(DATETIME,'" + oMot.DataDaPrimeiraHabilitacao + "' , 103)");
                    sql = sql.Replace("@DATADENASCIMENTO", "CONVERT(DATETIME,'" + oMot.DataDeNascimento + "' , 103)");
                    //sql = sql.Replace("@VENCIMENTONOGERENCIADORDERISCO", "CONVERT(DATETIME,'" + oMot.VencimentoNoGerenciadorDeRisco + "' , 103)");

                    Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, "");

                }
                else
                {
                    sql = "UPDATE  MOTORISTA set  AGUARDANDOLIBERACAO=GETDATE(), ";
                    sql += " CARTEIRADEHABILITACAO = '" + oMot.CarteiraDeHabilitacao + "', ";
                    sql += " CATEGORIA = '" + oMot.Categoria.ToUpper() + "', ";
                    sql += " IDCIDADENASCIMENTO= " + oMot.IDCidadeNascimento + " , ";
                    sql += " NOMEDOPAI='" + oMot.NomeDoPai.ToString() + "', ";
                    sql += " NOMEDAMAE='" + oMot.NomeDaMae.ToString() + "', ";
                    sql += " CONJUGE= '" + oMot.Conjuge.ToString() + "', ";
                    sql += " ATIVO='" + oMot.Ativo + "', ";
                    sql += " LIBERADO='" + oMot.Liberado + "', ";
                    sql += " ValidadeDaHabilitacao=CONVERT(DATETIME,'" + oMot.ValidadeDaHabilitacao + "',103),";
                    sql += " DATADENASCIMENTO=CONVERT(DATETIME,'" + oMot.DataDeNascimento + "',103),";
                    sql += " DataDaPrimeiraHabilitacao=CONVERT(DATETIME,'" + oMot.DataDaPrimeiraHabilitacao + "',103),";
                    //sql += " VencimentoNoGerenciadorDeRisco=CONVERT(DATETIME,'" + oMot.VencimentoNoGerenciadorDeRisco + "',103), ";


                    if (oMot.VencimentoPancary.ToString() == "")
                    {
                        sql += " VencimentoPancary=CONVERT(DATETIME,'" + oMot.VencimentoPancary + "',103),";
                    }

                    if (oMot.VencimentoBrasilrisk.ToString() == "")
                    {
                        sql += " VencimentoBrasilrisk=CONVERT(DATETIME,'" + oMot.VencimentoPancary + "',103),";
                    }

                    if (oMot.VencimentoBuonny.ToString() == "")
                    {
                        sql += " VencimentoBuonny=CONVERT(DATETIME,'" + oMot.VencimentoPancary + "',103),";
                    }

                    sql += " CARREGAMENTOAUTORIZADOATE =" + oMot.CarregamentoAutorizadoAte.ToString().Replace(",", ".") + " , ";
                    sql += " NUMEROPANCARD = '" + oMot.NumeroPancard.ToUpper() + "',  ";
                    sql += " MOPP = '" + oMot.MOOP.ToUpper() + "'  ";
                    sql += " WHERE IDMOTORISTA=" + oMot.IDMotorista.ToString();
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, "");

                    GravarTelefonesMotorista(oMot);

                }
            }

            private string MontarSql(string comando, string tipo, string endereco, string IDCADASTROCONTATOENDERECO)
            {

                if (comando.IndexOf("INSERT") >= 0)
                {
                    comando = comando.Replace("@ID", Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO"));
                    comando = comando.Replace("@TIPO", tipo);
                }
                else
                {
                    comando = comando.Replace("@IDCADASTROCONTATOENDERECO", IDCADASTROCONTATOENDERECO);
                }

                comando = comando.Replace("@ENDERECO", endereco);
                return comando;
            }
            
            private void GravarTelefonesMotorista(SistranMODEL.Cadastro.Motorista oMot)
            {               

                string sqlInsert = " INSERT INTO CADASTROCONTATOENDERECO ";
                sqlInsert += " ( ";
                sqlInsert += " IDCADASTROCONTATOENDERECO, ";
                sqlInsert += " IDCADASTRO, ";
                sqlInsert += " IDCADASTROTIPODECONTATO, ";
                sqlInsert += " ENDERECO ";
                sqlInsert += " ) ";
                sqlInsert += " VALUES ";
                sqlInsert += " ( ";
                sqlInsert += "@ID" + " , ";
                sqlInsert += oMot.IDMotorista + " , ";
                sqlInsert += "(SELECT IDCADASTROTIPODECONTATO FROM CADASTROTIPODECONTATO WHERE NOME='@TIPO'  ), ";
                sqlInsert += " '@ENDERECO' ";
                sqlInsert += " )";

                string sqlUpdate = "UPDATE CADASTROCONTATOENDERECO SET ENDERECO = '@ENDERECO' WHERE IDCADASTROCONTATOENDERECO = @IDCADASTROCONTATOENDERECO";

                string[] tel = new string[4];
                tel[0] = "TELEFONE RESIDENCIAL";
                tel[1] = "TELEFONE DE RECADO";
                tel[2] = "CELULAR";
                tel[3] = "NEXTEL";


                for (int i = 0; i < tel.Length; i++)
                {
                    switch (tel[i])
                    {
                        case "TELEFONE RESIDENCIAL":
                            if (oMot.TelefoneRes != "" && (oMot.IDTelefoneRes == "" || oMot.IDTelefoneRes == "0"))
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlInsert, tel[i], oMot.TelefoneRes, null), "");
                            }
                            else if (oMot.TelefoneRes != "")
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlUpdate, tel[i], oMot.TelefoneRes, oMot.IDTelefoneRes.ToString()), "");
                            }
                            break;

                        case "TELEFONE DE RECADO":
                            if (oMot.TelefoneRecado != "" && ( oMot.IDTelefoneRecado == "" || oMot.IDTelefoneRecado == "0"))
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlInsert, tel[i], oMot.TelefoneRecado, null), "");
                            }
                            else if (oMot.TelefoneRecado != "")
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlUpdate, tel[i], oMot.TelefoneRecado, oMot.IDTelefoneRecado.ToString()), "");
                            }
                            break;

                        case "CELULAR":
                            if (oMot.TelefoneCel != "" && ( oMot.IDTelefoneCel == "" || oMot.IDTelefoneCel == "0"))
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlInsert, tel[i], oMot.TelefoneCel, null), "");
                            }
                            else if (oMot.TelefoneCel != "")
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlUpdate, tel[i], oMot.TelefoneCel, oMot.IDTelefoneCel.ToString()), "");
                            }
                            break;

                        case "NEXTEL":
                            if (oMot.TelefoneNextel != "" && ( oMot.IDTelefoneNextel == "" || oMot.IDTelefoneNextel == "0"))
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlInsert, tel[i], oMot.TelefoneNextel, null), "");
                            }
                            else if (oMot.IDTelefoneNextel != "")
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlUpdate, tel[i], oMot.TelefoneNextel, oMot.IDTelefoneNextel.ToString()), "");
                            }
                            break;
                    }
                }
            }
            
            public void AlterarLiberacao(string IdMotorista, string Liberado)
            {
                string sql = "UPDATE  MOTORISTA SET  ";
                sql += " AGUARDANDOLIBERACAO = NULL, ";
                sql += " LIBERADO = '" + Liberado + "', ";
                sql += " ATIVO = '" + Liberado + "', ";

                if (Liberado == "SIM")
                    sql += " DataDeBloqueio=NULL ";
                else
                    sql += " DataDeBloqueio=getdate() ";

                sql += " WHERE IDMOTORISTA=" + IdMotorista;
                Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, "");

                SistranMODEL.Cadastro.Motorista.MotoristaHistorico oMotHst = new SistranMODEL.Cadastro.Motorista.MotoristaHistorico();
                List<SistranMODEL.Usuario> ILusuario = (List<SistranMODEL.Usuario>)System.Web.HttpContext.Current.Session["USUARIO"];

                oMotHst.Historico = (Liberado == "SIM" ? "USUÁRIO LIBEROU O MOTORISTA" : "USUÁRIO BLOQUEOU O MOTORISTA");
                oMotHst.IdMotorista = Convert.ToInt32(IdMotorista);
                oMotHst.IDUsuario = ILusuario[0].UsuarioId;
                new SistranBLL.Cadastro.Motorista.MotoristaHistorico().inserir(oMotHst);
            }

            public class MotoristaHistorico
            {
                public List<SistranMODEL.Cadastro.Motorista.MotoristaHistorico> RetornarHistorico(int IDMotorista)
                {
                    string strsql = " SELECT IdMotoristaHistorico, ";
                    strsql += " IdMotorista, ";
                    strsql += " Historico, ";
                    strsql += " MH.DataDeCadastro, ";
                    strsql += " MH.IDUsuario, USU.Nome NomeUsuario ";
                    strsql += " FROM MotoristaHistorico MH ";
                    strsql += " INNER JOIN USUARIO USU ON USU.IDUSUARIO = MH.IDUSUARIO  ";
                    strsql += " WHERE IdMotorista = " + IDMotorista.ToString();
                    strsql += " ORDER BY MH.DataDeCadastro DESC";
                    DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql);

                    List<SistranMODEL.Cadastro.Motorista.MotoristaHistorico> List = new List<SistranMODEL.Cadastro.Motorista.MotoristaHistorico>();


                    foreach (DataRow item in dt.Rows)
                    {
                        SistranMODEL.Cadastro.Motorista.MotoristaHistorico itemL = new SistranMODEL.Cadastro.Motorista.MotoristaHistorico();
                        itemL.IdMotorista = Convert.ToInt32(item["IdMotorista"]);
                        itemL.IdMotoristaHistorico = Convert.ToInt32(item["IdMotoristaHistorico"]);
                        itemL.IDUsuario = Convert.ToInt32(item["IDUsuario"]);
                        itemL.NomeUsuario = item["NomeUsuario"].ToString();
                        itemL.DataDeCadastro = Convert.ToDateTime(item["DataDeCadastro"]);
                        itemL.Historico = item["Historico"].ToString();
                        List.Add(itemL);
                    }
                    return List;
                }

                public void inserir(SistranMODEL.Cadastro.Motorista.MotoristaHistorico oMotHst)
                {
                    string sql = " INSERT INTO MOTORISTAHISTORICO ";
                    sql += " ( ";
                    sql += " IdMotoristaHistorico, ";
                    sql += " IdMotorista, ";
                    sql += " Historico, ";
                    sql += " DataDeCadastro, ";
                    sql += " IDUsuario ";
                    sql += " ) ";
                    sql += " VALUES ";
                    sql += " ( ";
                    sql += Sistran.Library.GetDataTables.RetornarIdTabela("MOTORISTAHISTORICO") + " , ";
                    sql += oMotHst.IdMotorista + ", ";
                    sql += " '" + oMotHst.Historico + "', ";
                    sql += " getdate(), ";
                    sql += oMotHst.IDUsuario;
                    sql += " ) ";

                    Sistran.Library.GetDataTables.ExecutarSemRetorno(sql, "");
                }
            }

            public class MotoristaFilial
            {
                public void Inserir(int idMotorista, ListBox ListDeFiliais)
                {
                    DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                    DbConnection cn = factory.CreateConnection();
                    DbCommand cd = factory.CreateCommand();
                    cn.ConnectionString = HttpContext.Current.Session["ConnLogin"].ToString();


                    cd.Connection = cn;
                    cn.Open();
                    DbTransaction oTrans;
                    oTrans = cn.BeginTransaction();

                    try
                    {
                        for (int i = 0; i < ListDeFiliais.Items.Count; i++)
                        {
                           // string ID = Sistran.Library.GetDataTables.RetornarIdTabela("MOTORISTAFILIAL");

                            //verificar e inserir
                            string sql = "SELECT COUNT(*) FROM MOTORISTAFILIAL WHERE IDFILIAL=" + ListDeFiliais.Items[i].Value + " AND IDMOTORISTA=" + idMotorista.ToString();
                            cd.CommandText = sql;
                            cd.CommandType = CommandType.Text;
                            cd.Transaction = oTrans;
                            
                            if (Convert.ToInt32(cd.ExecuteScalar())==0)
                            {
                                string ids = Sistran.Library.GetDataTables.RetornarIdTabela("IDMOTORISTAFILIAL");
                                sql = "INSERT INTO MOTORISTAFILIAL (IDMOTORISTAFILIAL,IDMOTORISTA,IDFILIAL) VALUES ("+ ids +"," + idMotorista.ToString() + " ," + ListDeFiliais.Items[i].Value + ")";
                                cd.CommandText = sql;
                                cd.CommandType = CommandType.Text;
                                cd.Transaction = oTrans;
                                cd.ExecuteNonQuery();
                      
                            }
                        }
                        oTrans.Commit();


                    }
                    catch (Exception EX)
                    {
                        oTrans.Rollback();
                        throw EX;
                    }
                    finally
                    {
                        cn.Close();
                    }

                }
            }
        }

        public class Proprietario
        {
            public DataTable Pesquisar(string nome, string cpf, int? IdProprietario)
            {
                string strsql = " SELECT * FROM PROPRIETARIO  P INNER JOIN CADASTRO C ON C.IDCADASTRO = P.IDPROPRIETARIO ";
                strsql += " LEFT JOIN CADASTROCOMPLEMENTO CC ON CC.IDCADASTRO = C.IDCADASTRO ";
                strsql += " WHERE  0=0";

                if (IdProprietario == null)
                {
                    strsql += " AND C.RAZAOSOCIALNOME LIKE '" + nome + "%' ";
                    strsql += " AND C.CNPJCPF LIKE '" + cpf + "%'";
                }
                else
                    strsql += " AND P.IDPROPRIETARIO =" + IdProprietario;

                strsql += " ORDER BY C.RAZAOSOCIALNOME";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
            }

            public DataTable Pesquisar(string cpf)
            {
                string strsql = " SELECT * FROM PROPRIETARIO  P INNER JOIN CADASTRO C ON C.IDCADASTRO = P.IDPROPRIETARIO ";
                strsql += " WHERE  0=0";
                strsql += " AND C.CNPJCPF = '" + cpf + "'";

                return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
            }

            public DataTable CarregarReportProprietario()
            {
                string strsql = "  SELECT *  ";
                strsql += " FROM PROPRIETARIO MOT ";
                strsql += " INNER JOIN Cadastro cAD ON cAD.IDCadastro = MOT.IDPROPRIETARIO ";
                strsql += " ORDER BY cAD.RAZAOSOCIALNOME ";

                return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
            }

            private string MontarSql(string comando, string tipo, string endereco, string IDCADASTROCONTATOENDERECO)
            {

                if (comando.IndexOf("INSERT") >= 0)
                {
                    comando = comando.Replace("@ID", Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROCONTATOENDERECO"));
                    comando = comando.Replace("@TIPO", tipo);
                }
                else
                {
                    comando = comando.Replace("@IDCADASTROCONTATOENDERECO", IDCADASTROCONTATOENDERECO);
                }

                comando = comando.Replace("@ENDERECO", endereco);
                return comando;
            }

            public void GravarTelefonesProprietario(SistranMODEL.Cadastro.Proprietario o)
            {

                string sqlInsert = " INSERT INTO CADASTROCONTATOENDERECO ";
                sqlInsert += " ( ";
                sqlInsert += " IDCADASTROCONTATOENDERECO, ";
                sqlInsert += " IDCADASTRO, ";
                sqlInsert += " IDCADASTROTIPODECONTATO, ";
                sqlInsert += " ENDERECO ";
                sqlInsert += " ) ";
                sqlInsert += " VALUES ";
                sqlInsert += " ( ";
                sqlInsert += "@ID" + " , ";
                sqlInsert += o.IDProprietario + " , ";
                sqlInsert += "(SELECT IDCADASTROTIPODECONTATO FROM CADASTROTIPODECONTATO WHERE NOME='@TIPO'  ), ";
                sqlInsert += " '@ENDERECO' ";
                sqlInsert += " )";

                string sqlUpdate = "UPDATE CADASTROCONTATOENDERECO SET ENDERECO = '@ENDERECO' WHERE IDCADASTROCONTATOENDERECO = @IDCADASTROCONTATOENDERECO";

                string[] tel = new string[4];
                tel[0] = "TELEFONE RESIDENCIAL";
                tel[1] = "TELEFONE DE RECADO";
                tel[2] = "CELULAR";
                tel[3] = "NEXTEL";


                for (int i = 0; i < tel.Length; i++)
                {
                    switch (tel[i])
                    {
                        case "TELEFONE RESIDENCIAL":
                            if (o.TelefoneRes != "" && (o.IDTelefoneRes == "" || o.IDTelefoneRes == "0"))
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlInsert, tel[i], o.TelefoneRes, null), "");
                            }
                            else if (o.TelefoneRes != "")
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlUpdate, tel[i], o.TelefoneRes, o.IDTelefoneRes.ToString()), "");
                            }
                            break;

                        case "TELEFONE DE RECADO":
                            if (o.TelefoneRecado != "" && (o.IDTelefoneRecado == "" || o.IDTelefoneRecado == "0"))
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlInsert, tel[i], o.TelefoneRecado, null), "");
                            }
                            else if (o.TelefoneRecado != "")
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlUpdate, tel[i], o.TelefoneRecado, o.IDTelefoneRecado.ToString()), "");
                            }
                            break;

                        case "CELULAR":
                            if (o.TelefoneCel != "" && (o.IDTelefoneCel == "" || o.IDTelefoneCel == "0"))
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlInsert, tel[i], o.TelefoneCel, null), "");
                            }
                            else if (o.TelefoneCel != "")
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlUpdate, tel[i], o.TelefoneCel, o.IDTelefoneCel.ToString()), "");
                            }
                            break;

                        case "NEXTEL":
                            if (o.TelefoneNextel != "" && (o.IDTelefoneNextel == "" || o.IDTelefoneNextel == "0"))
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlInsert, tel[i], o.TelefoneNextel, null), "");
                            }
                            else if (o.IDTelefoneNextel != "")
                            {
                                Sistran.Library.GetDataTables.ExecutarSemRetorno(MontarSql(sqlUpdate, tel[i], o.TelefoneNextel, o.IDTelefoneNextel.ToString()), "");
                            }
                            break;
                    }
                }
            }
        }

        public class Transportadora
        {
            public DataTable Pesquisar(int? idTransportadora, string cpf, string nome)
            {
                string strsql = " SELECT * FROM Transportadora T ";
                strsql += " INNER JOIN Cadastro C ON C.IDCadastro = T.IDTransportadora";
                strsql += " INNER JOIN ContaContabil CC ON CC.IDContaContabil = T.IDContaContabil ";
                strsql += " WHERE C.CNPJCPF LIKE '" + cpf + "%' ";
                strsql += idTransportadora == null ? "" : " AND T.IDTransportadora=" + idTransportadora.ToString();
                strsql += nome != "" ? " AND C.RazaoSocialNome like '" + nome + "%'" : "";

                strsql += "  ORDER BY C.RazaoSocialNome";

                return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
            }

            public DataTable CarregarCboCC()
            {
                string strsql = " SELECT CONTA + ' - ' + NOME NOME, IDCONTACONTABIL FROM CONTACONTABIL ORDER BY CONTA ";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");
            }

        }
    }
}