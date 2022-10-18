using System.Data;
using System;

namespace SistranBLL
{
    public sealed class Produto
    {

        public decimal RetornarSaldoDisponivelPorIdProdutoClienteDivisao(int idclienteDivisao, int idProdutoCliente)
        {
            return new  SistranDAO.Produto().RetornarSaldoDisponivelPorIdProdutoClienteDivisao(idclienteDivisao, idProdutoCliente);
        }
        public DataTable ConsultarProdutoComFoto(string idClienteDivisao)
        {
            return new SistranDAO.Produto().ConsultarProdutoComFoto(idClienteDivisao);
        }

        public DataTable ConsultarCodigoBarras(string CodigoBarras)
        {
            return new SistranDAO.Produto().ConsultarCodigoBarras(CodigoBarras);
        }

        public DataTable ConsultarProdutoClienteCodigo(string Codigo)
        {
            return new SistranDAO.Produto().ConsultarProdutoClienteCodigo(Codigo);
        }

        public void VoltarRegistrosCadProdutos(string delete)
        {
            SistranDAO.Produto o = new SistranDAO.Produto();
            o.VoltarRegistrosCadProdutos(delete);
        }
        public int Inserir(string CDBarras, string altura, string largura, string comprimento, string pesoLiquido, string pesoBruto, string especie)
        {
            return new SistranDAO.Produto().Inserir(CDBarras, altura, largura, comprimento, pesoLiquido, pesoBruto, especie);
        }

        public int InserirProdutoCliente(string idcliente, string UnidadeMedida, string Codigo, string Descricao, string MetodoArmz, string Ativo)
        {
            return new SistranDAO.Produto().InserirProdutoCliente(idcliente, UnidadeMedida, Codigo, Descricao, MetodoArmz, Ativo);
        }


        public void UpdateProdutoCliente(string idProdutoCliente, string UnidadeMedida, string Codigo, string Descricao, string MetodoArmz, string Ativo, string IdProdutoEmbalagem, string ValorUnitario, string UnidCliente, string UnidLogs)
        {
            SistranDAO.Produto o = new SistranDAO.Produto();
            o.UpdateProdutoCliente(idProdutoCliente, UnidadeMedida, Codigo, Descricao, MetodoArmz, Ativo, IdProdutoEmbalagem, ValorUnitario, UnidCliente, UnidLogs);
        }

        public int InserirProdutoImagem(string idProduto, byte[] imagem)
        {
            return new SistranDAO.Produto().InserirProdutoImagem(idProduto, imagem);
        }

        public int InserirProdutoEmbalagem(string IdProdCli, string IdProd, string UnidCliente, string UnidLogs, string UnidMed, string ValorUnit, string Conteudo)
        {
            return new SistranDAO.Produto().InserirProdutoEmbalagem(IdProdCli, IdProd, UnidCliente, UnidLogs, UnidMed, ValorUnit, Conteudo);
        }

        public DataTable ListarProdutoAleatorio(int idCliente)
        {
            return new SistranDAO.Produto().ListarProdutoAleatorio(idCliente);
        }

        public DataTable RetornarImagemProduto(int idProduto)
        {
            return new SistranDAO.Produto().RetornarImagemProduto(idProduto);
        }

        public DataTable RetornarUltimaImagemProduto(int idProduto)
        {
            return new SistranDAO.Produto().RetornarUltimaImagemProduto(idProduto);
        }

        public DataTable ListarProdutosByGrupo(int idCliente, string idGrupoDeProduto)
        {
            return new SistranDAO.Produto().ListarProdutosByGrupo(idCliente, idGrupoDeProduto, false);
        }

        public DataTable ListarProdutoByDivisaoCliente(int Iddivisao, int idCliente, bool MaiorqueZero)
        {
            return new SistranDAO.Produto().ListarProdutoByDivisaoCliente(Iddivisao, idCliente,  MaiorqueZero);
        }

        public DataTable ListarProdutoByCodigos(string Codigo, string CodigoDoCliente, string IdCliente, bool EstoqueZerado)
        {
            return new SistranDAO.Produto().ListarProdutoByCodigos(Codigo, CodigoDoCliente,  IdCliente,  EstoqueZerado);
        }

        public DataTable ListarProdutoByDivisaoCliente(int IDClienteDivisao, bool EstoqueMaiorZero)
        {
            return new SistranDAO.Produto().ListarProdutoByDivisaoCliente(IDClienteDivisao,  EstoqueMaiorZero);
        }

        public DataTable ListarProdutosByIdProdutoCliente(string IdProdutoCliente)
        {
            return new SistranDAO.Produto().ListarProdutosByIdProdutoCliente(IdProdutoCliente);
        }

        public DataTable ListarProdutosFiltroConsultarMaterial(string codigo, string descricao, bool ConsiderarSaldo, bool SemDivisao, string clientes)
        {
            return new SistranDAO.Produto().ListarProdutosFiltroConsultarMaterial(codigo, descricao, ConsiderarSaldo, SemDivisao, clientes);
        }

        public DataTable ListarProdutosCadastroProdutos(string codigo, string CodigoCliente)
        {
            return new SistranDAO.Produto().ListarProdutosCadastroProdutos(codigo, CodigoCliente);
        }


        public DataTable Listar(string codigo, string CodigoCliente, string Descricao)
        {
            return new SistranDAO.Produto().Listar(codigo, CodigoCliente, Descricao);
        }

        public DataTable ListarTelaCadastro(string codigo, string CodigoCliente, string Descricao)
        {
            return new SistranDAO.Produto().ListarTelaCadastro(codigo, CodigoCliente, Descricao);

        }

        public DataTable ListarSaldoOrigem(string IdProdutoCliente, string IdCliente)
        {
            return new SistranDAO.Produto().ListarSaldoOrigem(IdProdutoCliente, IdCliente);
        }

        public DataTable ListarProdutoIniciais(string Codigo, string Descricao, string IdCliente)
        {
            return new SistranDAO.Produto().ListarProdutoIniciais(Codigo, Descricao, IdCliente);
        }

        public DataTable ListarProdutoIniciais2(string str)
        {
            return new SistranDAO.Produto().ListarProdutoIniciais2(str);
        }

        public DataTable ListarRptPedidos(string Codigo)
        {
            return new SistranDAO.Produto().ListarRptPedidos(Codigo);
        }

        public DataTable ListarRptPedidosConsolidados(string Codigo, DateTime? inicio, DateTime? fim, int? IdDestinatario)
        {
            return new SistranDAO.Produto().ListarRptPedidosConsolidados(Codigo, inicio, fim, IdDestinatario);
        }
        
        
        public sealed class GrupoProduto
        {
            public DataTable ListarMenu(int idCliente)
            {
                return new SistranDAO.Produto.GrupoProduto().ListarMenu(idCliente);
            }

            public System.Web.UI.WebControls.DropDownList ListarGrupoDeProdutos(System.Web.UI.WebControls.DropDownList cbo)
            {
                cbo.DataSource = new SistranDAO.Produto.GrupoProduto().ListarGrupoDeProdutos();
                cbo.DataTextField = "Descricao";
                cbo.DataValueField = "IDGRUPODEPRODUTO";
                cbo.DataBind();

                //cbo.Items.Insert(0, "Todos");
                cbo.Items.Insert(0, "Selecione");
                return cbo;
            }
        }

        public DataTable Pesquisar(string texto, int idCliente)
        {
            return new SistranDAO.Produto().Pesquisar(texto, idCliente);
            
        }
        
        public sealed class Estoque
        {
            public void AdicionarSaldoTabelaEstoque(string IDEstoque, string Qtd)
            {
                SistranDAO.Produto.Estoque o = new SistranDAO.Produto.Estoque();
                o.AdicionarSaldoTabelaEstoque(IDEstoque, Qtd);
            }

            public int InserirEstoque(string IDProdutoCliente, string IDFilial, string Saldo)
            {
                return new SistranDAO.Produto.Estoque().InserirEstoque(IDProdutoCliente, IDFilial, Saldo);
            }

            public int ConsultarEstoqueByIDProdutoCliente(int IDProdutoCliente)
            {
                return new SistranDAO.Produto.Estoque().ConsultarEstoqueByIDProdutoCliente(IDProdutoCliente);
            }

            public int InserirMovimentacaoSaidaEDI(string idestoqueDivisaoDestino, string IDEstoqueDivisaoOrigem, string UsuarioID, string saldo)
            {
                return new SistranDAO.Produto.Estoque().InserirMovimentacaoSaidaEDI(idestoqueDivisaoDestino, IDEstoqueDivisaoOrigem, UsuarioID, saldo);
            }

            public int ConsultarEstoqueDivisao(int IDEstoque, int IdClienteDivisao)
            {
                return new SistranDAO.Produto.Estoque().ConsultarEstoqueDivisao(IDEstoque, IdClienteDivisao);
            }

            public int InserirEstoqueDivisao(int IDEstoque, int IDClienteDivisao, int Saldo)
            {
                return new SistranDAO.Produto.Estoque().InserirEstoqueDivisao(IDEstoque, IDClienteDivisao, Saldo);
            }

            public int AtualizarEstoqueDivisao(int IDESTOQUEDIVISAO, int Saldo, string Operador)
            {
                return new SistranDAO.Produto.Estoque().AtualizarEstoqueDivisao(IDESTOQUEDIVISAO, Saldo, Operador);
            }

            public int retornarIdEstoqueDivisao(string IDEstoque, string IDClienteDivisao)
            {
                return new SistranDAO.Produto.Estoque().retornarIdEstoqueDivisao(IDEstoque, IDClienteDivisao);

            }

            public int InserirMovimentacaoSaida(string idestoqueDivisaoDestino, string IDEstoqueDivisaoOrigem, string UsuarioID, string saldo)
            {
                return new SistranDAO.Produto.Estoque().InserirMovimentacaoSaida(idestoqueDivisaoDestino, IDEstoqueDivisaoOrigem, UsuarioID, saldo);

            }

            public int InserirMovimentacaoEntrada(string idestoqueDivisaoDestino, string IDEstoqueDivisaoOrigem, string UsuarioID, string saldo)
            {
                return new SistranDAO.Produto.Estoque().InserirMovimentacaoEntrada(idestoqueDivisaoDestino, IDEstoqueDivisaoOrigem, UsuarioID, saldo);

            }

            public int InserirMovimentacaoEntradaInicial(string idestoqueDivisaoDestino, string UsuarioID, string saldo)
            {
                return new SistranDAO.Produto.Estoque().InserirMovimentacaoEntradaInicial(idestoqueDivisaoDestino, UsuarioID, saldo);
            }
        }
    }

    
}