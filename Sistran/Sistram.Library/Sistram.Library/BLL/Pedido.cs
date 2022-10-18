using System.Data;

namespace SistranBLL
{
    public class Pedido
    {

        #region NovoPedido

        public DataTable RetornarProdutosDivisoesGrupodeProduto(int idUsuario, int IdCliente)
        {
            return new SistranDAO.Pedido().RetornarProdutos(idUsuario, IdCliente);
        }
        #endregion

        public void AlterarSituacao(string situacaoOrigem, string SituacaoDestino)
        {
            new SistranDAO.Pedido().AlterarSituacao(situacaoOrigem, SituacaoDestino);
        }

        public DataTable PedidoPendentes(string situacao)
        {
            return new SistranDAO.Pedido().PedidoPendentes(situacao);
        }

        public DataTable ConsultarByDocumento(int DocId)
        {
            return new SistranDAO.Pedido().ConsultarByDocumento(DocId);
        }

        public void CancelarPedido(string idDocumento)
        {
            new SistranDAO.Pedido().CancelarPedido(idDocumento);
        }

        public DataTable AprovarPedidoPrepararDadosEmail(string idDocumento)
        {
            return new SistranDAO.Pedido().AprovarPedidoPrepararDadosEmail(idDocumento);
        }

        public void AlterarSituacaoPedido(string situacao, string id_documento)
        {
            new SistranDAO.Pedido().AlterarSituacaoPedido(situacao, id_documento);
        }

        public void IncluirOcorrencia(string id_documento, string ocorrencia)
        {
            new SistranDAO.Pedido().IncluirOcorrencia(id_documento, ocorrencia);
        }

        public int InserirTabDocumentoPedido(string IDFilial, string IDFilialAtual, string IDCliente, string IDRemetente, string IDDestinatario, string Serie, string Numero, string PesoBruto, string MetragemCubica, string Volumes, string ValorDaNota)
        {
            return new SistranDAO.Pedido().InserirTabDocumentoPedido(IDFilial, IDFilialAtual, IDCliente, IDRemetente, IDDestinatario, Serie, Numero, PesoBruto, MetragemCubica, Volumes, ValorDaNota);
        }

        public int RetornarIdProdutoEmbalagem(int IdProduto, int IDProdutoCliente)
        {
            return new SistranDAO.Pedido().RetornarIdProdutoEmbalagem(IdProduto, IDProdutoCliente);
        }

        public int[] RetornarIdProdutoEmbalagemByIdprodutoCliente(string Codigo)
        {
            return new SistranDAO.Pedido().RetornarIdProdutoEmbalagemByIdprodutoCliente(Codigo);
        }

        public int InserirTabDocumentoItemPedido(int IdDocumento, int IdDocumentoEmbalagem, int IdUsuario, int IdClienteDivisao, int Quantidade, decimal VlUnitario, int IdUnidadeDeArmazenagemLote, string referencia)
        {
            return new SistranDAO.Pedido().InserirTabDocumentoItemPedido(IdDocumento, IdDocumentoEmbalagem, IdUsuario, IdClienteDivisao, Quantidade, VlUnitario, IdUnidadeDeArmazenagemLote, referencia);
        }

        public int InserirTabDocumentoFilial(int idDocumento, int IdFilial, int IdRegiao, string Situacao)
        {
            return new SistranDAO.Pedido().InserirTabDocumentoFilial(idDocumento, IdFilial, IdRegiao, Situacao);

        }

        public int Numerador(int IdFilial, string Nome)
        {
            return new SistranDAO.Pedido().Numerador(IdFilial, Nome);
        }

        public void CancelarPedidoByErro(string idDocumento)
        {
            new SistranDAO.Pedido().CancelarPedidoByErro(idDocumento);
        }

        public DataTable ExportarPedido(string situacao)
        {
            return new SistranDAO.Pedido().ExportarPedido(situacao);
        }

        public DataTable ItensByDocumento(string idDocumento)
        {
            return new SistranDAO.Pedido().ItensByDocumento(idDocumento);
        }
    }
}
