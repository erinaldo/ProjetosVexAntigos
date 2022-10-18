using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SistecnoColetor.Classes.DTO
{
   public  class Produto
    {
        public string CodigoDeBarras { get; set; }
        public string IdProdutoCliente { get; set; }
        public string IdProduto { get; set; }
        public string Lastro { get; set; }
        public string LastroAltura { get; set; }
        public string Altura { get; set; }
        public string Largura { get; set; }
        public string PesoLiquido { get; set; }
        public string PesoBruto { get; set; }
        public string Comprimento { get; set; }
    }
}
