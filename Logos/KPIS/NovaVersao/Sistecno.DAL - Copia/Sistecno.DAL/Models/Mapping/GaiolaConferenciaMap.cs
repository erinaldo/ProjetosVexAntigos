using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class GaiolaConferenciaMap : EntityTypeConfiguration<GaiolaConferencia>
    {
        public GaiolaConferenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdGaiolaConferencia);

            // Properties
            this.Property(t => t.IdGaiolaConferencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodigoDeBarras)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Roteiro)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.PertenceAFilial)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.Situacao)
                .HasMaxLength(100);

            this.Property(t => t.NumeroColetor)
                .HasMaxLength(30);

            this.Property(t => t.Emei)
                .HasMaxLength(100);

            this.Property(t => t.Ativo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("GaiolaConferencia");
            this.Property(t => t.IdGaiolaConferencia).HasColumnName("IdGaiolaConferencia");
            this.Property(t => t.IdGaiola).HasColumnName("IdGaiola");
            this.Property(t => t.CodigoDeBarras).HasColumnName("CodigoDeBarras");
            this.Property(t => t.Roteiro).HasColumnName("Roteiro");
            this.Property(t => t.PertenceAFilial).HasColumnName("PertenceAFilial");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.NumeroColetor).HasColumnName("NumeroColetor");
            this.Property(t => t.Emei).HasColumnName("Emei");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasRequired(t => t.Gaiola)
                .WithMany(t => t.GaiolaConferencias)
                .HasForeignKey(d => d.IdGaiola);

        }
    }
}
