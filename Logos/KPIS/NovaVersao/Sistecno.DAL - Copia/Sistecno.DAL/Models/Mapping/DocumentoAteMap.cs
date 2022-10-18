using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DocumentoAteMap : EntityTypeConfiguration<DocumentoAte>
    {
        public DocumentoAteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDocumentoAte);

            // Properties
            this.Property(t => t.IDDocumentoAte)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Numero)
                .HasMaxLength(500);

            this.Property(t => t.TipoVeiculo)
                .HasMaxLength(50);

            this.Property(t => t.NumeroOrcamento)
                .HasMaxLength(500);

            this.Property(t => t.MotivoAte)
                .HasMaxLength(50);

            this.Property(t => t.Solicitante)
                .HasMaxLength(50);

            this.Property(t => t.CentroCusto)
                .HasMaxLength(500);

            this.Property(t => t.MesFaturamento)
                .HasMaxLength(50);

            this.Property(t => t.Obs)
                .HasMaxLength(500);

            this.Property(t => t.TipoEmissao)
                .HasMaxLength(5);

            this.Property(t => t.TipoColeta)
                .HasMaxLength(5);

            this.Property(t => t.TipoEntrega)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("DocumentoAte");
            this.Property(t => t.IDDocumentoAte).HasColumnName("IDDocumentoAte");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.TipoVeiculo).HasColumnName("TipoVeiculo");
            this.Property(t => t.DataDeEmissao).HasColumnName("DataDeEmissao");
            this.Property(t => t.DataDeColeta).HasColumnName("DataDeColeta");
            this.Property(t => t.DataDeEntrega).HasColumnName("DataDeEntrega");
            this.Property(t => t.NumeroOrcamento).HasColumnName("NumeroOrcamento");
            this.Property(t => t.MotivoAte).HasColumnName("MotivoAte");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.Solicitante).HasColumnName("Solicitante");
            this.Property(t => t.CentroCusto).HasColumnName("CentroCusto");
            this.Property(t => t.MesFaturamento).HasColumnName("MesFaturamento");
            this.Property(t => t.CustoATE).HasColumnName("CustoATE");
            this.Property(t => t.Obs).HasColumnName("Obs");
            this.Property(t => t.TipoEmissao).HasColumnName("TipoEmissao");
            this.Property(t => t.TipoColeta).HasColumnName("TipoColeta");
            this.Property(t => t.TipoEntrega).HasColumnName("TipoEntrega");
        }
    }
}
