using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MotoristaFilialMap : EntityTypeConfiguration<MotoristaFilial>
    {
        public MotoristaFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMotoristaFilial);

            // Properties
            this.Property(t => t.IDMotoristaFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MotoristaFilial");
            this.Property(t => t.IDMotoristaFilial).HasColumnName("IDMotoristaFilial");
            this.Property(t => t.IDMotorista).HasColumnName("IDMotorista");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.MotoristaFilials)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.Motorista)
                .WithMany(t => t.MotoristaFilials)
                .HasForeignKey(d => d.IDMotorista);

        }
    }
}
