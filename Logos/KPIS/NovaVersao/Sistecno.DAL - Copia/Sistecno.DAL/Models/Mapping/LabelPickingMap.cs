using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LabelPickingMap : EntityTypeConfiguration<LabelPicking>
    {
        public LabelPickingMap()
        {
            // Primary Key
            this.HasKey(t => t.IdLabelPicking);

            // Properties
            this.Property(t => t.IdLabelPicking)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .HasMaxLength(30);

            this.Property(t => t.Picking)
                .HasMaxLength(30);

            this.Property(t => t.Embalagem)
                .HasMaxLength(10);

            this.Property(t => t.Empresa)
                .HasMaxLength(30);

            this.Property(t => t.Transportadora)
                .HasMaxLength(60);

            this.Property(t => t.Cliente)
                .HasMaxLength(60);

            this.Property(t => t.Destinatario)
                .HasMaxLength(60);

            this.Property(t => t.Destino)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("LabelPicking");
            this.Property(t => t.IdLabelPicking).HasColumnName("IdLabelPicking");
            this.Property(t => t.IdRomaneio).HasColumnName("IdRomaneio");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Picking).HasColumnName("Picking");
            this.Property(t => t.Embalagem).HasColumnName("Embalagem");
            this.Property(t => t.Empresa).HasColumnName("Empresa");
            this.Property(t => t.Transportadora).HasColumnName("Transportadora");
            this.Property(t => t.Cliente).HasColumnName("Cliente");
            this.Property(t => t.Destinatario).HasColumnName("Destinatario");
            this.Property(t => t.Destino).HasColumnName("Destino");
            this.Property(t => t.Volumes).HasColumnName("Volumes");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.Impressoes).HasColumnName("Impressoes");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.DataDeImpressao).HasColumnName("DataDeImpressao");

            // Relationships
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.LabelPickings)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
