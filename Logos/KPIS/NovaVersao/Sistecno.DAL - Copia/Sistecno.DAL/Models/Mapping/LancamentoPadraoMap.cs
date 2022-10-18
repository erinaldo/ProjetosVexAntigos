using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LancamentoPadraoMap : EntityTypeConfiguration<LancamentoPadrao>
    {
        public LancamentoPadraoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDLancamentoPadrao);

            // Properties
            this.Property(t => t.IDLancamentoPadrao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("LancamentoPadrao");
            this.Property(t => t.IDLancamentoPadrao).HasColumnName("IDLancamentoPadrao");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
