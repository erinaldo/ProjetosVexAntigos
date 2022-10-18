using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Rua
    {
        public int IDRua { get; set; }
        public Nullable<int> IDCidade { get; set; }
        public Nullable<int> IDBairroInicial { get; set; }
        public Nullable<int> IDBairroFinal { get; set; }
        public string Cep { get; set; }
        public string Tipo { get; set; }
        public string Preposicao { get; set; }
        public string Titulo { get; set; }
        public string Nome { get; set; }
        public string Complemento { get; set; }
        public string TemGrandeUsuario { get; set; }
        public string TipoDeRegistro { get; set; }
    }
}
