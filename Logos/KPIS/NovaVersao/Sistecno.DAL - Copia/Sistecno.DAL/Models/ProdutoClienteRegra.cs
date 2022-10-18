using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ProdutoClienteRegra
    {
        public int IdProdutoClienteRegra { get; set; }
        public int IdProdutoCliente { get; set; }
        public int IdDepositoPlantaLocalizacao { get; set; }
        public string TipoDeRegra { get; set; }
        public virtual DepositoPlantaLocalizacao DepositoPlantaLocalizacao { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
    }
}
