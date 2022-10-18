using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDITransferenciaMap : EntityTypeConfiguration<EDITransferencia>
    {
        public EDITransferenciaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDTransferencia, t.IDDestino, t.ChaveOrigem, t.Tabela, t.Finalizado });

            // Properties
            this.Property(t => t.IDTransferencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDDestino)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChaveOrigem)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Tabela)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Finalizado)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("EDITransferencia");
            this.Property(t => t.IDTransferencia).HasColumnName("IDTransferencia");
            this.Property(t => t.IDDestino).HasColumnName("IDDestino");
            this.Property(t => t.ChaveOrigem).HasColumnName("ChaveOrigem");
            this.Property(t => t.Tabela).HasColumnName("Tabela");
            this.Property(t => t.Finalizado).HasColumnName("Finalizado");
        }
    }
}
