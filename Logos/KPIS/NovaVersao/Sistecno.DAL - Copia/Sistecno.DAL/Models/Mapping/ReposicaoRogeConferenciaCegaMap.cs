using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ReposicaoRogeConferenciaCegaMap : EntityTypeConfiguration<ReposicaoRogeConferenciaCega>
    {
        public ReposicaoRogeConferenciaCegaMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CodigoRoge)
                .HasMaxLength(50);

            this.Property(t => t.CodigoDeBarrasLido)
                .HasMaxLength(50);

            this.Property(t => t.PertenceANota)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ReposicaoRogeConferenciaCega");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CodigoRoge).HasColumnName("CodigoRoge");
            this.Property(t => t.IdConferenciaItem).HasColumnName("IdConferenciaItem");
            this.Property(t => t.CodigoDeBarrasLido).HasColumnName("CodigoDeBarrasLido");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.PertenceANota).HasColumnName("PertenceANota");
        }
    }
}
