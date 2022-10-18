
namespace SistranMODEL
{
    public sealed class NotasFiscaisItens
    {
        public int Codigo { get; set; }
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public decimal Qtde { get; set; }
        public string Valor { get; set; }

        public NotasFiscaisItens()
        { }


        public NotasFiscaisItens(int codigo, string codigoBarras, string descricao, decimal qtde, string valor)
        {
            Codigo = codigo;
            CodigoBarras = codigoBarras;
            Descricao = descricao;
            Qtde = qtde;
            Valor = valor;
        }
    }
}
