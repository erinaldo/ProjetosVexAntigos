using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VW_LIBERACAO_PEDIDOS_DETALHEMap : EntityTypeConfiguration<VW_LIBERACAO_PEDIDOS_DETALHE>
    {
        public VW_LIBERACAO_PEDIDOS_DETALHEMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDDOCUMENTO, t.STATUS, t.IDCliente });

            // Properties
            this.Property(t => t.IDDOCUMENTO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SERIE)
                .HasMaxLength(20);

            this.Property(t => t.STATUS)
                .IsRequired()
                .HasMaxLength(23);

            this.Property(t => t.TIPODEDOCUMENTO)
                .HasMaxLength(20);

            this.Property(t => t.IDCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VW_LIBERACAO_PEDIDOS_DETALHE");
            this.Property(t => t.IDDOCUMENTO).HasColumnName("IDDOCUMENTO");
            this.Property(t => t.NUMERO).HasColumnName("NUMERO");
            this.Property(t => t.SERIE).HasColumnName("SERIE");
            this.Property(t => t.STATUS).HasColumnName("STATUS");
            this.Property(t => t.TIPODEDOCUMENTO).HasColumnName("TIPODEDOCUMENTO");
            this.Property(t => t.DATAPLANEJADA).HasColumnName("DATAPLANEJADA");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
        }
    }
}
