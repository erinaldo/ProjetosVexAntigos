using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDILayOutMap : EntityTypeConfiguration<EDILayOut>
    {
        public EDILayOutMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEDILayOut);

            // Properties
            this.Property(t => t.IDEDILayOut)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(7);

            this.Property(t => t.Versao)
                .HasMaxLength(5);

            this.Property(t => t.Revisao)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("EDILayOut");
            this.Property(t => t.IDEDILayOut).HasColumnName("IDEDILayOut");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Versao).HasColumnName("Versao");
            this.Property(t => t.Revisao).HasColumnName("Revisao");
        }
    }
}
