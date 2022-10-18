using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoEletronicoMap : EntityTypeConfiguration<DocumentoEletronico>
    {
        public DocumentoEletronicoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDocumentoEletronico);

            // Properties
            this.Property(t => t.IdDocumentoEletronico)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NumeroRecibo)
                .HasMaxLength(50);

            this.Property(t => t.NumeroProtocolo)
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.Lote)
                .HasMaxLength(50);

            this.Property(t => t.IdNota)
                .HasMaxLength(50);

            this.Property(t => t.TipoDeDocumento)
                .HasMaxLength(20);

            this.Property(t => t.MotivoDoCancelamento)
                .HasMaxLength(100);

            this.Property(t => t.MotivoDaInutilizacao)
                .HasMaxLength(100);

            this.Property(t => t.CStatus)
                .HasMaxLength(4);

            this.Property(t => t.StatusCompleto)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("DocumentoEletronico");
            this.Property(t => t.IdDocumentoEletronico).HasColumnName("IdDocumentoEletronico");
            this.Property(t => t.IdDocumento).HasColumnName("IdDocumento");
            this.Property(t => t.NumeroRecibo).HasColumnName("NumeroRecibo");
            this.Property(t => t.NumeroProtocolo).HasColumnName("NumeroProtocolo");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Lote).HasColumnName("Lote");
            this.Property(t => t.IdNota).HasColumnName("IdNota");
            this.Property(t => t.UltimoArquivoXml).HasColumnName("UltimoArquivoXml");
            this.Property(t => t.Rejeicao).HasColumnName("Rejeicao");
            this.Property(t => t.XMLCancelamento).HasColumnName("XMLCancelamento");
            this.Property(t => t.XMLInutilizacao).HasColumnName("XMLInutilizacao");
            this.Property(t => t.TipoDeDocumento).HasColumnName("TipoDeDocumento");
            this.Property(t => t.MotivoDoCancelamento).HasColumnName("MotivoDoCancelamento");
            this.Property(t => t.MotivoDaInutilizacao).HasColumnName("MotivoDaInutilizacao");
            this.Property(t => t.ArquivoXml).HasColumnName("ArquivoXml");
            this.Property(t => t.TextoCarta).HasColumnName("TextoCarta");
            this.Property(t => t.XMLRps).HasColumnName("XMLRps");
            this.Property(t => t.XMLCarta).HasColumnName("XMLCarta");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdLoteEletronico).HasColumnName("IdLoteEletronico");
            this.Property(t => t.CStatus).HasColumnName("CStatus");
            this.Property(t => t.StatusCompleto).HasColumnName("StatusCompleto");

            // Relationships
            this.HasRequired(t => t.Documento)
                .WithMany(t => t.DocumentoEletronicoes)
                .HasForeignKey(d => d.IdDocumento);

        }
    }
}
