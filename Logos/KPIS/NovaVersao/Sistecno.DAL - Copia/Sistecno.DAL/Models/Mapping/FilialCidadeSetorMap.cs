using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FilialCidadeSetorMap : EntityTypeConfiguration<FilialCidadeSetor>
    {
        public FilialCidadeSetorMap()
        {
            // Primary Key
            this.HasKey(t => t.IdFilialCidadeSetor);

            // Properties
            this.Property(t => t.IdFilialCidadeSetor)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FilialCidadeSetor");
            this.Property(t => t.IdFilialCidadeSetor).HasColumnName("IdFilialCidadeSetor");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.IdCidade).HasColumnName("IdCidade");
            this.Property(t => t.IdSetor).HasColumnName("IdSetor");

            // Relationships
            this.HasOptional(t => t.Cidade)
                .WithMany(t => t.FilialCidadeSetors)
                .HasForeignKey(d => d.IdCidade);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.FilialCidadeSetors)
                .HasForeignKey(d => d.IdFilial);
            this.HasOptional(t => t.Setor)
                .WithMany(t => t.FilialCidadeSetors)
                .HasForeignKey(d => d.IdSetor);

        }
    }
}
