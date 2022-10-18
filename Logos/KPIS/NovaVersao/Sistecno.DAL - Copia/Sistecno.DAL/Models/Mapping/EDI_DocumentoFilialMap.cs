using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_DocumentoFilialMap : EntityTypeConfiguration<EDI_DocumentoFilial>
    {
        public EDI_DocumentoFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.EDI_Chave);

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.EDI_Motivo)
                .HasMaxLength(500);

            this.Property(t => t.Situacao)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("EDI_DocumentoFilial");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IDDocumentoFilial).HasColumnName("IDDocumentoFilial");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDRegiaoItem).HasColumnName("IDRegiaoItem");
            this.Property(t => t.IdRegiaoItemFilial).HasColumnName("IdRegiaoItemFilial");
            this.Property(t => t.IdRegiaoItemCliente).HasColumnName("IdRegiaoItemCliente");
            this.Property(t => t.IdRegiaoItemTransportador).HasColumnName("IdRegiaoItemTransportador");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Data).HasColumnName("Data");
        }
    }
}
