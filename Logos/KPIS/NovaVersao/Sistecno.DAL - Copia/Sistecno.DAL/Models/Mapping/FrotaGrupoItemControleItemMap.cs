using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FrotaGrupoItemControleItemMap : EntityTypeConfiguration<FrotaGrupoItemControleItem>
    {
        public FrotaGrupoItemControleItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IDFrotaGrupoItemControleItem);

            // Properties
            this.Property(t => t.IDFrotaGrupoItemControleItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FrotaGrupoItemControleItem");
            this.Property(t => t.IDFrotaGrupoItemControleItem).HasColumnName("IDFrotaGrupoItemControleItem");
            this.Property(t => t.IDFrotaGrupoItemControle).HasColumnName("IDFrotaGrupoItemControle");
            this.Property(t => t.IDFrota).HasColumnName("IDFrota");

            // Relationships
            this.HasRequired(t => t.Frota)
                .WithMany(t => t.FrotaGrupoItemControleItems)
                .HasForeignKey(d => d.IDFrota);
            this.HasRequired(t => t.FrotaGrupoItemControle)
                .WithMany(t => t.FrotaGrupoItemControleItems)
                .HasForeignKey(d => d.IDFrotaGrupoItemControle);

        }
    }
}
