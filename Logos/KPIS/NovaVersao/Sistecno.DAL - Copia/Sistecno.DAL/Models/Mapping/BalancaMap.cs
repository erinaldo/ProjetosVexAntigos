using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BalancaMap : EntityTypeConfiguration<Balanca>
    {
        public BalancaMap()
        {
            // Primary Key
            this.HasKey(t => t.ETIQUETA);

            // Properties
            this.Property(t => t.ETIQUETA)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.USUARIO)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.TIPO)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.dsIntegradoErro)
                .HasMaxLength(4000);

            this.Property(t => t.dsLog)
                .HasMaxLength(8000);

            this.Property(t => t.DOCUMENTOSAIDA)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Balanca");
            this.Property(t => t.ETIQUETA).HasColumnName("ETIQUETA");
            this.Property(t => t.COMPRIMENTO).HasColumnName("COMPRIMENTO");
            this.Property(t => t.LARGURA).HasColumnName("LARGURA");
            this.Property(t => t.ALTURA).HasColumnName("ALTURA");
            this.Property(t => t.PESO).HasColumnName("PESO");
            this.Property(t => t.MULTIPLO).HasColumnName("MULTIPLO");
            this.Property(t => t.USUARIO).HasColumnName("USUARIO");
            this.Property(t => t.TIPO).HasColumnName("TIPO");
            this.Property(t => t.DATA).HasColumnName("DATA");
            this.Property(t => t.cdStatus).HasColumnName("cdStatus");
            this.Property(t => t.dtIntegrado).HasColumnName("dtIntegrado");
            this.Property(t => t.stIntegradoStore).HasColumnName("stIntegradoStore");
            this.Property(t => t.stIntegradoESL).HasColumnName("stIntegradoESL");
            this.Property(t => t.dtIntegradoStore).HasColumnName("dtIntegradoStore");
            this.Property(t => t.dtIntegradoESL).HasColumnName("dtIntegradoESL");
            this.Property(t => t.dsIntegradoErro).HasColumnName("dsIntegradoErro");
            this.Property(t => t.dsLog).HasColumnName("dsLog");
            this.Property(t => t.dtEmailErro).HasColumnName("dtEmailErro");
            this.Property(t => t.DOCUMENTOSAIDA).HasColumnName("DOCUMENTOSAIDA");
        }
    }
}
