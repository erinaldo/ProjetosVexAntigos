using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ModuloOpcaoMap : EntityTypeConfiguration<ModuloOpcao>
    {
        public ModuloOpcaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDModuloOpcao);

            // Properties
            this.Property(t => t.IDModuloOpcao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Programa)
                .HasMaxLength(80);

            this.Property(t => t.Pacote)
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Tipo)
                .HasMaxLength(10);

            this.Property(t => t.Link)
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ModuloOpcao");
            this.Property(t => t.IDModuloOpcao).HasColumnName("IDModuloOpcao");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Programa).HasColumnName("Programa");
            this.Property(t => t.Pacote).HasColumnName("Pacote");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Versao).HasColumnName("Versao");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
