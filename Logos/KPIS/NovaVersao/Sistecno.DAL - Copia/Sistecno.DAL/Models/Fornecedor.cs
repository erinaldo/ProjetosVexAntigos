using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Fornecedor
    {
        public Fornecedor()
        {
            this.FornecedorFilials = new List<FornecedorFilial>();
            this.Tituloes = new List<Titulo>();
        }

        public int IDFornecedor { get; set; }
        public int CodigoDoFornecedor { get; set; }
        public int CodigoDoFornecedorFilial { get; set; }
        public Nullable<int> IDRamoDeAtividade { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<int> IDContaContabilCredito { get; set; }
        public Nullable<int> IDContaContabilDebito { get; set; }
        public Nullable<int> IDCentroDeCustoCredito { get; set; }
        public Nullable<int> IDCentroDeCustoDebito { get; set; }
        public string Ativo { get; set; }
        public Nullable<int> IDCentroDeCusto { get; set; }
        public Nullable<int> IDContaContabil { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual CentroDeCusto CentroDeCusto { get; set; }
        public virtual CentroDeCusto CentroDeCusto1 { get; set; }
        public virtual CentroDeCusto CentroDeCusto2 { get; set; }
        public virtual ContaContabil ContaContabil { get; set; }
        public virtual ContaContabil ContaContabil1 { get; set; }
        public virtual ContaContabil ContaContabil2 { get; set; }
        public virtual ICollection<FornecedorFilial> FornecedorFilials { get; set; }
        public virtual ICollection<Titulo> Tituloes { get; set; }
    }
}
