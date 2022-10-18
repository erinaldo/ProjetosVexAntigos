using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.ComponentModel;


namespace ServicosWEB
{
    /// <summary>
    /// Summary description for ConsultarNF
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class ConsultarNF : System.Web.Services.WebService
    {

        [WebMethod]
        public List<RetornoNF> RetornarNF(string Usuario, string Senha, string Cnpj, string Numero, DateTime DataInicio, DateTime DataFim)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            string sql = "exec PRC_RetornarNfeOcorrencias '" + FormatarCnpj(Cnpj.Trim()).Trim() + "' ,  @Di@ , @Df@ , @Numero@";

            try
            {
                if(Senha=="" || Usuario=="")
                    throw new Exception("Informe Usuario e Senha");


                if (Cnpj == "" || Cnpj == null)
                    throw new Exception("Informe o CNPJ");

                if (Numero != null && Numero != "")
                {
                    sql = sql.Replace("@Di@", "NULL");
                    sql = sql.Replace("@Df@", "NULL");
                    sql = sql.Replace("@Numero@", Numero.ToString());
                }
                else
                {
                    if (DataInicio != null && DataFim != null)
                    {
                        TimeSpan ts = (DateTime)DataFim - (DateTime)DataInicio;

                        if (ts.Days > 7)
                            throw new Exception("O Intervalo entre as Datas de Pesquisa não Podem Ultrapassar 7 dias");
                        sql = sql.Replace("@Di@", "'" + ((DateTime)DataInicio).ToString("yyyy-MM-dd 00:00:00") + "'");
                        sql = sql.Replace("@Df@", "'" + ((DateTime)DataFim).ToString("yyyy-MM-dd 23:59:59") + "'");
                        sql = sql.Replace("@Numero@", "NULL");
                    }
                }

                DataTable dt = Sistran.Library.GetDataTables.RetornarDataTableWS(sql.ToUpper(), cnx);
                List<RetornoNF> ret = new List<RetornoNF>();

                if (dt.Rows.Count > 0)
                {


                    DataView view = new DataView(dt);
                    DataTable distinctValues = view.ToTable(true, "IdDocumento");


                   // var distinctsIds = distinctValues.Rows.
                    //var distinctsIds = 
                    //    (from row in dt.AsEnumerable()
                    //     select row.Field<Int32>("IdDocumento")).Distinct().ToArray();


                    for (int i = 0; i < distinctValues.Rows.Count; i++)
                    {
                        var linhas = dt.Select("IdDocumento=" + distinctValues.Rows[i][0].ToString(), "DataOcorrencia desc");
                        List<Ocorrencia> lo = new List<Ocorrencia>();
                        for (int ii = 0; ii < linhas.Length; ii++)
                        {
                            if (linhas[ii]["Arquivo"] == DBNull.Value)
                            {
                                lo.Add(new Ocorrencia()
                                {
                                    CodigoOcorrencia = linhas[ii]["Codigo"].ToString(),
                                    DataOcorrencia = linhas[ii]["DataOcorrencia"].ToString(),
                                    DescrOcorrencia = linhas[ii]["Nome"].ToString()
                                });
                            }
                            else
                            {
                                lo.Add(new Ocorrencia()
                            {
                                ComprovanteDeEntrega = (byte[])linhas[ii]["Arquivo"],
                                TipoDeArquivo = ".jpg",
                                CodigoOcorrencia = linhas[ii]["Codigo"].ToString(),
                                DataOcorrencia = linhas[ii]["DataOcorrencia"].ToString(),
                                DescrOcorrencia = linhas[ii]["Nome"].ToString()
                            });
                            }
                        }

                        ret.Add(new RetornoNF()
                        {
                            NumeroNF = linhas[0]["Numero"].ToString(),
                            Serie = linhas[0]["Serie"].ToString(),
                            Ocorrencias = lo
                        });

                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region CNPJ / CPF
        public string FormatarCnpj(string s)
        {
            s = s.Replace(".", "");
            s = s.Replace("-", "");
            s = s.Replace("/", "");
            s = s.Replace(@"\", "");

            string resultCNPJ = "SIM";
            string resultCPF = "SIM";

            if (!IsCnpj(s))
                resultCNPJ = "NAO";

            if (!IsCpf(s))
                resultCPF = "NAO";

            if (resultCNPJ == "NAO" && resultCPF == "NAO")
                throw new Exception("CNPJ/CPF Inválidos");



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
                s = ZerosEsquerda(s, 14);
                mtpCnpj.Set(s);
                return mtpCnpj.ToString();
            }
        }

        public string ZerosEsquerda(string strString, int intTamanho)
        {

            string strResult = "";

            for (int intCont = 1; intCont <= (intTamanho - strString.Length); intCont++)
            {

                strResult += "0";

            }

            return strResult + strString;

        }

        public static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        #endregion
    }


    public class RetornoNF
    {
        public string NumeroNF { get; set; }
        public string Serie { get; set; }
        public string TipoDeDocumento { get; set; }
        public List<Ocorrencia> Ocorrencias { get; set; }
    }

    public class Ocorrencia
    {
        public string CodigoOcorrencia { get; set; }
        public string DescrOcorrencia { get; set; }
        public string DataOcorrencia { get; set; }
        public byte[] ComprovanteDeEntrega { get; set; }
        public string TipoDeArquivo { get; set; }
    }
}
