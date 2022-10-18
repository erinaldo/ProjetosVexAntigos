using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LeiauteChequeMap : EntityTypeConfiguration<LeiauteCheque>
    {
        public LeiauteChequeMap()
        {
            // Primary Key
            this.HasKey(t => t.IDLeiauteCheque);

            // Properties
            this.Property(t => t.IDLeiauteCheque)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("LeiauteCheque");
            this.Property(t => t.IDLeiauteCheque).HasColumnName("IDLeiauteCheque");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
