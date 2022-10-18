using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RamoDeAtividadeMap : EntityTypeConfiguration<RamoDeAtividade>
    {
        public RamoDeAtividadeMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRamoDeAtividade);

            // Properties
            this.Property(t => t.IDRamoDeAtividade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsFixedLength()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("RamoDeAtividade");
            this.Property(t => t.IDRamoDeAtividade).HasColumnName("IDRamoDeAtividade");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
