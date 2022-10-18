using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroCDAMap : EntityTypeConfiguration<CadastroCDA>
    {
        public CadastroCDAMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCadastroCda);

            // Properties
            this.Property(t => t.IdCadastroCda)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("CadastroCDA");
            this.Property(t => t.IdCadastroCda).HasColumnName("IdCadastroCda");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
        }
    }
}
