using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class GrandeUsuarioEndereco
    {
        public int IDGrandeUsuario { get; set; }
        public Nullable<int> IDRua { get; set; }
        public string Preposicao { get; set; }
        public string Titulo { get; set; }
        public string Rua { get; set; }
        public string NumeroDoLote { get; set; }
        public string Complemento1 { get; set; }
        public string Numero1 { get; set; }
        public string Complemento2 { get; set; }
        public string Numero2 { get; set; }
        public string TipoUnidadeOcupacao { get; set; }
        public string NumeroUnidadeOcupacao { get; set; }
        public virtual GrandeUsuario GrandeUsuario { get; set; }
    }
}
