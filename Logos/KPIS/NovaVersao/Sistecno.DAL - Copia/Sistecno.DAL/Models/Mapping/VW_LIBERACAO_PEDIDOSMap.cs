using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VW_LIBERACAO_PEDIDOSMap : EntityTypeConfiguration<VW_LIBERACAO_PEDIDOS>
    {
        public VW_LIBERACAO_PEDIDOSMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCLIENTE);

            // Properties
            this.Property(t => t.IDCLIENTE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("VW_LIBERACAO_PEDIDOS");
            this.Property(t => t.IDCLIENTE).HasColumnName("IDCLIENTE");
            this.Property(t => t.DATA).HasColumnName("DATA");
            this.Property(t => t.QUANTIDADEDEPEDIDOS).HasColumnName("QUANTIDADEDEPEDIDOS");
            this.Property(t => t.AGUARDANDO_LIBERACAO).HasColumnName("AGUARDANDO_LIBERACAO");
            this.Property(t => t.LIBERADO_PARA_SEPARACAO).HasColumnName("LIBERADO_PARA_SEPARACAO");
            this.Property(t => t.EM_SEPARACAO).HasColumnName("EM_SEPARACAO");
            this.Property(t => t.SEPARACAO_FINALIZADA).HasColumnName("SEPARACAO_FINALIZADA");
            this.Property(t => t.LIBERADO_PARA_EMBALAGEM).HasColumnName("LIBERADO_PARA_EMBALAGEM");
            this.Property(t => t.PEDIDO_FATURADO).HasColumnName("PEDIDO_FATURADO");
            this.Property(t => t.AGUARDADO_EMISSAO_NFE).HasColumnName("AGUARDADO_EMISSAO_NFE");
            this.Property(t => t.NFE_EMITIDAS).HasColumnName("NFE_EMITIDAS");
            this.Property(t => t.AGUARDANDO_EMBARQUE).HasColumnName("AGUARDANDO_EMBARQUE");
            this.Property(t => t.EM_ENTREGA).HasColumnName("EM_ENTREGA");
            this.Property(t => t.ENTREGUE).HasColumnName("ENTREGUE");
        }
    }
}
