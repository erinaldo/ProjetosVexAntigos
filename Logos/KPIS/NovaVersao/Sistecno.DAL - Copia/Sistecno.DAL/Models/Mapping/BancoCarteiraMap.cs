using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BancoCarteiraMap : EntityTypeConfiguration<BancoCarteira>
    {
        public BancoCarteiraMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBancoCarteira);

            // Properties
            this.Property(t => t.IDBancoCarteira)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("BancoCarteira");
            this.Property(t => t.IDBancoCarteira).HasColumnName("IDBancoCarteira");
            this.Property(t => t.IDBanco).HasColumnName("IDBanco");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.Banco)
                .WithMany(t => t.BancoCarteiras)
                .HasForeignKey(d => d.IDBanco);

        }
    }
}
