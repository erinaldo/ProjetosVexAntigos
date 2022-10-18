using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class OcorrenciaDeParaMap : EntityTypeConfiguration<OcorrenciaDePara>
    {
        public OcorrenciaDeParaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdOcorrenciaDePara);

            // Properties
            this.Property(t => t.IdOcorrenciaDePara)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .HasMaxLength(10);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("OcorrenciaDePara");
            this.Property(t => t.IdOcorrenciaDePara).HasColumnName("IdOcorrenciaDePara");
            this.Property(t => t.IdOcorrencia).HasColumnName("IdOcorrencia");
            this.Property(t => t.CodigoDoCliente).HasColumnName("CodigoDoCliente");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.Ocorrencia)
                .WithMany(t => t.OcorrenciaDeParas)
                .HasForeignKey(d => d.IdOcorrencia);

        }
    }
}
