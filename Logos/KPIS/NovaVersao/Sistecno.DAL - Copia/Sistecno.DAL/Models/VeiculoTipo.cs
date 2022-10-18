using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VeiculoTipo
    {
        public VeiculoTipo()
        {
            this.Agendamentoes = new List<Agendamento>();
            this.UsuarioDeTabelaDeFretes = new List<UsuarioDeTabelaDeFrete>();
            this.Veiculoes = new List<Veiculo>();
            this.VeiculoLicenciamentoes = new List<VeiculoLicenciamento>();
            this.VeiculoTabelas = new List<VeiculoTabela>();
        }

        public int IDVeiculoTipo { get; set; }
        public string Nome { get; set; }
        public Nullable<decimal> CapacidadeDeCargaKG { get; set; }
        public Nullable<decimal> CapacidadeDeCargaM3 { get; set; }
        public string CategoriaPermitida { get; set; }
        public string Ativo { get; set; }
        public string TracaoReboque { get; set; }
        public string TipoDeRodado { get; set; }
        public virtual ICollection<Agendamento> Agendamentoes { get; set; }
        public virtual ICollection<UsuarioDeTabelaDeFrete> UsuarioDeTabelaDeFretes { get; set; }
        public virtual ICollection<Veiculo> Veiculoes { get; set; }
        public virtual ICollection<VeiculoLicenciamento> VeiculoLicenciamentoes { get; set; }
        public virtual ICollection<VeiculoTabela> VeiculoTabelas { get; set; }
    }
}
