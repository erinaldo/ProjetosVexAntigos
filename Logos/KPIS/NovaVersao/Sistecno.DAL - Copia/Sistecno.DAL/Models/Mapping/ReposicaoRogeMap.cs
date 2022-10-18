using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ReposicaoRogeMap : EntityTypeConfiguration<ReposicaoRoge>
    {
        public ReposicaoRogeMap()
        {
            // Primary Key
            this.HasKey(t => t.IdReposicaoRoge);

            // Properties
            this.Property(t => t.IdReposicaoRoge)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Chave)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IdNota)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CodigoRegiao)
                .HasMaxLength(20);

            this.Property(t => t.NomeRegiao)
                .HasMaxLength(50);

            this.Property(t => t.UsuarioColetor)
                .HasMaxLength(50);

            this.Property(t => t.UsuarioEnvioRoge)
                .HasMaxLength(50);

            this.Property(t => t.CodigoEnvioRoge)
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            this.Property(t => t.ClienteEspecial)
                .HasMaxLength(50);

            this.Property(t => t.UsuarioEnvioAuditoria)
                .HasMaxLength(50);

            this.Property(t => t.UsuarioMercadoriaRecebida)
                .HasMaxLength(50);

            this.Property(t => t.StatusMercadoriaRecebida)
                .HasMaxLength(50);

            this.Property(t => t.Observacao)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ReposicaoRoge");
            this.Property(t => t.IdReposicaoRoge).HasColumnName("IdReposicaoRoge");
            this.Property(t => t.Chave).HasColumnName("Chave");
            this.Property(t => t.IdNota).HasColumnName("IdNota");
            this.Property(t => t.CodigoRegiao).HasColumnName("CodigoRegiao");
            this.Property(t => t.NomeRegiao).HasColumnName("NomeRegiao");
            this.Property(t => t.DataDaInclusao).HasColumnName("DataDaInclusao");
            this.Property(t => t.UsuarioColetor).HasColumnName("UsuarioColetor");
            this.Property(t => t.DataColetor).HasColumnName("DataColetor");
            this.Property(t => t.UsuarioEnvioRoge).HasColumnName("UsuarioEnvioRoge");
            this.Property(t => t.DataEnvioRoge).HasColumnName("DataEnvioRoge");
            this.Property(t => t.CodigoEnvioRoge).HasColumnName("CodigoEnvioRoge");
            this.Property(t => t.DescricaoEnvioRoge).HasColumnName("DescricaoEnvioRoge");
            this.Property(t => t.QuantidadeDeProduto).HasColumnName("QuantidadeDeProduto");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Inicio).HasColumnName("Inicio");
            this.Property(t => t.Fim).HasColumnName("Fim");
            this.Property(t => t.ClienteEspecial).HasColumnName("ClienteEspecial");
            this.Property(t => t.UsuarioEnvioAuditoria).HasColumnName("UsuarioEnvioAuditoria");
            this.Property(t => t.DataEnvioAuditoria).HasColumnName("DataEnvioAuditoria");
            this.Property(t => t.Valor).HasColumnName("Valor");
            this.Property(t => t.MercadoriaRecebida).HasColumnName("MercadoriaRecebida");
            this.Property(t => t.UsuarioMercadoriaRecebida).HasColumnName("UsuarioMercadoriaRecebida");
            this.Property(t => t.StatusMercadoriaRecebida).HasColumnName("StatusMercadoriaRecebida");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
        }
    }
}
