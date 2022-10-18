using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BancoEspecieMap : EntityTypeConfiguration<BancoEspecie>
    {
        public BancoEspecieMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBancoEspecie);

            // Properties
            this.Property(t => t.IDBancoEspecie)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            this.Property(t => t.sigla)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("BancoEspecie");
            this.Property(t => t.IDBancoEspecie).HasColumnName("IDBancoEspecie");
            this.Property(t => t.IDBanco).HasColumnName("IDBanco");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.sigla).HasColumnName("sigla");

            // Relationships
            this.HasRequired(t => t.Banco)
                .WithMany(t => t.BancoEspecies)
                .HasForeignKey(d => d.IDBanco);

        }
    }
}
