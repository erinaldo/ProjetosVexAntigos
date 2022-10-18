using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class mMotoristaProprietario
    {
        public string CnpjCpf { get; set; }
        public string ProprietarioMotoristaAmbos { get; set; }
        public System.DateTime Cadastro { get; set; }
        public string Nome { get; set; }
        public string TipoDeEndereco { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Cnh { get; set; }
        public Nullable<System.DateTime> ValidadeDaCnh { get; set; }
        public string Ativo { get; set; }
        public string Rg { get; set; }
        public Nullable<System.DateTime> CnhEmissao { get; set; }
        public string CnhCategoria { get; set; }
        public string RgOrgaoEmissor { get; set; }
        public string RgUf { get; set; }
        public string PIS { get; set; }
        public Nullable<short> Dependentes { get; set; }
        public string Aposentado { get; set; }
        public string Acidente { get; set; }
        public Nullable<short> AcidenteQuantidade { get; set; }
        public string Assalto { get; set; }
        public Nullable<short> AssaltoQuantidade { get; set; }
        public string INSS { get; set; }
        public string INSSRecolhe { get; set; }
        public string SestSenatRecolhe { get; set; }
        public string IRRFRecolhe { get; set; }
        public string ISSRecolhe { get; set; }
        public Nullable<double> ISSAliquota { get; set; }
        public string ContaBanco { get; set; }
        public string ContaAgencia { get; set; }
        public string ContaNumero { get; set; }
        public string ContaTipo { get; set; }
        public string AceitaCreditoEmConta { get; set; }
        public string Agregado { get; set; }
        public Nullable<System.DateTime> DataNascimento { get; set; }
        public Nullable<double> ValorAposentadoria { get; set; }
        public Nullable<double> SestSenatAliquota { get; set; }
        public string FilialResponsavel { get; set; }
        public string ContaTitular { get; set; }
        public Nullable<System.DateTime> VencimentoDaLiberacao { get; set; }
        public string EstadoCivil { get; set; }
        public string FiliacaoPai { get; set; }
        public string FiliacaoMae { get; set; }
        public string Referencia { get; set; }
        public Nullable<System.DateTime> DataPrimeiraHabilitacao { get; set; }
        public Nullable<double> ValorLiberadoCarregamento { get; set; }
        public string Liberado { get; set; }
    }
}
