using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CondicaoDePagamentoItemMap : EntityTypeConfiguration<CondicaoDePagamentoItem>
    {
        public CondicaoDePagamentoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCondicaoDePagamentoItem);

            // Properties
            this.Property(t => t.IdCondicaoDePagamentoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CondicaoDePagamentoItem");
            this.Property(t => t.IdCondicaoDePagamentoItem).HasColumnName("IdCondicaoDePagamentoItem");
            this.Property(t => t.IdCondicaoDePagamento).HasColumnName("IdCondicaoDePagamento");
            this.Property(t => t.QtdeDias).HasColumnName("QtdeDias");
            this.Property(t => t.Percentual).HasColumnName("Percentual");
        }
    }
}
