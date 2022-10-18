using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ModalidadeDocTedMap : EntityTypeConfiguration<ModalidadeDocTed>
    {
        public ModalidadeDocTedMap()
        {
            // Primary Key
            this.HasKey(t => t.IdModalidadeDocTed);

            // Properties
            this.Property(t => t.IdModalidadeDocTed)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(100);

            this.Property(t => t.Codigo)
                .HasMaxLength(3);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ModalidadeDocTed");
            this.Property(t => t.IdModalidadeDocTed).HasColumnName("IdModalidadeDocTed");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
        }
    }
}
