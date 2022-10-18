using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TituloDuplicataHistoricoMap : EntityTypeConfiguration<TituloDuplicataHistorico>
    {
        public TituloDuplicataHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTituloDuplicataHistorico);

            // Properties
            this.Property(t => t.IDTituloDuplicataHistorico)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Historico)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("TituloDuplicataHistorico");
            this.Property(t => t.IDTituloDuplicataHistorico).HasColumnName("IDTituloDuplicataHistorico");
            this.Property(t => t.IDTituloDuplicata).HasColumnName("IDTituloDuplicata");
            this.Property(t => t.Historico).HasColumnName("Historico");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.NumeroDoArquivo).HasColumnName("NumeroDoArquivo");

            // Relationships
            this.HasRequired(t => t.TituloDuplicata)
                .WithMany(t => t.TituloDuplicataHistoricoes)
                .HasForeignKey(d => d.IDTituloDuplicata);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.TituloDuplicataHistoricoes)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
