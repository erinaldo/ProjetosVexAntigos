using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Mapa
    {
        public Mapa()
        {
            this.MovimentacaoRomaneioItems = new List<MovimentacaoRomaneioItem>();
        }

        public int IDMapa { get; set; }
        public int IDFilial { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<int> IDUsuarioProcessamento { get; set; }
        public int Numero { get; set; }
        public string Descricao { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string EstoqueProcessado { get; set; }
        public Nullable<System.DateTime> DataDeProcessamento { get; set; }
        public string Ativo { get; set; }
        public string TIPO { get; set; }
        public string Impresso { get; set; }
        public Nullable<System.DateTime> DataDeImpressao { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItems { get; set; }
    }
}
