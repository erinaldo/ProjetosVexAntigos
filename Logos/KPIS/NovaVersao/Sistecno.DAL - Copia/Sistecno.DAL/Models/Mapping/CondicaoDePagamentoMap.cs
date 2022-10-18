using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CondicaoDePagamentoMap : EntityTypeConfiguration<CondicaoDePagamento>
    {
        public CondicaoDePagamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCondicaoDePagamento);

            // Properties
            this.Property(t => t.IDCondicaoDePagamento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Condicao)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CondicaoDePagamento");
            this.Property(t => t.IDCondicaoDePagamento).HasColumnName("IDCondicaoDePagamento");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Condicao).HasColumnName("Condicao");
        }
    }
}
