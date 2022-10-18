using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ExtratoParaConciliacaoMap : EntityTypeConfiguration<ExtratoParaConciliacao>
    {
        public ExtratoParaConciliacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdExtratoParaConciliacao);

            // Properties
            this.Property(t => t.IdExtratoParaConciliacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Id)
                .HasMaxLength(50);

            this.Property(t => t.Documento)
                .HasMaxLength(50);

            this.Property(t => t.Descricao)
                .HasMaxLength(100);

            this.Property(t => t.TipoMovimento)
                .HasMaxLength(50);

            this.Property(t => t.AccountID)
                .HasMaxLength(50);

            this.Property(t => t.AccountType)
                .HasMaxLength(50);

            this.Property(t => t.Conciliado)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ExtratoParaConciliacao");
            this.Property(t => t.IdExtratoParaConciliacao).HasColumnName("IdExtratoParaConciliacao");
            this.Property(t => t.IdContaContabilFilial).HasColumnName("IdContaContabilFilial");
            this.Property(t => t.Processamento).HasColumnName("Processamento");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Documento).HasColumnName("Documento");
            this.Property(t => t.DataMovimento).HasColumnName("DataMovimento");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.Entrada).HasColumnName("Entrada");
            this.Property(t => t.Saida).HasColumnName("Saida");
            this.Property(t => t.TipoMovimento).HasColumnName("TipoMovimento");
            this.Property(t => t.BankID).HasColumnName("BankID");
            this.Property(t => t.AccountID).HasColumnName("AccountID");
            this.Property(t => t.AccountType).HasColumnName("AccountType");
            this.Property(t => t.InitialBalance).HasColumnName("InitialBalance");
            this.Property(t => t.FinalBalance).HasColumnName("FinalBalance");
            this.Property(t => t.Conciliado).HasColumnName("Conciliado");
            this.Property(t => t.CodigoDeConciliacao).HasColumnName("CodigoDeConciliacao");
        }
    }
}
