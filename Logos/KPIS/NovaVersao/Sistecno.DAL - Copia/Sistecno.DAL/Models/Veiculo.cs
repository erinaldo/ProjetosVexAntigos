using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Veiculo
    {
        public Veiculo()
        {
            this.DTs = new List<DT>();
            this.DTs1 = new List<DT>();
            this.Portarias = new List<Portaria>();
            this.VeiculoFilials = new List<VeiculoFilial>();
            this.VeiculoFotoes = new List<VeiculoFoto>();
            this.VeiculoTabelas = new List<VeiculoTabela>();
            this.VeiculoTabelaAgregadoes = new List<VeiculoTabelaAgregado>();
        }

        public int IDVeiculo { get; set; }
        public Nullable<int> IDVeiculoModelo { get; set; }
        public Nullable<int> IDVeiculoTipo { get; set; }
        public Nullable<int> IDVeiculoRastreador { get; set; }
        public Nullable<int> IDCidade { get; set; }
        public Nullable<int> IDProprietario { get; set; }
        public Nullable<int> IDMotorista { get; set; }
        public Nullable<int> IDCadastroTitular { get; set; }
        public Nullable<System.DateTime> Cadastro { get; set; }
        public string Placa { get; set; }
        public string Renavam { get; set; }
        public string Chassi { get; set; }
        public Nullable<int> Ano { get; set; }
        public string Cor { get; set; }
        public Nullable<decimal> CapacidadeDeCargaKG { get; set; }
        public Nullable<decimal> CapacidadeDeCargaM3 { get; set; }
        public Nullable<decimal> QuatidadeDeEixos { get; set; }
        public string CategoriasDeCNHPermitidas { get; set; }
        public string Antt { get; set; }
        public string NumeroSerieEquipamento { get; set; }
        public Nullable<System.DateTime> AnttVencimento { get; set; }
        public Nullable<System.DateTime> DataDeLicenciamento { get; set; }
        public Nullable<int> AnoModelo { get; set; }
        public string TipoDeCarroceria { get; set; }
        public Nullable<System.DateTime> ConfirmadoPara { get; set; }
        public Nullable<int> ConfirmadoPor { get; set; }
        public Nullable<int> IdLicenciamentoMes { get; set; }
        public virtual Cadastro Cadastro1 { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual ICollection<DT> DTs1 { get; set; }
        public virtual Frota Frota { get; set; }
        public virtual Motorista Motorista { get; set; }
        public virtual ICollection<Portaria> Portarias { get; set; }
        public virtual Proprietario Proprietario { get; set; }
        public virtual ICollection<VeiculoFilial> VeiculoFilials { get; set; }
        public virtual ICollection<VeiculoFoto> VeiculoFotoes { get; set; }
        public virtual ICollection<VeiculoTabela> VeiculoTabelas { get; set; }
        public virtual ICollection<VeiculoTabelaAgregado> VeiculoTabelaAgregadoes { get; set; }
        public virtual VeiculoModelo VeiculoModelo { get; set; }
        public virtual VeiculoRastreador VeiculoRastreador { get; set; }
        public virtual VeiculoTipo VeiculoTipo { get; set; }
    }
}
