using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ControleBriefingItemMap : EntityTypeConfiguration<ControleBriefingItem>
    {
        public ControleBriefingItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdControleBriefingItem);

            // Properties
            this.Property(t => t.IdControleBriefingItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Operacao)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ControleBriefingItem");
            this.Property(t => t.IdControleBriefingItem).HasColumnName("IdControleBriefingItem");
            this.Property(t => t.IdControleBriefing).HasColumnName("IdControleBriefing");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdProdutoEmbalagem).HasColumnName("IdProdutoEmbalagem");
            this.Property(t => t.Operacao).HasColumnName("Operacao");
            this.Property(t => t.Valor).HasColumnName("Valor");
        }
    }
}
