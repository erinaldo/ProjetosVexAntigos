using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoLicenciamentoMap : EntityTypeConfiguration<VeiculoLicenciamento>
    {
        public VeiculoLicenciamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdVeiculoLicenciamento);

            // Properties
            this.Property(t => t.IdVeiculoLicenciamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FinalDaPlaca)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("VeiculoLicenciamento");
            this.Property(t => t.IdVeiculoLicenciamento).HasColumnName("IdVeiculoLicenciamento");
            this.Property(t => t.IdVeiculoTipo).HasColumnName("IdVeiculoTipo");
            this.Property(t => t.FinalDaPlaca).HasColumnName("FinalDaPlaca");
            this.Property(t => t.DataLimite).HasColumnName("DataLimite");

            // Relationships
            this.HasRequired(t => t.VeiculoTipo)
                .WithMany(t => t.VeiculoLicenciamentoes)
                .HasForeignKey(d => d.IdVeiculoTipo);

        }
    }
}
