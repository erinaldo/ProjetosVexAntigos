using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RastreadorMap : EntityTypeConfiguration<Rastreador>
    {
        public RastreadorMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRastreador);

            // Properties
            this.Property(t => t.IdRastreador)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Chave)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Nome)
                .HasMaxLength(100);

            this.Property(t => t.EnviaPosicaoZerada)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.EnviaFotos)
                .HasMaxLength(10);

            this.Property(t => t.NumeroChip)
                .HasMaxLength(100);

            this.Property(t => t.EnviaFoto)
                .HasMaxLength(1);

            this.Property(t => t.UltimaPlaca)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("Rastreador");
            this.Property(t => t.IdRastreador).HasColumnName("IdRastreador");
            this.Property(t => t.Chave).HasColumnName("Chave");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Tempo).HasColumnName("Tempo");
            this.Property(t => t.EnviaPosicaoZerada).HasColumnName("EnviaPosicaoZerada");
            this.Property(t => t.EnviaFotos).HasColumnName("EnviaFotos");
            this.Property(t => t.NumeroChip).HasColumnName("NumeroChip");
            this.Property(t => t.EnviaFoto).HasColumnName("EnviaFoto");
            this.Property(t => t.UltimaSincronizacao).HasColumnName("UltimaSincronizacao");
            this.Property(t => t.UltimaDT).HasColumnName("UltimaDT");
            this.Property(t => t.UltimaPlaca).HasColumnName("UltimaPlaca");
            this.Property(t => t.InicioSincronizacao).HasColumnName("InicioSincronizacao");
            this.Property(t => t.FinalSincronizacao).HasColumnName("FinalSincronizacao");
            this.Property(t => t.TempoSincronizacao).HasColumnName("TempoSincronizacao");
            this.Property(t => t.UtltimoEnvioDeDados).HasColumnName("UtltimoEnvioDeDados");
        }
    }
}
