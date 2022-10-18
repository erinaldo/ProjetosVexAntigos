using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MotoristaHistoricoMap : EntityTypeConfiguration<MotoristaHistorico>
    {
        public MotoristaHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdMotoristaHistorico);

            // Properties
            this.Property(t => t.IdMotoristaHistorico)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Historico)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("MotoristaHistorico");
            this.Property(t => t.IdMotoristaHistorico).HasColumnName("IdMotoristaHistorico");
            this.Property(t => t.IdMotorista).HasColumnName("IdMotorista");
            this.Property(t => t.Historico).HasColumnName("Historico");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");

            // Relationships
            this.HasRequired(t => t.Motorista)
                .WithMany(t => t.MotoristaHistoricoes)
                .HasForeignKey(d => d.IdMotorista);

        }
    }
}
