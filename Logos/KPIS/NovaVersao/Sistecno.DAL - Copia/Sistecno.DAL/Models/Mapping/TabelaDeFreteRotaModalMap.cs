using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TabelaDeFreteRotaModalMap : EntityTypeConfiguration<TabelaDeFreteRotaModal>
    {
        public TabelaDeFreteRotaModalMap()
        {
            // Primary Key
            this.HasKey(t => t.IdTabelaDeFreteRotaModal);

            // Properties
            this.Property(t => t.IdTabelaDeFreteRotaModal)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("TabelaDeFreteRotaModal");
            this.Property(t => t.IdTabelaDeFreteRotaModal).HasColumnName("IdTabelaDeFreteRotaModal");
            this.Property(t => t.IdTabelaDeFreteRota).HasColumnName("IdTabelaDeFreteRota");
            this.Property(t => t.IdModal).HasColumnName("IdModal");
            this.Property(t => t.PrazoDeEntrega).HasColumnName("PrazoDeEntrega");
            this.Property(t => t.FatorDeCubagem).HasColumnName("FatorDeCubagem");

            // Relationships
            this.HasRequired(t => t.Modal)
                .WithMany(t => t.TabelaDeFreteRotaModals)
                .HasForeignKey(d => d.IdModal);
            this.HasRequired(t => t.TabelaDeFreteRota)
                .WithMany(t => t.TabelaDeFreteRotaModals)
                .HasForeignKey(d => d.IdTabelaDeFreteRota);

        }
    }
}
