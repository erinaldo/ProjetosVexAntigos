using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicosWEB.Barbosa
{
    /// <summary>
    /// Descrição resumida de CadastroMaterial
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class CadastroMaterial : System.Web.Services.WebService
    {

        [WebMethod]
        public Retorno Cadastrar(Autentica Credenciais, List<Material> Materiais)
        {
            string cnx = Sistran.Library.Robo.Robo.RetornarStringBaseNovaLogos();
            try
            {
                for (int i = 0; i < Materiais.Count; i++)
                {
                    string idProd = Sistran.Library.GetDataTables.RetornarIdTabela("Produto", cnx);
                    string sql = "INSERT INTO PRODUTO (IDProduto, CodigoDeBarras, PesoLiquido, Especie, DataDeCadastro, PesoBruto) VALUES (" + idProd + ", '" + Materiais[i].EAN + "', 0, 'UNI', Getdate(), 0)";

                    string IdprodCli = Sistran.Library.GetDataTables.RetornarIdTabela("PRODUTOCLIENTE", cnx);
                    sql += "; INSERT INTO PRODUTOCLIENTE (IDProdutoCliente, IDCliente, IDUnidadeDeMedida,Codigo, Descricao,MetodoDeMovimentacao, DesmembraNaNF, SolicitarDataDeValidade,UnidadeDoFornecedor,Ativo, CodigoNCM, FatorUsoPosicaoPallet) ";
                    sql += " VALUES(" + IdprodCli + ", " + 3497647 + ", '" + Materiais[i].UnidadeDeMedida.Trim() + "','" + Materiais[i].CodigoProduto + "', '" + Materiais[i].DescricaoProduto.Trim().Replace("'", "") + "','FEFO', 'NAO', 'SIM',1.00,'SIM', '" + Materiais[i].NCM + "', 1)";

                    string idProdEmb = Sistran.Library.GetDataTables.RetornarIdTabela("ProdutoEmbalagem", cnx);
                    sql += "; INSERT INTO PRODUTOEMBALAGEM(IDProdutoEmbalagem, IDProdutoCliente, IDProduto, Conteudo, UnidadeDoCliente, ValorUnitario, DataDeCadastro, Ativo) ";
                    sql += " VALUES(" + idProdEmb + ", " + IdprodCli + ", " + idProd + ", '" + Materiais[i].DescricaoProduto.Trim().Replace("'", "") + "', 1, 0 , GETDATE(), 'SIM')";


                    Sistran.Library.GetDataTables.ExecutarComandoSqlTrans(sql, cnx);
                }
                return new Retorno() { Erro = "", Sucesso = true };
            }
            catch (Exception ex)
            {
                return new Retorno() { Erro = ex.Message, Sucesso = false };
            }
        }



        public class Material
        {
            public string Status { get; set; } //eliminado/excluido
            public string CodigoProduto { get; set; }
            public string DescricaoProduto { get; set; }
            public string UnidadeDeMedida { get; set; }
            public string Centro /*(recebedor produto)*/ { get; set; }
            public string Deposito /*(guardar produto)*/ { get; set; }
            public string NCM { get; set; }
            public string EAN { get; set; }
            public string DataCriacao { get; set; }

            public string GrupoDeProduto { get; set; }
        }
    }
}