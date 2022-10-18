using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class CadastroCondicaoEntrega
    {
        public int IDCadastroCondicaoEntrega { get; set; }
        public string RecebeSabado { get; set; }
        public string RecebeDomingo { get; set; }
        public string HorarioRecebimentoInicial { get; set; }
        public string HorarioRecebimentoFinal { get; set; }
        public string ColetaSabado { get; set; }
        public string ColetaDomingo { get; set; }
        public string HorarioColetaInicial { get; set; }
        public string HorarioColetaFinal { get; set; }
        public string TempoMedioDeRecebimento { get; set; }
        public string TempoMedioDeColeta { get; set; }
        public string Historico { get; set; }
        public virtual Cadastro Cadastro { get; set; }
    }
}
