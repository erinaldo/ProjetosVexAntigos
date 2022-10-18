using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ModalidadeFinalidadeDocTedMap : EntityTypeConfiguration<ModalidadeFinalidadeDocTed>
    {
        public ModalidadeFinalidadeDocTedMap()
        {
            // Primary Key
            this.HasKey(t => t.IdModalidadeFinalidadeDocTed);

            // Properties
            this.Property(t => t.IdModalidadeFinalidadeDocTed)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ModalidadeFinalidadeDocTed");
            this.Property(t => t.IdModalidadeFinalidadeDocTed).HasColumnName("IdModalidadeFinalidadeDocTed");
            this.Property(t => t.IdModalidadeDocTed).HasColumnName("IdModalidadeDocTed");
            this.Property(t => t.IdFinalidadeDocTed).HasColumnName("IdFinalidadeDocTed");
            this.Property(t => t.IdModalidadeDePagamento).HasColumnName("IdModalidadeDePagamento");
        }
    }
}
