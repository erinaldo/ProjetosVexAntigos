using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BASEMap : EntityTypeConfiguration<BASE>
    {
        public BASEMap()
        {
            // Primary Key
            this.HasKey(t => t.F7);

            // Properties
            this.Property(t => t.CODIGO)
                .HasMaxLength(255);

            this.Property(t => t.DESCRICAO)
                .HasMaxLength(255);

            this.Property(t => t.CDA)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("BASE");
            this.Property(t => t.CODIGO).HasColumnName("CODIGO");
            this.Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            this.Property(t => t.CDA).HasColumnName("CDA");
            this.Property(t => t.IDCDA).HasColumnName("IDCDA");
            this.Property(t => t.SALDOATUAL).HasColumnName("SALDOATUAL");
            this.Property(t => t.SALDOCORRETO).HasColumnName("SALDOCORRETO");
            this.Property(t => t.F7).HasColumnName("F7");
        }
    }
}
