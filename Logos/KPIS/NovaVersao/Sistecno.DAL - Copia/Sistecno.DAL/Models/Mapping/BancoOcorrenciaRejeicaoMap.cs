using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BancoOcorrenciaRejeicaoMap : EntityTypeConfiguration<BancoOcorrenciaRejeicao>
    {
        public BancoOcorrenciaRejeicaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBancoOcorrenciaRejeicao);

            // Properties
            this.Property(t => t.IDBancoOcorrenciaRejeicao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(6);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("BancoOcorrenciaRejeicao");
            this.Property(t => t.IDBancoOcorrenciaRejeicao).HasColumnName("IDBancoOcorrenciaRejeicao");
            this.Property(t => t.IDBancoOcorrenciaRetorno).HasColumnName("IDBancoOcorrenciaRetorno");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.BancoOcorrenciaRetorno)
                .WithMany(t => t.BancoOcorrenciaRejeicaos)
                .HasForeignKey(d => d.IDBancoOcorrenciaRetorno);

        }
    }
}
