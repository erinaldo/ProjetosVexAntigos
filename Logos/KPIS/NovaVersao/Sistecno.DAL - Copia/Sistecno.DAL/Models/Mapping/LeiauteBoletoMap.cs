using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LeiauteBoletoMap : EntityTypeConfiguration<LeiauteBoleto>
    {
        public LeiauteBoletoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDLeiauteBoleto);

            // Properties
            this.Property(t => t.IDLeiauteBoleto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("LeiauteBoleto");
            this.Property(t => t.IDLeiauteBoleto).HasColumnName("IDLeiauteBoleto");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
