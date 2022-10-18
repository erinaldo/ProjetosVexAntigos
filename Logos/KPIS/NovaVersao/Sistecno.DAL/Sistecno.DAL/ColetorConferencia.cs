using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.DAL
{
   public  class ColetorConferencia
    {

       


       public DataTable RetornarVolumesByIdDocumento(int idDocumento, string cnx)
       {
           string strsql = "  SELECT CC.IDCOLETORCONFERENCIA, CCI.* FROM COLETORCONFERENCIA CC WITH (NOLOCK) LEFT JOIN COLETORCONFERENCIAVOLUME CCI WITH (NOLOCK) ON CCI.IDCOLETORCONFERENCIA = CC.IDCOLETORCONFERENCIA WHERE IDDOCUMENTO =" + idDocumento + "   ORDER BY CCI.IdColetorConferenciaVolume DESC";
           return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
       }

       public DataTable RetornarByIdDocumentoTop10(int idDocumento, string cnx)
       {
           string strsql = " SELECT TOP 10 CC.IDCOLETORCONFERENCIA, CCI.*, UNIDADEVENDA + ' | ' + CAST(QUANTIDADEUNIDADEVENDA AS VARCHAR(10)) EMBALAGEM ";
           strsql += " FROM COLETORCONFERENCIA CC WITH (NOLOCK)  ";
           strsql += " INNER JOIN COLETORCONFERENCIAITEM CCI WITH (NOLOCK) ON CCI.IDCOLETORCONFERENCIA = CC.IDCOLETORCONFERENCIA ";
           strsql += " LEFT JOIN COLETORCONFERENCIACDEAN CCEAN ON CCEAN.CODIGOBARRAS = CCI.CODIGODEBARRAS ";
           strsql += " WHERE IDDOCUMENTO =" + idDocumento;
           strsql += " ORDER BY CCI.IDCOLETORCONFERENCIAITEM DESC";

           return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
       }

       public DataTable RetornarByIdDocumentoTop(int idDocumento, int exibirQtosRegistros,  string cnx)
       {
           string strsql = " SELECT TOP "+ exibirQtosRegistros.ToString() +" CC.IDCOLETORCONFERENCIA, CCI.*, UNIDADEVENDA + ' | ' + CAST(QUANTIDADEUNIDADEVENDA AS VARCHAR(10)) EMBALAGEM ";
           strsql += " FROM COLETORCONFERENCIA CC WITH (NOLOCK)  ";
           strsql += " INNER JOIN COLETORCONFERENCIAITEM CCI WITH (NOLOCK) ON CCI.IDCOLETORCONFERENCIA = CC.IDCOLETORCONFERENCIA ";
           strsql += " LEFT JOIN COLETORCONFERENCIACDEAN CCEAN ON CCEAN.CODIGOBARRAS = CCI.CODIGODEBARRAS ";
           strsql += " WHERE IDDOCUMENTO =" + idDocumento;
           strsql += " ORDER BY CCI.IDCOLETORCONFERENCIAITEM DESC";

           return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
       }


       public string  RetornarByIdDocumento_Quantidade(int idDocumento, string cnx)
       {
           string strsql = " SELECT count(*) QTD ";
           strsql += " FROM COLETORCONFERENCIA CC WITH (NOLOCK)  ";
           strsql += " INNER JOIN COLETORCONFERENCIAITEM CCI WITH (NOLOCK) ON CCI.IDCOLETORCONFERENCIA = CC.IDCOLETORCONFERENCIA ";
           strsql += " LEFT JOIN COLETORCONFERENCIACDEAN CCEAN ON CCEAN.CODIGOBARRAS = CCI.CODIGODEBARRAS ";
           strsql += " WHERE IDDOCUMENTO =" + idDocumento;

           DataTable dt = DAL.BD.cDb.RetornarDataTable(strsql, cnx);

           if (dt.Rows.Count == 0)
               return "0";
           else
               return dt.Rows[0][0].ToString();
       }

       public DataTable RetornarByIdDocumento(int idDocumento ,string cnx)
       {
           string strsql = " SELECT  CC.IDCOLETORCONFERENCIA, CCI.*, UNIDADEVENDA + ' | ' + CAST(QUANTIDADEUNIDADEVENDA AS VARCHAR(10)) EMBALAGEM ";
           strsql += " FROM COLETORCONFERENCIA CC WITH (NOLOCK)  ";
           strsql += " INNER JOIN COLETORCONFERENCIAITEM CCI WITH (NOLOCK) ON CCI.IDCOLETORCONFERENCIA = CC.IDCOLETORCONFERENCIA ";
           strsql += " LEFT JOIN COLETORCONFERENCIACDEAN CCEAN ON CCEAN.CODIGOBARRAS = CCI.CODIGODEBARRAS ";
           strsql += " WHERE IDDOCUMENTO =" + idDocumento;
           return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
       }

       public DataTable Deletar(int IdColetorConferenciaItem, int IdDocumento,  string cnx)
       {
           DAL.BD.cDb.ExecutarSemRetorno("DELETE FROM COLETORCONFERENCIAITEM WHERE idCOLETORCONFERENCIAITEM=" + IdColetorConferenciaItem, cnx);
           return this.RetornarByIdDocumentoTop10(IdDocumento, cnx);
       }

       public DataTable DeletarVolumes(int IdColetorConferenciaVolume, int IdDocumento, string cnx)
       {
           DAL.BD.cDb.ExecutarSemRetorno("DELETE FROM COLETORCONFERENCIAVOLUME WHERE idCOLETORCONFERENCIAVOLUME=" + IdColetorConferenciaVolume, cnx);
           return this.RetornarVolumesByIdDocumento(IdDocumento, cnx);
       }

       public void CriarColetorConferencia(int idDocumento, int idFilial, int idUsuario,string CodigoDeBarras, int qtd , string CodPai,  string cnx)
       {
           string ssql = "";
           if (CodPai == "0")
           {
               CodPai = DAL.BD.cDb.RetornarIDTabela(cnx, "COLETORCONFERENCIA").ToString();

               ssql = "INSERT INTO COLETORCONFERENCIA (IdColetorConferencia, IdFilial,IdUsuario,IdDocumento,Data,Status,VolumesFaltantes) VALUES ";
               ssql += " (" + CodPai + " , " + idFilial + "," + idUsuario + "," + idDocumento + ",GETDATE(),'EM CONFERENCIA',1)";

               ssql += "; ";
           }

           string verificao = "SELECT COUNT(*) FROM COLETORCONFERENCIAVOLUME WHERE IdColetorConferencia=" + CodPai + "  AND CodigoDeBarras='" + CodigoDeBarras + "'";
           int qtds = DAL.BD.cDb.ExecutarRetornoID(verificao, cnx);

           if (qtds == 0)
           {
               ssql += " INSERT INTO COLETORCONFERENCIAVOLUME (IdColetorConferenciaVolume, IdColetorConferencia, CodigoDeBarras) VALUES ";
               ssql += "(" + DAL.BD.cDb.RetornarIDTabela(cnx, "COLETORCONFERENCIAVOLUME").ToString() + ", " + CodPai + ", '" + CodigoDeBarras + "')";
               DAL.BD.cDb.ExecutarSemRetorno(ssql, cnx);
           }

       }

       public void UpdateColetorConferencia(int IdColetorConferencia, string status, string codigoRetorno, string DescricaoRetorno, string cnx)
       {
           string ssql = " UPDATE COLETORCONFERENCIA SET STATUS ='" + status + "' , data=GetDate()";

           if (codigoRetorno.Length > 0)
           {
              ssql +=  " , CodigoRetorno='"+codigoRetorno+"',  DescricaoRetorno='"+DescricaoRetorno+"'";
           }

           ssql += " WHERE IdColetorConferencia=" + IdColetorConferencia;
           DAL.BD.cDb.ExecutarSemRetorno(ssql, cnx);
        
       }


     /// <summary>
     /// Grava com Log
     /// </summary>
     /// <param name="IdColetorConferencia"></param>
     /// <param name="status"></param>
     /// <param name="codigoRetorno"></param>
     /// <param name="DescricaoRetorno"></param>
     /// <param name="idFilial"></param>
     /// <param name="idUsuario"></param>
     /// <param name="idDocumento"></param>
     /// <param name="cnx"></param>
       public void UpdateColetorConferencia(int IdColetorConferencia, string status, string codigoRetorno, string DescricaoRetorno, string idFilial, int idUsuario, string idDocumento, string cnx)
       {
           string ssql = " UPDATE COLETORCONFERENCIA SET STATUS ='" + status + "' , data=GetDate()";

           if (codigoRetorno.Length > 0)
           {
               ssql += " , CodigoRetorno='" + codigoRetorno + "',  DescricaoRetorno='" + DescricaoRetorno + "'";
           }

           ssql += " WHERE IdColetorConferencia=" + IdColetorConferencia;
           DAL.BD.cDb.ExecutarSemRetorno(ssql, cnx);

           string ID = DAL.BD.cDb.RetornarIDTabela(cnx, "COLETORCONFERENCIALOG").ToString();
           ssql = "INSERT INTO COLETORCONFERENCIALOG (IdColetorConferenciaLog, IdFilial,IdUsuario,IdDocumento,Data,Status,VolumesFaltantes, DescricaoRetorno) VALUES ";
           ssql += " (" + ID + " , " + idFilial + "," + idUsuario + "," + idDocumento + ",GETDATE(),'ENVIOU DADOS' ,1, '" + DescricaoRetorno + "')";
           ssql += "; ";

           DAL.BD.cDb.ExecutarSemRetorno(ssql, cnx);

           string carta = "";

           carta += "IDCOLETOR CONFERENCIA: " + IdColetorConferencia + "<BR>";
           carta += "STATUS: " + status + "<BR>";
           carta += "CODIGO DE RETORNO: " + codigoRetorno + "<BR>";
           carta += "DESCRIÇÃO DE RETORNO: " + DescricaoRetorno + "<BR>";
           carta += "IDFILAIL: " + idFilial + "<BR>";
           carta += "IDUSUARIO: " + idUsuario + "<BR>";
           carta += "IDDOCUMENTO: " + idDocumento + "<BR>";


           try
           {
               //frwSistecno.OperacoesEmail.EnviarInfoConferencia(carta);
           }
           catch (Exception)
           { }
       }

       public void CriarItem(string CodPai, string CodigoDeBarras, int qtd, string cnx)
       {
           string ssql = "";
           ssql += " INSERT INTO COLETORCONFERENCIAITEM (IdColetorConferenciaItem, IdColetorConferencia, CodigoDeBarras, Quantidade) VALUES ";
           ssql += "(" + DAL.BD.cDb.RetornarIDTabela(cnx, "COLETORCONFERENCIAITEM").ToString() + ", " + CodPai + ", '" + CodigoDeBarras + "', (select isnull(QuantidadeUnidadeVenda, 1) from coletorConferenciaCdEan where codigobarras='"+CodigoDeBarras+"'))";
           DAL.BD.cDb.ExecutarSemRetorno(ssql, cnx);  
       }


       public DataTable RetornarCodigoDeBarras(string CodigoDeBarras, string cnx)
       {
           string ssql = "SELECT * FROM COLETORCONFERENCIACDEAN WHERE CODIGOBARRAS='" + CodigoDeBarras + "'";
           return DAL.BD.cDb.RetornarDataTable(ssql, cnx);
           
       }

       public void excluirAllItens(string p, string cnx)
       {
           string ssql = "DELETE FROM COLETORCONFERENCIAITEM WHERE IDCOLETORCONFERENCIA=" + p;
           DAL.BD.cDb.ExecutarSemRetorno(ssql, cnx);  
       }


       public DataTable RetornarRptEnvio(string idFilial, string Data, string Status, string cnx)
       {
           string strsql = "";
           strsql += "  SELECT CC.DATA, D.IDDOCUMENTO, D.NUMERO, F.NOME FILIAL, CC.IDCOLETORCONFERENCIA, U.LOGIN USUARIO, CC.STATUS, CC.DESCRICAORETORNO, CC.CODIGORETORNO, ";
           strsql += " COUNT(DISTINCT CCV.IDCOLETORCONFERENCIAVOLUME) VOLUMES, ";
           strsql += " COUNT(DISTINCT CCI.IDCOLETORCONFERENCIAITEM) ITENS ";
           strsql += " FROM COLETORCONFERENCIA CC ";
           strsql += " INNER JOIN COLETORCONFERENCIAVOLUME CCV ON CCV.IDCOLETORCONFERENCIA = CC.IDCOLETORCONFERENCIA ";
           strsql += " INNER JOIN COLETORCONFERENCIAITEM CCI ON CCI.IDCOLETORCONFERENCIA = CC.IDCOLETORCONFERENCIA ";
           strsql += " INNER JOIN FILIAL F ON F.IDFILIAL = CC.IDFILIAL ";
           strsql += " INNER JOIN USUARIO U ON U.IDUSUARIO = CC.IDUSUARIO ";
           strsql += " INNER JOIN DOCUMENTO D ON D.IDDOCUMENTO = CC.IDDOCUMENTO ";
           strsql += " WHERE DAY(GETDATE()) = DAY(DATA)  ";
           strsql += " AND MONTH(GETDATE())=MONTH(DATA) ";
           strsql += " AND YEAR(GETDATE()) = YEAR(DATA) ";
           strsql += " AND F.IDFILIAL =  " + idFilial;
           strsql += " GROUP BY CC.DATA,D.IDDOCUMENTO, D.NUMERO, F.NOME, CC.IDCOLETORCONFERENCIA, U.LOGIN, CC.STATUS, CC.DESCRICAORETORNO, CC.CODIGORETORNO ";
           strsql += " ORDER BY DATA DESC ";


           return DAL.BD.cDb.RetornarDataTable(strsql, cnx);
       }
    }
}
