using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ArquivoItemMap : EntityTypeConfiguration<ArquivoItem>
    {
        public ArquivoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdArquivoItem);

            // Properties
            this.Property(t => t.IdArquivoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NomeDoArquivo)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ArquivoItem");
            this.Property(t => t.IdArquivoItem).HasColumnName("IdArquivoItem");
            this.Property(t => t.IdArquivo).HasColumnName("IdArquivo");
            this.Property(t => t.NomeDoArquivo).HasColumnName("NomeDoArquivo");
            this.Property(t => t.ConteudoArquivo).HasColumnName("ConteudoArquivo");

            // Relationships
            this.HasRequired(t => t.Arquivo)
                .WithMany(t => t.ArquivoItems)
                .HasForeignKey(d => d.IdArquivo);

        }
    }
}
