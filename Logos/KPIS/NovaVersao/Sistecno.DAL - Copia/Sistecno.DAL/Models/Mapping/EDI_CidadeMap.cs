using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_CidadeMap : EntityTypeConfiguration<EDI_Cidade>
    {
        public EDI_CidadeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EDI_Chave, t.IDCidade, t.IDEstado, t.Nome });

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.IDCidade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDEstado)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.Cep)
                .HasMaxLength(8);

            this.Property(t => t.Tipo)
                .HasMaxLength(10);

            this.Property(t => t.CodificarPor)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("EDI_Cidade");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.IDEstado).HasColumnName("IDEstado");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Cep).HasColumnName("Cep");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.CodificarPor).HasColumnName("CodificarPor");
        }
    }
}
