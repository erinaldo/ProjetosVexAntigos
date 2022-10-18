using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VW_REENTREGAMap : EntityTypeConfiguration<VW_REENTREGA>
    {
        public VW_REENTREGAMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDDESTINATARIO, t.ROMIDDT, t.IDREGIAO });

            // Properties
            this.Property(t => t.IDDESTINATARIO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ROMIDDT)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDREGIAO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PROPRIETARIO)
                .HasMaxLength(30);

            this.Property(t => t.ENTREGAEFETUADA)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("VW_REENTREGA");
            this.Property(t => t.IDDESTINATARIO).HasColumnName("IDDESTINATARIO");
            this.Property(t => t.ROMIDDT).HasColumnName("ROMIDDT");
            this.Property(t => t.IDREGIAO).HasColumnName("IDREGIAO");
            this.Property(t => t.PROPRIETARIO).HasColumnName("PROPRIETARIO");
            this.Property(t => t.ENTREGAEFETUADA).HasColumnName("ENTREGAEFETUADA");
            this.Property(t => t.FRETEIDDDT).HasColumnName("FRETEIDDDT");
            this.Property(t => t.FRETE).HasColumnName("FRETE");
        }
    }
}
