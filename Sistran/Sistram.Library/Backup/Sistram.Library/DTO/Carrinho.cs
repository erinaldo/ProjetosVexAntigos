
namespace SistranMODEL
{
    public sealed class Carrinho
    {
        public int idProduto { get; set; }
        public string CodigoProdutoCliente { get; set; }
        public decimal Quantidade { get; set; }
        public string Descricao { get; set; }
        public int Disponivel { get; set; }
        public decimal ValorUnitario { get; set; }
        public string IdClienteDivisao { get; set; }
        public string Divisao { get; set; }
        public int IdProdutoCliente { get; set; }      


        public Carrinho()
        { }


    }
}