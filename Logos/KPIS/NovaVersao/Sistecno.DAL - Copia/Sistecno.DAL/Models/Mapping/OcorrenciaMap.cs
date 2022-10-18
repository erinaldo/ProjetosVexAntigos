using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class OcorrenciaMap : EntityTypeConfiguration<Ocorrencia>
    {
        public OcorrenciaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDOcorrencia);

            // Properties
            this.Property(t => t.IDOcorrencia)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Codigo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Responsabilidade)
                .HasMaxLength(15);

            this.Property(t => t.NomeReduzido)
                .HasMaxLength(30);

            this.Property(t => t.PagaEntrega)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Finalizador)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Sistema)
                .HasMaxLength(30);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            this.Property(t => t.RestringirCarregamento)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.AbrirFecharOcorrencia)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ApareceSiteCliente)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Situacao)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Ocorrencia");
            this.Property(t => t.IDOcorrencia).HasColumnName("IDOcorrencia");
            this.Property(t => t.IDEmpresa).HasColumnName("IDEmpresa");
            this.Property(t => t.IDOcorrenciaAcao).HasColumnName("IDOcorrenciaAcao");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Responsabilidade).HasColumnName("Responsabilidade");
            this.Property(t => t.NomeReduzido).HasColumnName("NomeReduzido");
            this.Property(t => t.PagaEntrega).HasColumnName("PagaEntrega");
            this.Property(t => t.Finalizador).HasColumnName("Finalizador");
            this.Property(t => t.Sistema).HasColumnName("Sistema");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.RestringirCarregamento).HasColumnName("RestringirCarregamento");
            this.Property(t => t.AbrirFecharOcorrencia).HasColumnName("AbrirFecharOcorrencia");
            this.Property(t => t.ApareceSiteCliente).HasColumnName("ApareceSiteCliente");
            this.Property(t => t.IdOcorrenciaSerie).HasColumnName("IdOcorrenciaSerie");
            this.Property(t => t.Situacao).HasColumnName("Situacao");

            // Relationships
            this.HasOptional(t => t.Empresa)
                .WithMany(t => t.Ocorrencias)
                .HasForeignKey(d => d.IDEmpresa);
            this.HasRequired(t => t.OcorrenciaAcao)
                .WithMany(t => t.Ocorrencias)
                .HasForeignKey(d => d.IDOcorrenciaAcao);

        }
    }
}
