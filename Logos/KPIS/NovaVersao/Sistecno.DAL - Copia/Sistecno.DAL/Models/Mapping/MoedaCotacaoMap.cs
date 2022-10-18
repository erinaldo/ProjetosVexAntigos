using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MoedaCotacaoMap : EntityTypeConfiguration<MoedaCotacao>
    {
        public MoedaCotacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMoedaCotacao);

            // Properties
            this.Property(t => t.IDMoedaCotacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MoedaCotacao");
            this.Property(t => t.IDMoedaCotacao).HasColumnName("IDMoedaCotacao");
            this.Property(t => t.IDMoeda).HasColumnName("IDMoeda");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Valor).HasColumnName("Valor");

            // Relationships
            this.HasRequired(t => t.Moeda)
                .WithMany(t => t.MoedaCotacaos)
                .HasForeignKey(d => d.IDMoeda);

        }
    }
}
