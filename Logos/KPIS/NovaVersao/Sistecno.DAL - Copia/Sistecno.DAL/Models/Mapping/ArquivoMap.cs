using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ArquivoMap : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdArquivo);

            // Properties
            this.Property(t => t.IdArquivo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Arquivo");
            this.Property(t => t.IdArquivo).HasColumnName("IdArquivo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
        }
    }
}
