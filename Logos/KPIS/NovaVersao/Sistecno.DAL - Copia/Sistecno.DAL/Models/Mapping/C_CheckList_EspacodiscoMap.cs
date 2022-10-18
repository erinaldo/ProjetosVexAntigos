using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class C_CheckList_EspacodiscoMap : EntityTypeConfiguration<C_CheckList_Espacodisco>
    {
        public C_CheckList_EspacodiscoMap()
        {
            // Primary Key
            this.HasKey(t => t.Ocupado_SQL__MB_);

            // Properties
            this.Property(t => t.Drive)
                .HasMaxLength(10);

            this.Property(t => t.Ocupado_SQL__MB_)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("_CheckList_Espacodisco");
            this.Property(t => t.Drive).HasColumnName("Drive");
            this.Property(t => t.Tamanho__MB_).HasColumnName("Tamanho (MB)");
            this.Property(t => t.Usado__MB_).HasColumnName("Usado (MB)");
            this.Property(t => t.Livre__MB_).HasColumnName("Livre (MB)");
            this.Property(t => t.Livre____).HasColumnName("Livre (%)");
            this.Property(t => t.Usado____).HasColumnName("Usado (%)");
            this.Property(t => t.Ocupado_SQL__MB_).HasColumnName("Ocupado SQL (MB)");
        }
    }
}
