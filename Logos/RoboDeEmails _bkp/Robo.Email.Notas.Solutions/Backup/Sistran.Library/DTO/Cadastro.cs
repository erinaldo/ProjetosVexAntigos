using System;
namespace SistranMODEL
{
    public sealed class Cadastro
    {
        public int IDCadastro { get; set; }
        public string CnpjCpf { get; set; }
        public string InscricaoRG { get; set; }
        public string RazaoSocialNome { get; set; }
        public string FantasiaApelido { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int IDCidade { get; set; }
        public int IDBairro { get; set; }
        public string Cep { get; set; }
        public string CnpjCpfErrado { get; set; }
        public string InscricaoErrada { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public string CEPValido { get; set; }
        public string Aniversario { get; set; }
        public string SUFRAMA { get; set; }
        public DateTime SUFRAMAVALIDADE { get; set; }
        public string OrgaoEmissor { get; set; }
        public string TipoDeCadastro { get; set; }
        public string SituacaoFiscal { get; set; }

        public sealed class CadastroComplemento
        {
            public int IDCadastroComplemento { get; set; }
            public int IDCadastro { get; set; }
            public string Aniversario { get; set; }
            public int Dependentes { get; set; }
            public string Banco { get; set; }
            public string Agencia { get; set; }
            public string AgenciaDigito { get; set; }
            public string Conta { get; set; }
            public string ContaDigito { get; set; }
            public string TipoConta { get; set; }
            public string CnpjCpfFavorecido { get; set; }
            public string NomeFavorecido { get; set; }
            public string InscricaoNoInss { get; set; }
            public string InscricaoPIS { get; set; }
            public decimal ValorPensaoAlimenticia { get; set; }
            public string VinculoFavorecido { get; set; }
            public string Aposentado { get; set; }
            public string Contratado { get; set; }
            public DateTime DataExpedicaoRG { get; set; }
            public string OrgaoExpedicaoRG { get; set; }
            public DateTime UltimaComprovacaoEndereco { get; set; }
        }

        public sealed class Motorista
        {
            public int IDMotorista { get; set; }
            public string CarteiraDeHabilitacao { get; set; }
            public DateTime ValidadeDaHabilitacao { get; set; }
            public DateTime DataDaPrimeiraHabilitacao { get; set; }
            public string Categoria { get; set; }
            public DateTime DataDeNascimento { get; set; }
            public string IDCidadeNascimento { get; set; }
            public string NomeDoPai { get; set; }
            public string NomeDaMae { get; set; }
            public string Conjuge { get; set; }
            public decimal VitimaDeRouboQuantidade { get; set; }
            public decimal SofreuAcidadeQuantidade { get; set; }
            public string EstadoCivil { get; set; }
            public DateTime DataDeCadastro { get; set; }
            public decimal CarregamentoAutorizadoAte { get; set; }
            
            public decimal AliquotaSestSenat { get; set; }
            public string VinculoComAEmpresa { get; set; }
            public string NumeroPancard { get; set; }
            public string Ativo { get; set; }
            public string Liberado { get; set; }
            public string MOOP { get; set; }

            public string TelefoneRes { get; set; }
            public string TelefoneCel { get; set; }
            public string TelefoneRecado { get; set; }
            public string TelefoneNextel { get; set; }

            public string IDTelefoneRes { get; set; }
            public string IDTelefoneCel { get; set; }
            public string IDTelefoneRecado { get; set; }
            public string IDTelefoneNextel { get; set; }

            public DateTime? VencimentoPancary { get; set; }
            public DateTime? VencimentoBrasilrisk { get; set; }
            public DateTime? VencimentoBuonny { get; set; }

            private Cadastro cadastro;
            public Cadastro Cadastro
            {
                get
                {
                    return cadastro;
                }
                set
                {
                    cadastro = value;
                }
            }


            private CadastroComplemento cadastroComplemento;
            public CadastroComplemento CadastroComplemento
            {
                get
                {
                    return cadastroComplemento;
                }
                set
                {
                    cadastroComplemento = value;
                }
            }

            private Cidade cidade;
            public Cidade Cidade
            {
                get
                {
                    return cidade;
                }
                set
                {
                    cidade = value;
                }
            }

            private Estado estado;
            public Estado Estado
            {
                get
                {
                    return estado;
                }
                set
                {
                    estado = value;
                }
            }

            public class MotoristaHistorico
            {
                public int IdMotoristaHistorico { get; set; }
                public int IdMotorista { get; set; }
                public string Historico { get; set; }
                public DateTime DataDeCadastro { get; set; }
                public int IDUsuario { get; set; }
                public string NomeUsuario { get; set; }

            }
        }

        public sealed class Proprietario
        {
            public int IDProprietario { get; set; }
            
            private Cadastro cadastro;
            public Cadastro Cadastro
            {
                get
                {
                    return cadastro;
                }
                set
                {
                    cadastro = value;
                }
            }

            private CadastroComplemento cadastroComplemento;
            public CadastroComplemento CadastroComplemento
            {
                get
                {
                    return cadastroComplemento;
                }
                set
                {
                    cadastroComplemento = value;
                }
            }

            private Cidade cidade;
            public Cidade Cidade
            {
                get
                {
                    return cidade;
                }
                set
                {
                    cidade = value;
                }
            }

            private Estado estado;
            public Estado Estado
            {
                get
                {
                    return estado;
                }
                set
                {
                    estado = value;
                }
            }

            public string TelefoneRes { get; set; }
            public string TelefoneCel { get; set; }
            public string TelefoneRecado { get; set; }
            public string TelefoneNextel { get; set; }

            public string IDTelefoneRes { get; set; }
            public string IDTelefoneCel { get; set; }
            public string IDTelefoneRecado { get; set; }
            public string IDTelefoneNextel { get; set; }
        }

        

        public sealed class Tranportadora
        {
            public int IDTransportadora { get; set; }
            public int IDContaContabil { get; set; }
        }

    } 


}