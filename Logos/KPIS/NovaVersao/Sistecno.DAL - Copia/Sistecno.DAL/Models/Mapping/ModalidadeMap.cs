using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ModalidadeMap : EntityTypeConfiguration<Modalidade>
    {
        public ModalidadeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDModalidade, t.IDModal, t.Nome });

            // Properties
            this.Property(t => t.IDModalidade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDModal)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Modalidade");
            this.Property(t => t.IDModalidade).HasColumnName("IDModalidade");
            this.Property(t => t.IDModal).HasColumnName("IDModal");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
