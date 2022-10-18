using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ComponentModel;

namespace ServicosWEB.InfraCommerce
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class WSS : System.Web.Services.WebService
    {

        [WebMethod]
        public string ReceberPedidos(List<Pedido> Lped, string ProducaoHomologacao)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();

            try
            {

                if (Lped == null)
                    throw new Exception("Não foi enviado Nenhum Pedido");

                for (int i = 0; i < Lped.Count; i++)
                {

                    if (Lped[i].CNPJCliente == "")
                        throw new Exception ("CNPJ Do CLiente Inválido");

                    Lped[i].CNPJCliente = FormatarCnpj(Lped[i].CNPJCliente);

                    #region Cadastro Do Destinatátio

                    Lped[i].DestinatarioCNPJCPF = FormatarCnpj(Lped[i].DestinatarioCNPJCPF);

                    string sql = "Select isnull(IdCadastro, 0) IdDestinatario from Cadastro where CNPJCPF='" + Lped[0].DestinatarioCNPJCPF + "'";

                    int  IdDestinatario = Sistran.Library.GetDataTables.ExecutarRetornoID_WIN(sql, cnx);

                    if (IdDestinatario == 0)
                    {
                        sql = "";
                        IdDestinatario = int.Parse(Sistran.Library.GetDataTables.RetornarIdTabela("Cadastro", cnx));

                        sql = "INSERT INTO CADASTRO (IDCADASTRO,CNPJCPF, INSCRICAORG, RAZAOSOCIALNOME,FANTASIAAPELIDO,ENDERECO,NUMERO,COMPLEMENTO,IDCIDADE,IDBAIRRO,CEP) ";
                        sql += " VALUES(@IDCADASTRO,'@CNPJCPF', '@INSCRICAORG', '@RAZAOSOCIALNOME','@FANTASIAAPELIDO','@ENDERECO','@NUMERO', '@COMPLEMENTO',@IDCIDADE,@IDBAIRRO,'@CEP')";

                    //    sql = sql.Replace("@IDCADASTRO", IdDestinatario );
                        sql = sql.Replace("@CNPJCPF", FormatarCnpj(Lped[i].DestinatarioCNPJCPF));
                        sql = sql.Replace("@INSCRICAORG", Lped[i].DestinatarioIERG); 
                        sql = sql.Replace("@RAZAOSOCIALNOME", Lped[i].DestinatarioRAZAOSOCIAL.Trim().Replace("'","") );
                        sql = sql.Replace("@FANTASIAAPELIDO",Lped[i].DestinatarioFANTASIA.Trim().Replace("'","") );
                        sql = sql.Replace("@ENDERECO", Lped[i].DestinatarioENDERECO.Trim().Replace("'",""));
                        sql = sql.Replace("@NUMERO", Lped[i].DestinatarioNUMERO.Trim().Replace("'",""));
                        sql = sql.Replace("@COMPLEMENTO", Lped[i].DestinatarioCOMPLEMENTO.Trim().Replace("'",""));
                        //sql = sql.Replace("@IDCIDADE", );
                        //sql = sql.Replace("@IDBAIRRO", );
                        sql = sql.Replace("@CEP", Lped[i].DestinatarioCEP.Trim().Replace("'",""));


                    }
                    else // alterar
                    {
                    }




                    #endregion




                }

                return "Pedido Recebido";

            }
            catch (Exception ex)
            {
                return "Não foi possível receber o peedido. - " + ex.Message;
            }
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
    }

    public class Pedido
    {
        public string CNPJCliente { get; set; }
        public string TipoDeDocumento { get; set; }
        public DateTime DataDeEmissao { get; set; }
        public string NumeroPedido { get; set; }
        public string SeriePedido { get; set; }

        public string DestinatarioCNPJCPF { get; set; }
        public string DestinatarioIERG { get; set; }
        public string DestinatarioRAZAOSOCIAL { get; set; }
        public string DestinatarioFANTASIA { get; set; }
        public string DestinatarioENDERECO { get; set; }
        public string DestinatarioNUMERO { get; set; }
        public string DestinatarioCOMPLEMENTO { get; set; }
        public string DestinatarioBAIRRO { get; set; }
        public string DestinatarioCodigoCidadeIbge { get; set; }
        public string DestinatarioCidadeNome { get; set; }
        public string DestinatarioCEP { get; set; }

        public string DestinatarioENTREGA_CEP { get; set; }
        public string DestinatarioENTREGA_ENDERECO { get; set; }
        public string DestinatarioENTREGA_NUMERO { get; set; }
        public string DestinatarioENTREGA_COMPLEMENTO { get; set; }
        public string DestinatarioENTREGA_BAIRRO { get; set; }
        public string DestinatarioENTREGA_CodigoCidadeIbge { get; set; }
        public string DestinatarioEmail { get; set; }
        public string DestinatarioTelefone { get; set; }

        public DateTime? DataParaEntrega { get; set; }
        public string CompraVenda { get; set; }
        public DateTime? PeriodoDeEntregaInicio { get; set; }
        public DateTime? PeriodoDeEntregaFim { get; set; }
        public List<Itens> itens { get; set; }
        public class Itens
        {
            public string SKU { get; set; }
            public string Descricao { get; set; }
            public string EAN { get; set; }
            public string CodigoNCM { get; set; }
            public int Quantidade { get; set; }
            public decimal ValorUnitario { get; set; }
            public decimal PesoLiquido { get; set; }
        }
    }
}
