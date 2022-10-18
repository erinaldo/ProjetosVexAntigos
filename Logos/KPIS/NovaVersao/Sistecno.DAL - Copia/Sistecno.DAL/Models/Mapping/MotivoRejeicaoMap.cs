using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MotivoRejeicaoMap : EntityTypeConfiguration<MotivoRejeicao>
    {
        public MotivoRejeicaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdMotivoRejeicao);

            // Properties
            this.Property(t => t.IdMotivoRejeicao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodigoRejeicao)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.MotivoRejeicao1)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("MotivoRejeicao");
            this.Property(t => t.IdMotivoRejeicao).HasColumnName("IdMotivoRejeicao");
            this.Property(t => t.CodigoRejeicao).HasColumnName("CodigoRejeicao");
            this.Property(t => t.MotivoRejeicao1).HasColumnName("MotivoRejeicao");
        }
    }
}
