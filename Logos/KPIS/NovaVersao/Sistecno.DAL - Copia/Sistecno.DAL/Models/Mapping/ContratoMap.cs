using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContratoMap : EntityTypeConfiguration<Contrato>
    {
        public ContratoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContrato);

            // Properties
            this.Property(t => t.IdContrato)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClienteFornecedor)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Numero)
                .HasMaxLength(50);

            this.Property(t => t.Referencia)
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ValorReal)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Contrato");
            this.Property(t => t.IdContrato).HasColumnName("IdContrato");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.IdFornecedor).HasColumnName("IdFornecedor");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.IdUsuarioCadastro).HasColumnName("IdUsuarioCadastro");
            this.Property(t => t.ClienteFornecedor).HasColumnName("ClienteFornecedor");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Referencia).HasColumnName("Referencia");
            this.Property(t => t.Parcelas).HasColumnName("Parcelas");
            this.Property(t => t.ValorTotal).HasColumnName("ValorTotal");
            this.Property(t => t.PrimeiroVencimento).HasColumnName("PrimeiroVencimento");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.ValorReal).HasColumnName("ValorReal");
        }
    }
}
