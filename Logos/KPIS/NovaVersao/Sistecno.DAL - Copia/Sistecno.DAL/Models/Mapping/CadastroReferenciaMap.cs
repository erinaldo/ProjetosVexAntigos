using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroReferenciaMap : EntityTypeConfiguration<CadastroReferencia>
    {
        public CadastroReferenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroReferencia);

            // Properties
            this.Property(t => t.IDCadastroReferencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeReferencia)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Referencia)
                .HasMaxLength(60);

            this.Property(t => t.Contato)
                .HasMaxLength(60);

            this.Property(t => t.Observacao)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CadastroReferencia");
            this.Property(t => t.IDCadastroReferencia).HasColumnName("IDCadastroReferencia");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.TipoDeReferencia).HasColumnName("TipoDeReferencia");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Referencia).HasColumnName("Referencia");
            this.Property(t => t.Contato).HasColumnName("Contato");
            this.Property(t => t.Observacao).HasColumnName("Observacao");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.CadastroReferencias)
                .HasForeignKey(d => d.IDCadastro);

        }
    }
}
