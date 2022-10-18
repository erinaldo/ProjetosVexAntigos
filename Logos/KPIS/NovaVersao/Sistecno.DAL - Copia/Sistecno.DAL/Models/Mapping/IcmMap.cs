using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class IcmMap : EntityTypeConfiguration<Icm>
    {
        public IcmMap()
        {
            // Primary Key
            this.HasKey(t => t.IDIcms);

            // Properties
            this.Property(t => t.IDIcms)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Icms");
            this.Property(t => t.IDIcms).HasColumnName("IDIcms");
            this.Property(t => t.IDEstadoOrigem).HasColumnName("IDEstadoOrigem");
            this.Property(t => t.IDEstadoDestino).HasColumnName("IDEstadoDestino");
            this.Property(t => t.AliquotaContribuinte).HasColumnName("AliquotaContribuinte");
            this.Property(t => t.AliquotaNaoContribuinte).HasColumnName("AliquotaNaoContribuinte");
            this.Property(t => t.AliquotaSeguroAcidente).HasColumnName("AliquotaSeguroAcidente");
            this.Property(t => t.AliquotaSeguroRoubo).HasColumnName("AliquotaSeguroRoubo");
            this.Property(t => t.FatorDeInclusaoDeIcmsContribuinte).HasColumnName("FatorDeInclusaoDeIcmsContribuinte");
            this.Property(t => t.AliquotaNaoContribuinteNF).HasColumnName("AliquotaNaoContribuinteNF");
            this.Property(t => t.AliquotaContribuinteNF).HasColumnName("AliquotaContribuinteNF");
            this.Property(t => t.FCP).HasColumnName("FCP");

            // Relationships
            this.HasRequired(t => t.Estado)
                .WithMany(t => t.Icms)
                .HasForeignKey(d => d.IDEstadoDestino);
            this.HasRequired(t => t.Estado1)
                .WithMany(t => t.Icms1)
                .HasForeignKey(d => d.IDEstadoOrigem);

        }
    }
}
