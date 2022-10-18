using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RegiaoItem
    {
        public RegiaoItem()
        {
            this.CadastroRegiaos = new List<CadastroRegiao>();
        }

        public int IDRegiaoItem { get; set; }
        public int IDRegiao { get; set; }
        public Nullable<int> IDCadastro { get; set; }
        public Nullable<int> IDSetor { get; set; }
        public Nullable<int> IDCidade { get; set; }
        public Nullable<int> IDEstado { get; set; }
        public Nullable<int> IDPais { get; set; }
        public Nullable<int> IDRoteirizacaoTipo { get; set; }
        public Nullable<int> Ordem { get; set; }
        public Nullable<decimal> Distancia { get; set; }
        public Nullable<int> Ordenar { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual ICollection<CadastroRegiao> CadastroRegiaos { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Pai Pai { get; set; }
        public virtual Regiao Regiao { get; set; }
        public virtual RoteirizacaoTipo RoteirizacaoTipo { get; set; }
        public virtual Setor Setor { get; set; }
    }
}
