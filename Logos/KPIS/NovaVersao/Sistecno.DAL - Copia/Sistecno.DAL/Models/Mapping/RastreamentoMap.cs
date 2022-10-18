using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RastreamentoMap : EntityTypeConfiguration<Rastreamento>
    {
        public RastreamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRastreamento);

            // Properties
            this.Property(t => t.IdRastreamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PontodeOcorrencia)
                .HasMaxLength(3);

            this.Property(t => t.LATI)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LONGI)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Rastreamento");
            this.Property(t => t.IdRastreamento).HasColumnName("IdRastreamento");
            this.Property(t => t.IdRastreador).HasColumnName("IdRastreador");
            this.Property(t => t.IdDt).HasColumnName("IdDt");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Satelites).HasColumnName("Satelites");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
            this.Property(t => t.PontodeOcorrencia).HasColumnName("PontodeOcorrencia");
            this.Property(t => t.LATI).HasColumnName("LATI");
            this.Property(t => t.LONGI).HasColumnName("LONGI");
            this.Property(t => t.DataHoraTransmissao).HasColumnName("DataHoraTransmissao");

            // Relationships
            this.HasOptional(t => t.DT)
                .WithMany(t => t.Rastreamentoes)
                .HasForeignKey(d => d.IdDt);
            this.HasRequired(t => t.Rastreador)
                .WithMany(t => t.Rastreamentoes)
                .HasForeignKey(d => d.IdRastreador);

        }
    }
}
