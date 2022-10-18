using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ProjetoProducaoMap : EntityTypeConfiguration<ProjetoProducao>
    {
        public ProjetoProducaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdProjetoProducao);

            // Properties
            this.Property(t => t.IdProjetoProducao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Turno)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("ProjetoProducao");
            this.Property(t => t.IdProjetoProducao).HasColumnName("IdProjetoProducao");
            this.Property(t => t.IdProjeto).HasColumnName("IdProjeto");
            this.Property(t => t.Lancamento).HasColumnName("Lancamento");
            this.Property(t => t.Turno).HasColumnName("Turno");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.HoraInicial).HasColumnName("HoraInicial");
            this.Property(t => t.HoraFinal).HasColumnName("HoraFinal");
            this.Property(t => t.MaoDeObra).HasColumnName("MaoDeObra");
            this.Property(t => t.QuantidadeEfetuada).HasColumnName("QuantidadeEfetuada");

            // Relationships
            this.HasRequired(t => t.Projeto)
                .WithMany(t => t.ProjetoProducaos)
                .HasForeignKey(d => d.IdProjeto);

        }
    }
}
