using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RomaneioConferenciaItemMap : EntityTypeConfiguration<RomaneioConferenciaItem>
    {
        public RomaneioConferenciaItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRomaneioConferenciaItem);

            // Properties
            this.Property(t => t.IdRomaneioConferenciaItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("RomaneioConferenciaItem");
            this.Property(t => t.IdRomaneioConferenciaItem).HasColumnName("IdRomaneioConferenciaItem");
            this.Property(t => t.IdRomaneioConferencia).HasColumnName("IdRomaneioConferencia");
            this.Property(t => t.IdEAN13).HasColumnName("IdEAN13");
            this.Property(t => t.IdDUN14).HasColumnName("IdDUN14");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Validade).HasColumnName("Validade");
            this.Property(t => t.Fator).HasColumnName("Fator");

            // Relationships
            this.HasOptional(t => t.Produto)
                .WithMany(t => t.RomaneioConferenciaItems)
                .HasForeignKey(d => d.IdDUN14);
            this.HasOptional(t => t.Produto1)
                .WithMany(t => t.RomaneioConferenciaItems1)
                .HasForeignKey(d => d.IdEAN13);
            this.HasRequired(t => t.RomaneioConferencia)
                .WithMany(t => t.RomaneioConferenciaItems)
                .HasForeignKey(d => d.IdRomaneioConferencia);

        }
    }
}
