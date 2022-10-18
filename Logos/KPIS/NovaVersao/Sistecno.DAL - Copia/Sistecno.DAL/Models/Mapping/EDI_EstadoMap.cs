using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_EstadoMap : EntityTypeConfiguration<EDI_Estado>
    {
        public EDI_EstadoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EDI_Chave, t.IDEstado, t.IDPais, t.Uf, t.Nome });

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.IDEstado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDPais)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Uf)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(80);

            // Table & Column Mappings
            this.ToTable("EDI_Estado");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IDEstado).HasColumnName("IDEstado");
            this.Property(t => t.IDPais).HasColumnName("IDPais");
            this.Property(t => t.Uf).HasColumnName("Uf");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
