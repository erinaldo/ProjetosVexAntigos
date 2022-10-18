using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BancoInstrucaoMap : EntityTypeConfiguration<BancoInstrucao>
    {
        public BancoInstrucaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBancoInstrucao);

            // Properties
            this.Property(t => t.IDBancoInstrucao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("BancoInstrucao");
            this.Property(t => t.IDBancoInstrucao).HasColumnName("IDBancoInstrucao");
            this.Property(t => t.IDBanco).HasColumnName("IDBanco");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.Banco)
                .WithMany(t => t.BancoInstrucaos)
                .HasForeignKey(d => d.IDBanco);

        }
    }
}
