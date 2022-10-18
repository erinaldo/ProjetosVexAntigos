using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProjetoItemMap : EntityTypeConfiguration<ProjetoItem>
    {
        public ProjetoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IdProjetoItem);

            // Properties
            this.Property(t => t.IdProjetoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Descricao)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ProjetoItem");
            this.Property(t => t.IdProjetoItem).HasColumnName("IdProjetoItem");
            this.Property(t => t.IdProjeto).HasColumnName("IdProjeto");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.QuantidadeRecebida).HasColumnName("QuantidadeRecebida");
            this.Property(t => t.UltimoRecebimento).HasColumnName("UltimoRecebimento");

            // Relationships
            this.HasRequired(t => t.Projeto)
                .WithMany(t => t.ProjetoItems)
                .HasForeignKey(d => d.IdProjeto);

        }
    }
}
