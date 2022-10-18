using System;

namespace SistranMODEL
{
    public sealed class NotasFiscais
    {
        public int? IdDocumento { get; set; }
	    public int? Numero { get; set; }
	    public string CnpjCpfDestinatario { get; set; }
	    public string RazaoNomeDestinatario { get; set; }
	    public string FantasiaApelidoDestinatario { get; set; }
	    public string CnpjCpfRemetente { get; set; }
	    public string RazaoNomeRemetente { get; set; }
	    public string Origem { get; set; }
	    public DateTime? DataDeEmissao { get; set; }
	    public DateTime? DataDeEntrada { get; set; }
	    public DateTime? DataDeSaida { get; set; }
	    public DateTime? DataDeConclusao { get; set; }
	    public string Situacao { get; set; }
	    public string Ocorrencia { get; set; }
	    public int? CodigoDoRecexp { get; set; }
	    public string CidaDeRemetente { get; set; }
	    public string UfRemetente { get; set; }
	    public string CidadeDestinatario { get; set; }
        public string UfDestinatario { get; set; }

        public string EnderecoDestinatario { get; set; }
        public string NumeroDestinatario { get; set; }
        public string ComplementoDestinatario { get; set; }
        public string Ativo { get; set; }

        public decimal ValorTotalNota { get; set; }
        public decimal BaseICM { get; set; }
        public decimal ValorICMS { get; set; }
        public decimal BaseIPI { get; set; }
        public decimal ValorIPI { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal PesoBruto { get; set; }
        public decimal PesoLiquido { get; set; }
        

        public NotasFiscais()
        { }


        public NotasFiscais(int? iddocumento, int? numero, string cnpjcpfdestinatario, string razaonomedestinatario, string fantasiaapelidodestinatario, 
             string cnpjcpfremetente, string razaonomeremetente, string origem, DateTime? datadeemissao, DateTime? datadeentrada,
	         DateTime? datadesaida, DateTime? datadeconclusao, string situacao, string ocorrencia, int? codigodorecexp, string cidaderemetente,
            string ufremetente, string cidadedestinatario, string ufdestinatario, string enderecoDestinatario, string numeroDestinatario, string complementoDestinatario, string ativo,
            decimal valortotalnota, decimal baseicm, decimal valoricms, decimal baseipi, decimal valoripi, decimal valordesconto, decimal pesobruto, decimal pesoliquido)
        { 
            IdDocumento =iddocumento;
            Numero =numero ;
            CnpjCpfDestinatario = cnpjcpfdestinatario;
            RazaoNomeDestinatario = razaonomedestinatario;
            FantasiaApelidoDestinatario = fantasiaapelidodestinatario;
            CnpjCpfRemetente = cnpjcpfremetente;
            RazaoNomeRemetente = razaonomeremetente;
            Origem = origem;
            DataDeEmissao = datadeemissao;
            DataDeEntrada = datadeentrada;
            DataDeSaida = datadesaida;
            DataDeConclusao = datadeconclusao;
            Situacao = situacao;
            Ocorrencia = ocorrencia;
            CodigoDoRecexp = codigodorecexp;
            CidaDeRemetente = cidaderemetente;
            UfRemetente = ufremetente;
            CidadeDestinatario = cidadedestinatario;
            UfDestinatario = ufdestinatario;
            EnderecoDestinatario = enderecoDestinatario;
            NumeroDestinatario = numeroDestinatario;
            ComplementoDestinatario = complementoDestinatario;
            Ativo = ativo;
            ValorTotalNota = valortotalnota;
            BaseICM = baseicm;
            ValorICMS =valoricms;
            BaseIPI  =baseipi;
            ValorIPI  =valoripi;
            ValorDesconto =valordesconto;
            PesoBruto =pesobruto;
            PesoLiquido = pesoliquido;
        }
    }
}
