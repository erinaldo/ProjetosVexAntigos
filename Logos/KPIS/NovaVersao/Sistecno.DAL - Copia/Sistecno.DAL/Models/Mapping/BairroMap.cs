using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BairroMap : EntityTypeConfiguration<Bairro>
    {
        public BairroMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBairro);

            // Properties
            this.Property(t => t.IDBairro)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.Origem)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Bairro");
            this.Property(t => t.IDBairro).HasColumnName("IDBairro");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Origem).HasColumnName("Origem");

            // Relationships
            this.HasRequired(t => t.Cidade)
                .WithMany(t => t.Bairroes)
                .HasForeignKey(d => d.IDCidade);

        }
    }
}
