using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PalletMap : EntityTypeConfiguration<Pallet>
    {
        public PalletMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPallet);

            // Properties
            this.Property(t => t.IdPallet)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Pallet");
            this.Property(t => t.IdPallet).HasColumnName("IdPallet");
            this.Property(t => t.IdRomaneio).HasColumnName("IdRomaneio");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdUsuarioCarregamento).HasColumnName("IdUsuarioCarregamento");
            this.Property(t => t.DataDeAbertura).HasColumnName("DataDeAbertura");
            this.Property(t => t.DataDeFechamento).HasColumnName("DataDeFechamento");
            this.Property(t => t.DataDeAlteracao).HasColumnName("DataDeAlteracao");
            this.Property(t => t.InicioCarregamento).HasColumnName("InicioCarregamento");
            this.Property(t => t.FinalCarregamento).HasColumnName("FinalCarregamento");
            this.Property(t => t.ReAberturaDoPallet).HasColumnName("ReAberturaDoPallet");
            this.Property(t => t.ReaberturaDoPalletIdUsuario).HasColumnName("ReaberturaDoPalletIdUsuario");

            // Relationships
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Pallets)
                .HasForeignKey(d => d.IdUsuario);
            this.HasOptional(t => t.Usuario1)
                .WithMany(t => t.Pallets1)
                .HasForeignKey(d => d.IdUsuarioCarregamento);
            this.HasRequired(t => t.Usuario2)
                .WithMany(t => t.Pallets2)
                .HasForeignKey(d => d.IdUsuario);
            this.HasRequired(t => t.UnidadeDeArmazenagem)
                .WithOptional(t => t.Pallet);

        }
    }
}
