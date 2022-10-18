using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RequisicaoDeMaterialDocumentoMap : EntityTypeConfiguration<RequisicaoDeMaterialDocumento>
    {
        public RequisicaoDeMaterialDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRequisicaoDeMaterialDocumento);

            // Properties
            this.Property(t => t.IDRequisicaoDeMaterialDocumento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("RequisicaoDeMaterialDocumento");
            this.Property(t => t.IDRequisicaoDeMaterialDocumento).HasColumnName("IDRequisicaoDeMaterialDocumento");
            this.Property(t => t.IDRequisicaoDeMaterialItem).HasColumnName("IDRequisicaoDeMaterialItem");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IdDocumentoItem).HasColumnName("IdDocumentoItem");
            this.Property(t => t.IdCotacaoDeCompraItem).HasColumnName("IdCotacaoDeCompraItem");
            this.Property(t => t.IdDocumentoNFSaida).HasColumnName("IdDocumentoNFSaida");
        }
    }
}
