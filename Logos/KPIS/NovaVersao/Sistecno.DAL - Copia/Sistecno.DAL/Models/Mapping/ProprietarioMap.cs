using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProprietarioMap : EntityTypeConfiguration<Proprietario>
    {
        public ProprietarioMap()
        {
            // Primary Key
            this.HasKey(t => t.IDProprietario);

            // Properties
            this.Property(t => t.IDProprietario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Proprietario");
            this.Property(t => t.IDProprietario).HasColumnName("IDProprietario");
        }
    }
}
