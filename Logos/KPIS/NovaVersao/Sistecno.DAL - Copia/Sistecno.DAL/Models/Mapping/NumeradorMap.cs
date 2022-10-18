using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class NumeradorMap : EntityTypeConfiguration<Numerador>
    {
        public NumeradorMap()
        {
            // Primary Key
            this.HasKey(t => t.IDNumerador);

            // Properties
            this.Property(t => t.IDNumerador)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Serie)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Numerador");
            this.Property(t => t.IDNumerador).HasColumnName("IDNumerador");
            this.Property(t => t.IdEmpresa).HasColumnName("IdEmpresa");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Serie).HasColumnName("Serie");
            this.Property(t => t.ProximoNumero).HasColumnName("ProximoNumero");

            // Relationships
            this.HasOptional(t => t.Empresa)
                .WithMany(t => t.Numeradors)
                .HasForeignKey(d => d.IdEmpresa);
            this.HasOptional(t => t.Filial)
                .WithMany(t => t.Numeradors)
                .HasForeignKey(d => d.IDFilial);
            this.HasOptional(t => t.Filial1)
                .WithMany(t => t.Numeradors1)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
