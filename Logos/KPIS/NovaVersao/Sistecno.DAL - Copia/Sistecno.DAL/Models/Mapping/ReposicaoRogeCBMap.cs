using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ReposicaoRogeCBMap : EntityTypeConfiguration<ReposicaoRogeCB>
    {
        public ReposicaoRogeCBMap()
        {
            // Primary Key
            this.HasKey(t => t.IdReposicaoRogeCB);

            // Properties
            this.Property(t => t.IdReposicaoRogeCB)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodigoDeBarras)
                .HasMaxLength(50);

            this.Property(t => t.Embalagem)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ReposicaoRogeCB");
            this.Property(t => t.IdReposicaoRogeCB).HasColumnName("IdReposicaoRogeCB");
            this.Property(t => t.IdReposicaoRogeItem).HasColumnName("IdReposicaoRogeItem");
            this.Property(t => t.CodigoDeBarras).HasColumnName("CodigoDeBarras");
            this.Property(t => t.Embalagem).HasColumnName("Embalagem");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");

            // Relationships
            this.HasOptional(t => t.ReposicaoRogeItem)
                .WithMany(t => t.ReposicaoRogeCBs)
                .HasForeignKey(d => d.IdReposicaoRogeItem);

        }
    }
}
