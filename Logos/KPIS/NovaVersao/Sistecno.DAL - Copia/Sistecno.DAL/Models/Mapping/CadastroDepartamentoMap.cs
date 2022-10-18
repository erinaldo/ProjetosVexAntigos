using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroDepartamentoMap : EntityTypeConfiguration<CadastroDepartamento>
    {
        public CadastroDepartamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroDepartamento);

            // Properties
            this.Property(t => t.IDCadastroDepartamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("CadastroDepartamento");
            this.Property(t => t.IDCadastroDepartamento).HasColumnName("IDCadastroDepartamento");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
