using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroRegiaoMap : EntityTypeConfiguration<CadastroRegiao>
    {
        public CadastroRegiaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCadastroRegiao);

            // Properties
            this.Property(t => t.IDCadastroRegiao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CadastroRegiao");
            this.Property(t => t.IDCadastroRegiao).HasColumnName("IDCadastroRegiao");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDRegiaoItem).HasColumnName("IDRegiaoItem");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.CadastroRegiaos)
                .HasForeignKey(d => d.IDCadastro);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.CadastroRegiaos)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.RegiaoItem)
                .WithMany(t => t.CadastroRegiaos)
                .HasForeignKey(d => d.IDRegiaoItem);

        }
    }
}
