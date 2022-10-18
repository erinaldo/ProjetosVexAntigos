using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FilialPortariaMap : EntityTypeConfiguration<FilialPortaria>
    {
        public FilialPortariaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdFilialPortaria, t.IdFilialPai, t.IdFilialFilho });

            // Properties
            this.Property(t => t.IdFilialPortaria)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdFilialPai)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IdFilialFilho)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FilialPortaria");
            this.Property(t => t.IdFilialPortaria).HasColumnName("IdFilialPortaria");
            this.Property(t => t.IdFilialPai).HasColumnName("IdFilialPai");
            this.Property(t => t.IdFilialFilho).HasColumnName("IdFilialFilho");
        }
    }
}
