using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContaDespesaEventoMap : EntityTypeConfiguration<ContaDespesaEvento>
    {
        public ContaDespesaEventoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContaDespesaEvento);

            // Properties
            this.Property(t => t.IdContaDespesaEvento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.TipoDeDado)
                .HasMaxLength(10);

            this.Property(t => t.Comentario)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("ContaDespesaEvento");
            this.Property(t => t.IdContaDespesaEvento).HasColumnName("IdContaDespesaEvento");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.TipoDeDado).HasColumnName("TipoDeDado");
            this.Property(t => t.Comentario).HasColumnName("Comentario");
        }
    }
}
