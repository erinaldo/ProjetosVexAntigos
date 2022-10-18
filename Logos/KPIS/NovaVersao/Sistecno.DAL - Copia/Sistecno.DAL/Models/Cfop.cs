using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Cfop
    {
        public Cfop()
        {
            this.Clientes = new List<Cliente>();
            this.DocumentoCfops = new List<DocumentoCfop>();
            this.ProdutoClientes = new List<ProdutoCliente>();
            this.TESCFOPs = new List<TESCFOP>();
        }

        public int IDCfop { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string Estadual { get; set; }
        public string Interestadual { get; set; }
        public string Internacional { get; set; }
        public string Descricao { get; set; }
        public string Aplicacao { get; set; }
        public Nullable<System.DateTime> Vigencia { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<DocumentoCfop> DocumentoCfops { get; set; }
        public virtual ICollection<ProdutoCliente> ProdutoClientes { get; set; }
        public virtual ICollection<TESCFOP> TESCFOPs { get; set; }
    }
}
