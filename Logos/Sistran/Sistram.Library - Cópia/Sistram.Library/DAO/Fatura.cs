using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SistranDAO
{
    public class Fatura
    {
        public DataTable Listar(string IDTITULODUPLICATA)
        {
            //string strsql = " SELECT  * FROM TITULODUPLICATA TD   INNER JOIN TITULO T ON (T.IDTITULO = TD.IDTITULO)    INNER JOIN CADASTRO CL ON (CL.IDCADASTRO = T.IDCLIENTE) WHERE   T.PAGARRECEBER = 'RECEBER'  AND TD.IDTITULODUPLICATA =" + IDTITULODUPLICATA;
            string strsql = " SELECT  ";
            strsql += " ISNULL(TD.VALOR,0) + (ABS(DATEDIFF(DAY, DATADEVENCIMENTOREAL , GETDATE())) * ISNULL(JUROSDIARIO,0)) + ISNULL(MULTA,0) VALORATUALIZADO,  ";
            strsql += " RAZAOSOCIALNOME,  ";
            strsql += " T.NUMERO,   ";
            strsql += " T.IDTITULO,  ";
            strsql += " GETDATE() DATADEVENCIMENTO , TD.VALOR, TD.DATADEVENCIMENTOREAL  ";
            strsql += " FROM TITULODUPLICATA TD     ";
            strsql += " INNER JOIN TITULO T ON (T.IDTITULO = TD.IDTITULO)      ";
            strsql += " INNER JOIN CADASTRO CL ON (CL.IDCADASTRO = T.IDCLIENTE)   ";
            strsql += " WHERE   T.PAGARRECEBER = 'RECEBER'    ";
            strsql += " AND TD.IDTITULODUPLICATA =  " + IDTITULODUPLICATA;
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }

        public DataTable ListarbyIdTitulo(string IdTitulo)
        {
            string strsql = " Select ";
            strsql += " CT.Numero CTR, ";
            strsql += " Convert(Varchar(10), CT.DataDeEmissao,103) DataDeEmissao,  ";
            strsql += " CT.ValorDaNota,  ";
            strsql += " Convert(Varchar(10), CT.DataDeConclusao,103) DataDeConclusao,  ";
            strsql += " NF.IdDocumento IdNotaFiscal,  ";
            strsql += " NF.Numero NotaFiscal  ";
            strsql += " From TituloDocumento TD  ";
            strsql += " Inner Join Documento CT on (CT.IDDocumento = TD.IdDocumento)  ";
            strsql += " Left Join DocumentoRelacionado DR on (DR.IdDocumentoPai = CT.IDDocumento)  ";
            strsql += " Left Join Documento NF on (NF.IDDocumento = DR.IdDocumentoFilho and NF.TipoDeDocumento = 'NOTA FISCAL')  ";
            strsql += " where TD.IdTitulo=" + IdTitulo;
            return Sistran.Library.GetDataTables.RetornarDataTable(strsql);
        }

        public DataTable Boleto(string IdTitulo)
        {
            string strsql = "";
            /*strsql += " TOP 1 ";
            strsql += " '1' SEQUENCIAL, NOSSONUMERO,";
            strsql += " GETDATE() DATADEEMISSAO, ";
            strsql += " DATADEVENCIMENTO , ";
            strsql += " getdate() DATADODOCUMENTO, ";
            strsql += " GETDATE() DADADEPROCESSAMENTO, ";
            strsql += " TD.IDTITULO NUMERO, ";
            strsql += " TD.VALOR VALOR, ";
            strsql += " BNC.CODIGO BANCO,  ";
            strsql += " BNC.NOME BANCONOME, ";
            strsql += " BCART.CODIGO CARTEIRA, ";
            strsql += " TD.IDTITULO DOCUMENTO, ";
            strsql += " BC.DESCRICAO NOME, ";
            strsql += " BC.AGENCIA AGENCIA, ";
            strsql += " BC.CONTA CONTA, ";
            strsql += " BC.CONTADIGITO CONTADV, ";
            strsql += " 'NAO RECEBER APOS VENCIMENTO' INSTRUCAO, ";
            strsql += " CAD.RAZAOSOCIALNOME NOMESACADO, ";
            strsql += " CAD.CNPJCPF CNPJSACADO, ";
            strsql += " CAD.ENDERECO + ', ' + ISNULL(CAD.NUMERO,'') + ' - ' + ISNULL(CAD.COMPLEMENTO,'') ENDERECOSACADO , ";
            strsql += " CAD.CEP, ";
            strsql += " CIDADE.NOME CIDADE, ";
            strsql += " ESTADO.UF ESTADO ";
            strsql += " FROM TITULODUPLICATA  TD ";
            strsql += " INNER JOIN TITULO TIT ON TIT.IDTITULO = TD.IDTITULO ";
            strsql += " INNER JOIN BANCOCONTA  BC ON BC.IDBANCOCONTA = TD.IDBANCOCONTA ";
            strsql += " INNER JOIN BANCO BNC ON BNC.IDBANCO = BC.IDBANCO ";
            strsql += " INNER JOIN BANCOCARTEIRA BCART ON BCART.IDBANCO = BC.IDBANCO ";
            strsql += " INNER JOIN CADASTRO CAD ON CAD.IDCADASTRO = IDCLIENTE ";
            strsql += " LEFT JOIN CIDADE ON CIDADE.IDCIDADE = CAD.IDCIDADE ";
            strsql += " LEFT JOIN ESTADO ON ESTADO.IDESTADO = CIDADE.IDESTADO ";
            strsql += " WHERE TIT.IDTITULO=" + IdTitulo; */

            strsql += "  SELECT  TOP 1  '1' SEQUENCIAL, ";
            strsql += " ISNULL(BCB.NOSSONUMERO, 0) NOSSONUMERO,  ";
            strsql += " BCB.CODIGODOCEDENTE, ";
            strsql += " GETDATE() DATADEEMISSAO,   ";
            strsql += " GETDATE() DATADEVENCIMENTO ,   ";
            strsql += " GETDATE() DATADODOCUMENTO,   ";
            strsql += " GETDATE() DADADEPROCESSAMENTO,   ";
            strsql += " TD.IDTITULO NUMERO,   ";
            strsql += " TD.SALDO VALOR,   ";
            strsql += " BNC.CODIGO BANCO,    ";
            strsql += " BNC.NOME BANCONOME,   ";
            strsql += " BCOCART.CODIGO CARTEIRA, ";
            strsql += " TD.IDTITULO DOCUMENTO,   ";
            strsql += " BC.DESCRICAO NOME,   ";
            strsql += " BC.AGENCIA AGENCIA,   ";
            strsql += " BC.CONTA CONTA,   ";
            strsql += " BC.CONTADIGITO CONTADV,   ";
            //strsql += " 'NAO RECEBER APOS VENCIMENTO' INSTRUCAO,   ";
            strsql += " 'NAO RECEBER APOS VENCIMENTO' +  '<br>'+";
            strsql += " isnull(InstrucaoLivre01, '') + '<br>'+ ";
            strsql += " isnull(InstrucaoLivre02, '') +'<br>'+ ";
            strsql += " isnull(InstrucaoLivre03 , '') +'<br>'+ ";
            strsql += " isnull(InstrucaoLivre04, '') +'<br>'+ ";
            strsql += " isnull(InstrucaoLivre05, '')INSTRUCAO,     ";
            strsql += " CAD.RAZAOSOCIALNOME NOMESACADO,   ";
            strsql += " CAD.CNPJCPF CNPJSACADO,   ";
            strsql += " CAD.ENDERECO + ', ' + ISNULL(CAD.NUMERO,'') + ' - ' + ISNULL(CAD.COMPLEMENTO,'') ENDERECOSACADO ,   ";
            strsql += " CAD.CEP,  CIDADE.NOME CIDADE,  ESTADO.UF ESTADO   ";
            strsql += " FROM TITULODUPLICATA  TD   ";
            strsql += " INNER JOIN TITULO TIT ON TIT.IDTITULO = TD.IDTITULO   ";
            strsql += " INNER JOIN BANCOCONTA  BC ON BC.IDBANCOCONTA = TD.IDBANCOCONTA   ";
            strsql += " INNER JOIN BANCO BNC ON BNC.IDBANCO = BC.IDBANCO   ";

            strsql += " INNER JOIN BANCOCONTABLOQUETO BCB ON (BCB.IDBANCOCONTABLOQUETO = TD.IDBANCOCONTABLOQUETO)  ";
            strsql += " INNER JOIN BANCOCARTEIRA BCOCART ON BCOCART.IDBANCOCARTEIRA = BCB.IDBANCOCARTEIRA  ";
            strsql += " INNER JOIN CADASTRO CAD ON CAD.IDCADASTRO = IDCLIENTE   ";
            strsql += " LEFT JOIN CIDADE ON CIDADE.IDCIDADE = CAD.IDCIDADE   ";
            strsql += " LEFT JOIN ESTADO ON ESTADO.IDESTADO = CIDADE.IDESTADO  ";
            strsql += " WHERE TIT.IDTITULO=" + IdTitulo;
            return Sistran.Library.GetDataTables.RetornarDataTableBoleto(strsql);
        }

        public void AtualizarDataValorBoleto(string idtitulo)
        {
            string strsql = "";
            strsql += " UPDATE TITULODUPLICATA SET DATADEVENCIMENTO = GETDATE(), SALDO=VALOR + ABS(DATEDIFF(DAY, DATADEVENCIMENTOREAL , GETDATE())) * ISNULL(JUROSDIARIO,0) + ISNULL(MULTA,0) ";
            strsql += "WHERE IDTITULODUPLICATA IN  ";
            strsql += "(SELECT   ";
            strsql += "IDTITULODUPLICATA ";
            strsql += "FROM TITULODUPLICATA  TD   ";
            strsql += "INNER JOIN TITULO TIT ON TIT.IDTITULO = TD.IDTITULO    ";
            strsql += "WHERE TIT.IDTITULO= " + idtitulo + ") select '1'";

            Sistran.Library.GetDataTables.RetornarDataTableBoleto(strsql);
        }
    }
}