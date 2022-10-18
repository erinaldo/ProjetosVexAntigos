using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_BairroMap : EntityTypeConfiguration<EDI_Bairro>
    {
        public EDI_BairroMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EDI_Chave, t.IDBairro, t.IDCidade, t.Nome });

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.IDBairro)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDCidade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(80);

            // Table & Column Mappings
            this.ToTable("EDI_Bairro");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IDBairro).HasColumnName("IDBairro");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
