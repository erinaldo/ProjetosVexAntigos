using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RomaneioOcorrenciaMap : EntityTypeConfiguration<RomaneioOcorrencia>
    {
        public RomaneioOcorrenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRomaneioOcorrencia);

            // Properties
            this.Property(t => t.IDRomaneioOcorrencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("RomaneioOcorrencia");
            this.Property(t => t.IDRomaneioOcorrencia).HasColumnName("IDRomaneioOcorrencia");
            this.Property(t => t.IDRomaneio).HasColumnName("IDRomaneio");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDOcorrencia).HasColumnName("IDOcorrencia");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.DataOcorrencia).HasColumnName("DataOcorrencia");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasOptional(t => t.Ocorrencia)
                .WithMany(t => t.RomaneioOcorrencias)
                .HasForeignKey(d => d.IDOcorrencia);
            this.HasRequired(t => t.Romaneio)
                .WithMany(t => t.RomaneioOcorrencias)
                .HasForeignKey(d => d.IDRomaneio);
            this.HasOptional(t => t.Usuario)
                .WithMany(t => t.RomaneioOcorrencias)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
