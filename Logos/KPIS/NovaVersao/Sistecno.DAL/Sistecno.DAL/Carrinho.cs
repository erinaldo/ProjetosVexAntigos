using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistecno.DAL
{
    public class Carrinho
    {
        public int idProduto { get; set; }
        public string CodigoProdutoCliente { get; set; }
        public decimal Quantidade { get; set; }
        public string Descricao { get; set; }
        public int Disponivel { get; set; }
        public decimal ValorUnitario { get; set; }
        public string IdClienteDivisao { get; set; }
        public string Divisao { get; set; }
        public int IdProdutoCliente { get; set; }
        public int IdProdutoEmbalagem { get; set; }
        public int? IdUnidadeDeArmazenagemLote { get; set; }
        public decimal M3 { get; set; }
        public decimal Peso { get; set; }

        public Carrinho()
        { }

    }
}
