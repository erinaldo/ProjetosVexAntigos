using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RequisicaoDeMaterialMap : EntityTypeConfiguration<RequisicaoDeMaterial>
    {
        public RequisicaoDeMaterialMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRequisicaoDeMaterial);

            // Properties
            this.Property(t => t.IDRequisicaoDeMaterial)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.TipoDocumento)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Impresso)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Prioridade)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("RequisicaoDeMaterial");
            this.Property(t => t.IDRequisicaoDeMaterial).HasColumnName("IDRequisicaoDeMaterial");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDUsuarioCompra).HasColumnName("IDUsuarioCompra");
            this.Property(t => t.IDUnidadeDeNegocios).HasColumnName("IDUnidadeDeNegocios");
            this.Property(t => t.DataDeSolicitacao).HasColumnName("DataDeSolicitacao");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.PrevisaoDeEntrega).HasColumnName("PrevisaoDeEntrega");
            this.Property(t => t.TipoDocumento).HasColumnName("TipoDocumento");
            this.Property(t => t.IDCentroDeCustoFilial).HasColumnName("IDCentroDeCustoFilial");
            this.Property(t => t.IdProjeto).HasColumnName("IdProjeto");
            this.Property(t => t.Impresso).HasColumnName("Impresso");
            this.Property(t => t.IdDepartamento).HasColumnName("IdDepartamento");
            this.Property(t => t.Prioridade).HasColumnName("Prioridade");
        }
    }
}
