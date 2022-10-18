using ServicosWEB.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de CadastroFornecedor
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class CadastroFornecedor : System.Web.Services.WebService
    {

        [WebMethod]
        public Retorno Cadastrar(Autentica Credenciais, Fornecedor Fornecedor)
        {
            try
            {
                string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
                string rs = Fornecedor.NomeFornecedor.Trim().Replace("'", "");
                string fant = "";
                string end = Fornecedor.Endereco.Trim().Replace("'", "");

                if (rs.Length > 60)
                {
                    rs = rs.Substring(0, 59);
                    fant = rs.Substring(0, 29);
                }

                if (end.Length > 60)
                    end = end.Substring(0, 59);
         
                int idCidade = CidadeBairro.RetornarCidade(Fornecedor.CEP.Trim().Replace("-", ""), cnx);
                int idBairro = CidadeBairro.RetornarBairro(Fornecedor.Bairro, idCidade.ToString(), cnx);
                string idCadastro = Sistran.Library.GetDataTables.RetornarIdTabela("Cadastro", cnx);

                string sql = "Insert into Cadastro (IDCadastro, CnpjCpf, InscricaoRG, RazaoSocialNome,FantasiaApelido, Endereco, IDCidade,IDBairro,Cep,DataDeCadastro, ativo) ";
                sql += "Values (" + idCadastro + ", '" + Validacao.FormatarCnpj(Fornecedor.CNPJFornecedor) + "', '" + Fornecedor.InscricaoEstadual + "', '" + rs + "','" + fant + "', '" + end + "', " + idCidade + "," + idBairro + ",'" + Fornecedor.CEP.Trim().Replace("-", "") + "',getDate(), 'SIM')";


                Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);
                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }
        }


        public class Fornecedor
        {
            public string StatusCriado { get; set; }//          criado/novo
            public string StatusEliminado { get; set; }// eliminado/excluido
            public string CodigoFornecedor { get; set; }
            public string NomeFornecedor { get; set; }
            public string Endereco { get; set; }//: Rua + Nr
            public string Bairro { get; set; }
            public string CEP { get; set; }
            public string Cidade { get; set; }
            public string Regiao { get; set; }
            public string País { get; set; }
            public string CNPJFornecedor { get; set; }//(BR1)
            public string InscricaoEstadual { get; set; }//(BR3)
        }
    }
}