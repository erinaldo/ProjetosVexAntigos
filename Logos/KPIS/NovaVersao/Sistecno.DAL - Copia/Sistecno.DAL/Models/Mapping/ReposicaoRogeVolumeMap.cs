using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ReposicaoRogeVolumeMap : EntityTypeConfiguration<ReposicaoRogeVolume>
    {
        public ReposicaoRogeVolumeMap()
        {
            // Primary Key
            this.HasKey(t => t.IdReposicaoRogeVolume);

            // Properties
            this.Property(t => t.IdReposicaoRogeVolume)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodigoDeBarras)
                .HasMaxLength(50);

            this.Property(t => t.Conferido)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ReposicaoRogeVolume");
            this.Property(t => t.IdReposicaoRogeVolume).HasColumnName("IdReposicaoRogeVolume");
            this.Property(t => t.IdResposicaoRoge).HasColumnName("IdResposicaoRoge");
            this.Property(t => t.CodigoDeBarras).HasColumnName("CodigoDeBarras");
            this.Property(t => t.Conferido).HasColumnName("Conferido");
            this.Property(t => t.DataDaInclusao).HasColumnName("DataDaInclusao");
            this.Property(t => t.DataConferido).HasColumnName("DataConferido");

            // Relationships
            this.HasOptional(t => t.ReposicaoRoge)
                .WithMany(t => t.ReposicaoRogeVolumes)
                .HasForeignKey(d => d.IdResposicaoRoge);

        }
    }
}
