using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DTHistoricoMap : EntityTypeConfiguration<DTHistorico>
    {
        public DTHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDTHistorico);

            // Properties
            this.Property(t => t.IdDTHistorico)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Historico)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("DTHistorico");
            this.Property(t => t.IdDTHistorico).HasColumnName("IdDTHistorico");
            this.Property(t => t.IdDT).HasColumnName("IdDT");
            this.Property(t => t.Historico).HasColumnName("Historico");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.DataDaOcorrencia).HasColumnName("DataDaOcorrencia");

            // Relationships
            this.HasRequired(t => t.DT)
                .WithMany(t => t.DTHistoricoes)
                .HasForeignKey(d => d.IdDT);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.DTHistoricoes)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
