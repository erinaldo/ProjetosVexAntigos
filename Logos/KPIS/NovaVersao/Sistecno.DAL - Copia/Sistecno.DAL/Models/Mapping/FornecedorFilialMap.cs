using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FornecedorFilialMap : EntityTypeConfiguration<FornecedorFilial>
    {
        public FornecedorFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDFornecedorFilial);

            // Properties
            this.Property(t => t.IDFornecedorFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FornecedorFilial");
            this.Property(t => t.IDFornecedorFilial).HasColumnName("IDFornecedorFilial");
            this.Property(t => t.IDFornecedor).HasColumnName("IDFornecedor");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.FornecedorFilials)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.Fornecedor)
                .WithMany(t => t.FornecedorFilials)
                .HasForeignKey(d => d.IDFornecedor);

        }
    }
}
