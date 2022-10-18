using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RequisicaoDeMaterial
    {
        public RequisicaoDeMaterial()
        {
            this.EstoqueComprasMovs = new List<EstoqueComprasMov>();
            this.RequisicaoDeMaterialItems = new List<RequisicaoDeMaterialItem>();
        }

        public int IDRequisicaoDeMaterial { get; set; }
        public int IDFilial { get; set; }
        public int IDUsuarioCompra { get; set; }
        public Nullable<int> IDUnidadeDeNegocios { get; set; }
        public System.DateTime DataDeSolicitacao { get; set; }
        public string Status { get; set; }
        public string Ativo { get; set; }
        public Nullable<System.DateTime> PrevisaoDeEntrega { get; set; }
        public string TipoDocumento { get; set; }
        public int IDCentroDeCustoFilial { get; set; }
        public Nullable<int> IdProjeto { get; set; }
        public string Impresso { get; set; }
        public Nullable<int> IdDepartamento { get; set; }
        public string Prioridade { get; set; }
        public virtual ICollection<EstoqueComprasMov> EstoqueComprasMovs { get; set; }
        public virtual ICollection<RequisicaoDeMaterialItem> RequisicaoDeMaterialItems { get; set; }
    }
}
