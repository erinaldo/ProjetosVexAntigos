using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDI_Cadastro
    {
        public string EDI_Chave { get; set; }
        public string EDI_Motivo { get; set; }
        public Nullable<System.DateTime> EDI_Data { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public int IDCadastro { get; set; }
        public string CnpjCpf { get; set; }
        public string InscricaoRG { get; set; }
        public string RazaoSocialNome { get; set; }
        public string FantasiaApelido { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int IDCidade { get; set; }
        public Nullable<int> IDBairro { get; set; }
        public string Cep { get; set; }
        public string CnpjCpfErrado { get; set; }
        public string InscricaoErrada { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string CEPValido { get; set; }
        public string Aniversario { get; set; }
        public Nullable<int> idedi_cadastro { get; set; }
    }
}
