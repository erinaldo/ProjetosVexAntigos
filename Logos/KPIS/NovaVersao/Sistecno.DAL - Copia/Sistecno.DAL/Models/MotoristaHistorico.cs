using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class MotoristaHistorico
    {
        public int IdMotoristaHistorico { get; set; }
        public int IdMotorista { get; set; }
        public string Historico { get; set; }
        public System.DateTime DataDeCadastro { get; set; }
        public int IDUsuario { get; set; }
        public virtual Motorista Motorista { get; set; }
    }
}
