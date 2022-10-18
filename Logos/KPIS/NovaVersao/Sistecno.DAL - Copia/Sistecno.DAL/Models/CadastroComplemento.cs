using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroComplemento
    {
        public int IDCadastroComplemento { get; set; }
        public int IDCadastro { get; set; }
        public Nullable<int> IDBanco { get; set; }
        public string Aniversario { get; set; }
        public Nullable<decimal> Dependentes { get; set; }
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
        public Nullable<decimal> ValorPensaoAlimenticia { get; set; }
        public string VinculoFavorecido { get; set; }
        public string Aposentado { get; set; }
        public string Contratado { get; set; }
        public Nullable<System.DateTime> DataExpedicaoRG { get; set; }
        public string OrgaoExpedicaoRG { get; set; }
        public Nullable<System.DateTime> UltimaComprovacaoEndereco { get; set; }
        public virtual Banco Banco1 { get; set; }
        public virtual Cadastro Cadastro { get; set; }
    }
}
