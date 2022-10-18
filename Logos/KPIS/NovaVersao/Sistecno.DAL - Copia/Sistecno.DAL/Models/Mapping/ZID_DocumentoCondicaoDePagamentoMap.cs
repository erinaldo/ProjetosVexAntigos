using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ZID_DocumentoCondicaoDePagamentoMap : EntityTypeConfiguration<ZID_DocumentoCondicaoDePagamento>
    {
        public ZID_DocumentoCondicaoDePagamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ZID_DocumentoCondicaoDePagamento");
            this.Property(t => t.ID).HasColumnName("ID");
        }
    }
}
