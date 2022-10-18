using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ChequeMap : EntityTypeConfiguration<Cheque>
    {
        public ChequeMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCheque);

            // Properties
            this.Property(t => t.IDCheque)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Numero)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Serie)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Situacao)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Portador)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.PagarReceber)
                .IsRequired()
                .HasMaxLength(7);

            // Table & Column Mappings
            this.ToTable("Cheque");
            this.Property(t => t.IDCheque).HasColumnName("IDCheque");
            this.Property(t => t.IDBancoConta).HasColumnName("IDBancoConta");
            this.Property(t => t.IDBanco).HasColumnName("IDBanco");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Serie).HasColumnName("Serie");
            this.Property(t => t.DataDeEmissao).HasColumnName("DataDeEmissao");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Portador).HasColumnName("Portador");
            this.Property(t => t.DataDeDisponibilidade).HasColumnName("DataDeDisponibilidade");
            this.Property(t => t.PagarReceber).HasColumnName("PagarReceber");

            // Relationships
            this.HasRequired(t => t.Banco)
                .WithMany(t => t.Cheques)
                .HasForeignKey(d => d.IDBanco);
            this.HasOptional(t => t.BancoConta)
                .WithMany(t => t.Cheques)
                .HasForeignKey(d => d.IDBancoConta);

        }
    }
}
