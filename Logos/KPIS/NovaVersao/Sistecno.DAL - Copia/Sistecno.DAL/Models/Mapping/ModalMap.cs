using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ModalMap : EntityTypeConfiguration<Modal>
    {
        public ModalMap()
        {
            // Primary Key
            this.HasKey(t => t.IDModal);

            // Properties
            this.Property(t => t.IDModal)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Cte)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Modal");
            this.Property(t => t.IDModal).HasColumnName("IDModal");
            this.Property(t => t.IDEmpresa).HasColumnName("IDEmpresa");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Cte).HasColumnName("Cte");

            // Relationships
            this.HasOptional(t => t.Empresa)
                .WithMany(t => t.Modals)
                .HasForeignKey(d => d.IDEmpresa);

        }
    }
}
