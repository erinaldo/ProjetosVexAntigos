using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Movimentacao
    {
        public Movimentacao()
        {
            this.MovimentacaoRomaneios = new List<MovimentacaoRomaneio>();
            this.Romaneios = new List<Romaneio>();
        }

        public int IDMovimentacao { get; set; }
        public int IDFilial { get; set; }
        public int IDUsuario { get; set; }
        public int Numero { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Modo { get; set; }
        public string EstoqueProcessado { get; set; }
        public string Tipo { get; set; }
        public string Motivo { get; set; }
        public string Observacao { get; set; }
        public string Ativo { get; set; }
        public string MapaGerado { get; set; }
        public string PedidoNotaFiscal { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<MovimentacaoRomaneio> MovimentacaoRomaneios { get; set; }
        public virtual ICollection<Romaneio> Romaneios { get; set; }
    }
}
