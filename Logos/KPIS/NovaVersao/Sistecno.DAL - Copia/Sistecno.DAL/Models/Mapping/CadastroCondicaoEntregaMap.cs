using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroCondicaoEntregaMap : EntityTypeConfiguration<CadastroCondicaoEntrega>
    {
        public CadastroCondicaoEntregaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroCondicaoEntrega);

            // Properties
            this.Property(t => t.IDCadastroCondicaoEntrega)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RecebeSabado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.RecebeDomingo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.HorarioRecebimentoInicial)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.HorarioRecebimentoFinal)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.ColetaSabado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ColetaDomingo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.HorarioColetaInicial)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.HorarioColetaFinal)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.TempoMedioDeRecebimento)
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.TempoMedioDeColeta)
                .IsFixedLength()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CadastroCondicaoEntrega");
            this.Property(t => t.IDCadastroCondicaoEntrega).HasColumnName("IDCadastroCondicaoEntrega");
            this.Property(t => t.RecebeSabado).HasColumnName("RecebeSabado");
            this.Property(t => t.RecebeDomingo).HasColumnName("RecebeDomingo");
            this.Property(t => t.HorarioRecebimentoInicial).HasColumnName("HorarioRecebimentoInicial");
            this.Property(t => t.HorarioRecebimentoFinal).HasColumnName("HorarioRecebimentoFinal");
            this.Property(t => t.ColetaSabado).HasColumnName("ColetaSabado");
            this.Property(t => t.ColetaDomingo).HasColumnName("ColetaDomingo");
            this.Property(t => t.HorarioColetaInicial).HasColumnName("HorarioColetaInicial");
            this.Property(t => t.HorarioColetaFinal).HasColumnName("HorarioColetaFinal");
            this.Property(t => t.TempoMedioDeRecebimento).HasColumnName("TempoMedioDeRecebimento");
            this.Property(t => t.TempoMedioDeColeta).HasColumnName("TempoMedioDeColeta");
            this.Property(t => t.Historico).HasColumnName("Historico");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithOptional(t => t.CadastroCondicaoEntrega);

        }
    }
}
