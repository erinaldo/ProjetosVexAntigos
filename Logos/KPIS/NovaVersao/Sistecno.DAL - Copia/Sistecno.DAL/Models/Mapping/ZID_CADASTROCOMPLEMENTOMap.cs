using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_CADASTROCOMPLEMENTOMap : EntityTypeConfiguration<ZID_CADASTROCOMPLEMENTO>
    {
        public ZID_CADASTROCOMPLEMENTOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_CADASTROCOMPLEMENTO");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}