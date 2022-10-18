using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sistran.Library.DTO
{
    public class Volumes
    {
        public string CodigoBarras { get; set; }
    }

    public class Itens
    {
        public int CodigoRoge { get; set; }        
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public string Valor { get; set; }
        public List<CodigoBarras> ItensCodigoBarras { get; set; }
        
    }

    public class CodigoBarras
    {
        public string CodigoDeBarras { get; set; }
        public string Embalagem { get; set; }
        public string EmbalagemQuantidade { get; set; }
        

    }
}
