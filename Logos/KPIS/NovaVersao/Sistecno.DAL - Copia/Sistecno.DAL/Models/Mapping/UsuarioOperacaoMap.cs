using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioOperacaoMap : EntityTypeConfiguration<UsuarioOperacao>
    {
        public UsuarioOperacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdUsuarioOperacao);

            // Properties
            this.Property(t => t.IdUsuarioOperacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UsuarioOperacao");
            this.Property(t => t.IdUsuarioOperacao).HasColumnName("IdUsuarioOperacao");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdOperacao).HasColumnName("IdOperacao");
            this.Property(t => t.IdCentroDeCusto).HasColumnName("IdCentroDeCusto");
            this.Property(t => t.Valor).HasColumnName("Valor");

            // Relationships
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.UsuarioOperacaos)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
