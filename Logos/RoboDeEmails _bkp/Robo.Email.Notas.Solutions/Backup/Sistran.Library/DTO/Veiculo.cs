using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SistranMODEL
{
    public sealed class Veiculo
    {
        //public int IDProprietario { get; set; }

        public int IDVeiculo { get; set; }
        public int IDVeiculoModelo { get; set; }
        public int IDVeiculoTipo { get; set; }
        public int IDVeiculoRastreador { get; set; }
        public int IDCidade { get; set; }
        public int IDProprietario { get; set; }
        public int IDMotorista { get; set; }
        public int IDCadastroTitular { get; set; }
        public DateTime Cadastro { get; set; }
        public string Placa { get; set; }
        public string Renavam { get; set; }
        public string Chassi { get; set; }
        public int Ano { get; set; }
        public int AnoModelo { get; set; }
        public string Cor { get; set; }
        public decimal CapacidadeDeCargaKG { get; set; }
        public decimal CapacidadeDeCargaM3 { get; set; }
        public decimal QuatidadeDeEixos { get; set; }
        public string CategoriasDeCNHPermitidas { get; set; }
        public string Antt { get; set; }
        public string NumeroSerieEquipamento { get; set; }
    }
}
