using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RomaneioDocumentoConferenciaMap : EntityTypeConfiguration<RomaneioDocumentoConferencia>
    {
        public RomaneioDocumentoConferenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRomaneioDocumentoConferencia);

            // Properties
            this.Property(t => t.IDRomaneioDocumentoConferencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Referencia)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("RomaneioDocumentoConferencia");
            this.Property(t => t.IDRomaneioDocumentoConferencia).HasColumnName("IDRomaneioDocumentoConferencia");
            this.Property(t => t.IDRomaneioDocumento).HasColumnName("IDRomaneioDocumento");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");
            this.Property(t => t.IDProdutoCliente).HasColumnName("IDProdutoCliente");
            this.Property(t => t.IDUnidadeDeArmazenagem).HasColumnName("IDUnidadeDeArmazenagem");
            this.Property(t => t.IdUnidadeDeArmazenagemPai).HasColumnName("IdUnidadeDeArmazenagemPai");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDProdutoEmbalagem).HasColumnName("IDProdutoEmbalagem");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.ValorUnitario).HasColumnName("ValorUnitario");
            this.Property(t => t.Referencia).HasColumnName("Referencia");
            this.Property(t => t.Validade).HasColumnName("Validade");
            this.Property(t => t.DataDeEntrada).HasColumnName("DataDeEntrada");
            this.Property(t => t.IDDocumentoItem).HasColumnName("IDDocumentoItem");
            this.Property(t => t.idRomaneio).HasColumnName("idRomaneio");
            this.Property(t => t.IdProdutoEmbalagemRecebido).HasColumnName("IdProdutoEmbalagemRecebido");
            this.Property(t => t.IdEan13).HasColumnName("IdEan13");
            this.Property(t => t.IdDun14).HasColumnName("IdDun14");

            // Relationships
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.RomaneioDocumentoConferencias)
                .HasForeignKey(d => d.IDDocumento);
            this.HasOptional(t => t.ProdutoCliente)
                .WithMany(t => t.RomaneioDocumentoConferencias)
                .HasForeignKey(d => d.IDProdutoCliente);
            this.HasOptional(t => t.ProdutoEmbalagem)
                .WithMany(t => t.RomaneioDocumentoConferencias)
                .HasForeignKey(d => d.IDProdutoEmbalagem);
            this.HasRequired(t => t.Romaneio)
                .WithMany(t => t.RomaneioDocumentoConferencias)
                .HasForeignKey(d => d.idRomaneio);
            this.HasOptional(t => t.RomaneioDocumento)
                .WithMany(t => t.RomaneioDocumentoConferencias)
                .HasForeignKey(d => d.IDRomaneioDocumento);
            this.HasOptional(t => t.Usuario)
                .WithMany(t => t.RomaneioDocumentoConferencias)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
