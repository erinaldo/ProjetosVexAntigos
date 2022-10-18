using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class SetorMap : EntityTypeConfiguration<Setor>
    {
        public SetorMap()
        {
            // Primary Key
            this.HasKey(t => t.IDSetor);

            // Properties
            this.Property(t => t.IDSetor)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Setor");
            this.Property(t => t.IDSetor).HasColumnName("IDSetor");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.IdBairro).HasColumnName("IdBairro");
        }
    }
}
