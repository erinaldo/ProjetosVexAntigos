using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class OcorrenciaSerieMap : EntityTypeConfiguration<OcorrenciaSerie>
    {
        public OcorrenciaSerieMap()
        {
            // Primary Key
            this.HasKey(t => t.IDOcorrenciaSerie);

            // Properties
            this.Property(t => t.IDOcorrenciaSerie)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("OcorrenciaSerie");
            this.Property(t => t.IDOcorrenciaSerie).HasColumnName("IDOcorrenciaSerie");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
