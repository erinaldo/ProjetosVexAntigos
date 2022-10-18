using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BancoInstrucaoDeProtestoMap : EntityTypeConfiguration<BancoInstrucaoDeProtesto>
    {
        public BancoInstrucaoDeProtestoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdBancoInstrucaoDeProtesto);

            // Properties
            this.Property(t => t.IdBancoInstrucaoDeProtesto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("BancoInstrucaoDeProtesto");
            this.Property(t => t.IdBancoInstrucaoDeProtesto).HasColumnName("IdBancoInstrucaoDeProtesto");
            this.Property(t => t.IdBanco).HasColumnName("IdBanco");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Dias).HasColumnName("Dias");

            // Relationships
            this.HasRequired(t => t.Banco)
                .WithMany(t => t.BancoInstrucaoDeProtestoes)
                .HasForeignKey(d => d.IdBanco);

        }
    }
}
