using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BancoOcorrenciaRetornoMap : EntityTypeConfiguration<BancoOcorrenciaRetorno>
    {
        public BancoOcorrenciaRetornoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBancoOcorrenciaRetorno);

            // Properties
            this.Property(t => t.IDBancoOcorrenciaRetorno)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            this.Property(t => t.TipoRetorno)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("BancoOcorrenciaRetorno");
            this.Property(t => t.IDBancoOcorrenciaRetorno).HasColumnName("IDBancoOcorrenciaRetorno");
            this.Property(t => t.IDBanco).HasColumnName("IDBanco");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.TipoRetorno).HasColumnName("TipoRetorno");

            // Relationships
            this.HasRequired(t => t.Banco)
                .WithMany(t => t.BancoOcorrenciaRetornoes)
                .HasForeignKey(d => d.IDBanco);

        }
    }
}
