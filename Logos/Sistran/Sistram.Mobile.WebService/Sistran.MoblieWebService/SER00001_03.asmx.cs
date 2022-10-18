using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.IO;
using System.Text;

namespace Sistran.MoblieWebService
{
    /// <summary>
    /// Summary description for SER00001_03
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class SER00001_03 : System.Web.Services.WebService
    {

        string conn = "Data Source=192.168.10.5;Initial Catalog=GrupoLogosTeste;User ID=sa;Password=@logos09022005$;";

        [WebMethod]
        public DataTable verificar_documento(string num_dc, string num_pl, string num_dt)
        {
            //HttpContext.Current.Session["ConnLogin"] = conn;
            string strsql = " ";
            strsql += " SELECT ";
            strsql += " Doc.IDDocumento, ";
            strsql += " Doc.Numero AS NumeroDocumento,  ";
            strsql += " Doc.IDFilialAtual, ";
            strsql += " Doc.Volumes, ";
            strsql += " Doc.PesoBruto, ";
            strsql += " Vei.Placa,  ";
            strsql += " substring(Vei.placa,5,8) as NumeroPlaca, ";
            strsql += " DT.Numero AS NumeroDT, ";
            strsql += " dbo.FREMOVE_ACENTOS(CadRem.RazaoSocialNome) As Remetente, ";
            strsql += " dbo.FREMOVE_ACENTOS(CadDes.RazaoSocialNome) As Destinatario ";
            strsql += " FROM Documento Doc ";
            strsql += " INNER JOIN RomaneioDocumento RomDoc ";
            strsql += "  on(Doc.IDDocumento = RomDoc.IDDocumento) ";
            strsql += " INNER JOIN DTRomaneio DTRom ";
            strsql += " on(DTRom.IDRomaneio = RomDoc.IDRomaneio) ";
            strsql += " INNER JOIN DT ";
            strsql += " on(DT.IDDT = DTRom.IDDT) ";
            strsql += " INNER JOIN Veiculo Vei ";
            strsql += " on(Vei.IDVeiculo = DT.IDPrimeiroVeiculo) ";
            strsql += " LEFT JOIN Cadastro CadRem ";
            strsql += " on(CadRem.IDCadastro = Doc.IDRemetente) ";
            strsql += " LEFT JOIN Cadastro CadDes ";
            strsql += " on(CadDes.IDCadastro = Doc.IDDestinatario) ";
            strsql += " WHERE Doc.Numero = '" + num_dc + "' ";
            strsql += " and substring(Vei.placa,5,8) = '" + num_pl + "' ";
            strsql += " and DT.Numero = '" + num_dt + "' ";

            return Sistran.Library.GetDataTables.RetornarDataTable(strsql, conn);

        }

        [WebMethod]
        public DataTable listar_ocorrencias()
        {
            string strsql = " ";
            strsql += " SELECT ";
            strsql += " dbo.FREMOVE_ACENTOS(Oco.NomeReduzido) AS NomeReduzido, ";
            strsql += " Oco.Codigo, ";
            strsql += " Oco.IDOcorrencia, ";
            strsql += " Oco.Finalizador ";
            strsql += " FROM Ocorrencia Oco ";
            strsql += " WHERE ";
            strsql += " Oco.NomeReduzido is not null ";
            return Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, conn);
        }

        [WebMethod]
        public DataTable listar_documentos(string placa, string dt)
        {
            string strsql = " ";
            strsql += " SELECT ";
            strsql += " Doc.IdDocumentoOcorrencia, ";
            strsql += " Doc.Numero As NumeroDocumento, ";
            strsql += " Doc.IDDocumento, ";
            strsql += " Doc.IDFilialAtual, ";
            strsql += " coalesce(Doc.Volumes,0) AS Volumes, ";
            strsql += " coalesce(Doc.PesoBruto,0) AS PesoBruto, ";
            strsql += " Vei.Placa, ";
            strsql += " substring(Vei.placa,5,8) as NumeroPlaca, ";
            strsql += " DT.IDDT, ";
            strsql += " dbo.FREMOVE_ACENTOS(CadRem.RazaoSocialNome) As Remetente, ";
            strsql += " dbo.FREMOVE_ACENTOS(CadDes.RazaoSocialNome)As Destinatario, ";
            strsql += " dbo.FREMOVE_ACENTOS(CadDes.Endereco) As Endereco, ";
            strsql += " CadDes.Numero, ";
            strsql += " dbo.FREMOVE_ACENTOS(Bai.Nome) As Bairro, ";
            strsql += " dbo.FREMOVE_ACENTOS(Cid.Nome) As Cidade, ";
            strsql += " dbo.FREMOVE_ACENTOS(Est.Nome) As Estado, ";
            strsql += " dbo.FREMOVE_ACENTOS(Pai.Nome) As Pais, ";
            strsql += " Oco.Codigo As Ocorrencia, ";
            strsql += " COALESCE(Ras.Tempo, 60) As Tempo, ";
            strsql += " Ras.EnviaPosicaoZerada, ";
            strsql += " NULL AS ChaveOrigem ";
            strsql += " From DT ";
            strsql += " INNER JOIN DTRomaneio DTRom on(DTRom.IdDt = Dt.IdDt) ";
            strsql += " INNER JOIN RomaneioDocumento RomDoc on (RomDoc.IdRomaneio = DtRom.IdRomaneio) ";
            strsql += " INNER JOIN Documento Doc on (Doc.IdDocumento = RomDoc.IdDocumento) ";
            strsql += " INNER JOIN Veiculo Vei on(Vei.IDVeiculo = DT.IDPrimeiroVeiculo) ";
            strsql += " LEFT JOIN Cadastro CadRem  on(CadRem.IDCadastro = Doc.IDRemetente) ";
            strsql += " LEFT JOIN Cadastro CadDes on(CadDes.IDCadastro = Doc.IDDestinatario) ";
            strsql += " LEFT JOIN Bairro Bai  on(Bai.IDBairro = CadDes.IDBairro) ";
            strsql += " LEFT JOIN Cidade Cid  on(Cid.IDCidade = CadDes.IDCidade) ";
            strsql += " LEFT JOIN Estado Est on(Est.IDEstado = Cid.IDEstado)        ";
            strsql += " LEFT JOIN Pais Pai  on(Pai.IDPais = Est.IDPais) ";
            strsql += " Left Join DocumentoOcorrencia DocOco on (DocOco.IdDocumentoOcorrencia=Doc.IdDocumentoOcorrencia) ";
            strsql += " Left Join Ocorrencia Oco on (Oco.IdOcorrencia=DocOco.IdOcorrencia) ";
            strsql += " Left Join Rastreador Ras on (Ras.IdRastreador=DT.IdRastreador) ";
            strsql += " WHERE substring(Vei.placa,5,4) = '" + placa + "'";
            strsql += " and DT.Numero ='" + dt + "' ";
            strsql += " ORDER by Doc.Numero ASC ";
            return Sistran.Library.GetDataTables.RetornarDataTableWS(strsql, conn);
        }

        [WebMethod]
        public string file_listar_ocorrencias()
        {
            string Retorno = "";

            DataTable dt = listar_ocorrencias();

            foreach (DataRow item in dt.Rows)
            {
                Retorno += item["NomeReduzido"] + "|";
                Retorno += item["Codigo"] + "|";
                Retorno += item["IDOcorrencia"] + "|";
                Retorno += item["Finalizador"] + "|";
                Retorno += ";";
            }
            return Retorno;

        }

        [WebMethod]
        public void file_listar_documentos(string placa, string dt)
        {
            DataTable dts = listar_documentos(placa, dt);
            string Retorno = "";

            foreach (DataRow item in dts.Rows)
            {
                Retorno += item["IDDocumento"] + "|";
                Retorno += item["IDDT"] + "|";
                Retorno += item["NumeroDocumento"] + "|";
                Retorno += item["IDFilialAtual"] + "|";
                Retorno += item["Volumes"] + "|";
                Retorno += item["PesoBruto"] + "|";
                Retorno += item["Placa"] + "|";
                Retorno += item["Remetente"] + "|";
                Retorno += item["Destinatario"] + "|";
                //Retorno += item["End"] + "|";
                Retorno += item["Ocorrencia"] + "|";
                Retorno += item["Tempo"] + "|";
                Retorno += item["EnviaPosicaoZerada"] + "|";
                Retorno += item["ChaveOrigem"] + "|;";
            }

            string pt = Server.MapPath("~/webservice_file");
            StreamWriter valor = new StreamWriter(pt + "\\"+placa+".txt", true, Encoding.ASCII);

            valor.Write(Retorno);
            valor.Close();

        }

        [WebMethod]
        public void insert_ocorrencia(string ocorrencia_codigo, string ocorrencia_id_documento, string ocorrencia_id_filial, string ocorrencia_id_romaneio, string ocorrencia_descricao, string ocorrencia_data_inicio)
        {

        }
    }
}