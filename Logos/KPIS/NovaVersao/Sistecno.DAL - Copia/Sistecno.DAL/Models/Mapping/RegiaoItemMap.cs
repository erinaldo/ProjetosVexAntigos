using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RegiaoItemMap : EntityTypeConfiguration<RegiaoItem>
    {
        public RegiaoItemMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRegiaoItem);

            // Properties
            this.Property(t => t.IDRegiaoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("RegiaoItem");
            this.Property(t => t.IDRegiaoItem).HasColumnName("IDRegiaoItem");
            this.Property(t => t.IDRegiao).HasColumnName("IDRegiao");
            this.Property(t => t.IDCadastro).HasColumnName("IDCadastro");
            this.Property(t => t.IDSetor).HasColumnName("IDSetor");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.IDEstado).HasColumnName("IDEstado");
            this.Property(t => t.IDPais).HasColumnName("IDPais");
            this.Property(t => t.IDRoteirizacaoTipo).HasColumnName("IDRoteirizacaoTipo");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.Distancia).HasColumnName("Distancia");
            this.Property(t => t.Ordenar).HasColumnName("Ordenar");

            // Relationships
            this.HasOptional(t => t.Cadastro)
                .WithMany(t => t.RegiaoItems)
                .HasForeignKey(d => d.IDCadastro);
            this.HasOptional(t => t.Cidade)
                .WithMany(t => t.RegiaoItems)
                .HasForeignKey(d => d.IDCidade);
            this.HasOptional(t => t.Estado)
                .WithMany(t => t.RegiaoItems)
                .HasForeignKey(d => d.IDEstado);
            this.HasOptional(t => t.Pai)
                .WithMany(t => t.RegiaoItems)
                .HasForeignKey(d => d.IDPais);
            this.HasRequired(t => t.Regiao)
                .WithMany(t => t.RegiaoItems)
                .HasForeignKey(d => d.IDRegiao);
            this.HasOptional(t => t.RoteirizacaoTipo)
                .WithMany(t => t.RegiaoItems)
                .HasForeignKey(d => d.IDRoteirizacaoTipo);
            this.HasOptional(t => t.Setor)
                .WithMany(t => t.RegiaoItems)
                .HasForeignKey(d => d.IDSetor);

        }
    }
}
