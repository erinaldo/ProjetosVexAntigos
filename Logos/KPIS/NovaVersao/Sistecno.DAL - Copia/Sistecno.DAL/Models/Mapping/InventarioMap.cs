using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class InventarioMap : EntityTypeConfiguration<Inventario>
    {
        public InventarioMap()
        {
            // Primary Key
            this.HasKey(t => t.IdInventario);

            // Properties
            this.Property(t => t.IdInventario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Situacao)
                .HasMaxLength(30);

            this.Property(t => t.Descricao)
                .HasMaxLength(100);

            this.Property(t => t.TipoDeInventario)
                .HasMaxLength(15);

            this.Property(t => t.SolicitarUA)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Inventario");
            this.Property(t => t.IdInventario).HasColumnName("IdInventario");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.TipoDeInventario).HasColumnName("TipoDeInventario");
            this.Property(t => t.SolicitarUA).HasColumnName("SolicitarUA");
            this.Property(t => t.PosicoesContadas).HasColumnName("PosicoesContadas");
            this.Property(t => t.PosicoesCorretas).HasColumnName("PosicoesCorretas");
            this.Property(t => t.PosicoesContadasABC).HasColumnName("PosicoesContadasABC");
            this.Property(t => t.PosicoesCorretasABC).HasColumnName("PosicoesCorretasABC");
            this.Property(t => t.SKUCorretos).HasColumnName("SKUCorretos");
            this.Property(t => t.SKUTotal).HasColumnName("SKUTotal");
            this.Property(t => t.IdCda).HasColumnName("IdCda");

            // Relationships
            this.HasOptional(t => t.Cadastro)
                .WithMany(t => t.Inventarios)
                .HasForeignKey(d => d.IdCda);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Inventarios)
                .HasForeignKey(d => d.IdFilial);

        }
    }
}
