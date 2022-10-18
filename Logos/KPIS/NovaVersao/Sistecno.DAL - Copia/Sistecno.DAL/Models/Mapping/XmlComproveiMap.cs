using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class XmlComproveiMap : EntityTypeConfiguration<XmlComprovei>
    {
        public XmlComproveiMap()
        {
            // Primary Key
            this.HasKey(t => t.IdXmlComprovei);

            // Properties
            this.Property(t => t.IdXmlComprovei)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("XmlComprovei");
            this.Property(t => t.IdXmlComprovei).HasColumnName("IdXmlComprovei");
            this.Property(t => t.XML).HasColumnName("XML");
        }
    }
}
