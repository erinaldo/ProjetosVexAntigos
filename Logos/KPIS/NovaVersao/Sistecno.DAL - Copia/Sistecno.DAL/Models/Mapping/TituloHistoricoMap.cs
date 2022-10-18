using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TituloHistoricoMap : EntityTypeConfiguration<TituloHistorico>
    {
        public TituloHistoricoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTituloHistorico);

            // Properties
            this.Property(t => t.IDTituloHistorico)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Historico)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("TituloHistorico");
            this.Property(t => t.IDTituloHistorico).HasColumnName("IDTituloHistorico");
            this.Property(t => t.IDTitulo).HasColumnName("IDTitulo");
            this.Property(t => t.Historico).HasColumnName("Historico");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");

            // Relationships
            this.HasRequired(t => t.Titulo)
                .WithMany(t => t.TituloHistoricoes)
                .HasForeignKey(d => d.IDTitulo);
            this.HasOptional(t => t.Usuario)
                .WithMany(t => t.TituloHistoricoes)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
