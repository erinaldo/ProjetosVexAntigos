using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FrotaGrupoItemControleMap : EntityTypeConfiguration<FrotaGrupoItemControle>
    {
        public FrotaGrupoItemControleMap()
        {
            // Primary Key
            this.HasKey(t => t.IDFrotaGrupoItemControle);

            // Properties
            this.Property(t => t.IDFrotaGrupoItemControle)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("FrotaGrupoItemControle");
            this.Property(t => t.IDFrotaGrupoItemControle).HasColumnName("IDFrotaGrupoItemControle");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
