using System.Text;
using System.Data;
using System;
using System.Web;
using System.Collections.Generic;

namespace SistranDAO
{
    public class Veiculo
    {
        public DataTable ListarMonitoramento(string data, string filiais, string cliente, string ordem, string Conn)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append(" Select    fil.Nome filial, Dt.Numero,   Dt.Numero NDt,   Dt.Emissao,   Vei.Placa, Dt.IDDT, ");
            strsql.Append("Coalesce(Count(Doc.Numero),0) As Documentos, ");
            strsql.Append("Coalesce(Count(Distinct Doc.Endereco),0) As TotalDeServicos,   ");
            strsql.Append("Coalesce(Count(Case when Doc.IdDocumentoOcorrencia is Not Null then Doc.Numero else Null end),0) As Ocorrencias,  ");
            strsql.Append("Coalesce(Count(Case when Doc.DataDeConclusao is Not Null then Doc.Numero else Null end),0) As DocumentosConcluido,   ");
            strsql.Append("Coalesce(Count(Case when Doc.IdDocumentoOcorrencia is Not Null then Doc.Numero else Null end),0) -   Coalesce(Count(Case when Doc.DataDeConclusao is Not Null then Doc.Numero else Null end),0) As DocumentosNaoFinalizado,  ");
            strsql.Append("Coalesce(Count(Doc.Numero),0)  -   Coalesce(Count(Case when Doc.IdDocumentoOcorrencia is Not Null then Doc.Numero else Null end),0) As Pendentes,   ");
            strsql.Append("Coalesce(Sum(Doc.PesoBruto),0) As PesoBruto,   Coalesce(Sum(Doc.MetragemCubica),0) As MetragemCubica,   Coalesce(Sum(Doc.PesoCubado),0) As PesoCubado,  ");
            strsql.Append("Coalesce(Sum(Doc.Volumes),0) as Volumes, Cad.RazaoSocialNome as Motorista");
            strsql.Append(" From Dt Dt    ");
            strsql.Append(" Left Join DTRomaneio DTRom on (DtRom.IdDt=Dt.IdDt)    ");
            strsql.Append(" Left Join Romaneio Rom on (Rom.IdRomaneio=DtRom.IdRomaneio)    ");
            strsql.Append(" Left Join RomaneioDocumento RomDoc on (RomDoc.IdRomaneio=Rom.IdRomaneio)    ");
            strsql.Append(" Left Join Documento Doc on (Doc.IdDocumento=RomDoc.IdDocumento)    ");
            strsql.Append(" Left Join Veiculo Vei on (Vei.IdVeiculo=Dt.IdPrimeiroVeiculo)  ");
            strsql.Append(" Left join Cadastro Cad on Vei.IDMotorista = Cad.IDCadastro");
            strsql.Append(" Left join Filial fil on DT.IDFilial = fil.IDFilial");
            strsql.Append(" where  @filial ");
            strsql.Append(" and  cast(DT.DATADESAIDA as date) = cast(CONVERT(datetime, '@data',103) as date) ");
            strsql.Append(" and Vei.Placa is not null ");
            strsql.Append(" and (Doc.IdRemetente in (@cliente)  or IDCliente in(@cliente)) ");
            strsql.Append(" Group By    fil.Nome, Dt.Numero,   Dt.IdDt,   Dt.Emissao,   Vei.Placa, Dt.IDDT, Cad.RazaoSocialNome  ");
            strsql.Append(" Having (Coalesce(Count(Doc.Numero),0) >0)  ");
            strsql.Append("  @ordem ");

            strsql.Replace("@data", data);

            if (filiais.Trim() != "")
                strsql.Replace("@filial", " Dt.IdFilial in (" + filiais + ") ");
            else
                strsql.Replace("@filial", " 0=0 ");

            strsql.Replace("@cliente", cliente);
            strsql.Replace("@ordem", ordem);
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), Conn);
        }

        public DataTable Pesquisar(int? idVeiculo, string placa, bool AnttVencido, bool LicencVencido)
        {
            /*
            string strsql = " SELECT DISTINCT V.ANTTVENCIMENTO, VL.DATALIMITE,";
            strsql += " VMOD.NOME MODELO,  VM.NOME MARCA, VT.NOME TIPO, ";
            strsql += " IDVEICULO, V.IDVEICULOMODELO, VM.IDVEICULOMARCA, V.IDVEICULOTIPO,  ";
            strsql += " IDVEICULORASTREADOR, V.IDCIDADE, V.IDPROPRIETARIO, V.IDMOTORISTA, IDCADASTROTITULAR, CADASTRO, PLACA, RENAVAM, CHASSI, ANO, COR,  ";
            strsql += " V.CAPACIDADEDECARGAKG, V.CAPACIDADEDECARGAM3 , QUATIDADEDEEIXOS, CATEGORIASDECNHPERMITIDAS, ANTT,ANTTVENCIMENTO, NUMEROSERIEEQUIPAMENTO, CM.RAZAOSOCIALNOME NOMEMOTORISTA,  CM.CNPJCPF CPFMOTORISTA, CP.RAZAOSOCIALNOME NOMEPROPRIETARIO, CP.CNPJCPF CPFPROPRIETARIO   ";
            strsql += " FROM VEICULO V  ";
            strsql += " LEFT JOIN MOTORISTA M ON M.IDMOTORISTA = V.IDMOTORISTA  ";
            strsql += " LEFT JOIN CADASTRO CM ON CM.IDCADASTRO = V.IDMOTORISTA   ";
            strsql += " LEFT JOIN PROPRIETARIO P ON  P.IDPROPRIETARIO = V.IDPROPRIETARIO  ";
            strsql += " LEFT JOIN CADASTRO CP ON CP.IDCADASTRO = P.IDPROPRIETARIO   ";
            strsql += " LEFT JOIN VEICULOMODELO VMOD ON VMOD.IDVEICULOMODELO = V.IDVEICULOMODELO ";
            strsql += " LEFT JOIN VEICULOMODELO VM ON VM.IDVEICULOMARCA = V.IDVEICULOMODELO ";
            strsql += " LEFT JOIN VEICULOTIPO VT ON VT.IDVEICULOTIPO = V.IDVEICULOTIPO ";
            strsql += "  LEFT JOIN  VEICULOLICENCIAMENTO VL ON VL.IDVEICULOTIPO = V.IDVEICULOTIPO ";


            strsql += " IDVEICULO,";
            strsql += " V.IDVEICULOMODELO, VM.IDVEICULOMARCA, VMOD.Nome MODELO,  VM.Nome MARCA,";
            strsql += " IDVEICULOTIPO,";
            strsql += " IDVEICULORASTREADOR,";
            strsql += " V.IDCIDADE,";
            strsql += " V.IDPROPRIETARIO,";
            strsql += " V.IDMOTORISTA,";
            strsql += " IDCADASTROTITULAR,";
            strsql += " CADASTRO,";
            strsql += " PLACA,";
            strsql += " RENAVAM,";
            strsql += " CHASSI,";
            strsql += " ANO,";
            strsql += " COR,";
            strsql += " CAPACIDADEDECARGAKG,";
            strsql += " CAPACIDADEDECARGAM3,";
            strsql += " QUATIDADEDEEIXOS,";
            strsql += " CATEGORIASDECNHPERMITIDAS,";
            strsql += " ANTT,ANTTVENCIMENTO,";
            strsql += " NUMEROSERIEEQUIPAMENTO,";
            strsql += " CM.RAZAOSOCIALNOME NOMEMOTORISTA, ";
            strsql += " CM.CNPJCPF CPFMOTORISTA,";
            strsql += " CP.RAZAOSOCIALNOME NOMEPROPRIETARIO,";
            strsql += " CP.CNPJCPF CPFPROPRIETARIO";
            strsql += "  FROM VEICULO V";
            strsql += " LEFT JOIN MOTORISTA M ON M.IDMOTORISTA = V.IDMOTORISTA";
            strsql += " LEFT JOIN CADASTRO CM ON CM.IDCADASTRO = V.IDMOTORISTA";
            strsql += " LEFT JOIN PROPRIETARIO P ON  P.IDPROPRIETARIO = V.IDPROPRIETARIO";
            strsql += " LEFT JOIN CADASTRO CP ON CP.IDCADASTRO = P.IDPROPRIETARIO";
            strsql += " LEFT JOIN VEICULOMODELO VMOD ON VMOD.IDVEICULOMODELO = V.IDVEICULOMODELO";
            strsql += " LEFT JOIN VEICULOMODELO VM ON VM.IDVEICULOMARCA = V.IDVEICULOMODELO ";
             */

            string strsql = " SELECT ";
            strsql += " DISTINCT ";
            strsql += " V.ANTTVENCIMENTO, ";
            strsql += " VMOD.NOME MODELO,  ";
            strsql += " VM.NOME MARCA, ";
            strsql += " VT.NOME TIPO,  ";
            strsql += " IDVEICULO, ";
            strsql += " V.IDVEICULOMODELO, ";
            strsql += " VM.IDVEICULOMARCA, ";
            strsql += " V.IDVEICULOTIPO,   ";
            strsql += " IDVEICULORASTREADOR, ";
            strsql += " V.IDCIDADE, ";
            strsql += " V.IDPROPRIETARIO,";
            strsql += " V.IDMOTORISTA, ";
            strsql += " IDCADASTROTITULAR, ";
            strsql += " CADASTRO, ";
            strsql += " PLACA, ";
            strsql += " RENAVAM, ";
            strsql += " CHASSI, ";
            strsql += " ANO, AnoModelo, ";
            strsql += " COR,   ";
            strsql += " V.CAPACIDADEDECARGAKG, ";
            strsql += " V.CAPACIDADEDECARGAM3 , ";
            strsql += " QUATIDADEDEEIXOS, ";
            strsql += " CATEGORIASDECNHPERMITIDAS, ";
            strsql += " ANTT,ANTTVENCIMENTO, ";
            strsql += " NUMEROSERIEEQUIPAMENTO,";
            strsql += " substring(CM.RAZAOSOCIALNOME, 0, 15) NOMEMOTORISTA,  ";
            strsql += " CM.CNPJCPF CPFMOTORISTA, ";
            strsql += " substring(CP.RAZAOSOCIALNOME, 0, 15) NOMEPROPRIETARIO, ";
            strsql += " CP.CNPJCPF CPFPROPRIETARIO,";
            strsql += " V.DATADELICENCIAMENTO,";
            strsql += " (SELECT TOP 1 DataLimite FROM VeiculoLicenciamento VL  WHERE VL.IdVeiculoTipo=V.IDVeiculoTipo AND VL.FINALDAPLACA LIKE '%' + RIGHT(RTRIM(LTRIM(V.PLACA)),1) +'%') AS DATALIMITE     ";
            strsql += " FROM VEICULO V   ";
            strsql += " LEFT JOIN MOTORISTA M ON M.IDMOTORISTA = V.IDMOTORISTA   ";
            strsql += " LEFT JOIN CADASTRO CM ON CM.IDCADASTRO = V.IDMOTORISTA    ";
            strsql += " LEFT JOIN PROPRIETARIO P ON  P.IDPROPRIETARIO = V.IDPROPRIETARIO   ";
            strsql += " LEFT JOIN CADASTRO CP ON CP.IDCADASTRO = P.IDPROPRIETARIO    ";
            strsql += " LEFT JOIN VEICULOMODELO VMOD ON VMOD.IDVEICULOMODELO = V.IDVEICULOMODELO  ";
            strsql += " LEFT JOIN VEICULOMARCA VM ON VM.IDVEICULOMARCA  = VMOD.IDVEICULOMARCA  ";
            strsql += " LEFT JOIN VEICULOTIPO VT ON VT.IDVEICULOTIPO = V.IDVEICULOTIPO";
            strsql += " WHERE PLACA LIKE '" + placa.Replace("___-____", "") + "%' " + (idVeiculo == null ? "" : "   AND IDVEICULO=" + idVeiculo.ToString());
            strsql += " AND PLACA <>''";

            if (AnttVencido)
                strsql += " AND (V.ANTTVENCIMENTO <=GETDATE() OR V.ANTTVENCIMENTO IS NULL  )";


            if (LicencVencido)
                strsql += " AND (V.DATADELICENCIAMENTO <= (SELECT TOP 1 DataLimite FROM VeiculoLicenciamento VL  WHERE VL.IdVeiculoTipo=V.IDVeiculoTipo AND VL.FINALDAPLACA LIKE '%' + RIGHT(RTRIM(LTRIM(V.PLACA)),1) +'%') OR V.DATADELICENCIAMENTO IS NULL)";

            strsql += " ORDER BY PLACA";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public int Inserir(int IDVEICULOMODELO, int IDVEICULOTIPO, int IDVEICULORASTREADOR, int IDCIDADE, int IDPROPRIETARIO, int IDMOTORISTA, string PLACA, string RENAVAN, string CHASSI, int ANO, string COR, decimal CAPACIDADEDECARGAKG, decimal CAPACIDADEDECARGAM3, decimal QUATIDADEDEEIXOS, string CATEGORIASDECNHPERMITIDAS, string ANTT, string NUMEROSERIEEQUIPAMENTO, DateTime VencAntt, DateTime DataLicenc, string MarcaDescricao, string anoModelo)
        {
            string strsql = " INSERT INTO Veiculo ";
            strsql += "  ( ";
            strsql += " IDVEICULO, ";
            strsql += " IDVEICULOMODELO, ";
            strsql += " IDVEICULOTIPO, ";
            strsql += " IDVEICULORASTREADOR, ";
            strsql += " IDCIDADE, ";
            strsql += " IDPROPRIETARIO, ";
            strsql += " IDMOTORISTA, ";
            strsql += " CADASTRO, ";
            strsql += " PLACA, ";
            strsql += " RENAVAM, ";
            strsql += " CHASSI, ";
            strsql += " ANO, anomodelo, ";
            strsql += " COR, ";
            strsql += " CAPACIDADEDECARGAKG, ";
            strsql += " CAPACIDADEDECARGAM3, ";
            strsql += " QUATIDADEDEEIXOS, ";
            strsql += " CATEGORIASDECNHPERMITIDAS, ";
            strsql += " ANTT, ";
            strsql += " ANTTVENCIMENTO, ";
            strsql += " NUMEROSERIEEQUIPAMENTO, DATADELICENCIAMENTO ";
            strsql += " ) ";
            strsql += " VALUES ";
            strsql += " ( ";

            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("Veiculo");
            strsql += ID + " , ";
            strsql += IDVEICULOMODELO + " , ";
            strsql += IDVEICULOTIPO + " , ";
            strsql += IDVEICULORASTREADOR + " , ";
            strsql += IDCIDADE + " , ";
            strsql += IDPROPRIETARIO + ", ";
            strsql += IDMOTORISTA + ", ";
            strsql += " GETDATE(), ";
            strsql += " '" + PLACA + "', ";
            strsql += " '" + RENAVAN + "', ";
            strsql += " '" + CHASSI + "', ";
            strsql += ANO + ", " + anoModelo + ",";
            strsql += " '" + COR.ToUpper() + "', ";
            strsql += CAPACIDADEDECARGAKG.ToString().Replace(",", ".") + ", ";
            strsql += CAPACIDADEDECARGAM3.ToString().Replace(",", ".") + ", ";
            strsql += QUATIDADEDEEIXOS.ToString().Replace(",", ".") + ", ";
            strsql += "'" + CATEGORIASDECNHPERMITIDAS + "', ";
            strsql += " '" + ANTT.ToUpper() + "', ";
            strsql += " CONVERT(DATETIME,'" + VencAntt + "',103), ";
            strsql += " '" + NUMEROSERIEEQUIPAMENTO.ToUpper() + "', ";
            strsql += " CONVERT(DATETIME,'" + DataLicenc + "',103) )";

            strsql += " SELECT ISNULL(MAX(IDVEICULO),0) FROM Veiculo   ";

            if (HttpContext.Current.Session["ConnLogin"].ToString().Contains("GrupoLogos") && HttpContext.Current.Session["AtivarBaseAntiga"].ToString() == "S")
            {
                /*SistranMODEL.Cadastro.Motorista lMot = PrepararMotorista(IDMOTORISTA);
                SistranMODEL.Cadastro.Proprietario lProp = PrepararProprietario(IDPROPRIETARIO);

                HttpContext.Current.Session["Conn"] = Sistran.Library.BaseAntigaConfig.Default["BDAntigo"].ToString();
                Sistran.Library.GetDataTables.TransacaoInserirVeiculoBaseAntiga(Sistran.Library.BaseAntigaConfig.Default["BDAntigo"].ToString(), 0, IDVEICULOMODELO, IDVEICULOTIPO, IDVEICULORASTREADOR, IDCIDADE, IDPROPRIETARIO, IDMOTORISTA, PLACA, RENAVAN, CHASSI, ANO, COR, CAPACIDADEDECARGAKG, CAPACIDADEDECARGAM3, QUATIDADEDEEIXOS, CATEGORIASDECNHPERMITIDAS, ANTT, NUMEROSERIEEQUIPAMENTO, VencAntt, DataLicenc, lMot, lProp, MarcaDescricao);
                HttpContext.Current.Session["Conn"] = null;*/
            }

            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
        }

        public int Inserir(int IDVEICULOMODELO, int IDVEICULOTIPO, int IDVEICULORASTREADOR, int IDCIDADE, int IDPROPRIETARIO, int IDMOTORISTA, string PLACA, string RENAVAN, string CHASSI, int ANO, string COR, decimal CAPACIDADEDECARGAKG, decimal CAPACIDADEDECARGAM3, decimal QUATIDADEDEEIXOS, string CATEGORIASDECNHPERMITIDAS, string ANTT, string NUMEROSERIEEQUIPAMENTO, DateTime VencAntt, DateTime DataLicenc, string MarcaDescricao, string anoModelo, List<SistranMODEL.Cadastro.CadastroReferencia> referenciasComPes)
        {
            string strsql = " INSERT INTO Veiculo ";
            strsql += "  ( ";
            strsql += " IDVEICULO, ";
            strsql += " IDVEICULOMODELO, ";
            strsql += " IDVEICULOTIPO, ";
            strsql += " IDVEICULORASTREADOR, ";
            strsql += " IDCIDADE, ";
            strsql += " IDPROPRIETARIO, ";
            strsql += " IDMOTORISTA, ";
            strsql += " CADASTRO, ";
            strsql += " PLACA, ";
            strsql += " RENAVAM, ";
            strsql += " CHASSI, ";
            strsql += " ANO, anomodelo, ";
            strsql += " COR, ";
            strsql += " CAPACIDADEDECARGAKG, ";
            strsql += " CAPACIDADEDECARGAM3, ";
            strsql += " QUATIDADEDEEIXOS, ";
            strsql += " CATEGORIASDECNHPERMITIDAS, ";
            strsql += " ANTT, ";
            strsql += " ANTTVENCIMENTO, ";
            strsql += " NUMEROSERIEEQUIPAMENTO, DATADELICENCIAMENTO ";
            strsql += " ) ";
            strsql += " VALUES ";
            strsql += " ( ";

            string ID = Sistran.Library.GetDataTables.RetornarIdTabela("Veiculo");
            strsql += ID + " , ";
            strsql += IDVEICULOMODELO + " , ";
            strsql += IDVEICULOTIPO + " , ";
            strsql += IDVEICULORASTREADOR + " , ";
            strsql += IDCIDADE + " , ";
            strsql += IDPROPRIETARIO + ", ";
            strsql += IDMOTORISTA + ", ";
            strsql += " GETDATE(), ";
            strsql += " '" + PLACA + "', ";
            strsql += " '" + RENAVAN + "', ";
            strsql += " '" + CHASSI + "', ";
            strsql += ANO + ", " + anoModelo + ",";
            strsql += " '" + COR.ToUpper() + "', ";
            strsql += CAPACIDADEDECARGAKG.ToString().Replace(",", ".") + ", ";
            strsql += CAPACIDADEDECARGAM3.ToString().Replace(",", ".") + ", ";
            strsql += QUATIDADEDEEIXOS.ToString().Replace(",", ".") + ", ";
            strsql += "'" + CATEGORIASDECNHPERMITIDAS + "', ";
            strsql += " '" + ANTT.ToUpper() + "', ";
            strsql += " CONVERT(DATETIME,'" + VencAntt + "',103), ";
            strsql += " '" + NUMEROSERIEEQUIPAMENTO.ToUpper() + "', ";
            strsql += " CONVERT(DATETIME,'" + DataLicenc + "',103) )";

            strsql += " SELECT ISNULL(MAX(IDVEICULO),0) FROM Veiculo   ";

            if (HttpContext.Current.Session["ConnLogin"].ToString().Contains("GrupoLogos") && HttpContext.Current.Session["AtivarBaseAntiga"].ToString() == "S")
            {
                /*SistranMODEL.Cadastro.Motorista lMot = PrepararMotorista(IDMOTORISTA);
                SistranMODEL.Cadastro.Proprietario lProp = PrepararProprietario(IDPROPRIETARIO);

                HttpContext.Current.Session["Conn"] = Sistran.Library.BaseAntigaConfig.Default["BDAntigo"].ToString();
                Sistran.Library.GetDataTables.TransacaoInserirVeiculoBaseAntiga(Sistran.Library.BaseAntigaConfig.Default["BDAntigo"].ToString(), 0, IDVEICULOMODELO, IDVEICULOTIPO, IDVEICULORASTREADOR, IDCIDADE, IDPROPRIETARIO, IDMOTORISTA, PLACA, RENAVAN, CHASSI, ANO, COR, CAPACIDADEDECARGAKG, CAPACIDADEDECARGAM3, QUATIDADEDEEIXOS, CATEGORIASDECNHPERMITIDAS, ANTT, NUMEROSERIEEQUIPAMENTO, VencAntt, DataLicenc, lMot, lProp, MarcaDescricao);
                HttpContext.Current.Session["Conn"] = null;*/
            }


            processarContatoReferencia(referenciasComPes, IDPROPRIETARIO);



            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
        }

        private void processarContatoReferencia(List<SistranMODEL.Cadastro.CadastroReferencia> referenciasComPes, int IdCadastro)
        {
            foreach (var item in referenciasComPes)
            {
                if (item.IDCadastroReferencia == 0 && item.Referencia != "")
                {

                    string ID = Sistran.Library.GetDataTables.RetornarIdTabela("CADASTROREFERENCIA");
                    string strsql = "INSERT INTO CADASTROREFERENCIA (IDCadastroReferencia, IDCadastro, TipoDeReferencia, Data, Referencia, Contato, Observacao)";
                    strsql += " VALUES ("+ID+", "+IdCadastro+", '"+ item.TipoDeReferencia+"', getdate(), '"+item.Referencia+"', '"+item.Contato+"', 'Captação')";
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
                }
                else
                {
                    string strsql = "UPDATE CADASTROREFERENCIA SET REFERENCIA ='"+item.Referencia+"', CONTATO='"+item.Contato+"' WHERE IDCadastroReferencia="+item.IDCadastroReferencia;
                    Sistran.Library.GetDataTables.ExecutarSemRetorno(strsql, "");
                }
            }
        }

        public void Update(int IDVEICULO, int IDVEICULOMODELO, int IDVEICULOTIPO, int IDVEICULORASTREADOR, int IDCIDADE, int IDPROPRIETARIO, int IDMOTORISTA, string PLACA, string RENAVAN, string CHASSI, int ANO, string COR, decimal CAPACIDADEDECARGAKG, decimal CAPACIDADEDECARGAM3, decimal QUATIDADEDEEIXOS, string CATEGORIASDECNHPERMITIDAS, string ANTT, string NUMEROSERIEEQUIPAMENTO, DateTime VenctoAntt, DateTime DataLiecenc, string MarcaDescricao, string AnoModelo)
        {
            string strsql = " UPDATE Veiculo ";
            strsql += "  SET ";

            strsql += " IDVEICULOMODELO =" + IDVEICULOMODELO.ToString() + " , ";
            strsql += " IDVEICULOTIPO = " + IDVEICULOTIPO + ", ";
            strsql += " IDVEICULORASTREADOR = " + IDVEICULORASTREADOR.ToString() + ", ";
            strsql += " IDCIDADE = " + IDCIDADE + ", ";
            strsql += " IDPROPRIETARIO = " + IDPROPRIETARIO + ", ";
            strsql += " IDMOTORISTA=" + IDMOTORISTA + ", ";
            strsql += " PLACA = '" + PLACA.ToUpper() + "', ";
            strsql += " RENAVAM = '" + RENAVAN.ToUpper() + "', ";
            strsql += " CHASSI= '" + CHASSI.ToUpper() + "', ";
            strsql += " ANO= " + ANO.ToString() + ", ANOMODELO= " + ANO.ToString() + ",";
            strsql += " COR= '" + COR.ToUpper() + "', ";
            strsql += " CAPACIDADEDECARGAKG =" + CAPACIDADEDECARGAKG.ToString().Replace(",", ".") + ", ";
            strsql += " CAPACIDADEDECARGAM3 =" + CAPACIDADEDECARGAM3.ToString().Replace(",", ".") + ", ";
            strsql += " QUATIDADEDEEIXOS = " + QUATIDADEDEEIXOS.ToString().Replace(",", ".") + ", ";
            strsql += " CATEGORIASDECNHPERMITIDAS= '" + CATEGORIASDECNHPERMITIDAS + "', ";
            strsql += " ANTT= '" + ANTT.ToUpper() + "', ";
            strsql += " ANTTVENCIMENTO= CONVERT(DATETIME,'" + VenctoAntt + "', 103), ";
            strsql += " DATADELICENCIAMENTO= CONVERT(DATETIME,'" + DataLiecenc + "', 103), ";
            strsql += " NUMEROSERIEEQUIPAMENTO = '" + NUMEROSERIEEQUIPAMENTO.ToUpper() + "' ";
            strsql += " WHERE IDVEICULO = " + IDVEICULO;
            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");


            if (HttpContext.Current.Session["ConnLogin"].ToString().Contains("GrupoLogos") && HttpContext.Current.Session["AtivarBaseAntiga"].ToString() == "S")
            {
               /* SistranMODEL.Cadastro.Motorista lMot = PrepararMotorista(IDMOTORISTA);
                SistranMODEL.Cadastro.Proprietario lProp = PrepararProprietario(IDPROPRIETARIO);

                HttpContext.Current.Session["Conn"] = Sistran.Library.BaseAntigaConfig.Default["BDAntigo"].ToString();
                Sistran.Library.GetDataTables.TransacaoInserirVeiculoBaseAntiga(Sistran.Library.BaseAntigaConfig.Default["BDAntigo"].ToString(), IDVEICULO, IDVEICULOMODELO, IDVEICULOTIPO, IDVEICULORASTREADOR, IDCIDADE, IDPROPRIETARIO, IDMOTORISTA, PLACA, RENAVAN, CHASSI, ANO, COR, CAPACIDADEDECARGAKG, CAPACIDADEDECARGAM3, QUATIDADEDEEIXOS, CATEGORIASDECNHPERMITIDAS, ANTT, NUMEROSERIEEQUIPAMENTO, VenctoAntt, DataLiecenc, lMot, lProp, MarcaDescricao);
                HttpContext.Current.Session["Conn"] = null;*/
            }
        }

        public void Update(int IDVEICULO, int IDVEICULOMODELO, int IDVEICULOTIPO, int IDVEICULORASTREADOR, int IDCIDADE, int IDPROPRIETARIO, int IDMOTORISTA, string PLACA, string RENAVAN, string CHASSI, int ANO, string COR, decimal CAPACIDADEDECARGAKG, decimal CAPACIDADEDECARGAM3, decimal QUATIDADEDEEIXOS, string CATEGORIASDECNHPERMITIDAS, string ANTT, string NUMEROSERIEEQUIPAMENTO, DateTime VenctoAntt, DateTime DataLiecenc, string MarcaDescricao, string AnoModelo , List<SistranMODEL.Cadastro.CadastroReferencia> referenciasComPes )
        {
            string strsql = " UPDATE Veiculo ";
            strsql += "  SET ";
            strsql += " IDVEICULOMODELO =" + IDVEICULOMODELO.ToString() + " , ";
            strsql += " IDVEICULOTIPO = " + IDVEICULOTIPO + ", ";
            strsql += " IDVEICULORASTREADOR = " + IDVEICULORASTREADOR.ToString() + ", ";
            strsql += " IDCIDADE = " + IDCIDADE + ", ";
            strsql += " IDPROPRIETARIO = " + IDPROPRIETARIO + ", ";
            strsql += " IDMOTORISTA=" + IDMOTORISTA + ", ";
            strsql += " PLACA = '" + PLACA.ToUpper() + "', ";
            strsql += " RENAVAM = '" + RENAVAN.ToUpper() + "', ";
            strsql += " CHASSI= '" + CHASSI.ToUpper() + "', ";
            strsql += " ANO= " + ANO.ToString() + ", ANOMODELO= " + ANO.ToString() + ",";
            strsql += " COR= '" + COR.ToUpper() + "', ";
            strsql += " CAPACIDADEDECARGAKG =" + CAPACIDADEDECARGAKG.ToString().Replace(",", ".") + ", ";
            strsql += " CAPACIDADEDECARGAM3 =" + CAPACIDADEDECARGAM3.ToString().Replace(",", ".") + ", ";
            strsql += " QUATIDADEDEEIXOS = " + QUATIDADEDEEIXOS.ToString().Replace(",", ".") + ", ";
            strsql += " CATEGORIASDECNHPERMITIDAS= '" + CATEGORIASDECNHPERMITIDAS + "', ";
            strsql += " ANTT= '" + ANTT.ToUpper() + "', ";
            strsql += " ANTTVENCIMENTO= CONVERT(DATETIME,'" + VenctoAntt + "', 103), ";
            strsql += " DATADELICENCIAMENTO= CONVERT(DATETIME,'" + DataLiecenc + "', 103), ";
            strsql += " NUMEROSERIEEQUIPAMENTO = '" + NUMEROSERIEEQUIPAMENTO.ToUpper() + "' ";
            strsql += " WHERE IDVEICULO = " + IDVEICULO;
            Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");

            processarContatoReferencia(referenciasComPes, IDPROPRIETARIO);

            if (HttpContext.Current.Session["ConnLogin"].ToString().Contains("GrupoLogos") && HttpContext.Current.Session["AtivarBaseAntiga"].ToString() == "S")
            {
                /* SistranMODEL.Cadastro.Motorista lMot = PrepararMotorista(IDMOTORISTA);
                 SistranMODEL.Cadastro.Proprietario lProp = PrepararProprietario(IDPROPRIETARIO);

                 HttpContext.Current.Session["Conn"] = Sistran.Library.BaseAntigaConfig.Default["BDAntigo"].ToString();
                 Sistran.Library.GetDataTables.TransacaoInserirVeiculoBaseAntiga(Sistran.Library.BaseAntigaConfig.Default["BDAntigo"].ToString(), IDVEICULO, IDVEICULOMODELO, IDVEICULOTIPO, IDVEICULORASTREADOR, IDCIDADE, IDPROPRIETARIO, IDMOTORISTA, PLACA, RENAVAN, CHASSI, ANO, COR, CAPACIDADEDECARGAKG, CAPACIDADEDECARGAM3, QUATIDADEDEEIXOS, CATEGORIASDECNHPERMITIDAS, ANTT, NUMEROSERIEEQUIPAMENTO, VenctoAntt, DataLiecenc, lMot, lProp, MarcaDescricao);
                 HttpContext.Current.Session["Conn"] = null;*/
            }
        }

        private SistranMODEL.Cadastro.Proprietario PrepararProprietario(int IDPROPRIETARIO)
        {
            DataTable dtProp = new SistranDAO.Cadastro.Proprietario().Pesquisar("", "", IDPROPRIETARIO);
            SistranMODEL.Cadastro.Proprietario lProp = new SistranMODEL.Cadastro.Proprietario();

            foreach (DataRow item in dtProp.Rows)
            {
                lProp.IDProprietario = IDPROPRIETARIO;

                SistranMODEL.Cadastro lCad = new SistranMODEL.Cadastro();
                SistranMODEL.Cadastro.CadastroComplemento lCadComp = new SistranMODEL.Cadastro.CadastroComplemento();
                SistranMODEL.Cidade lCid = new SistranMODEL.Cidade();
                SistranMODEL.Estado lEst = new SistranMODEL.Estado();

                lCad.Aniversario = item["Aniversario"].ToString();
                lCad.Cep = item["Cep"].ToString();
                lCad.CEPValido = item["CEPValido"].ToString();
                lCad.CnpjCpf = item["CnpjCpf"].ToString();
                lCad.CnpjCpfErrado = item["CnpjCpfErrado"].ToString();
                lCad.Complemento = item["Complemento"].ToString();
                lCad.DataDeCadastro = Convert.ToDateTime(item["DataDeCadastro"]);

                lCad.Endereco = item["Endereco"].ToString();
                lCad.FantasiaApelido = item["FantasiaApelido"].ToString();
                lCad.IDBairro = Convert.ToInt32(item["IDBairro"]);
                lCad.IDCadastro = Convert.ToInt32(item["IDCadastro"]);
                lCad.IDCidade = Convert.ToInt32(item["IDCidade"]);
                lCad.InscricaoErrada = item["InscricaoErrada"].ToString();
                lCad.InscricaoRG = item["InscricaoRG"].ToString();
                lCad.Numero = item["Numero"].ToString();
                lCad.OrgaoEmissor = item["OrgaoEmissor"].ToString();
                lCad.RazaoSocialNome = item["RazaoSocialNome"].ToString();
                lCad.SituacaoFiscal = item["SituacaoFiscal"].ToString();
                lCad.SUFRAMA = item["SUFRAMA"].ToString();

                if (item["SUFRAMAVALIDADE"] != DBNull.Value)
                    lCad.SUFRAMAVALIDADE = Convert.ToDateTime(item["SUFRAMAVALIDADE"]);

                lCad.TipoDeCadastro = item["TipoDeCadastro"].ToString();
                lProp.Cadastro = lCad;

                lCadComp.Agencia = item["Agencia"].ToString();
                lCadComp.AgenciaDigito = item["AgenciaDigito"].ToString();
                lCadComp.Aniversario = item["Aniversario"].ToString();
                lCadComp.Banco = item["Banco"].ToString();
                lCadComp.CnpjCpfFavorecido = item["CnpjCpfFavorecido"].ToString();
                lCadComp.Conta = item["Conta"].ToString();
                lCadComp.ContaDigito = item["ContaDigito"].ToString();
                lCadComp.Dependentes = item["Dependentes"] == DBNull.Value ? 0 : Convert.ToInt32(item["Dependentes"]);
                lCadComp.IDCadastro = Convert.ToInt32(item["IDCadastro"]);
                lCadComp.IDCadastroComplemento = Convert.ToInt32(item["IDCadastroComplemento"] == DBNull.Value ? 0 : item["IDCadastroComplemento"]);
                lCadComp.InscricaoNoInss = item["InscricaoNoInss"].ToString();
                lCadComp.InscricaoPIS = item["InscricaoPIS"].ToString();
                lCadComp.NomeFavorecido = item["NomeFavorecido"].ToString();
                lCadComp.TipoConta = item["TipoConta"].ToString();
                lCadComp.ValorPensaoAlimenticia = item["ValorPensaoAlimenticia"] == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(item["ValorPensaoAlimenticia"]);
                lProp.CadastroComplemento = lCadComp;


                if (lProp.Cadastro.IDCidade.ToString() != "")
                {
                    DataTable dtCid = new SistranBLL.Localizacao.Cidade().Read(lProp.Cadastro.IDCidade);

                    foreach (DataRow it in dtCid.Rows)
                    {
                        lCid.IDCidade = Convert.ToInt32(it["IDCIDADE"]);
                        lCid.IDEstado = Convert.ToInt32(it["IDESTADO"]);
                        lCid.Nome = it["NOME"].ToString();

                        lEst.IDEstado = Convert.ToInt32(it["IDESTADO"]);
                        DataTable dtEst = new SistranBLL.Localizacao.Estado().Read(lEst.IDEstado);

                        foreach (DataRow ite in dtEst.Rows)
                        {
                            lEst.Nome = ite["NOME"].ToString();
                            lEst.Uf = ite["UF"].ToString();
                        }
                    }
                }

                lProp.Estado = lEst;
                lProp.Cidade = lCid;
            }

            return lProp;
        }

        private SistranMODEL.Cadastro.Motorista PrepararMotorista(int IDMOTORISTA)
        {
            DataTable dtMot = new SistranDAO.Cadastro.Motorista().Pesquisar("", "", "", "", IDMOTORISTA, "", "", false);

            SistranMODEL.Cadastro.Motorista lMot = new SistranMODEL.Cadastro.Motorista();

            foreach (DataRow item in dtMot.Rows)
            {
                lMot.AliquotaSestSenat = (item["AliquotaSestSenat"] == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(item["AliquotaSestSenat"]));
                lMot.Ativo = item["Ativo"].ToString();
                lMot.CarregamentoAutorizadoAte = (item["CarregamentoAutorizadoAte"] == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(item["CarregamentoAutorizadoAte"]));
                lMot.CarteiraDeHabilitacao = item["CarteiraDeHabilitacao"].ToString();
                lMot.Categoria = item["Categoria"].ToString();
                lMot.Conjuge = item["Conjuge"].ToString();
                lMot.DataDeCadastro = Convert.ToDateTime(item["DataDeCadastro"]);

                if (item["DataDaPrimeiraHabilitacao"] != DBNull.Value)
                    lMot.DataDaPrimeiraHabilitacao = Convert.ToDateTime(item["DataDaPrimeiraHabilitacao"]);

                if (item["DataDeNascimento"] != DBNull.Value)
                    lMot.DataDeNascimento = Convert.ToDateTime(item["DataDeNascimento"]);

                lMot.EstadoCivil = item["EstadoCivil"].ToString();
                lMot.IDCidadeNascimento = item["IDCidadeNascimento"].ToString();
                lMot.IDMotorista = Convert.ToInt32(item["IDMotorista"]);
                lMot.Liberado = item["Liberado"].ToString();
                lMot.MOOP = item["MOPP"].ToString();
                lMot.NomeDaMae = item["NomeDaMae"].ToString();
                lMot.NomeDoPai = item["NomeDoPai"].ToString();
                lMot.NumeroPancard = item["NumeroPancard"].ToString();
                lMot.VinculoComAEmpresa = item["VinculoComAEmpresa"].ToString();
                lMot.SofreuAcidadeQuantidade = item["SofreuAcidadeQuantidade"].ToString() == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(item["SofreuAcidadeQuantidade"]);
                lMot.VitimaDeRouboQuantidade = item["VitimaDeRouboQuantidade"].ToString() == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(item["VitimaDeRouboQuantidade"]);

                if (item["ValidadeDaHabilitacao"] != DBNull.Value)
                    lMot.ValidadeDaHabilitacao = Convert.ToDateTime(item["ValidadeDaHabilitacao"]);

                // if (item["VencimentoNoGerenciadorDeRisco"] != DBNull.Value)
                //     lMot.VencimentoNoGerenciadorDeRisco = Convert.ToDateTime(item["VencimentoNoGerenciadorDeRisco"]);


                SistranMODEL.Cadastro lCad = new SistranMODEL.Cadastro();
                SistranMODEL.Cadastro.CadastroComplemento lCadComp = new SistranMODEL.Cadastro.CadastroComplemento();


                lCad.Aniversario = item["Aniversario"].ToString();
                lCad.Cep = item["Cep"].ToString();
                lCad.CEPValido = item["CEPValido"].ToString();
                lCad.CnpjCpf = item["CnpjCpf"].ToString();
                lCad.CnpjCpfErrado = item["CnpjCpfErrado"].ToString();
                lCad.Complemento = item["Complemento"].ToString();
                lCad.DataDeCadastro = Convert.ToDateTime(item["DataDeCadastro"]);

                lCad.Endereco = item["Endereco"].ToString();
                lCad.FantasiaApelido = item["FantasiaApelido"].ToString();
                lCad.IDBairro = item["IDBairro"] == DBNull.Value ? 0 : Convert.ToInt32(item["IDBairro"]);
                lCad.IDCadastro = Convert.ToInt32(item["IDCadastro"]);
                lCad.IDCidade = Convert.ToInt32(item["IDCidade"]);
                lCad.InscricaoErrada = item["InscricaoErrada"].ToString();
                lCad.InscricaoRG = item["InscricaoRG"].ToString();
                lCad.Numero = item["Numero"].ToString();
                lCad.OrgaoEmissor = item["OrgaoEmissor"].ToString();
                lCad.RazaoSocialNome = item["RazaoSocialNome"].ToString();
                lCad.SituacaoFiscal = item["SituacaoFiscal"].ToString();
                lCad.SUFRAMA = item["SUFRAMA"].ToString();

                if (item["SUFRAMAVALIDADE"] != DBNull.Value)
                    lCad.SUFRAMAVALIDADE = Convert.ToDateTime(item["SUFRAMAVALIDADE"]);
                lCad.TipoDeCadastro = item["TipoDeCadastro"].ToString();

                lMot.Cadastro = lCad;
                lCadComp.Agencia = item["Agencia"].ToString();
                lCadComp.AgenciaDigito = item["AgenciaDigito"].ToString();
                lCad.Aniversario = item["Aniversario"].ToString();
                lCadComp.Banco = item["Banco"].ToString();
                lCadComp.CnpjCpfFavorecido = item["CnpjCpfFavorecido"].ToString();
                lCadComp.Conta = item["Conta"].ToString();
                lCadComp.ContaDigito = item["ContaDigito"].ToString();
                lCadComp.Dependentes = item["Dependentes"] == DBNull.Value ? 0 : Convert.ToInt32(item["Dependentes"]);
                lCadComp.IDCadastro = Convert.ToInt32(item["IDCadastro"]);
                lCadComp.IDCadastroComplemento = Convert.ToInt32(item["IDCadastroComplemento"]);
                lCadComp.InscricaoNoInss = item["InscricaoNoInss"].ToString();
                lCadComp.InscricaoPIS = item["InscricaoPIS"].ToString();
                lCadComp.NomeFavorecido = item["NomeFavorecido"].ToString();
                lCadComp.TipoConta = item["TipoConta"].ToString();
                lCadComp.ValorPensaoAlimenticia = item["ValorPensaoAlimenticia"] == DBNull.Value ? Convert.ToDecimal(0) : Convert.ToDecimal(item["ValorPensaoAlimenticia"]);
                lMot.CadastroComplemento = lCadComp;

                SistranMODEL.Cidade lCid = new SistranMODEL.Cidade();
                SistranMODEL.Estado lEst = new SistranMODEL.Estado();

                if (lMot.Cadastro.IDCidade.ToString() != "")
                {
                    DataTable dtCid = new SistranBLL.Localizacao.Cidade().Read(lMot.Cadastro.IDCidade);

                    foreach (DataRow it in dtCid.Rows)
                    {
                        lCid.IDCidade = Convert.ToInt32(it["IDCIDADE"]);
                        lCid.IDEstado = Convert.ToInt32(it["IDESTADO"]);
                        lCid.Nome = it["NOME"].ToString();

                        lEst.IDEstado = Convert.ToInt32(it["IDESTADO"]);
                        DataTable dtEst = new SistranBLL.Localizacao.Estado().Read(lEst.IDEstado);

                        foreach (DataRow ite in dtEst.Rows)
                        {
                            lEst.Nome = ite["NOME"].ToString();
                            lEst.Uf = ite["UF"].ToString();
                        }

                    }
                }

                lMot.Estado = lEst;
                lMot.Cidade = lCid;

            }
            return lMot;
        }

        public DataTable GerarReportVeiculo(bool AnntVenc)
        {
            string strsql = " SELECT  ";
            strsql += " V.PLACA,  ";
            strsql += " V.RENAVAM,  ";
            strsql += " V.CHASSI,  ";
            strsql += " V.ANO,  ";
            strsql += " V.COR,  ";
            strsql += " V.Antt,  ";
            strsql += " V.AnttVencimento,  ";
            strsql += " CADMOT.CNPJCPF CPFMOTORISTA, ";
            strsql += " CADMOT.RAZAOSOCIALNOME NOMEMOTORISTA, ";
            strsql += " CADPROP.CNPJCPF CPFPROPRIETARIO, ";
            strsql += " CADPROP.RAZAOSOCIALNOME NOMEPROPRIETARIO, ";
            strsql += " VM.NOME MODELOVEICULO, ";
            strsql += " VT.NOME TIPOVEICULO ";
            strsql += " FROM VEICULO V ";
            strsql += " LEFT JOIN MOTORISTA MOT ON MOT.IDMOTORISTA = V.IDMOTORISTA ";
            strsql += " LEFT JOIN PROPRIETARIO PRO ON PRO.IDPROPRIETARIO = V.IDPROPRIETARIO ";
            strsql += " LEFT JOIN CADASTRO CADMOT ON  CADMOT.IDCADASTRO = MOT.IDMOTORISTA ";
            strsql += " LEFT JOIN CADASTRO CADPROP ON  CADPROP.IDCADASTRO = PRO.IDPROPRIETARIO ";
            strsql += " INNER JOIN VEICULOMODELO VM ON VM.IDVEICULOMODELO = V.IDVEICULOMODELO ";
            strsql += " INNER JOIN VEICULOTIPO VT ON VT.IDVEICULOTIPO = V.IDVEICULOTIPO  ";
            strsql += " WHERE " + (AnntVenc == true ? "ANTTVENCIMENTO" : "Licenciamento") + " <=GETDATE()";
            strsql += " OR " + (AnntVenc == true ? " ANTTVENCIMENTO IS NULL " : "Licenciamento");
            strsql += " ORDER BY V.PLACA";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public class Modelo
        {
            public DataTable Listar()
            {
                string strsql = " SELECT IDVEICULOMODELO, NOME FROM VEICULOMODELO ORDER BY NOME";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
            }

            public DataTable Listar(string IdVeiculoMarca)
            {
                string strsql = " SELECT IDVEICULOMODELO, NOME FROM VEICULOMODELO where IDVeiculoMarca=" + IdVeiculoMarca + " ORDER BY NOME";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
            }

            public void AlterarIncluir(string texto, int IDVeiculoMarca, int IDVeiculoModelo)
            {
                if (IDVeiculoModelo == 0)
                {
                    string ids = Sistran.Library.GetDataTables.RetornarIdTabela("VEICULOMODELO");
                    Sistran.Library.GetDataTables.ExecutarSemRetorno("INSERT INTO VEICULOMODELO (IDVEICULOMODELO, IDVEICULOMARCA, NOME) VALUES (" + ids + "," + IDVeiculoMarca.ToString() + ", '" + texto.ToUpper() + "')", "");
                }
                else
                {
                    Sistran.Library.GetDataTables.ExecutarSemRetorno("UPDATE VEICULOMODELO  SET NOME ='" + texto.ToUpper() + "' WHERE IDVEICULOMODELO=" + IDVeiculoModelo.ToString(), "");

                }
            }
        }

        public class Marca
        {
            public DataTable Listar()
            {
                string strsql = "SELECT IDVEICULOMARCA, NOME FROM VEICULOMARCA ORDER BY NOME";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
            }

            public void AlterarIncluir(string texto, int IDVeiculoMarca)
            {
                if (IDVeiculoMarca == 0)
                {
                    //                    Sistran.Library.GetDataTables.ExecutarSemRetorno("INSERT INTO VEICULOMARCA (IDVEICULOMARCA, NOME) VALUES ((SELECT ISNULL(MAX(IDVeiculoMarca),0) +1 FROM VEICULOMARCA),'" + texto.ToUpper() + "')", "");
                    Sistran.Library.GetDataTables.ExecutarSemRetorno("INSERT INTO VEICULOMARCA (IDVEICULOMARCA, NOME) VALUES (" + Sistran.Library.GetDataTables.RetornarIdTabela("VEICULOMARCA") + ",'" + texto.ToUpper() + "')", "");
                }
                else
                {
                    Sistran.Library.GetDataTables.ExecutarSemRetorno("UPDATE VEICULOMARCA  SET NOME ='" + texto.ToUpper() + "' WHERE IDVeiculoMarca=" + IDVeiculoMarca.ToString(), "");

                }
            }
        }

        public class Tipo
        {
            public DataTable Listar()
            {
                string strsql = "SELECT IDVEICULOTIPO, NOME, CATEGORIAPERMITIDA  FROM VEICULOTIPO ORDER BY NOME";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
            }
        }

        public class Rastreador
        {
            public DataTable Listar()
            {
                string strsql = "SELECT IDVEICULORASTREADOR, NOME FROM VEICULORASTREADOR ORDER BY NOME";
                return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
            }
        }
    }

    public class DocumentoDeTransporte
    {
        public DataTable Pesquisar(int IdDt, DateTime? ini, DateTime? fim, bool Pendente, string NumeroDT)
        {
            string strsql = " SELECT  DISTINCT ";
            strsql += " DT.IDDT,  ";
            strsql += " DT.NUMERO,  ";
            strsql += " DT.EMISSAO,   ";
            strsql += " DT.ANDAMENTO,  ";
            strsql += " DT.IDTRANSPORTADORA,  ";
            strsql += " DT.IDFILIAL,  ";
            strsql += " FIL.NOME FILIAL,  ";
            strsql += " CADTRANS.RAZAOSOCIALNOME TRANSPORTADORA,  ";
            strsql += " DT.IDPRIMEIROMOTORISTA,  ";
            strsql += " CADMOT.RAZAOSOCIALNOME MOTORISTA,  ";
            strsql += " DT.IDAGREGADO,  ";
            strsql += " CADAGR.RAZAOSOCIALNOME AGREGADO,  ";
            strsql += " DT.IDPRIMEIROVEICULO,  ";
            strsql += " VEICULO.PLACA,  ";
            strsql += " CASE WHEN DOC.DataDeConclusao IS NULL THEN 'PENDENTE' ELSE 'CONCLUÍDO' END SITUACAO";
            strsql += " FROM DT   ";
            strsql += " INNER JOIN CADASTRO CADTRANS ON CADTRANS.IDCADASTRO = DT.IDTRANSPORTADORA  ";
            strsql += " INNER JOIN FILIAL FIL ON FIL.IDFILIAL = DT.IDFILIAL  ";
            strsql += " LEFT JOIN CADASTRO CADMOT ON CADMOT.IDCADASTRO = DT.IDPRIMEIROMOTORISTA  ";
            strsql += "  INNER JOIN DTROMANEIO DTROM ON DTROM.IDDT=DT.IDDT ";
            strsql += " INNER JOIN ROMANEIODOCUMENTO ROMDOC ON ROMDOC.IDROMANEIO = DTROM.IDROMANEIO";
            strsql += " INNER JOIN DOCUMENTO DOC ON DOC.IDDOCUMENTO = ROMDOC.IDDOCUMENTO";
            strsql += " LEFT JOIN CADASTRO CADAGR ON CADAGR.IDCADASTRO = DT.IDAGREGADO  ";
            strsql += " LEFT JOIN VEICULO ON VEICULO.IDVEICULO = DT.IDPRIMEIROVEICULO  ";
            strsql += " WHERE IDTRANSPORTADORA IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")  ";

            if (Pendente)
                strsql += " AND DOC.DATADECONCLUSAO IS NULL ";

            if (NumeroDT.Length > 0)
                strsql += " AND DT.NUMERO LIKE '" + NumeroDT + "%' ";

            if (ini != null && fim != null)
                strsql += " AND DT.EMISSAO BETWEEN CONVERT(DATETIME, '" + ini + "',  103) AND  CONVERT(DATETIME, '" + fim + "',  103)";

            if (IdDt != 0)
                strsql += " AND  DT.IDDT = " + IdDt;


            strsql += " ORDER BY DT.NUMERO  ";


            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public DataTable RetornarDt(int IdDt, int IdFilial)
        {
            string strsql = " SELECT ";
            strsql += " DT.Numero NUMERODT,";
            strsql += " ROM.IDRomaneio  NUMEROROMANEIO,";
            strsql += " CADREM.RAZAOSOCIALNOME REMETENTE,";
            strsql += " CADEST.RAZAOSOCIALNOME DESTINATARIO,";
            strsql += " DOC.DATADOMOVIMENTO,";
            strsql += " DOC.NUMERO, ";
            strsql += " DOC.VOLUMES,";
            strsql += " DOC.PESOBRUTO,";
            strsql += " DOC.PESOCUBADO,";
            strsql += " DOC.VOLUMES,";
            strsql += " DOC.VALORDANOTA,";
            strsql += " OCO.NOME OCORRENCIA,";
            strsql += " CADFILIAL.RAZAOSOCIALNOME NOMELOGOS,";
            strsql += " CADFILIAL.ENDERECO ENDERECOLOGOS,";
            strsql += " CADFILIAL.NUMERO NUMEROLOGOS,";
            strsql += " CADFILIAL.CEP CEPLOGOS,";
            strsql += " CIDLOGOS.NOME CIDADELOGOS,";
            strsql += " ESTLOGOS.UF UFLOGOS,";
            strsql += " CADFILIAL.CNPJCPF CNPJLOGOS,";
            strsql += " CADFILIAL.INSCRICAORG IELOGOS,";
            strsql += " CADTRANS.RazaoSocialNome NOMETRANSPORTADORA,";
            strsql += " CADTRANS.Endereco ENDERECOTRANSP,";
            strsql += " CADTRANS.Numero NUMEROTRANSP,";
            strsql += " CIDTRANP.Nome CIDADETRANSP,";
            strsql += " ESTTRANSP.Uf UFTRANSP,";
            strsql += " CADTRANS.CnpjCpf CNPJTRANSP,";
            strsql += " CADTRANS.InscricaoRG IETRANSP,";
            strsql += " VEICULO.Placa,";
            strsql += " CIDVEICULO.Nome CIDADEVEICULO,";
            strsql += " ESTVEICULO.Uf UFVEICULO,";
            strsql += " CADMOT.RazaoSocialNome NOMEMOTORISTA,";
            strsql += " CADMOT.CnpjCpf CPFMOTORISTA,";
            strsql += " CADMOT.InscricaoRG RGMOTORISTA";

            strsql += " FROM DT";
            strsql += " INNER JOIN DTROMANEIO DTROM ON DTROM.IDDT = DT.IDDT";
            strsql += " INNER JOIN ROMANEIO ROM ON ROM.IDROMANEIO = DTROM.IDDTROMANEIO";
            strsql += " INNER JOIN ROMANEIODOCUMENTO ROMDOC ON ROMDOC.IDROMANEIO = ROM.IDROMANEIO";
            strsql += " INNER JOIN DOCUMENTO DOC ON DOC.IDDOCUMENTO = ROMDOC.IDDOCUMENTO";
            strsql += " INNER JOIN CADASTRO CADTRANS ON CADTRANS.IDCADASTRO = DT.IDTRANSPORTADORA ";
            strsql += " LEFT JOIN CADASTRO CADMOT ON CADMOT.IDCADASTRO = DT.IDPRIMEIROMOTORISTA   ";
            strsql += " LEFT JOIN CADASTRO CADAGR ON CADAGR.IDCADASTRO = DT.IDAGREGADO   ";
            strsql += " LEFT JOIN VEICULO ON VEICULO.IDVEICULO = DT.IDPRIMEIROVEICULO   ";
            strsql += " LEFT JOIN CADASTRO CADREM ON CADREM.IDCADASTRO = DOC.IDREMETENTE";
            strsql += " LEFT JOIN CADASTRO CADEST ON CADEST.IDCADASTRO = DOC.IDDESTINATARIO";
            strsql += " LEFT JOIN DOCUMENTOOCORRENCIA DOCOCO ON DOCOCO.IDDOCUMENTOOCORRENCIA = DOC.IDDOCUMENTOOCORRENCIA";
            strsql += " LEFT JOIN OCORRENCIA OCO ON OCO.IDOCORRENCIA = DOCOCO.IDOCORRENCIA ";
            strsql += " LEFT JOIN FILIAL ON FILIAL.IDFILIAL = DOC.IDFILIAL ";
            strsql += " LEFT JOIN CADASTRO CADFILIAL ON CADFILIAL.IDCADASTRO = " + IdFilial;
            strsql += " LEFT JOIN CIDADE CIDLOGOS ON CIDLOGOS.IDCIDADE = CADFILIAL.IDCIDADE";
            strsql += " LEFT JOIN ESTADO ESTLOGOS ON ESTLOGOS.IDESTADO = CIDLOGOS.IDESTADO";
            strsql += " LEFT JOIN CIDADE CIDTRANP ON CIDTRANP.IDCIDADE = CADTRANS.IDCidade";
            strsql += " LEFT JOIN ESTADO ESTTRANSP ON ESTTRANSP.IDESTADO = CIDTRANP.IDEstado";
            strsql += " LEFT JOIN CIDADE CIDVEICULO ON CIDVEICULO.IDCIDADE = Veiculo.IDCidade";
            strsql += " LEFT JOIN ESTADO ESTVEICULO ON ESTVEICULO.IDESTADO = CIDVEICULO.IDEstado";
            strsql += " LEFT JOIN Motorista MOT ON MOT.IDMotorista = DT.IDPrimeiroMotorista";
            strsql += " WHERE DT.IDDT=" + IdDt;
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }

        public DataTable RetornarRomaneios(int IdDt, int IdFilial)
        {
            string strsql = " SELECT DISTINCT ";
            strsql += " DT.Emissao, DT.IDDT,Veiculo.Ano, Veiculo.CapacidadeDeCargaKG, ";
            strsql += " DT.NUMERO NUMERODT, ";
            strsql += " ROM.IDROMANEIO  NUMEROROMANEIO, ";
            strsql += " VEICULO.PLACA, ";
            strsql += " CIDVEICULO.NOME CIDADEVEICULO, ";
            strsql += " ESTVEICULO.UF UFVEICULO, ";
            strsql += " CADMOT.RAZAOSOCIALNOME NOMEMOTORISTA, ";
            strsql += " CADMOT.CNPJCPF CPFMOTORISTA, ";
            strsql += " CADMOT.INSCRICAORG RGMOTORISTA ";
            strsql += " FROM DT ";
            strsql += " INNER JOIN DTROMANEIO DTROM ON DTROM.IDDT = DT.IDDT ";
            strsql += " INNER JOIN ROMANEIO ROM ON ROM.IDROMANEIO = DTROM.IDDTROMANEIO ";
            strsql += " INNER JOIN ROMANEIODOCUMENTO ROMDOC ON ROMDOC.IDROMANEIO = ROM.IDROMANEIO ";
            strsql += " INNER JOIN CADASTRO CADTRANS ON CADTRANS.IDCADASTRO = DT.IDTRANSPORTADORA  ";
            strsql += " LEFT JOIN CADASTRO CADMOT ON CADMOT.IDCADASTRO = DT.IDPRIMEIROMOTORISTA    ";
            strsql += " LEFT JOIN VEICULO ON VEICULO.IDVEICULO = DT.IDPRIMEIROVEICULO    ";
            strsql += " LEFT JOIN CADASTRO CADFILIAL ON CADFILIAL.IDCADASTRO = " + IdFilial;
            strsql += " LEFT JOIN CIDADE CIDVEICULO ON CIDVEICULO.IDCIDADE = VEICULO.IDCIDADE ";
            strsql += " LEFT JOIN ESTADO ESTVEICULO ON ESTVEICULO.IDESTADO = CIDVEICULO.IDESTADO ";
            strsql += " LEFT JOIN MOTORISTA MOT ON MOT.IDMOTORISTA = DT.IDPRIMEIROMOTORISTA ";
            strsql += " WHERE DT.IDDT IN (" + IdDt + ")";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql.ToString(), "");
        }
    }

    public class KPI
    {
        public decimal Form01(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();
            strsql = " SELECT  ";
            strsql += " (DATEDIFF(MI, ";
            strsql += " (SELECT MIN(PP.DATAHORADEENTRADA) FROM PORTARIA PP WHERE PP.DATAHORADEENTRADA BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND GETDATE() AND PP.IDFILIAL=PORT.IDFILIAL), ";
            strsql += " (SELECT MAX(PP.DATAHORADESAIDA) FROM PORTARIA PP WHERE PP.DATAHORADEENTRADA BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND GETDATE() AND PP.IDFILIAL=PORT.IDFILIAL))/60) / COUNT(DISTINCT PLACA) 	 ";
            strsql += " FROM ROMANEIO ROM ";
            strsql += " INNER JOIN ROMANEIODOCUMENTO ROMDOC ON ROMDOC.IDROMANEIO = ROM.IDROMANEIO ";
            strsql += " INNER JOIN DOCUMENTO DOC ON DOC.IDDOCUMENTO = ROMDOC.IDDOCUMENTO ";
            strsql += " INNER JOIN DOCUMENTOITEM DI ON DI.IDDOCUMENTO = DOC.IDDOCUMENTO ";
            strsql += " INNER JOIN PORTARIA PORT ON PORT.IDFILIAL = DOC.IDFILIAL ";
            strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = DI.IDPRODUTOEMBALAGEM ";
            strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE  ";
            strsql += " WHERE ROM.TIPO='ENTRADA' ";
            strsql += " AND DOC.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            strsql += " AND PORT.DATAHORADEENTRADA BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND CONVERT(DATETIME, '" + DataFim + "', 103) ";
            strsql += " AND PORT.DATAHORADESAIDA IS NOT NULL ";

            if (linha != "Todos")
                strsql += " AND PC.IDGRUPODEPRODUTO = " + linha;

            strsql += " GROUP BY PORT.IDFILIAL ";            
            return Sistran.Library.GetDataTables.RetornarDecimal(strsql, "");
        }       

        public decimal Form02(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();         

            strsql = " SELECT ";
            strsql += " COUNT(*) LINHASATENDIDAS ";
            strsql += " FROM DOCUMENTO NF ";
            strsql += " INNER JOIN DOCUMENTOITEM NFI ON (NFI.IDDOCUMENTO = NF.IDDOCUMENTO) ";
            strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM) ";
            strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ";
            strsql += " INNER JOIN GRUPODEPRODUTO GP ON (GP.IDGRUPODEPRODUTO = PC.IDGRUPODEPRODUTO) WHERE ";
            strsql += " NF.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ")   ";
            strsql += " AND NF.TIPODEDOCUMENTO = 'PEDIDO' ";
            strsql += " AND NF.ATIVO = 'SIM' ";
            strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103) ";

            if (linha != "Todos")
                strsql += " AND PC.IDGRUPODEPRODUTO = " + linha;

            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
        }

        public decimal Form03(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();        

            //este nao precisa idgrupoproduto
            strsql = " SELECT ";
            strsql += "   COUNT(DISTINCT MP.NUMERO) UAPENDENTE ";
            strsql += " FROM MAPA MP ";
            strsql += " INNER JOIN MOVIMENTACAOITEM MI ON (MI.IDMAPA = MP.IDMAPA) ";
            strsql += " INNER JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = MI.IDDOCUMENTO)  ";
            strsql += " where NF.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            strsql += " AND MP.ESTOQUEPROCESSADO = 'NAO' ";
            strsql += " AND MP.TIPO = 'ENTRADA' ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103)  AND CONVERT(DATETIME, '" + DataFim + "', 103) ";

            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
        }

        public decimal Form04(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();         


            // nao precisa do idgrupodeproduto

            strsql = " select  ";
            strsql += "(( ";
            strsql += "Select ";
            strsql += "CAST( Count(DISTINCT NFI.IDDocumentoItem)   as decimal) LinhasConferidas  From Mapa MP ";
            strsql += "Inner Join MovimentacaoItem MI on (MI.IDMapa = MP.IDMapa) ";
            strsql += "Inner Join Documento NF on (NF.IDDocumento = MI.IDDocumento) ";
            strsql += "Inner Join DocumentoItem NFI on (NFI.IDDocumento = NF.IDDocumento) where ";
            strsql += "NF.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            strsql += "and MP.EstoqueProcessado = 'SIM' ";
            strsql += " and nf.ENTRADASAIDA='saida' and nf.tipodedocumento='pedido'";
            strsql += "and MP.DataDeCadastro BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103)  AND CONVERT(DATETIME, '" + DataFim + "', 103)  "; 
       
            strsql += ") /  ";
            strsql += "( ";
            strsql += " SELECT ";
            strsql += " COUNT(*) LINHASATENDIDAS ";
            strsql += " FROM DOCUMENTO NF ";
            strsql += " INNER JOIN DOCUMENTOITEM NFI ON (NFI.IDDOCUMENTO = NF.IDDOCUMENTO) ";
            strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM) ";
            strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ";
            strsql += " INNER JOIN GRUPODEPRODUTO GP ON (GP.IDGRUPODEPRODUTO = PC.IDGRUPODEPRODUTO) WHERE ";
            strsql += " NF.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ")   ";
            strsql += " AND NF.TIPODEDOCUMENTO = 'PEDIDO' ";
            strsql += " AND NF.ATIVO = 'SIM' ";
            strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103) ";
            strsql += ")) *100 ";

            decimal ret = Sistran.Library.GetDataTables.RetornarDecimal(strsql, "");

            if (ret > Convert.ToDecimal(100))
                ret = Convert.ToDecimal(100);

            return ret;
        }        

        public decimal Form05(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();
           

            strsql = " SELECT ";
            strsql += " COUNT(*) LINHASATENDIDAS ";
            strsql += " FROM DOCUMENTO NF ";
            strsql += " INNER JOIN DOCUMENTOITEM NFI ON (NFI.IDDOCUMENTO = NF.IDDOCUMENTO) ";
            strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM) ";
            strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ";
            strsql += " INNER JOIN GRUPODEPRODUTO GP ON (GP.IDGRUPODEPRODUTO = PC.IDGRUPODEPRODUTO) WHERE ";
            strsql += " NF.IDCLIENTE IN (" + Sistran.Library.FuncoesUteis.retornarClientes() + ")   ";
            strsql += " AND NF.TIPODEDOCUMENTO = 'PEDIDO' ";
            strsql += " AND NF.ATIVO = 'SIM' ";
            strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103) ";

            if (linha != "Todos")
                strsql += " AND PC.IDGRUPODEPRODUTO = " + linha;

            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
          
        }

        public decimal Form06(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();         

            strsql = "  SELECT    ";
            strsql += " COUNT(*) valor    ";
            strsql += " FROM DOCUMENTO PD    ";
            strsql += " INNER JOIN DOCUMENTOITEM PDI ON (PDI.IDDOCUMENTO = PD.IDDOCUMENTO)    ";

            if (linha != "Todos")
            {
                strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON PE.IDPRODUTOEMBALAGEM = PDI.IDPRODUTOEMBALAGEM    ";
                strsql += " INNER JOIN PRODUTOCLIENTE PC ON PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE    ";
            }

            strsql += " WHERE    ";
            strsql += " PD.IDCLIENTE in(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")   ";
            strsql += " AND PD.TIPODEDOCUMENTO = 'PEDIDO'    ";
            strsql += " AND PD.ATIVO = 'NAO'    ";
            strsql += " AND PD.ENTRADASAIDA = 'SAIDA'    ";
            //strsql += " AND PD.SERIE = 'VD'    ";
            strsql += " AND PDI.STATUSDOITEM = 'CANCELADO'    ";

            if (linha != "Todos")
                strsql += " AND PC.IDGRUPODEPRODUTO = " + linha;

            strsql += " AND PD.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)";
            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
        }
        
        public decimal Form08(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();
             strsql = " SELECT  ";
            strsql += " (CAST(ISNULL(SUM(I.POSICOESCORRETAS),0) AS DECIMAL) / CAST( ISNULL(SUM(I.POSICOESCONTADAS),1) AS DECIMAL))*100 ";
            strsql += " FROM INVENTARIO I ";
            strsql += " WHERE IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            strsql += " AND DATA BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";

            return Sistran.Library.GetDataTables.RetornarDecimal(strsql, "");
        }

        public decimal Form09(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();
             strsql = " SELECT  ";
            strsql += " (CAST(ISNULL(SUM(I.SKUTotal),0) AS DECIMAL) / CAST( ISNULL(SUM(I.SKUCorretos),1) AS DECIMAL))*100 ";
            strsql += " FROM INVENTARIO I ";
            strsql += " WHERE IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            strsql += " AND DATA BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";

            return Sistran.Library.GetDataTables.RetornarDecimal(strsql, "");
        }

        public decimal Form10(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();
             strsql = " SELECT  ";
            strsql += " (CAST(ISNULL(SUM(I.PosicoesCorretasABC),0) AS DECIMAL) / CAST( ISNULL(SUM(I.PosicoesContadasABC),1) AS DECIMAL))*100 ";
            strsql += " FROM INVENTARIO I ";
            strsql += " WHERE IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            strsql += " AND DATA BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";

            return Sistran.Library.GetDataTables.RetornarDecimal(strsql, "");
        }

        public decimal Form07(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();
             strsql = " SELECT  ";
            strsql += " ( ";
            strsql += " SELECT  ";
            strsql += " CAST( COUNT(DISTINCT UA.IDDEPOSITOPLANTALOCALIZACAO) AS DECIMAL) ";
            strsql += " FROM UNIDADEDEARMAZENAGEMLOTE UAL  ";
            strsql += " INNER JOIN  UNIDADEDEARMAZENAGEM UA ON (UA.IDUNIDADEDEARMAZENAGEM = UAL.IDUNIDADEDEARMAZENAGEM)  ";
            strsql += " INNER JOIN LOTE LT ON (LT.IDLOTE = UAL.IDLOTE)  ";
            strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = LT.IDPRODUTOCLIENTE)  WHERE PC.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") AND UAL.SALDO > 0 ";
            if (linha != "Todos")
                strsql += " AND PC.IDGRUPODEPRODUTO = " + linha;

            strsql += " )/ ";
            strsql += " (    ";
            strsql += " SELECT CAST(COUNT(DISTINCT DPL.IDDEPOSITOPLANTALOCALIZACAO) AS DECIMAL) POSICOESLIVRES ";
            strsql += " FROM DEPOSITOPLANTALOCALIZACAOREGRA DPLR ";
            strsql += " INNER JOIN DEPOSITOPLANTALOCALIZACAO DPL ON DPL.IDDEPOSITOPLANTALOCALIZACAO = DPLR.IDDEPOSITOPLANTALOCALIZACAO ";
            strsql += " LEFT JOIN PRODUTOCLIENTE PC ON PC.IDDEPOSITOPLANTALOCALIZACAO = DPL.IDDEPOSITOPLANTALOCALIZACAO ";
            strsql += " WHERE DPLR.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            strsql += " AND DPL.ATIVO='SIM' ";
            strsql += " AND PC.IDPRODUTOCLIENTE IS NULL ";
            //if (linha != "Todos")
            //    strsql += " AND PC.IDGRUPODEPRODUTO = " + linha;
            strsql += " )*100  ";
            
            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
        }

        public decimal Form11(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();                    

             strsql = " SELECT ";
            strsql += " (( ";
            strsql += "SELECT  isnull(CAST(SUM(PCA.Unidades) AS DECIMAL),0) FROM ProdutoClienteAvaria PCA ";
            strsql += "INNER JOIN ProdutoCliente PC ON PC.IDProdutoCliente = PCA.IdProdutoCliente ";
            strsql += "WHERE IDCliente IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            strsql += "AND PCA.Data BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            if (linha != "Todos")
            {
                strsql += " AND PC.IDGrupoDeProduto = " + linha;
            }

            strsql += ") ";
            strsql += "/ ";
            strsql += "( ";
            strsql += "SELECT isnull(CAST(SUM(EST.Saldo) AS DECIMAL),0) FROM ProdutoCliente PC ";
            strsql += "INNER JOIN Estoque EST ON EST.IDProdutoCliente = PC.IDProdutoCliente ";
            strsql += "WHERE PC.IDCliente IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ") ";
            strsql += "AND PC.Ativo ='SIM' ";
            if (linha != "Todos")
            {
                strsql += " AND PC.IDGrupoDeProduto = " + linha;
            }

            strsql += "))*100 ";

            return Sistran.Library.GetDataTables.RetornarDecimal(strsql, "");
        }

        public decimal Form12(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();

            strsql = " SELECT ";
            strsql += "   COUNT(distinct NFI.IDDOCUMENTOITEM) LINHASNAOENVIADASVIAINTERFACE FROM MAPA MP ";
            strsql += " INNER JOIN MOVIMENTACAOITEM MI ON (MI.IDMAPA = MP.IDMAPA) ";
            strsql += " INNER JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = MI.IDDOCUMENTO) ";
            strsql += " INNER JOIN DOCUMENTOITEM NFI ON (NFI.IDDOCUMENTO = NF.IDDOCUMENTO) WHERE ";
            strsql += " NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND MP.ESTOQUEPROCESSADO = 'SIM' ";
            strsql += " AND MI.PEDIDONOTAFISCAL IS  NULL ";

            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
        }

        public decimal Form15(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();
             strsql = " SELECT ";
            strsql += " ISNULL(SUM(NF.VOLUMES), 0) VOLUMES ";
            strsql += " FROM DOCUMENTO NF ";
            strsql += " INNER JOIN DOCUMENTOFILIAL DF ON (DF.IDDOCUMENTO = NF.IDDOCUMENTO)  ";

            //if (linha != "Todos")
            //{
            //    strsql += " INNER JOIN DOCUMENTOITEM NFI ON NFI.IDDOCUMENTO = NF.IDDOCUMENTO ";
            //    strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM) ";
            //    strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ";
            //}
            strsql += " WHERE ";
            strsql += " NF.TIPODEDOCUMENTO = 'NOTA FISCAL' ";
            strsql += " AND NF.ATIVO = 'SIM' ";
            strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
            strsql += " AND DF.SITUACAO = 'AGUARDANDO EMBARQUE'       ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            strsql += " AND NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";

            // strsql += " AND PC.IDGRUPODEPRODUTO>1000 ";


            //if (linha != "Todos")
            //    strsql += " AND PC.IDGrupoDeProduto = " + linha;

            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
        }

        public decimal Form13(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();

            strsql = " ";

            strsql += " SELECT  ";
            strsql += " (( ";
            strsql += " ( ";
            strsql += " SELECT ";
            strsql += " CAST( COUNT(*) AS DECIMAL) NOTASEMBARCADAS ";
            strsql += " FROM DT  ";
            strsql += " INNER JOIN DTROMANEIO DTR ON (DTR.IDDT = DT.IDDT)  ";
            strsql += " INNER JOIN ROMANEIO R ON (R.IDROMANEIO = DTR.IDROMANEIO)  ";
            strsql += " INNER JOIN ROMANEIODOCUMENTO RD ON (RD.IDROMANEIO = R.IDROMANEIO)  ";
            strsql += " INNER JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = RD.IDDOCUMENTO)  ";
            strsql += " WHERE  ";
            strsql += " NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND NF.IDROMANEIO = R.IDROMANEIO ";
            strsql += " AND DT.DATADESAIDA  BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            strsql += " ) ";
            strsql += " - ";
            strsql += " ( ";
            strsql += " SELECT ";
            strsql += " CAST(COUNT(*) AS DECIMAL) NOTASAGENDADAS ";
            strsql += " FROM DT  ";
            strsql += " INNER JOIN DTROMANEIO DTR ON (DTR.IDDT = DT.IDDT)  ";
            strsql += " INNER JOIN ROMANEIO R ON (R.IDROMANEIO = DTR.IDROMANEIO)  ";
            strsql += " INNER JOIN ROMANEIODOCUMENTO RD ON (RD.IDROMANEIO = R.IDROMANEIO)  ";
            strsql += " INNER JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = RD.IDDOCUMENTO)  ";
            strsql += " WHERE  ";
            strsql += " NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND NF.IDROMANEIO = R.IDROMANEIO ";
            strsql += " AND DT.DATADESAIDA BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            strsql += " AND NF.DOCUMENTODOCLIENTE2 = 'COM AGENDAMENTO'  ";
            strsql += " )      ";
            strsql += " ) ";
            strsql += " / ";
            strsql += " (    ";
            strsql += " SELECT CAST(COUNT (DISTINCT NF.IDDOCUMENTO) AS DECIMAL)  ";
            strsql += " NOTASENVIADASVIAINTERFACE FROM MAPA MP ";
            strsql += " INNER JOIN MOVIMENTACAOITEM MI ON (MI.IDMAPA = MP.IDMAPA) ";
            strsql += " INNER JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = MI.IDDOCUMENTO) WHERE NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND MP.ESTOQUEPROCESSADO = 'SIM' ";
            strsql += " AND MP.DATADECADASTRO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            strsql += " AND MI.PEDIDONOTAFISCAL IS NOT NULL ";
            strsql += ")) *100 ";


            return Sistran.Library.GetDataTables.RetornarDecimal(strsql, "");
        }

        public decimal Form14(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();       


            strsql = " SELECT  ";
            strsql += " ((SELECT ";
            strsql += " CAST( COUNT(NF.IDDOCUMENTO) AS DECIMAL) RECLAMADOS ";
            strsql += " FROM DOCUMENTORECLAMACAO DR ";
            strsql += " INNER JOIN DOCUMENTO NF ON (NF.IDDOCUMENTO = DR.IDDOCUMENTO)  ";

            if (linha != "Todos")
            {
                strsql += " INNER JOIN DOCUMENTOITEM NFI ON NFI.IDDOCUMENTO = NF.IDDOCUMENTO ";
                strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM) ";
                strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ";
            }

            strsql += " WHERE ";
            strsql += " DR.DATADARECLAMACAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            strsql += " AND NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";

            if (linha != "Todos")
            {
                strsql += " AND PC.IDGRUPODEPRODUTO=" + linha;
            }


            strsql += " ) ";
            strsql += " / ";
            strsql += " (SELECT  ";
            strsql += " CAST( CASE WHEN COUNT(NF.IDDOCUMENTO)=0 THEN 1 ELSE COUNT(NF.IDDOCUMENTO) END  AS DECIMAL) NOTASEMBARCADAS ";
            strsql += " FROM DOCUMENTO NF  ";

            if (linha != "Todos")
            {
                strsql += " INNER JOIN DOCUMENTOITEM NFI ON NFI.IDDOCUMENTO = NF.IDDOCUMENTO ";
                strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM) ";
                strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ";
            }

            strsql += " WHERE  ";
            strsql += " NF.TIPODEDOCUMENTO = 'NOTA FISCAL' ";
            strsql += " AND NF.ATIVO = 'SIM' ";
            strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            strsql += " AND NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND NF.IDROMANEIO > 0 ";

            if (linha != "Todos")
            {
                strsql += " AND PC.IDGRUPODEPRODUTO = " + linha;
            }

            strsql += " )) ";
            strsql += "   *100 ";

            return Sistran.Library.GetDataTables.ExecutarRetornoID(strsql, "");
        }

        public decimal Form16(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();

            strsql = "  SELECT  ";
            strsql += " (SELECT ";
            strsql += " CAST (COUNT(*) AS DECIMAL)  ";
            strsql += " FROM DOCUMENTO NF ";
            strsql += " INNER JOIN DOCUMENTOITEM NFI ON (NFI.IDDOCUMENTO = NF.IDDOCUMENTO)  ";

            if (linha != "Todos")
            {
                strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM) ";
                strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ";
            }
            strsql += "   WHERE  ";
            strsql += " NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL' ";
            strsql += " AND NF.ATIVO = 'SIM' ";
            strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";

            if (linha != "Todos")
            {
                strsql += " AND PC.IDGRUPODEPRODUTO=" + linha;
            }
            strsql += " )LINHASSOLICITADAS ";
            strsql += " , ";
            strsql += " ( ";
            strsql += " SELECT ";
            strsql += " CAST(COUNT(*) AS DECIMAL)   ";

            strsql += " FROM DOCUMENTO NF ";
            strsql += " INNER JOIN DOCUMENTOITEM NFI ON (NFI.IDDOCUMENTO = NF.IDDOCUMENTO)  ";
            if (linha != "Todos")
            {

                strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM) ";
                strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ";
            }
            strsql += " WHERE ";
            strsql += " NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL' ";
            strsql += " AND NF.ATIVO = 'SIM' ";
            strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            strsql += " AND NFI.STATUSDOITEM = 'EXPEDICAO AUDITADA COM ERRO' ";
            
            if (linha != "Todos")
            {
                strsql += " AND PC.IDGRUPODEPRODUTO=" + linha;
            }
            
            strsql += " )LINHASERRADAS ";

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");

            if (dt.Rows.Count >= 1)
            {
                if (dt.Rows[0]["LINHASERRADAS"].ToString() == "0")
                {
                    return Convert.ToDecimal(0);
                }
                else
                {
                    return Convert.ToDecimal(dt.Rows[0]["LINHASSOLICITADAS"].ToString()) / (Convert.ToDecimal(dt.Rows[0]["LINHASERRADAS"].ToString()) + Convert.ToDecimal(dt.Rows[0]["LINHASSOLICITADAS"].ToString())) * Convert.ToDecimal(100);
                }
            }
            else
            {
                return Convert.ToDecimal(0);
            }
        }

        public decimal Form17(string linha, string DataInicio, string DataFim, ref string strsql)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();

            strsql = "  SELECT ( ";
            strsql += " SELECT ";
            strsql += " ISNULL(COUNT(*),0) ";
            strsql += " FROM DOCUMENTO NF ";
            strsql += " INNER JOIN DOCUMENTOITEM NFI ON (NFI.IDDOCUMENTO = NF.IDDOCUMENTO)  ";
            if (linha != "Todos")
            {
                strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM) ";
                strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ";
            }
            strsql += " WHERE ";
            strsql += " NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL' ";
            strsql += " AND NF.ATIVO = 'SIM' ";
            strsql += " AND NF.ENTRADASAIDA = 'ENTRADA' ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";

            if (linha != "Todos")
            {
                strsql += " AND PC.IDGRUPODEPRODUTO= " + linha;
            }

            strsql += " ) LINHASSOLICITADAS, ";

            strsql += " (SELECT ";
            strsql += " ISNULL(COUNT(*),0) LINHASERRADAS ";
            strsql += " FROM DOCUMENTO NF ";
            strsql += " INNER JOIN DOCUMENTOITEM NFI ON (NFI.IDDOCUMENTO = NF.IDDOCUMENTO)  ";

            if (linha != "Todos")
            {
                strsql += " INNER JOIN PRODUTOEMBALAGEM PE ON (PE.IDPRODUTOEMBALAGEM = NFI.IDPRODUTOEMBALAGEM) ";
                strsql += " INNER JOIN PRODUTOCLIENTE PC ON (PC.IDPRODUTOCLIENTE = PE.IDPRODUTOCLIENTE) ";
            }
            strsql += " WHERE ";
            strsql += " NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL' ";
            strsql += " AND NF.ATIVO = 'SIM' ";
            strsql += " AND NF.ENTRADASAIDA = 'ENTRADA' ";
            strsql += " AND NF.DATADEEMISSAO BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            strsql += " AND NFI.STATUSDOITEM = 'RECEBIMENTO AUDITADO COM ERRO' ";
            if (linha != "Todos")
            {
                strsql += " AND PC.IDGRUPODEPRODUTO= " + linha;
            }
            strsql += " ) LINHASERRADAS ";

            DataTable dt = Sistran.Library.GetDataTables.RetornarDataTable(strsql, "");

            if (dt.Rows.Count >= 1)
            {
                if (dt.Rows[0]["LINHASERRADAS"].ToString() == "0")
                {
                    return Convert.ToDecimal(0);
                }
                else
                {
                    //return Convert.ToDecimal(dt.Rows[0]["LINHASSOLICITADAS"].ToString()) / Convert.ToDecimal(dt.Rows[0]["LINHASERRADAS"].ToString()) + Convert.ToDecimal(dt.Rows[0]["LINHASSOLICITADAS"].ToString()) * Convert.ToDecimal(100);
                    return Convert.ToDecimal(dt.Rows[0]["LINHASSOLICITADAS"].ToString()) / (Convert.ToDecimal(dt.Rows[0]["LINHASERRADAS"].ToString()) + Convert.ToDecimal(dt.Rows[0]["LINHASSOLICITADAS"].ToString())) * Convert.ToDecimal(100);

                }
            }
            else
            {
                return Convert.ToDecimal(0);
            }
        }
    }

    public class Faturamento
    {
        public DataTable PalletsRecebidos(string DataInicio, string DataFim)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();
            string strsql = " SELECT ";
            strsql += " NF.IDDOCUMENTO,";
            strsql += " NF.NUMERO,";
            strsql += " NF.DATADEENTRADA,";
            strsql += " UAL.IDUNIDADEDEARMAZENAGEM ";
            strsql += " FROM DOCUMENTO NF ";
            strsql += " INNER JOIN LOTE LT ON (LT.IDDOCUMENTO = NF.IDDOCUMENTO) ";
            strsql += " INNER JOIN UNIDADEDEARMAZENAGEMLOTE UAL ON (UAL.IDLOTE = LT.IDLOTE)";
            strsql += " WHERE ";
            strsql += " NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND NF.ENTRADASAIDA = 'ENTRADA'";
            strsql += " AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL'";
            strsql += " AND NF.DATADEENTRADA BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            strsql += " ORDER BY NF.DATADEENTRADA ";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);

        }

        public DataTable LinhasExpedidas(string DataInicio, string DataFim)
        {
            DateTime DataFimTemp = Convert.ToDateTime(DataFim);
            DataFimTemp = DataFimTemp.AddHours(23);
            DataFim = DataFimTemp.ToString();
            string strsql = "  SELECT  ";
            strsql += " NF.IDDOCUMENTO, ";
            strsql += " NF.NUMERO, ";
            strsql += " NF.DATADEEMISSAO, ";
            strsql += " NFI.IDDOCUMENTOITEM LINHAEXPEDIDA ";
            strsql += " FROM DOCUMENTO NF  ";
            strsql += " INNER JOIN DOCUMENTOITEM NFI ON (NFI.IDDOCUMENTO = NF.IDDOCUMENTO) ";
            strsql += " WHERE  ";
            strsql += " NF.IDCLIENTE IN(" + Sistran.Library.FuncoesUteis.retornarClientes() + ")";
            strsql += " AND NF.ENTRADASAIDA = 'SAIDA' ";
            strsql += " AND NF.TIPODEDOCUMENTO = 'NOTA FISCAL' ";
            strsql += " AND NF.DATADEENTRADA BETWEEN CONVERT(DATETIME, '" + DataInicio + "', 103) AND  CONVERT(DATETIME, '" + DataFim + "', 103)  ";
            strsql += " ORDER BY NF.DATADEEMISSAO ";
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);

        }
    }
}