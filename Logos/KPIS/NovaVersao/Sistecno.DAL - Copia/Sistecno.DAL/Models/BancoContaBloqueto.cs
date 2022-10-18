using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class BancoContaBloqueto
    {
        public BancoContaBloqueto()
        {
            this.TituloDuplicatas = new List<TituloDuplicata>();
        }

        public int IDBancoContaBloqueto { get; set; }
        public int IDBancoConta { get; set; }
        public int Sequencia { get; set; }
        public Nullable<int> IDBancoCarteira { get; set; }
        public Nullable<int> IDBancoEspecie { get; set; }
        public string Aceite { get; set; }
        public string CodigoDoCedente { get; set; }
        public string DigitoDoCedente { get; set; }
        public string Convenio { get; set; }
        public string NossoNumero { get; set; }
        public Nullable<int> IDBancoInstrucao { get; set; }
        public string InstrucaoLivre01 { get; set; }
        public string InstrucaoLivre02 { get; set; }
        public string InstrucaoLivre03 { get; set; }
        public string InstrucaoLivre04 { get; set; }
        public string InstrucaoLivre05 { get; set; }
        public string TipoImpressao { get; set; }
        public string TipoDeBloqueto { get; set; }
        public Nullable<int> IdBancoInstrucaoDeProtesto { get; set; }
        public Nullable<int> Disponibilidade { get; set; }
        public virtual BancoCarteira BancoCarteira { get; set; }
        public virtual BancoConta BancoConta { get; set; }
        public virtual BancoContaBloqueto BancoContaBloqueto1 { get; set; }
        public virtual BancoContaBloqueto BancoContaBloqueto2 { get; set; }
        public virtual BancoContaBloqueto BancoContaBloqueto11 { get; set; }
        public virtual BancoContaBloqueto BancoContaBloqueto3 { get; set; }
        public virtual BancoContaBloqueto BancoContaBloqueto12 { get; set; }
        public virtual BancoContaBloqueto BancoContaBloqueto4 { get; set; }
        public virtual BancoEspecie BancoEspecie { get; set; }
        public virtual BancoInstrucao BancoInstrucao { get; set; }
        public virtual BancoInstrucaoDeProtesto BancoInstrucaoDeProtesto { get; set; }
        public virtual ICollection<TituloDuplicata> TituloDuplicatas { get; set; }
    }
}
