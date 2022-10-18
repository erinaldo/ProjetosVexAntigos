using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EdiPlanilhaMap : EntityTypeConfiguration<EdiPlanilha>
    {
        public EdiPlanilhaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdEdiPlanilha);

            // Properties
            this.Property(t => t.IdEdiPlanilha)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Arquivo)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("EdiPlanilha");
            this.Property(t => t.IdEdiPlanilha).HasColumnName("IdEdiPlanilha");
            this.Property(t => t.Arquivo).HasColumnName("Arquivo");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.UltimaAtualizacao).HasColumnName("UltimaAtualizacao");
        }
    }
}
