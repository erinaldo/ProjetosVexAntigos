using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Motorista
    {
        public Motorista()
        {
            this.DTs = new List<DT>();
            this.DTs1 = new List<DT>();
            this.MotoristaFilials = new List<MotoristaFilial>();
            this.MotoristaHistoricoes = new List<MotoristaHistorico>();
            this.Veiculoes = new List<Veiculo>();
        }

        public int IDMotorista { get; set; }
        public string CarteiraDeHabilitacao { get; set; }
        public Nullable<System.DateTime> ValidadeDaHabilitacao { get; set; }
        public Nullable<System.DateTime> DataDaPrimeiraHabilitacao { get; set; }
        public string Categoria { get; set; }
        public Nullable<System.DateTime> DataDeNascimento { get; set; }
        public string IDCidadeNascimento { get; set; }
        public string NomeDoPai { get; set; }
        public string NomeDaMae { get; set; }
        public string Conjuge { get; set; }
        public Nullable<decimal> VitimaDeRouboQuantidade { get; set; }
        public Nullable<decimal> SofreuAcidadeQuantidade { get; set; }
        public string EstadoCivil { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public Nullable<decimal> CarregamentoAutorizadoAte { get; set; }
        public Nullable<System.DateTime> VencimentoNoGerenciadorDeRisco { get; set; }
        public Nullable<decimal> AliquotaSestSenat { get; set; }
        public string VinculoComAEmpresa { get; set; }
        public string NumeroPancard { get; set; }
        public string Ativo { get; set; }
        public string Liberado { get; set; }
        public string MOPP { get; set; }
        public Nullable<System.DateTime> AguardandoLiberacao { get; set; }
        public Nullable<System.DateTime> VencimentoPancary { get; set; }
        public Nullable<System.DateTime> VencimentoBrasilrisk { get; set; }
        public Nullable<System.DateTime> VencimentoBuonny { get; set; }
        public Nullable<System.DateTime> DataDeBloqueio { get; set; }
        public string NumeroRegistroCNH { get; set; }
        public string NumeroInss { get; set; }
        public string Origem { get; set; }
        public string RecolheINSS { get; set; }
        public string RecolheIRRF { get; set; }
        public string RecolheSESTSENAT { get; set; }
        public string LocalRG { get; set; }
        public string LocalEmissaoRG { get; set; }
        public string Senha { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual ICollection<DT> DTs1 { get; set; }
        public virtual ICollection<MotoristaFilial> MotoristaFilials { get; set; }
        public virtual ICollection<MotoristaHistorico> MotoristaHistoricoes { get; set; }
        public virtual ICollection<Veiculo> Veiculoes { get; set; }
    }
}
