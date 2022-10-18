using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ComponentModel;
using System.Data;

namespace ServicosWEB
{
    /// <summary>
    /// Summary description for VilaNova
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VilaNova : System.Web.Services.WebService
    {

        [WebMethod]
        public string RetornarDataDeEntrega(string CnpjCliente, string NumeroNF, string Serie)
        {
            try
            {
                CnpjCliente = FormatarCnpj(CnpjCliente.Trim());

                string sql = "select (Select DataOcorrencia from DocumentoOcorrencia do where do.IdDocumentoOcorrencia=d.IdDocumentoOcorrencia) DataDeEntrega from Cadastro c ";
                sql += " inner join Cliente cc on cc.idCliente = c.IDCadastro ";
                sql += " inner join Documento d on d.IdCliente = cc.IdCliente ";
                sql += " where c.CnpjCpf = '" + CnpjCliente.Trim() + "' ";
                sql += " and d.numero ='" + NumeroNF.Trim() + "' and Serie='" + Serie.Trim() + "' ";
                sql += " and d.TipoDeDocumento='Nota Fiscal' and d.Ativo='SIM'";

                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
               DataTable dt =  Sistran.Library.GetDataTables.RetornarDataSetWS(sql, cnx).Tables[0];

               if (dt.Rows.Count == 0)
                   return "Nota Fiscal não encontrada";


               if (dt.Rows[0][0].ToString() != "")
               {
                   return DateTime.Parse(dt.Rows[0][0].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
               }
               else
                   return "";


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string FormatarCnpj(string s)
        {
            s = s.Replace(".", "");
            s = s.Replace("-", "");
            s = s.Replace("/", "");
            s = s.Replace(@"\", "");

            if (s.Length == 0)
            {
                return "";
            }

            if (s.Length <= 11)
            {
                MaskedTextProvider mtpCpf = new MaskedTextProvider(@"000\.000\.000-00");
                mtpCpf.Set(ZerosEsquerda(s, 11));
                return mtpCpf.ToString();
            }
            else
            {
                MaskedTextProvider mtpCnpj = new MaskedTextProvider(@"00\.000\.000/0000-00");
                mtpCnpj.Set(ZerosEsquerda(s, 11));
                return mtpCnpj.ToString();
            }
        }

        public static string ZerosEsquerda(string strString, int intTamanho)
        {

            string strResult = "";

            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {

                strResult += "0";

            }

            return strResult + strString;

        }
    }
}
