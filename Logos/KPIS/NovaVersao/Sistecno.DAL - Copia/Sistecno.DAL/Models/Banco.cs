using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Banco
    {
        public Banco()
        {
            this.BancoCarteiras = new List<BancoCarteira>();
            this.BancoContas = new List<BancoConta>();
            this.BancoEspecies = new List<BancoEspecie>();
            this.BancoInstrucaos = new List<BancoInstrucao>();
            this.BancoInstrucaoDeProtestoes = new List<BancoInstrucaoDeProtesto>();
            this.BancoOcorrenciaRemessas = new List<BancoOcorrenciaRemessa>();
            this.BancoOcorrenciaRetornoes = new List<BancoOcorrenciaRetorno>();
            this.CadastroComplementoes = new List<CadastroComplemento>();
            this.Cheques = new List<Cheque>();
        }

        public int IDBanco { get; set; }
        public int Digito { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Ativo { get; set; }
        public byte[] LogoTipo { get; set; }
        public Nullable<int> IDLeiauteCheque { get; set; }
        public Nullable<int> IDLeiauteBoleto { get; set; }
        public Nullable<int> IDPais { get; set; }
        public Nullable<int> DiasRetencao { get; set; }
        public Nullable<decimal> TaxaCobrancaSimples { get; set; }
        public virtual ICollection<BancoCarteira> BancoCarteiras { get; set; }
        public virtual ICollection<BancoConta> BancoContas { get; set; }
        public virtual ICollection<BancoEspecie> BancoEspecies { get; set; }
        public virtual ICollection<BancoInstrucao> BancoInstrucaos { get; set; }
        public virtual ICollection<BancoInstrucaoDeProtesto> BancoInstrucaoDeProtestoes { get; set; }
        public virtual ICollection<BancoOcorrenciaRemessa> BancoOcorrenciaRemessas { get; set; }
        public virtual ICollection<BancoOcorrenciaRetorno> BancoOcorrenciaRetornoes { get; set; }
        public virtual ICollection<CadastroComplemento> CadastroComplementoes { get; set; }
        public virtual ICollection<Cheque> Cheques { get; set; }
    }
}
