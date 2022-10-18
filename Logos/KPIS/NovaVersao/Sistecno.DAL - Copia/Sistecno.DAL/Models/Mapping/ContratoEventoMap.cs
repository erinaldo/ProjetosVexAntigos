using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ContratoEventoMap : EntityTypeConfiguration<ContratoEvento>
    {
        public ContratoEventoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdContratoEvento);

            // Properties
            this.Property(t => t.IdContratoEvento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.TipoDeDado)
                .HasMaxLength(10);

            this.Property(t => t.Comentario)
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("ContratoEvento");
            this.Property(t => t.IdContratoEvento).HasColumnName("IdContratoEvento");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.TipoDeDado).HasColumnName("TipoDeDado");
            this.Property(t => t.Comentario).HasColumnName("Comentario");
        }
    }
}
