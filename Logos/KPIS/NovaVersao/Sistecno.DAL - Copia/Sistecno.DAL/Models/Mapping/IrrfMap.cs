using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class IrrfMap : EntityTypeConfiguration<Irrf>
    {
        public IrrfMap()
        {
            // Primary Key
            this.HasKey(t => t.IdIrrf);

            // Properties
            this.Property(t => t.IdIrrf)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Irrf");
            this.Property(t => t.IdIrrf).HasColumnName("IdIrrf");
            this.Property(t => t.AcimaDe).HasColumnName("AcimaDe");
            this.Property(t => t.Aliquota).HasColumnName("Aliquota");
            this.Property(t => t.Deduzir).HasColumnName("Deduzir");
            this.Property(t => t.ValorDependente).HasColumnName("ValorDependente");
            this.Property(t => t.BaseDeCalculo).HasColumnName("BaseDeCalculo");
        }
    }
}
