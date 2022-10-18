using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoSituacaoMap : EntityTypeConfiguration<DocumentoSituacao>
    {
        public DocumentoSituacaoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Codigo, t.Descricao });

            // Properties
            this.Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("DocumentoSituacao");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
