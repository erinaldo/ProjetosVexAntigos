using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroTipoDeContatoMap : EntityTypeConfiguration<CadastroTipoDeContato>
    {
        public CadastroTipoDeContatoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroTipoDeContato);

            // Properties
            this.Property(t => t.IDCadastroTipoDeContato)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("CadastroTipoDeContato");
            this.Property(t => t.IDCadastroTipoDeContato).HasColumnName("IDCadastroTipoDeContato");
            this.Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}
