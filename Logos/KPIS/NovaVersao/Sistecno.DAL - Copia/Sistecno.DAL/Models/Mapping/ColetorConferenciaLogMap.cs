using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ColetorConferenciaLogMap : EntityTypeConfiguration<ColetorConferenciaLog>
    {
        public ColetorConferenciaLogMap()
        {
            // Primary Key
            this.HasKey(t => t.IdColetorConferenciaLog);

            // Properties
            this.Property(t => t.IdColetorConferenciaLog)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CodigoRetorno)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("ColetorConferenciaLog");
            this.Property(t => t.IdColetorConferenciaLog).HasColumnName("IdColetorConferenciaLog");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.VolumesFaltantes).HasColumnName("VolumesFaltantes");
            this.Property(t => t.CodigoRetorno).HasColumnName("CodigoRetorno");
            this.Property(t => t.DescricaoRetorno).HasColumnName("DescricaoRetorno");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.ColetorConferenciaLogs)
                .HasForeignKey(d => d.IdDocumento);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.ColetorConferenciaLogs)
                .HasForeignKey(d => d.IdFilial);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.ColetorConferenciaLogs)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
