using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MovimentacaoBancariaMap : EntityTypeConfiguration<MovimentacaoBancaria>
    {
        public MovimentacaoBancariaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMovimentacaoBancaria);

            // Properties
            this.Property(t => t.IDMovimentacaoBancaria)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.DebitoCredito)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.origem)
                .HasMaxLength(50);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(13);

            this.Property(t => t.Documento)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("MovimentacaoBancaria");
            this.Property(t => t.IDMovimentacaoBancaria).HasColumnName("IDMovimentacaoBancaria");
            this.Property(t => t.IDBancoConta).HasColumnName("IDBancoConta");
            this.Property(t => t.IDTituloDuplicata).HasColumnName("IDTituloDuplicata");
            this.Property(t => t.IDCheque).HasColumnName("IDCheque");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.DebitoCredito).HasColumnName("DebitoCredito");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.DataDisponibilidade).HasColumnName("DataDisponibilidade");
            this.Property(t => t.IDContaOrigem).HasColumnName("IDContaOrigem");
            this.Property(t => t.IDContaDestino).HasColumnName("IDContaDestino");
            this.Property(t => t.origem).HasColumnName("origem");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Documento).HasColumnName("Documento");

            // Relationships
            this.HasOptional(t => t.BancoConta)
                .WithMany(t => t.MovimentacaoBancarias)
                .HasForeignKey(d => d.IDBancoConta);
            this.HasOptional(t => t.BancoConta1)
                .WithMany(t => t.MovimentacaoBancarias1)
                .HasForeignKey(d => d.IDContaDestino);
            this.HasOptional(t => t.BancoConta2)
                .WithMany(t => t.MovimentacaoBancarias2)
                .HasForeignKey(d => d.IDContaOrigem);
            this.HasOptional(t => t.Cheque)
                .WithMany(t => t.MovimentacaoBancarias)
                .HasForeignKey(d => d.IDCheque);
            this.HasOptional(t => t.Filial)
                .WithMany(t => t.MovimentacaoBancarias)
                .HasForeignKey(d => d.IDFilial);
            this.HasOptional(t => t.TituloDuplicata)
                .WithMany(t => t.MovimentacaoBancarias)
                .HasForeignKey(d => d.IDTituloDuplicata);

        }
    }
}
