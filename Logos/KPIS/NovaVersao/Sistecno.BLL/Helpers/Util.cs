using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistecno.DAL.BD;

namespace Sistecno.BLL.Helpers
{
    public static class Util
    {
        public static class Validacoes
        {

            public static string RemoveEspacosDuplos(string strTexto)
            {
                while (strTexto.IndexOf("  ") > 0)
                    strTexto = strTexto.Replace("  ", " ");

                return strTexto;
            }

            public static string LimparCnpjCpf(string cnpj)
            {
                return cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace("\\", "");
            }

            public static string RemoverAcentos(string strTexto)
            {
                const string ComAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
                const string SemAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

                for (int i = 0; i < ComAcentos.Length; i++)
                    strTexto = strTexto.Replace(ComAcentos[i].ToString(), SemAcentos[i].ToString()).Trim();

                return strTexto;
            }



            public static string RemoverCarEsp(string strTexto)
            {
                strTexto = strTexto.Trim();
                strTexto = RemoverAcentos(strTexto);
                strTexto = RemoveEspacosDuplos(strTexto);

                //Remover Caracteres Especiais - página 82 / manual 4.0.0
                strTexto = strTexto.Replace("<", "&lt;");
                strTexto = strTexto.Replace(">", "&gt;");
                strTexto = strTexto.Replace("&", "&amp;");
                strTexto = strTexto.Replace("\"", "&quot;");
                strTexto = strTexto.Replace("'", "&#39;");

                strTexto = strTexto.Replace(".", "");
                strTexto = strTexto.Replace("/", "");
                strTexto = strTexto.Replace("-", "");
                strTexto = strTexto.Replace("°", "");
                strTexto = strTexto.Replace("º", "");
                strTexto = strTexto.Replace("ª", "");

                return strTexto;
            }

            public static string FormatarPlaca(string s)
            {
                s = s.Replace(".", "");
                s = s.Replace("-", "");
                s = s.Replace("/", "");
                s = s.Replace(@"\", "");

                if (s.Length == 0)
                {
                    return "";
                }

                if (s.Length == 7)
                {
                    MaskedTextProvider mtpCpf = new MaskedTextProvider(@"AAA-0000");
                    mtpCpf.Set(s);
                    return mtpCpf.ToString();
                }
                else
                {
                    return "";
                }
            }

            public static string FormatarCnpjCPF(string s)
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

            public static bool CnpjValido(string cnpj)
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

            public static bool CpfValido(string cpf)
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
        }

        public class ImportacaoNFE
        {
            public string campo { get; set; }
            public string valor { get; set; }

            public ImportacaoNFE(string Campo, string Valor)
            {
                campo = Campo;
                valor = Valor;
            }

            public ImportacaoNFE()
            {
            }




            public string Sustituicoes(string textNode)
            {

                textNode = textNode.Replace("\"", "'").Replace("fieldset", "").Replace("<legend class='titulo-aba'>Dados do Emitente</legend>", "").Replace("</legend>", "");
                textNode = textNode.Replace("<table>", "");
                textNode = textNode.Replace("</table>", "");
                textNode = textNode.Replace("<td class='fixo-nfe-uf'>", "");
                textNode = textNode.Replace("<tr class='col-2'>", "");
                textNode = textNode.Replace("</tr>", "");
                textNode = textNode.Replace("<tr>", "");
                textNode = textNode.Replace("<td>", "");
                textNode = textNode.Replace("</td>", "");
                textNode = textNode.Replace("<>", "");
                textNode = textNode.Replace("class='multiline'", "");
                textNode = textNode.Replace("�", "");
                textNode = textNode.Replace("Nome / Razo Social", "RazaoSocial");
                textNode = textNode.Replace("Nome Fantasia", "Fantasia");
                textNode = textNode.Replace("Endereo", "Endereco");
                textNode = textNode.Replace("<span >", "<span>");
                textNode = textNode.Replace("<span class='linha'>", "<span>");
                textNode = textNode.Replace("<label>", "");
                textNode = textNode.Replace("</label>", "");
                textNode = textNode.Replace("<span>", "|");
                textNode = textNode.Replace("</span>", ";\n");
                textNode = textNode.Replace("</>", "");
                textNode = textNode.Replace("    ", " ");
                textNode = textNode.Replace("   ", " ");
                textNode = textNode.Replace("  ", " ");
                textNode = textNode.Replace("\r", "");
                textNode = textNode.Replace("\n", "");
                textNode = textNode.Replace("  ", "");
                textNode = textNode.Replace("Bairro / Distrito", "Bairro");
                textNode = textNode.Replace("Municpio", "Municipio");
                textNode = textNode.Replace("Inscrio Municipal", "IE");
                textNode = textNode.Replace("Inscrio", "Inscricao");
                textNode = textNode.Replace("CNAE Fiscal", "CNAE");
                textNode = textNode.Replace("Cdigo", "Codigo");
                textNode = textNode.Replace("<legend class='titulo-aba'>", "");
                textNode = textNode.Replace("<td colspan='2'>", "");
                textNode = textNode.Replace("Dados do Destinatrio</legend>", "");
                textNode = textNode.Replace("<td class='fixo-nro-serie'>", "");
                textNode = textNode.Replace("<td class='fixo-versao-xml'>", "");
                textNode = textNode.Replace("Nmero", "Numero");
                textNode = textNode.Replace("Chave de Acesso", "ChavedeAcesso");
                textNode = textNode.Replace("</legend><tr class='col-6'>", "");
                textNode = textNode.Replace("<legend class='titulo-aba-interna'>", "");
                textNode = textNode.Replace("</legend><td class='fixo-nfe-cpf-cnpj'>", "");
                textNode = textNode.Replace("<td class='fixo-nfe-iest'>", "");
                textNode = textNode.Replace("<td width='50%'>", "");
                textNode = textNode.Replace("<td width='15%'>", "");
                textNode = textNode.Replace("<td width='20%'>", "");
                textNode = textNode.Replace("<td colspan='2' width='30%'>", "");
                textNode = textNode.Replace("</legend><tr class='col-4'>", "");
                textNode = textNode.Replace("<td width='25%'>", "");
                textNode = textNode.Replace("Emisso", "Emissao");
                textNode = textNode.Replace("Sada/Entrada", "Saida/Entrada");
                textNode = textNode.Replace("operao", "Operacao");
                textNode = textNode.Replace("Presena", "Presenca");
                textNode = textNode.Replace("<td width='35%'>", "");
                textNode = textNode.Replace("<tr class='col-3'>", "");
                textNode = textNode.Replace("Dados do Transporte", "");
                textNode = textNode.Replace("<trclass='col-6'>", "");
                textNode = textNode.Replace("Srie", "Serie");

                textNode = textNode.Replace("DadosdaNF-e<tableclass='box'>", "");
                textNode = textNode.Replace("<tableclass='box'>", "");
                textNode = textNode.Replace("DadosdoDestinatrio<tableclass='box'><tdcolspan='3'>", "");
                textNode = textNode.Replace("<tableclass='box'>", "");

                textNode = textNode.Replace("DadosdosProdutoseServios<div><tableclass='prod-serv-headerbox'><tdclass='fixo-prod-serv-numero'>", "");
                textNode = textNode.Replace("<tdclass='fixo-prod-serv-descricao'>", "");
                textNode = textNode.Replace("<tdcolspan='4'>", "");
                textNode = textNode.Replace("<tableclass='toggablebox'style='background-color:#ECECEC'><tableclass='box'><trclass='col-4'><tdcolspan='4'>", "");
                textNode = textNode.Replace("<tdcolspan='4'>", "");
                textNode = textNode.Replace("<tdclass='fixo-prod-serv-descricao'>", "");
                textNode = textNode.Replace("<td style='width:50%'>", "");
                textNode = textNode.Replace("<td style='width:25%'>", "");
                textNode = textNode.Replace("<table class='box'>", "");
                textNode = textNode.Replace("<table class='box'>", "");
                textNode = textNode.Replace("<tr class='col-1'>", "");
                textNode = textNode.Replace("<td colspan='3'", "");
                textNode = textNode.Replace("style='border-bottom:solid 1px #CCC'>", "");
                textNode = textNode.Replace("Dados dos Produtos e Servios<div>", "");
                textNode = textNode.Replace("<table class='prod-serv-header box'><td class='fixo-prod-serv-numero'>", "");
                textNode = textNode.Replace("<td class='fixo-prod-serv-descricao'>", "");
                textNode = textNode.Replace("<td class='fixo-prod-serv-qtd'>", "");
                textNode = textNode.Replace("<td class='fixo-prod-serv-uc'>", "");
                textNode = textNode.Replace("<td class='fixo-prod-serv-vb'>", "");
                textNode = textNode.Replace("<table class='toggle box'><td class='fixo-prod-serv-numero'>", "");
                textNode = textNode.Replace("<td class='fixo-prod-serv-descricao'>", "");
                textNode = textNode.Replace("<td class='fixo-prod-serv-qtd'>", "");
                textNode = textNode.Replace("<td class='fixo-prod-serv-uc'>", "");
                textNode = textNode.Replace("<td class='fixo-prod-serv-vb'>", "");
                textNode = textNode.Replace("<table class='toggable box' style='background-color:#ECECEC'>", "");
                textNode = textNode.Replace("<table class='box'>", "");
                textNode = textNode.Replace("<tr class='col-4'>", "");
                textNode = textNode.Replace("<td colspan='4'>", "");

                textNode = textNode.Replace("<td colspan='12'>", "");
                textNode = textNode.Replace("<legend class='toggle'>", "");
                textNode = textNode.Replace("div class='toggable'>>", "");
                textNode = textNode.Replace("<div class='toggable'>>", "");
                textNode = textNode.Replace("</div>", "");
                textNode = textNode.Replace("</div>", "");
                textNode = textNode.Replace("<legend>", "");
                textNode = textNode.Replace("<", "");

                textNode = textNode.Replace("td class='col-5'>", "");
                textNode = textNode.Replace("td class='col-2'>", "");
                textNode = textNode.Replace("class='col-3'>", "");
                textNode = textNode.Replace("td class='col-10'>", "");
                textNode = textNode.Replace("td colspan='2' class='col-3'>", "");
                textNode = textNode.Replace("Dados do Destinatrio>", "Dados do Destinatrio");
                textNode = textNode.Replace("br>", "");
                textNode = textNode.Replace("tr class='col-12'>", "");

                return textNode;

            }
        }

        public static class CadastroImagem
        {
            public static void GravarImagem(byte[] img, int idCadastro, string cnx)
            {

                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection cn = factory.CreateConnection();
                DbCommand cd = factory.CreateCommand();



                cn.ConnectionString = cnx;
                string ID = cDb.RetornarIDTabela(cnx, "CADASTROIMAGEM").ToString();

                try
                {
                    cd.Connection = cn;
                    cn.Open();
                    string m = "";
                    m += " INSERT INTO CADASTROIMAGEM(";
                    m += " IDCADASTROIMAGEM,";
                    m += " IDCADASTRO,";
                    m += " IMAGEM,";
                    m += " NOME";
                    m += " ) VALUES";
                    m += " (";


                    m += ID + " ,";
                    m += idCadastro + " ,";
                    m += " @IMAGEM ,";
                    m += " 'LOGO TIPO'";
                    m += " )";

                    cd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IMAGEM", img));
                    cd.CommandText = m;
                    cd.CommandType = CommandType.Text;
                    cd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }

            public static DataTable RetornarImagemSite(int idCadastro, bool todas, string cnx)
            {
                try
                {
                    string strsql = "SELECT TOP 1 IDCADASTROIMAGEM, IMAGEM FROM CADASTROIMAGEM WHERE " + (todas == true ? "0=0" : "TIPOIMAGEM='SITE'") + " AND IDCADASTRO=" + idCadastro;
                    return cDb.RetornarDataTable(strsql, cnx);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static class PermissoesMenuPagina
        {
            public static bool Permissoes(string link, DataTable dtPermissoes)
            {
              //  System.Web.AspNetHostingPermissionAttribute x;

                if (dtPermissoes == null)
                    return false;

                if (dtPermissoes.Select("link='" + link.ToLower() + "'", "").Length > 0)
                    return true;
                else
                    return false;

            }

        }

        public static class Log
        {
            public static void GravarLog(string Caminho, string MenssagemLog, string NomeFuncao)
            {
                try
                {
                    StreamWriter valor = new StreamWriter(Caminho + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true, Encoding.Unicode);
                    valor.Write(DateTime.Now.ToString() + " | " + NomeFuncao + " | " + MenssagemLog + "\r\n");
                    valor.Close();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
