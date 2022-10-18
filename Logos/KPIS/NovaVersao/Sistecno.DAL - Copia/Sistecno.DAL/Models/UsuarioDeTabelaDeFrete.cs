using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class UsuarioDeTabelaDeFrete
    {
        public int IDUsuarioDeTabelaDeFrete { get; set; }
        public int IDTabelaDeFrete { get; set; }
        public Nullable<int> IDFilial { get; set; }
        public Nullable<int> IDCliente { get; set; }
        public Nullable<int> IDTransportadora { get; set; }
        public Nullable<int> IDVeiculoFilial { get; set; }
        public Nullable<int> IDFilialTabela { get; set; }
        public Nullable<int> IDVeiculoTipo { get; set; }
        public string TipoDeTabela { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Descricao { get; set; }
        public string Ativo { get; set; }
        public Nullable<System.DateTime> DataDesativacao { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Cliente Cliente1 { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Filial Filial1 { get; set; }
        public virtual TabelaDeFrete TabelaDeFrete { get; set; }
        public virtual Transportadora Transportadora { get; set; }
        public virtual VeiculoFilial VeiculoFilial { get; set; }
        public virtual VeiculoTipo VeiculoTipo { get; set; }
    }
}
