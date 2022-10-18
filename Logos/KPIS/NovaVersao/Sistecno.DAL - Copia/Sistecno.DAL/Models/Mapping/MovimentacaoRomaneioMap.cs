using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MovimentacaoRomaneioMap : EntityTypeConfiguration<MovimentacaoRomaneio>
    {
        public MovimentacaoRomaneioMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMovimentacaoRomaneio);

            // Properties
            this.Property(t => t.IDMovimentacaoRomaneio)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MovimentacaoRomaneio");
            this.Property(t => t.IDMovimentacaoRomaneio).HasColumnName("IDMovimentacaoRomaneio");
            this.Property(t => t.IDMovimentacao).HasColumnName("IDMovimentacao");
            this.Property(t => t.IDRomaneio).HasColumnName("IDRomaneio");

            // Relationships
            this.HasOptional(t => t.Movimentacao)
                .WithMany(t => t.MovimentacaoRomaneios)
                .HasForeignKey(d => d.IDMovimentacao);
            this.HasOptional(t => t.Romaneio)
                .WithMany(t => t.MovimentacaoRomaneios)
                .HasForeignKey(d => d.IDRomaneio);

        }
    }
}
