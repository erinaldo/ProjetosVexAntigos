using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class OcorrenciaCodigoMap : EntityTypeConfiguration<OcorrenciaCodigo>
    {
        public OcorrenciaCodigoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDOcorrenciaCodigo);

            // Properties
            this.Property(t => t.IDOcorrenciaCodigo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Nome)
                .HasMaxLength(70);

            // Table & Column Mappings
            this.ToTable("OcorrenciaCodigo");
            this.Property(t => t.IDOcorrenciaCodigo).HasColumnName("IDOcorrenciaCodigo");
            this.Property(t => t.IDOcorrencia).HasColumnName("IDOcorrencia");
            this.Property(t => t.IDOcorrenciaSerie).HasColumnName("IDOcorrenciaSerie");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Nome).HasColumnName("Nome");

            // Relationships
            this.HasRequired(t => t.Ocorrencia)
                .WithMany(t => t.OcorrenciaCodigoes)
                .HasForeignKey(d => d.IDOcorrencia);
            this.HasRequired(t => t.OcorrenciaSerie)
                .WithMany(t => t.OcorrenciaCodigoes)
                .HasForeignKey(d => d.IDOcorrenciaSerie);

        }
    }
}
