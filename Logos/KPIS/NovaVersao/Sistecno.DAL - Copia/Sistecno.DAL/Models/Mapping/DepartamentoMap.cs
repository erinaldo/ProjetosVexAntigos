using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DepartamentoMap : EntityTypeConfiguration<Departamento>
    {
        public DepartamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDepartamento);

            // Properties
            this.Property(t => t.IDDepartamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Departamento");
            this.Property(t => t.IDDepartamento).HasColumnName("IDDepartamento");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
