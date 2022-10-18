using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProdutoClienteAvariaMap : EntityTypeConfiguration<ProdutoClienteAvaria>
    {
        public ProdutoClienteAvariaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdProdutoClienteAvaria);

            // Properties
            this.Property(t => t.IdProdutoClienteAvaria)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TipoDeAvaria)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ProdutoClienteAvaria");
            this.Property(t => t.IdProdutoClienteAvaria).HasColumnName("IdProdutoClienteAvaria");
            this.Property(t => t.IdProdutoCliente).HasColumnName("IdProdutoCliente");
            this.Property(t => t.Unidades).HasColumnName("Unidades");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.TipoDeAvaria).HasColumnName("TipoDeAvaria");

            // Relationships
            this.HasRequired(t => t.ProdutoCliente)
                .WithMany(t => t.ProdutoClienteAvarias)
                .HasForeignKey(d => d.IdProdutoCliente);

        }
    }
}
