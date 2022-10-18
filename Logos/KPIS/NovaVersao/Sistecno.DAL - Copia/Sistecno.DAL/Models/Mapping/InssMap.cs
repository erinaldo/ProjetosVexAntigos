using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class InssMap : EntityTypeConfiguration<Inss>
    {
        public InssMap()
        {
            // Primary Key
            this.HasKey(t => t.IdInss);

            // Properties
            this.Property(t => t.IdInss)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Inss");
            this.Property(t => t.IdInss).HasColumnName("IdInss");
            this.Property(t => t.IdEmpresa).HasColumnName("IdEmpresa");
            this.Property(t => t.Aliquota).HasColumnName("Aliquota");
            this.Property(t => t.AliquotaEmpresa).HasColumnName("AliquotaEmpresa");
            this.Property(t => t.BaseDeCalculo).HasColumnName("BaseDeCalculo");
            this.Property(t => t.ValorTeto).HasColumnName("ValorTeto");
            this.Property(t => t.SestSenat).HasColumnName("SestSenat");

            // Relationships
            this.HasRequired(t => t.Empresa)
                .WithMany(t => t.Insses)
                .HasForeignKey(d => d.IdEmpresa);

        }
    }
}
