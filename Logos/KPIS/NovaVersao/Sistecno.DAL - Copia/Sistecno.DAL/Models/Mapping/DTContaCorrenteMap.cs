using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DTContaCorrenteMap : EntityTypeConfiguration<DTContaCorrente>
    {
        public DTContaCorrenteMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDtContaCorrente);

            // Properties
            this.Property(t => t.IdDtContaCorrente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Historico)
                .HasMaxLength(200);

            this.Property(t => t.Status)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("DTContaCorrente");
            this.Property(t => t.IdDtContaCorrente).HasColumnName("IdDtContaCorrente");
            this.Property(t => t.IdDT).HasColumnName("IdDT");
            this.Property(t => t.DataDoLancamento).HasColumnName("DataDoLancamento");
            this.Property(t => t.DataDoEvento).HasColumnName("DataDoEvento");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.IdOcorrencia).HasColumnName("IdOcorrencia");
            this.Property(t => t.Historico).HasColumnName("Historico");
            this.Property(t => t.Debito).HasColumnName("Debito");
            this.Property(t => t.Credito).HasColumnName("Credito");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.IdDTLancamento).HasColumnName("IdDTLancamento");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");

            // Relationships
            this.HasRequired(t => t.DT)
                .WithMany(t => t.DTContaCorrentes)
                .HasForeignKey(d => d.IdDT);

        }
    }
}
