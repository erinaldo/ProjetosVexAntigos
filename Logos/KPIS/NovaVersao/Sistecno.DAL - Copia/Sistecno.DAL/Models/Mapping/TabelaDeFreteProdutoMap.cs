using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TabelaDeFreteProdutoMap : EntityTypeConfiguration<TabelaDeFreteProduto>
    {
        public TabelaDeFreteProdutoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTabelaDeFreteProduto);

            // Properties
            this.Property(t => t.IdTabelaDeFreteProduto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Acesso)
                .HasMaxLength(30);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("TabelaDeFreteProduto");
            this.Property(t => t.IdTabelaDeFreteProduto).HasColumnName("IdTabelaDeFreteProduto");
            this.Property(t => t.IdTabelaDeFrete).HasColumnName("IdTabelaDeFrete");
            this.Property(t => t.Acesso).HasColumnName("Acesso");
            this.Property(t => t.Nome).HasColumnName("Nome");

            // Relationships
            this.HasRequired(t => t.TabelaDeFrete)
                .WithMany(t => t.TabelaDeFreteProdutoes)
                .HasForeignKey(d => d.IdTabelaDeFrete);

        }
    }
}
