using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EstoqueOperacaoMap : EntityTypeConfiguration<EstoqueOperacao>
    {
        public EstoqueOperacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEstoqueOperacao);

            // Properties
            this.Property(t => t.IDEstoqueOperacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.Sistema)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("EstoqueOperacao");
            this.Property(t => t.IDEstoqueOperacao).HasColumnName("IDEstoqueOperacao");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Sistema).HasColumnName("Sistema");
        }
    }
}
