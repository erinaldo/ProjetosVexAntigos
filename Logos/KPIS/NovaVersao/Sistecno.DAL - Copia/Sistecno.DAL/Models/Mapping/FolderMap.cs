using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FolderMap : EntityTypeConfiguration<Folder>
    {
        public FolderMap()
        {
            // Primary Key
            this.HasKey(t => t.IdFolder);

            // Properties
            this.Property(t => t.IdFolder)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Folder1)
                .HasMaxLength(10);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Folder");
            this.Property(t => t.IdFolder).HasColumnName("IdFolder");
            this.Property(t => t.Folder1).HasColumnName("Folder");
            this.Property(t => t.Inicio).HasColumnName("Inicio");
            this.Property(t => t.Fim).HasColumnName("Fim");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
        }
    }
}
