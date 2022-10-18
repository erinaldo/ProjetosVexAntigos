using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ModuloOpcaoAcaoMap : EntityTypeConfiguration<ModuloOpcaoAcao>
    {
        public ModuloOpcaoAcaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDModuloOpcaoAcao);

            // Properties
            this.Property(t => t.IDModuloOpcaoAcao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Componente)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ModuloOpcaoAcao");
            this.Property(t => t.IDModuloOpcaoAcao).HasColumnName("IDModuloOpcaoAcao");
            this.Property(t => t.IDModuloOpcao).HasColumnName("IDModuloOpcao");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Componente).HasColumnName("Componente");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasRequired(t => t.ModuloOpcao)
                .WithMany(t => t.ModuloOpcaoAcaos)
                .HasForeignKey(d => d.IDModuloOpcao);

        }
    }
}
