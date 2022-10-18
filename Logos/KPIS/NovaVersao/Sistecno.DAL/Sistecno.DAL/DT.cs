using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Sistecno.DAL
{
    public class DT
    {
        public DataTable RetornarInfoEletronico(int iddt, string cnx)
        {
            string strsql = "SELECT * FROM DTELETRONICO WHERE IDDT = " + iddt;
           return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
        }

        public int CriarDt(DAL.Models.Romaneio rom, List<DAL.Models.RomaneioDocumento> ldocs, DAL.Models.DT dt, int idEmpresa, List<string> CtesExcluidos, string cnx)
        {
            SqlConnection conn = new SqlConnection(cnx);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();

            string strsql = "";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.Transaction = trans;

            try
            {

                #region Insere as informações

                #region 1) Romaneio
                int idRomaneio = 0;

                if (rom.IDRomaneio == 0)
                {
                    idRomaneio = DAL.BD.cDb.RetornarIDTabela(cnx, "Romaneio");
                    strsql = "INSERT INTO ROMANEIO(IDROMANEIO, IDFILIAL,IDUSUARIO,EMISSAO,TIPO,DIVISAO,CONFERENCIA,OBSERVACAO1,SITUACAO,ANDAMENTO,SEPARADOPOR) ";
                    strsql += "  VALUES ( ";
                    strsql += idRomaneio + ", ";
                    strsql += rom.IDFilial + "  , ";
                    strsql += rom.IDUsuario + "  , ";
                    strsql += "  GETDATE(), ";
                    strsql += "  '" + rom.Tipo + "',";
                    strsql += "  '" + rom.Divisao + "', ";
                    strsql += "  'NOTA FISCAL', ";
                    strsql += "  '" + rom.Observacao1 + "', ";
                    strsql += "  '" + rom.Situacao + "', ";
                    strsql += "  '" + rom.Andamento + "', ";
                    strsql += "  'DOCUMENTO' ";
                    strsql += " )  ";
                }
                else
                {
                    idRomaneio = rom.IDRomaneio;
                    strsql = "UPDATE ROMANEIO SET OBSERVACAO1='" + rom.Observacao1 + "', IDUSUARIO=" + rom.IDUsuario + " WHERE IDROMANEIO=" + rom.IDRomaneio;
                }
                cmd.CommandText = strsql;
                cmd.ExecuteNonQuery();

                #endregion

                #region 2) DocumentoRomaneio

                if (rom.IDRomaneio > 0)
                {
                    //DOCUMENTO EXCLUIDO DO ROMANEIO
                    //exclui os ctes
                    strsql = "";
                    for (int i = 0; i < CtesExcluidos.Count; i++)
                    {
                        strsql = "UPDATE DOCUMENTOFILIAL SET SITUACAO='AGUARDANDO EMBARQUE', DATA=GETDATE() WHERE IDDOCUMENTO=" + CtesExcluidos[i] + "; ";

                        strsql += "DELETE FROM ROMANEIODOCUMENTO WHERE IDDOCUMENTO = " + CtesExcluidos[i] + "; ";

                        int IdDocOco = DAL.BD.cDb.RetornarIDTabela(cnx, "DOCUMENTOOCORRENCIA");
                        strsql += " INSERT INTO DOCUMENTOOCORRENCIA ( ";
                        strsql += " IDDocumentoOcorrencia, ";
                        strsql += " IdRomaneio, ";
                        strsql += " IDDocumento,";
                        strsql += " IDFilial,";
                        strsql += " IDOcorrencia,";
                        strsql += " DataOcorrencia,";
                        strsql += " Descricao,";
                        strsql += " Sistema";
                        strsql += " ) VALUES (";
                        strsql += IdDocOco + " ,";
                        strsql +=  "NULL ,";
                        strsql += CtesExcluidos[i] + " ,";
                        strsql +=  rom.IDFilial+" ,";
                        strsql += " NULL,";
                        strsql += " GETDATE(),";
                        strsql += " 'DOCUMENTO EXCLUIDO DO ROMANEIO',";
                        strsql += " 'SIM')";                       

                        cmd.CommandText = strsql;
                        cmd.ExecuteNonQuery();
                    }
                }


                for (int i = 0; i < ldocs.Count; i++)
                {
                    strsql = "SELECT COUNT(*) FROM ROMANEIODOCUMENTO WHERE IDDOCUMENTO=" + ldocs[i].IDDocumento;
                    cmd.CommandText = strsql;
                  int x=  int.Parse( cmd.ExecuteScalar().ToString());

                  if (x > 0)
                      continue;

                    int idRomaneioDocumento = DAL.BD.cDb.RetornarIDTabela(cnx, "RomaneioDocumento");

                    strsql = " INSERT INTO ROMANEIODOCUMENTO (IDROMANEIODOCUMENTO,IDROMANEIO,IDDOCUMENTO,VOLUMES,PESO,PESOCUBADO,CUBAGEM,VALORDODOCUMENTO,STATUS,IDDOCUMENTOVERIFICADO,VALORDOFRETE) VALUES (";
                    strsql += idRomaneioDocumento + " , ";
                    strsql += idRomaneio + " , ";
                    strsql += ldocs[i].IDDocumento + " , ";
                    strsql += ldocs[i].Volumes.ToString().Replace(",", ".") + " , ";
                    strsql += ldocs[i].Peso.ToString().Replace(",", ".") + " , ";
                    strsql += ldocs[i].PesoCubado.ToString().Replace(",", ".") + " , ";
                    strsql += ldocs[i].PesoCubado.ToString().Replace(",", ".") + " , ";
                    strsql += ldocs[i].ValorDoDocumento.ToString().Replace(",", ".") + " , ";
                    strsql += " 'OK', ";
                    strsql += " NULL, ";
                    strsql += " NULL ";
                    strsql += " )";

                    cmd.CommandText = strsql;
                    cmd.ExecuteNonQuery();

                    strsql = "UPDATE DOCUMENTOFILIAL SET SITUACAO='MERCADORIA EMBARCADA', DATA=GETDATE() WHERE IDDOCUMENTO=" + ldocs[i].IDDocumento;
                    cmd.CommandText = strsql;
                    cmd.ExecuteNonQuery();

                    int IdDocOco = DAL.BD.cDb.RetornarIDTabela(cnx, "DOCUMENTOOCORRENCIA");
                    strsql = " INSERT INTO DOCUMENTOOCORRENCIA ( ";
                    strsql += " IDDocumentoOcorrencia, ";
                    strsql += " IdRomaneio, ";
                    strsql += " IDDocumento,";
                    strsql += " IDFilial,";
                    strsql += " IDOcorrencia,";
                    strsql += " DataOcorrencia,";
                    strsql += " Descricao,";
                    strsql += " Sistema";
                    strsql += " ) VALUES (";
                    strsql += IdDocOco + " ,";
                    strsql += idRomaneio + " ,";
                    strsql += ldocs[i].IDDocumento + " ,";
                    strsql += rom.IDFilial + " ,";
                    strsql += " NULL,";
                    strsql += " NULL,";
                    strsql += " 'DOCUMENTO EM ROMANEIO',";
                    strsql += " 'SIM')";
                    cmd.CommandText = strsql;
                    cmd.ExecuteNonQuery();

                }


                #endregion

                #region 3) DT
                int iddt = 0;
                if (dt.IDDT == 0)
                {
                    iddt = DAL.BD.cDb.RetornarIDTabela(cnx, "DT");

                    strsql = " INSERT INTO DT (IDDT,IDFILIAL,NUMERO,IDDTTIPO,IDFILIALDESTINO,IDTRANSPORTADORA,IDREDESPACHO,IDPRIMEIROVEICULO,IDSEGUNDOVEICULO,IDPRIMEIROMOTORISTA,IDSEGUNDOMOTORISTA,IDCADASTROTITULAR, ";
                    strsql += " IDPROPRIETARIOPRIMEIROVEICULO,IDPROPRIETARIOSEGUNDOVEICULO,IDUSUARIOEMITIU,IDUSUARIOBAIXOU,IDMODAL,IDTIPODEMONITORAMENTO,IDTIPODEESCOLTA,IDEMPRESAESCOLTA,IDEMPRESAMONITORAMENTO,IDCIDADEDEORIGEM,IDCIDADEDEDESTINO, ";
                    strsql += " IDRPCI,IDRASTREADOR,CADASTRO,EMISSAO,BAIXADO,DATADESAIDA,HORADESAIDA,DATADECHEGADA,HORADECHEGADA,LACRES,PALLETSEXPEDIDO,PALLETSCHEP,PALLETSPBR,VALORDANOTA,VOLUMES,PESOLIQUIDO,PESOCUBADO,PESOBRUTO,VALORFRETECTR, ";
                    strsql += " VALORICMSCTR,CREDITOVALORDOSERVICO,CREDITOAGENCIAMENTO,CREDITOPEDAGIO,CREDITOCARGA,CREDITODESCARGA,CREDITODIARIA,CREDITOCOLETA,CREDITOENTREGA,CREDITOAJUDANTE,CREDITOADICIONAL,CREDITOOUTROS,DEBITOSEGURO,DEBITOOUTROS, ";
                    strsql += " DEBITOADIANTAMENTO,SUBTOTAL,SALDOARECEBER,KMINICIAL,KMFINAL,KMTOTAL,ATIVO,IMPRESSO,SITUACAO,ANDAMENTO,IDPORTARIA,CREDITOSFORADOCALCULO,DEBITOSFORADOCALCULO,SITUACAOIMPRESSAO,VOLUMESCOMFATOR,IDAGREGADO,ENTREGA,GRUPOS, ";
                    strsql += " SETOR,CHAPATEX,SITUACAOFATURAMENTO,IDDTTIPORE,CIOT) VALUES ( ";

                    strsql += iddt + ", ";
                    strsql += dt.IDFilial + " , ";
                    strsql += "'" + Sistecno.DAL.Documento.Numerador.RetornarNumerador(idEmpresa, dt.IDFilial, "DT", "", cnx) + "', ";
                    strsql += dt.IDDTTipo + " , ";
                    strsql += dt.IDFilialDestino + " , ";
                    strsql += (dt.IDTransportadora==null?"NULL": dt.IDTransportadora.ToString()) + " , ";
                    strsql += " null, ";
                    strsql += dt.IDPrimeiroVeiculo + " , ";
                    strsql += (dt.IDSegundoVeiculo == null ? "NULL" : dt.IDSegundoVeiculo.ToString()) + ", ";
                    strsql += dt.IDPrimeiroMotorista + ", ";
                    strsql += (dt.IDSegundoMotorista == null ? "NULL" : dt.IDSegundoMotorista.ToString()) + ", ";
                    strsql += dt.IDCadastroTitular + ", ";
                    strsql += dt.IDProprietarioPrimeiroVeiculo + ", ";
                    strsql += (dt.IDProprietarioSegundoVeiculo == null ? "NULL" : dt.IDProprietarioSegundoVeiculo.ToString()) + ", ";
                    strsql += dt.IDUsuarioEmitiu + " , ";
                    strsql += " NULL, ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += dt.IDCidadeDeOrigem + ", ";
                    strsql += dt.IDCidadeDeDestino + ", ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " getdate(), ";
                    strsql += " getdate(), ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += "'" + dt.Lacres + "', ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += dt.ValorDaNota.ToString().Replace(",", ".") + ", ";
                    strsql += dt.Volumes.ToString().Replace(",", ".") + ", ";
                    strsql += dt.PesoLiquido.ToString().Replace(",", ".") + ", ";
                    strsql += dt.PesoCubado.ToString().Replace(",", ".") + ", ";
                    strsql += dt.PesoBruto.ToString().Replace(",", ".") + ", ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 'SIM', ";
                    strsql += " 'NAO', ";
                    strsql += " '" + dt.Situacao + "', ";
                    strsql += " '" + dt.Andamento + "', ";
                    strsql += " null, ";
                    strsql += " 0, ";
                    strsql += " 0, ";
                    strsql += " 'DOCUMENTACAO LIBERADA', ";
                    strsql += " null, ";
                    strsql += (dt.IDAgregado == null ? "NULL" : dt.IDAgregado.ToString()) + ", ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " null, ";
                    strsql += " 'NORMAL', ";
                    strsql += " NULL, ";
                    strsql += " '' ";

                    strsql += ")";

                    dt.IDDT = iddt;
                }
                else
                {
                    strsql = "UPDATE DT SET IDTransportadora=" + (dt.IDTransportadora == null ? "NULL" : dt.IDTransportadora.ToString()) + ", ";
                    strsql += "IDPrimeiroVeiculo=" + dt.IDPrimeiroVeiculo + " , ";
                    strsql += "IDSegundoVeiculo=" + (dt.IDSegundoVeiculo == null ? "NULL" : dt.IDSegundoVeiculo.ToString()) + ", ";
                    strsql += "IDPrimeiroMotorista=" + dt.IDPrimeiroMotorista + ", ";
                    strsql += "IDSegundoMotorista=" + (dt.IDSegundoMotorista == null ? "NULL" : dt.IDSegundoMotorista.ToString()) + ", ";
                    strsql += "IDCadastroTitular=" + dt.IDCadastroTitular + ", ";
                    strsql += "IDProprietarioPrimeiroVeiculo=" + dt.IDProprietarioPrimeiroVeiculo + ", ";
                    strsql += "IDProprietarioSegundoVeiculo=" + (dt.IDProprietarioSegundoVeiculo == null ? "NULL" : dt.IDProprietarioSegundoVeiculo.ToString()) + ", ";
                    strsql += "IDUsuarioEmitiu=" + dt.IDUsuarioEmitiu + " , ";
                    strsql += "IDCidadeDeOrigem=" + dt.IDCidadeDeOrigem + ", ";
                    strsql += "IDCidadeDeDestino=" + dt.IDCidadeDeDestino + ", ";
                    strsql += "ValorDaNota=" + dt.ValorDaNota.ToString().Replace(",", ".") + ", ";
                    strsql += "Volumes=" + dt.Volumes.ToString().Replace(",", ".") + ", ";
                    strsql += "PesoLiquido=" + dt.PesoLiquido.ToString().Replace(",", ".") + ", ";
                    strsql += "PesoCubado=" + dt.PesoCubado.ToString().Replace(",", ".") + ", ";
                    strsql += "PesoBruto=" + dt.PesoBruto.ToString().Replace(",", ".") + ", ";
                    strsql += "IDAgregado=" + (dt.IDAgregado == null ? "Null" : dt.IDAgregado.ToString()) + ", ";
                    strsql += "IDDTTipo=" + dt.IDDTTipo + " , ";
                    strsql += "Lacres='" + dt.Lacres + "', ";
                    strsql += "CIOT= '' ";
                    strsql += " WHERE IDDT = " + dt.IDDT;
                }

                cmd.CommandText = strsql;
                cmd.ExecuteNonQuery();


                #endregion

                #region 4) DT Romaneio

                strsql = "SELECT COUNT(*) FROM DTROMANEIO WHERE IDROMANEIO=" + idRomaneio;
                cmd.CommandText = strsql;
                int xx = int.Parse(cmd.ExecuteScalar().ToString());

                if (xx == 0)
                {
                    if (iddt == 0)
                        iddt = dt.IDDT;


                    int iddtRomaneio = DAL.BD.cDb.RetornarIDTabela(cnx, "dtRomaneio");
                    strsql = "INSERT INTO DTROMANEIO VALUES (" + iddtRomaneio + ", " + iddt + "," + idRomaneio + " )";
                    cmd.CommandText = strsql;
                    cmd.ExecuteNonQuery();
                }
                #endregion

                #endregion


                trans.Commit();    

                return dt.IDDT;
            }
            catch (System.Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Dispose();
                trans.Dispose();
            }

        }

        public DataTable Retornar(string numero, int idFilial, string Andamento, string cnx)
        {
            string sql = " SELECT TOP 50 ";
            sql += " DT.IDDT CODIGO, ";
            sql += " DT.NUMERO, ";
            sql += " TP.NOME TIPO, ";
            sql += " convert(varchar(10),  DT.EMISSAO, 103) EMISSAO, ";
            sql += " PMOT.RAZAOSOCIALNOME AS MOTORISTA, ";
            sql += " PMOT.CNPJCPF AS [CNPJ/CPF]  , ";
            sql += " FILDEST.NOME, ";
            sql += " LEFT(TRA.RAZAOSOCIALNOME, 30) TRANSPORTADORA, ";
            sql += "  LEFT(CADTITULAR.RAZAOSOCIALNOME,30) AS [NOME TITULAR], UPPER(LEFT(DTE.STATUS, 30)) STATUS ";
            sql += " FROM   DT   ";
            sql += " LEFT JOIN CADASTRO AS TRA ON ( TRA.IDCADASTRO = DT.IDTRANSPORTADORA)   ";
            sql += " LEFT JOIN FILIAL AS FILDEST ON (FILDEST.IDFILIAL = DT.IDFILIALDESTINO)  ";
            sql += " LEFT JOIN VEICULO AS PVEICULO ON (PVEICULO.IDVEICULO = DT.IDPRIMEIROVEICULO )   ";
            sql += " LEFT JOIN CADASTRO AS PMOT ON (PMOT.IDCADASTRO = DT.IDPRIMEIROMOTORISTA )   ";
            sql += " LEFT JOIN CADASTRO AS CADTITULAR ON (CADTITULAR.IDCADASTRO = DT.IDCADASTROTITULAR)   ";
            sql += " LEFT JOIN DTTIPO TP ON TP.IDDTTIPO = DT.IDDTTIPO ";
            sql += " LEFT JOIN DTELETRONICO DTE ON DTE.IDDT = DT.IDDT ";
            sql += " WHERE  DT.IDFILIAL = " + idFilial;
            sql += " AND DT.ANDAMENTO = '" + Andamento + "' ";

            if (numero.Length > 0)
                sql += " and dt.numero like '" + numero + "%'";

            DataTable dt = DAL.BD.cDb.RetornarDataTable(sql, cnx);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["MOTORISTA"] = (dt.Rows[i]["MOTORISTA"].ToString().Length > 20 ? dt.Rows[i]["MOTORISTA"].ToString().Substring(0, 19) : dt.Rows[i]["MOTORISTA"].ToString());
                dt.Rows[i]["TRANSPORTADORA"] = (dt.Rows[i]["TRANSPORTADORA"].ToString().Length > 25 ? dt.Rows[i]["TRANSPORTADORA"].ToString().Substring(0, 24) : dt.Rows[i]["TRANSPORTADORA"].ToString());
            }

            return dt;

        }

        public DataTable RetornarDocumentosDoRomaneioByDT(int IdDt, string cnx)
        {
            string sql = " SELECT * FROM ROMANEIO R INNER JOIN DTROMANEIO DTR ON DTR.IDROMANEIO = R.IDROMANEIO INNER JOIN ROMANEIODOCUMENTO RD ON RD.IDROMANEIO = R.IDROMANEIO WHERE IDDT =  " + IdDt;
            return DAL.BD.cDb.RetornarDataTable(sql, cnx);
        }


        public DataTable RetornarByIdDt(int IdDt, string cnx)
        {
            string sql = "SELECT DT.*, ";
            sql += " Tra.RazaoSocialNome AS NOME_TRANSPORTADORA,         ";
            sql += " Tra.CnpjCpf AS CNPJCPF_TRANSPORTADORA,         ";
            sql += " FilDest.Nome AS NOME_FILIALDESTINO,         ";
            sql += " FilDest.NumeroDaFilial AS Numero_FilialDESTINO,         ";
            sql += " PVeiculo.Placa AS PV_PLACA,         ";
            sql += " SVeiculo.Placa AS SV_PLACA,         ";
            sql += " PMot.RazaoSocialNome AS NOME_PRI_MOTORISTA,         ";
            sql += " PMot.CnpjCpf AS CNPJCPF_PRI_MOTORISTA,         ";
            sql += " SMot.RazaoSocialNome AS NOME_SEG_MOTORISTA,       ";
            sql += " SMot.CnpjCpf AS CNPJCPF_SEG_MOTORISTA,         ";
            sql += " PVProp.RazaoSocialNome AS NOME_PRI_PROPRIETARIO,  ";
            sql += "  SVProp.RazaoSocialNome AS NOME_SEG_PROPRIETARIO,     ";
            sql += " Agr.RazaoSocialNome AS NOME_AGREGADO,         ";
            sql += "  Agr.CnpjCpf AS CNPJCPF_AGREGADO,         ";
            sql += " CadTitular.RazaoSocialNome AS NOME_TITULAR, ";
            sql += " UsEmit.Nome AS NOME_USU_EMITIU,         ";
            sql += " UsBaix.Nome AS NOME_USU_BAIXOU,         ";
            sql += " CadRdp.RazaoSocialNome Nome_Dedespacho,    ";
            sql += " CadRdp.CnpjCPf CnpjCPf_Dedespacho,         ";
            sql += " port.Placa  PlacaPortaria,         ";
            sql += " dt.impresso, FilDest.IdFilial ,  ";
            sql += " DTEL.* , LET.CStatus CodStatusLote, LET.Status StatusDoLote ";
            sql += " FROM   DT   ";
            sql += " LEFT JOIN       portaria port on (port.idPortaria = dt.idPortaria)   ";
            sql += " LEFT JOIN       Redespacho rdp on (rdp.idRedespacho = dt.idRedespacho)   ";
            sql += " LEFT JOIN       Cadastro CadRdp on (cadRdp.idCadastro = dt.IdRedespacho)   ";
            sql += " LEFT JOIN       Cadastro AS Tra ON ( Tra.IDCadastro = DT.IDTransportadora)   ";
            sql += " LEFT JOIN       Cadastro AS Agr on (Agr.IDCadastro = DT.IDAgregado)   ";
            sql += " LEFT JOIN       Filial AS FilDest ON (FilDest.IDFilial = DT.IDFilialDestino)   ";
            sql += " LEFT JOIN       Veiculo AS PVeiculo ON (PVeiculo.IDVeiculo = DT.IDPrimeiroVeiculo )   ";
            sql += " LEFT JOIN       Veiculo AS SVeiculo ON (SVeiculo.IDVeiculo = DT.IDSegundoVeiculo )   ";
            sql += " LEFT JOIN       Cadastro AS PMot ON (PMot.IDCadastro = DT.IDPrimeiroMotorista )   ";
            sql += " LEFT JOIN       Cadastro AS SMot ON ( SMot.IDCadastro = DT.IDSegundoMotorista )   ";
            sql += " LEFT JOIN       Cadastro AS PVProp ON ( PVProp.IDCadastro = DT.IDProprietarioPrimeiroVeiculo)   ";
            sql += " LEFT JOIN       Cadastro AS SVProp ON (SVProp.IDCadastro = DT.IDProprietarioSegundoVeiculo)   ";
            sql += " LEFT JOIN       Usuario AS UsEmit ON (UsEmit.IDUsuario = DT.IDUsuarioEmitiu )   ";
            sql += " LEFT JOIN       Usuario AS UsBaix ON (UsBaix.IDUsuario = DT.IDUsuarioBaixou  )   ";
            sql += " LEFT JOIN       Cadastro AS CadTitular ON (CadTitular.IDCadastro = DT.IDCadastroTitular)  ";
            sql += " LEFT JOIN       DTELETRONICO DTEL ON DTEL.IDDT = DT.IDDT "; ;
            sql += " LEFT JOIN       LOTEELETRONICO LET ON LET.IDLOTEELETRONICO = DTEL.IDLOTEELETRONICO";
            sql += " where DT.IDDT = " + IdDt;

            return DAL.BD.cDb.RetornarDataTable(sql, cnx);

        }


        public class Tipo
        {
            public DataTable Retornar(string cnx)
            {
                string sql = " SELECT * FROM DTTIPO ORDER BY NOME";
                return DAL.BD.cDb.RetornarDataTable(sql, cnx);
            }

        }
    }
}