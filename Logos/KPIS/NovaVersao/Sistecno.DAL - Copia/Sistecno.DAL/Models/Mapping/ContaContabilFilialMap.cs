using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaContabilFilialMap : EntityTypeConfiguration<ContaContabilFilial>
    {
        public ContaContabilFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDContaContabilFilial);

            // Properties
            this.Property(t => t.IDContaContabilFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ContaContabilFilial");
            this.Property(t => t.IDContaContabilFilial).HasColumnName("IDContaContabilFilial");
            this.Property(t => t.IDContaContabil).HasColumnName("IDContaContabil");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.SaldoInicial).HasColumnName("SaldoInicial");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.DataSaldoInicial).HasColumnName("DataSaldoInicial");
            this.Property(t => t.DataDeFechamento).HasColumnName("DataDeFechamento");
            this.Property(t => t.IdContaContabilFilialRelacionada).HasColumnName("IdContaContabilFilialRelacionada");

            // Relationships
            this.HasRequired(t => t.ContaContabil)
                .WithMany(t => t.ContaContabilFilials)
                .HasForeignKey(d => d.IDContaContabil);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.ContaContabilFilials)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
