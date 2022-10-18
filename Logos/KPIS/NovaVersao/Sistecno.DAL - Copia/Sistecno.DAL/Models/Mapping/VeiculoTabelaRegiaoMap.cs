using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoTabelaRegiaoMap : EntityTypeConfiguration<VeiculoTabelaRegiao>
    {
        public VeiculoTabelaRegiaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdVeiculoTabelaRegiao);

            // Properties
            this.Property(t => t.IdVeiculoTabelaRegiao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VeiculoTabelaRegiao");
            this.Property(t => t.IdVeiculoTabelaRegiao).HasColumnName("IdVeiculoTabelaRegiao");
            this.Property(t => t.IdVeiculoTabela).HasColumnName("IdVeiculoTabela");
            this.Property(t => t.IdRegiao).HasColumnName("IdRegiao");
            this.Property(t => t.Cadastro).HasColumnName("Cadastro");

            // Relationships
            this.HasRequired(t => t.Regiao)
                .WithMany(t => t.VeiculoTabelaRegiaos)
                .HasForeignKey(d => d.IdRegiao);
            this.HasRequired(t => t.VeiculoTabela)
                .WithMany(t => t.VeiculoTabelaRegiaos)
                .HasForeignKey(d => d.IdVeiculoTabela);

        }
    }
}
