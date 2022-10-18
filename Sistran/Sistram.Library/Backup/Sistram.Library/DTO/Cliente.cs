
namespace SistranMODEL
{
    public sealed class Cliente
    {
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public byte[] ClienteImagen { get; set; }

        public Cliente()
        { }

        public Cliente(int clienteId, string clienteNome, byte[] clienteImagen)
        {
            ClienteId = clienteId;
            ClienteNome = clienteNome;
            ClienteImagen = clienteImagen;
        }
    }

    public sealed class Estoque
    {

        public int IDEstoque { get; set; }
        public int IDProdutoCliente { get; set; }
        public int IDFilial { get; set; }
        public int Saldo { get; set; }

        public Estoque()
        { }


    }
}
