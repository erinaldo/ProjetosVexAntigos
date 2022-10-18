using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EdiArquivoMap : EntityTypeConfiguration<EdiArquivo>
    {
        public EdiArquivoMap()
        {
            // Primary Key
            this.HasKey(t => t.Chave);

            // Properties
            this.Property(t => t.Chave)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Linha)
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("EdiArquivo");
            this.Property(t => t.Chave).HasColumnName("Chave");
            this.Property(t => t.Linha).HasColumnName("Linha");
        }
    }
}
