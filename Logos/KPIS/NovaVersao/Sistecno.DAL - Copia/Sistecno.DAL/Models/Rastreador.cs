using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Rastreador
    {
        public Rastreador()
        {
            this.DTs = new List<DT>();
            this.Rastreamentoes = new List<Rastreamento>();
        }

        public int IdRastreador { get; set; }
        public string Chave { get; set; }
        public string Nome { get; set; }
        public Nullable<int> Tempo { get; set; }
        public string EnviaPosicaoZerada { get; set; }
        public string EnviaFotos { get; set; }
        public string NumeroChip { get; set; }
        public string EnviaFoto { get; set; }
        public Nullable<System.DateTime> UltimaSincronizacao { get; set; }
        public Nullable<int> UltimaDT { get; set; }
        public string UltimaPlaca { get; set; }
        public Nullable<System.DateTime> InicioSincronizacao { get; set; }
        public Nullable<System.DateTime> FinalSincronizacao { get; set; }
        public Nullable<int> TempoSincronizacao { get; set; }
        public Nullable<System.DateTime> UtltimoEnvioDeDados { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual ICollection<Rastreamento> Rastreamentoes { get; set; }
    }
}
