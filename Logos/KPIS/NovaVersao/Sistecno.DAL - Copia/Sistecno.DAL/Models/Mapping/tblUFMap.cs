using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class tblUFMap : EntityTypeConfiguration<tblUF>
    {
        public tblUFMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Codigo, t.Descricao });

            // Properties
            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(72);

            // Table & Column Mappings
            this.ToTable("tblUF");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
