using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PortariaMap : EntityTypeConfiguration<Portaria>
    {
        public PortariaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDPortaria);

            // Properties
            this.Property(t => t.IDPortaria)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Placa)
                .HasMaxLength(8);

            this.Property(t => t.Situacao)
                .HasMaxLength(50);

            this.Property(t => t.Transportadora)
                .HasMaxLength(100);

            this.Property(t => t.Cliente)
                .HasMaxLength(100);

            this.Property(t => t.Motorista)
                .HasMaxLength(100);

            this.Property(t => t.RGMotorista)
                .HasMaxLength(30);

            this.Property(t => t.Ajudante)
                .HasMaxLength(100);

            this.Property(t => t.RGAjudante)
                .HasMaxLength(100);

            this.Property(t => t.Nome)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Portaria");
            this.Property(t => t.IDPortaria).HasColumnName("IDPortaria");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IdDt).HasColumnName("IdDt");
            this.Property(t => t.IDTransportadora).HasColumnName("IDTransportadora");
            this.Property(t => t.IDVeiculo).HasColumnName("IDVeiculo");
            this.Property(t => t.IdAgendamento).HasColumnName("IdAgendamento");
            this.Property(t => t.Placa).HasColumnName("Placa");
            this.Property(t => t.DataHoraDeEntrada).HasColumnName("DataHoraDeEntrada");
            this.Property(t => t.DataHoraDeSaida).HasColumnName("DataHoraDeSaida");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Transportadora).HasColumnName("Transportadora");
            this.Property(t => t.Cliente).HasColumnName("Cliente");
            this.Property(t => t.Motorista).HasColumnName("Motorista");
            this.Property(t => t.RGMotorista).HasColumnName("RGMotorista");
            this.Property(t => t.Ajudante).HasColumnName("Ajudante");
            this.Property(t => t.RGAjudante).HasColumnName("RGAjudante");
            this.Property(t => t.Nome).HasColumnName("Nome");

            // Relationships
            this.HasOptional(t => t.Agendamento)
                .WithMany(t => t.Portarias)
                .HasForeignKey(d => d.IdAgendamento);
            this.HasOptional(t => t.DT)
                .WithMany(t => t.Portarias)
                .HasForeignKey(d => d.IdDt);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Portarias)
                .HasForeignKey(d => d.IDFilial);
            this.HasOptional(t => t.Veiculo)
                .WithMany(t => t.Portarias)
                .HasForeignKey(d => d.IDVeiculo);

        }
    }
}
