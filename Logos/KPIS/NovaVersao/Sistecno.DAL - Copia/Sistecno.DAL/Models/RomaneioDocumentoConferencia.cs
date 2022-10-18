using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class RomaneioDocumentoConferencia
    {
        public int IDRomaneioDocumentoConferencia { get; set; }
        public Nullable<int> IDRomaneioDocumento { get; set; }
        public Nullable<int> IDDocumento { get; set; }
        public Nullable<int> IDProdutoCliente { get; set; }
        public Nullable<int> IDUnidadeDeArmazenagem { get; set; }
        public Nullable<int> IdUnidadeDeArmazenagemPai { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public Nullable<int> IDProdutoEmbalagem { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public string Referencia { get; set; }
        public Nullable<System.DateTime> Validade { get; set; }
        public System.DateTime DataDeEntrada { get; set; }
        public Nullable<int> IDDocumentoItem { get; set; }
        public int idRomaneio { get; set; }
        public Nullable<int> IdProdutoEmbalagemRecebido { get; set; }
        public Nullable<int> IdEan13 { get; set; }
        public Nullable<int> IdDun14 { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual ProdutoCliente ProdutoCliente { get; set; }
        public virtual ProdutoEmbalagem ProdutoEmbalagem { get; set; }
        public virtual Romaneio Romaneio { get; set; }
        public virtual RomaneioDocumento RomaneioDocumento { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
