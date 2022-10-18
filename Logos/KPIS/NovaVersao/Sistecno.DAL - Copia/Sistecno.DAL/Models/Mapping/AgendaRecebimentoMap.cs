using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AgendaRecebimentoMap : EntityTypeConfiguration<AgendaRecebimento>
    {
        public AgendaRecebimentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAgendaRecebimento);

            // Properties
            this.Property(t => t.IdAgendaRecebimento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Hora)
                .HasMaxLength(5);

            this.Property(t => t.Disponivel)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("AgendaRecebimento");
            this.Property(t => t.IdAgendaRecebimento).HasColumnName("IdAgendaRecebimento");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.IdDepositoPlantaLocalizacao).HasColumnName("IdDepositoPlantaLocalizacao");
            this.Property(t => t.IdAgendamento).HasColumnName("IdAgendamento");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Hora).HasColumnName("Hora");
            this.Property(t => t.Disponivel).HasColumnName("Disponivel");

            // Relationships
            this.HasOptional(t => t.Agendamento)
                .WithMany(t => t.AgendaRecebimentoes)
                .HasForeignKey(d => d.IdAgendamento);
            this.HasRequired(t => t.DepositoPlantaLocalizacao)
                .WithMany(t => t.AgendaRecebimentoes)
                .HasForeignKey(d => d.IdDepositoPlantaLocalizacao);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.AgendaRecebimentoes)
                .HasForeignKey(d => d.IdFilial);

        }
    }
}
