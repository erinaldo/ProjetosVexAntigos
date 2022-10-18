using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Services;


namespace ServicosWEB
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class OcorrenciaCliente : System.Web.Services.WebService
    {

        public class RetornoStatusNfesClientes
        {
            public bool Erro { get; set; }
            public string DescErro { get; set; }
            public string Chave { get; set; }

            public List<OcorrenciasClientes> Ocorrencias { get; set; }


        }

        public class OcorrenciasClientes
        {
            public string CodigoOcorrencia { get; set; }
            public string Ocorrencia { get; set; }
            public string OcorrenciaReduzida { get; set; }
            public string DataOcorrencia { get; set; }

            public string Comprovante { get; set; }
            public string Cidade { get; set; }
            public string Uf { get; set; }
        }

        [WebMethod]
        public RetornoStatusNfesClientes RetornarStatusNfesClientes(string user, string senha, string cnpj, string chave)
        {
            RetornoStatusNfesClientes ret = new RetornoStatusNfesClientes();

            try
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

                if (user == "oco2021" && senha == "F7DCE6CBD1AE")
                {
                    cnpj = FormatarCnpj(cnpj);
                    cnpj = cnpj.Substring(0, 10) + "%";

                    //38.776.381/0001-78
                    string sql = "exec RetornarOcorrenciasPorChave '" + chave + "', '" + cnpj + "'";
                    var dt = Sistran.Library.GetDataTables.RetornarDataTableWin(sql, cnx);


                    List<OcorrenciasClientes> oco = new List<OcorrenciasClientes>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ret.Chave = chave;
                        oco.Add(new OcorrenciasClientes()
                        {
                            CodigoOcorrencia = dt.Rows[i]["CodigoOcorrencia"].ToString(),
                            DataOcorrencia = DateTime.Parse(dt.Rows[i]["DataOcorrencia"].ToString()).ToString("yyyy-MM-dd HH:mm:ss"),
                            Ocorrencia = dt.Rows[i]["Ocorrencia"].ToString(),
                            OcorrenciaReduzida = dt.Rows[i]["OcorrenciaReduzida"].ToString(),
                            Comprovante = (dt.Rows[i]["Imagem"].ToString()==""? "": Convert.ToBase64String((Byte[])dt.Rows[i]["Imagem"])),
                            Cidade = dt.Rows[i]["Cidade"].ToString(),
                            Uf = dt.Rows[i]["Uf"].ToString(),
                        });
                    }
                    ret.Ocorrencias = oco;
                    return ret;
                }
                else
                {
                    ret.Erro = true;
                    ret.DescErro = "Usuário e Senha Inválidos.";
                    return ret;
                }


            }
            catch (Exception)
            {
                ret.Erro = true;
                ret.DescErro = "Não foi possível recuperdar informações de entrega desta NFe.";
                return ret;
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
                mtpCnpj.Set(ZerosEsquerda(s, 11));
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

}
