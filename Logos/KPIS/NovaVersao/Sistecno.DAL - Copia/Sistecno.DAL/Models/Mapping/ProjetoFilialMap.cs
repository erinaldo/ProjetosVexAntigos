using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProjetoFilialMap : EntityTypeConfiguration<ProjetoFilial>
    {
        public ProjetoFilialMap()
        {
            // Primary Key
            this.HasKey(t => t.IdProjetoFilial);

            // Properties
            this.Property(t => t.IdProjetoFilial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ProjetoFilial");
            this.Property(t => t.IdProjetoFilial).HasColumnName("IdProjetoFilial");
            this.Property(t => t.IdProjeto).HasColumnName("IdProjeto");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.Tipo).HasColumnName("Tipo");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.ProjetoFilials)
                .HasForeignKey(d => d.IdFilial);
            this.HasRequired(t => t.Projeto)
                .WithMany(t => t.ProjetoFilials)
                .HasForeignKey(d => d.IdProjeto);

        }
    }
}
