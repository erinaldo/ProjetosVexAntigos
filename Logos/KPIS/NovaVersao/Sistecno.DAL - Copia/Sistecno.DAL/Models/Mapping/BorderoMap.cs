using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BorderoMap : EntityTypeConfiguration<Bordero>
    {
        public BorderoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBordero);

            // Properties
            this.Property(t => t.IDBordero)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(12);

            this.Property(t => t.Descricao)
                .HasMaxLength(50);

            this.Property(t => t.Modalidade)
                .HasMaxLength(50);

            this.Property(t => t.TipoDeAgendamento)
                .HasMaxLength(50);

            this.Property(t => t.TipoDeMovimento)
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Bordero");
            this.Property(t => t.IDBordero).HasColumnName("IDBordero");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.IdBancoConta).HasColumnName("IdBancoConta");
            this.Property(t => t.ValorTotal).HasColumnName("ValorTotal");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Modalidade).HasColumnName("Modalidade");
            this.Property(t => t.TipoDeAgendamento).HasColumnName("TipoDeAgendamento");
            this.Property(t => t.TipoDeMovimento).HasColumnName("TipoDeMovimento");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Borderoes)
                .HasForeignKey(d => d.IDFilial);

        }
    }
}
