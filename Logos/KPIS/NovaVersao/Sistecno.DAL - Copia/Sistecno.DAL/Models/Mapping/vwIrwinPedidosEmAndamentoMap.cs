using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class vwIrwinPedidosEmAndamentoMap : EntityTypeConfiguration<vwIrwinPedidosEmAndamento>
    {
        public vwIrwinPedidosEmAndamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDDOCUMENTO);

            // Properties
            this.Property(t => t.EMISSAOPEDIDO)
                .HasMaxLength(10);

            this.Property(t => t.IDDOCUMENTO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NOMEDOARQUIVO)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("vwIrwinPedidosEmAndamento");
            this.Property(t => t.EMISSAOPEDIDO).HasColumnName("EMISSAOPEDIDO");
            this.Property(t => t.IDDOCUMENTO).HasColumnName("IDDOCUMENTO");
            this.Property(t => t.NUMERO).HasColumnName("NUMERO");
            this.Property(t => t.DATAHORARECEBIMENTOINTERFACE).HasColumnName("DATAHORARECEBIMENTOINTERFACE");
            this.Property(t => t.INICIODASEPARACAO).HasColumnName("INICIODASEPARACAO");
            this.Property(t => t.DATAHORATERMINOCONFERENCIA).HasColumnName("DATAHORATERMINOCONFERENCIA");
            this.Property(t => t.CONCLUSAOPALETS).HasColumnName("CONCLUSAOPALETS");
            this.Property(t => t.DATAHORABAIXADOESTOQUE).HasColumnName("DATAHORABAIXADOESTOQUE");
            this.Property(t => t.DATAHORAPEDIDONOTAFISCAL).HasColumnName("DATAHORAPEDIDONOTAFISCAL");
            this.Property(t => t.NOMEDOARQUIVO).HasColumnName("NOMEDOARQUIVO");
            this.Property(t => t.HORANF).HasColumnName("HORANF");
            this.Property(t => t.NF).HasColumnName("NF");
        }
    }
}
