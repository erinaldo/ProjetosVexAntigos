using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RPCIDocumentoMap : EntityTypeConfiguration<RPCIDocumento>
    {
        public RPCIDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRPCIDocumento);

            // Properties
            this.Property(t => t.IDRPCIDocumento)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("RPCIDocumento");
            this.Property(t => t.IDRPCIDocumento).HasColumnName("IDRPCIDocumento");
            this.Property(t => t.IDRPCI).HasColumnName("IDRPCI");
            this.Property(t => t.IDDT).HasColumnName("IDDT");
            this.Property(t => t.IDDocumento).HasColumnName("IDDocumento");

            // Relationships
            this.HasOptional(t => t.Documento)
                .WithMany(t => t.RPCIDocumentoes)
                .HasForeignKey(d => d.IDDocumento);
            this.HasOptional(t => t.DT)
                .WithMany(t => t.RPCIDocumentoes)
                .HasForeignKey(d => d.IDDT);
            this.HasRequired(t => t.RPCI)
                .WithMany(t => t.RPCIDocumentoes)
                .HasForeignKey(d => d.IDRPCI);

        }
    }
}
