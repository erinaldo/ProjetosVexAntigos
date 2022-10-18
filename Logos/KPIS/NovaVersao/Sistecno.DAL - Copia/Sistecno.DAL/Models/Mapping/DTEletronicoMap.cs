using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DTEletronicoMap : EntityTypeConfiguration<DTEletronico>
    {
        public DTEletronicoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDTEletronico);

            // Properties
            this.Property(t => t.IdDTEletronico)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Lote)
                .HasMaxLength(50);

            this.Property(t => t.Chave)
                .HasMaxLength(50);

            this.Property(t => t.ReciboNumero)
                .HasMaxLength(50);

            this.Property(t => t.ReciboCStatus)
                .HasMaxLength(4);

            this.Property(t => t.ReciboStatus)
                .HasMaxLength(300);

            this.Property(t => t.ProtocoloNumero)
                .HasMaxLength(50);

            this.Property(t => t.ProtocoloCStatus)
                .HasMaxLength(4);

            this.Property(t => t.ProtocoloStatus)
                .HasMaxLength(300);

            this.Property(t => t.DTEletronicoTipo)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("DTEletronico");
            this.Property(t => t.IdDTEletronico).HasColumnName("IdDTEletronico");
            this.Property(t => t.IdDT).HasColumnName("IdDT");
            this.Property(t => t.IdLoteEletronico).HasColumnName("IdLoteEletronico");
            this.Property(t => t.Lote).HasColumnName("Lote");
            this.Property(t => t.Chave).HasColumnName("Chave");
            this.Property(t => t.ReciboNumero).HasColumnName("ReciboNumero");
            this.Property(t => t.ReciboCStatus).HasColumnName("ReciboCStatus");
            this.Property(t => t.ReciboStatus).HasColumnName("ReciboStatus");
            this.Property(t => t.ProtocoloNumero).HasColumnName("ProtocoloNumero");
            this.Property(t => t.ProtocoloCStatus).HasColumnName("ProtocoloCStatus");
            this.Property(t => t.ProtocoloStatus).HasColumnName("ProtocoloStatus");
            this.Property(t => t.XMLRetorno).HasColumnName("XMLRetorno");
            this.Property(t => t.XMLFinal).HasColumnName("XMLFinal");
            this.Property(t => t.DTEletronicoTipo).HasColumnName("DTEletronicoTipo");
        }
    }
}
