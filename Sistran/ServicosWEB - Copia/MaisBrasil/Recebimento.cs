using System;
using System.Collections.Generic;
using System.Web.UI.MobileControls;

namespace ServicosWEB.MaisBrasil
{
    public class Recebimento
    {
        public DateTime DataEmissaoNf { get; set; }

        public int NumeroNfe { get; set; }
        public string Serie { get; set; }
        public string Chave { get; set; }
        public Cadastro Remetente { get; set; }
        public Cadastro Destinatario { get; set; }

        public List<RecebimentoItens> Itens { get; set; }
    }


    public class RecebimentoItens
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public string CodigoDeBarras { get; set; }
        public int FatorDeConversao { get; set; }
        public decimal Peso { get; set; }
         public int QuantidadeNF { get; set; }


    }

    public class Cadastro
    {
        public string CNPJ { get; set; }
        public string IE { get; set; }
        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public string TipoDeEndereco { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public int CodigoIBGE { get; set; }
        public string UF { get; set; }

    }
}