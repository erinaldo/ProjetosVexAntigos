using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RomaneioPrevisaoMap : EntityTypeConfiguration<RomaneioPrevisao>
    {
        public RomaneioPrevisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRomaneioPrevisao);

            // Properties
            this.Property(t => t.IdRomaneioPrevisao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Observacao)
                .HasMaxLength(200);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("RomaneioPrevisao");
            this.Property(t => t.IdRomaneioPrevisao).HasColumnName("IdRomaneioPrevisao");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IdRegiao).HasColumnName("IdRegiao");
        }
    }
}
