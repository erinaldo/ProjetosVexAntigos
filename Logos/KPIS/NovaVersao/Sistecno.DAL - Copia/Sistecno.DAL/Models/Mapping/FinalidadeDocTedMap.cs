using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FinalidadeDocTedMap : EntityTypeConfiguration<FinalidadeDocTed>
    {
        public FinalidadeDocTedMap()
        {
            // Primary Key
            this.HasKey(t => t.IdfinalidadeDocTed);

            // Properties
            this.Property(t => t.IdfinalidadeDocTed)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .HasMaxLength(100);

            this.Property(t => t.Codigo)
                .HasMaxLength(3);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("FinalidadeDocTed");
            this.Property(t => t.IdfinalidadeDocTed).HasColumnName("IdfinalidadeDocTed");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
        }
    }
}
