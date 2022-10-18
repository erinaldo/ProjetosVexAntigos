using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDI_CadastroContatoEnderecoMap : EntityTypeConfiguration<EDI_CadastroContatoEndereco>
    {
        public EDI_CadastroContatoEnderecoMap()
        {
            // Primary Key
            this.HasKey(t => t.EDI_Chave);

            // Properties
            this.Property(t => t.EDI_Chave)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.EDI_Motivo)
                .HasMaxLength(500);

            this.Property(t => t.Endereco)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("EDI_CadastroContatoEndereco");
            this.Property(t => t.EDI_Chave).HasColumnName("EDI_Chave");
            this.Property(t => t.EDI_Motivo).HasColumnName("EDI_Motivo");
            this.Property(t => t.EDI_Data).HasColumnName("EDI_Data");
            this.Property(t => t.IDCadastroContatoEndereco).HasColumnName("IDCadastroContatoEndereco");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.IDCadastroTipoDeContato).HasColumnName("IDCadastroTipoDeContato");
            this.Property(t => t.Endereco).HasColumnName("Endereco");
        }
    }
}
