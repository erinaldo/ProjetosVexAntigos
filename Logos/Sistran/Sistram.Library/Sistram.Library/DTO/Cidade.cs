namespace SistranMODEL
{
    public class Cidade
    {
      public   int IDCidade { get; set; }
      public int IDEstado { get; set; }
      public string Nome { get; set; }
      public string Cep { get; set; }
      public string Tipo { get; set; }
      public string CodificarPor { get; set; }
      public string Regiao { get; set; }
      public int PrazoDeEntregaPadrao { get; set; }
      public string CodigoDoIBGE { get; set; }
      public string CodigoDipam { get; set; }        
    }

    public class Estado
    {
        public int IDEstado { get; set; }
        public int IDPais { get; set; }
        public string Uf { get; set; }
        public string Nome { get; set; }
        public string CodigoDoIbge { get; set; }
    }
}