using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RomaneioDocumentoItemMap : EntityTypeConfiguration<RomaneioDocumentoItem>
    {
        public RomaneioDocumentoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRomaneioDocumentoItem);

            // Properties
            this.Property(t => t.IDRomaneioDocumentoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Status)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("RomaneioDocumentoItem");
            this.Property(t => t.IDRomaneioDocumentoItem).HasColumnName("IDRomaneioDocumentoItem");
            this.Property(t => t.IDRomaneioDocumento).HasColumnName("IDRomaneioDocumento");
            this.Property(t => t.IDDocumentoItem).HasColumnName("IDDocumentoItem");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.QuantidadeUnidadeEstoque).HasColumnName("QuantidadeUnidadeEstoque");

            // Relationships
            this.HasRequired(t => t.DocumentoItem)
                .WithMany(t => t.RomaneioDocumentoItems)
                .HasForeignKey(d => d.IDDocumentoItem);

        }
    }
}
