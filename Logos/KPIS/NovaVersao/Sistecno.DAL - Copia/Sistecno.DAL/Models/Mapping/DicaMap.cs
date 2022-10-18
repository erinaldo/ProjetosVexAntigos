using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DicaMap : EntityTypeConfiguration<Dica>
    {
        public DicaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDica);

            // Properties
            this.Property(t => t.IDDica)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Titulo)
                .HasMaxLength(60);

            this.Property(t => t.Texto)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Dica");
            this.Property(t => t.IDDica).HasColumnName("IDDica");
            this.Property(t => t.Titulo).HasColumnName("Titulo");
            this.Property(t => t.DicaDoDia).HasColumnName("DicaDoDia");
            this.Property(t => t.Texto).HasColumnName("Texto");
        }
    }
}
