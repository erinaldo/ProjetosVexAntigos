using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BancoOcorrenciaRemessaMap : EntityTypeConfiguration<BancoOcorrenciaRemessa>
    {
        public BancoOcorrenciaRemessaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBancoOcorrenciaRemessa);

            // Properties
            this.Property(t => t.IDBancoOcorrenciaRemessa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.Descricao)
                .HasMaxLength(150);

            this.Property(t => t.Sistema)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BancoOcorrenciaRemessa");
            this.Property(t => t.IDBancoOcorrenciaRemessa).HasColumnName("IDBancoOcorrenciaRemessa");
            this.Property(t => t.IDBanco).HasColumnName("IDBanco");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Sistema).HasColumnName("Sistema");

            // Relationships
            this.HasRequired(t => t.Banco)
                .WithMany(t => t.BancoOcorrenciaRemessas)
                .HasForeignKey(d => d.IDBanco);

        }
    }
}
