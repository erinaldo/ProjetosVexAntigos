using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RomaneioPrevisaoRegiaoMap : EntityTypeConfiguration<RomaneioPrevisaoRegiao>
    {
        public RomaneioPrevisaoRegiaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRomaneioPrevisaoRegiao);

            // Properties
            this.Property(t => t.IdRomaneioPrevisaoRegiao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("RomaneioPrevisaoRegiao");
            this.Property(t => t.IdRomaneioPrevisaoRegiao).HasColumnName("IdRomaneioPrevisaoRegiao");
            this.Property(t => t.IdRomaneioPrevisao).HasColumnName("IdRomaneioPrevisao");
            this.Property(t => t.IdRegiao).HasColumnName("IdRegiao");
            this.Property(t => t.OrdemDeCarregamento).HasColumnName("OrdemDeCarregamento");
            this.Property(t => t.data).HasColumnName("data");

            // Relationships
            this.HasRequired(t => t.Regiao)
                .WithMany(t => t.RomaneioPrevisaoRegiaos)
                .HasForeignKey(d => d.IdRegiao);
            this.HasRequired(t => t.RomaneioPrevisao)
                .WithMany(t => t.RomaneioPrevisaoRegiaos)
                .HasForeignKey(d => d.IdRomaneioPrevisao);

        }
    }
}
