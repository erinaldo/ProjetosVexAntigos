using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AuditoriaMap : EntityTypeConfiguration<Auditoria>
    {
        public AuditoriaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TABELA, t.IDREGISTRO, t.OPERACAO, t.IDUSUARIO, t.USUARIO });

            // Properties
            this.Property(t => t.TABELA)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IDREGISTRO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OPERACAO)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.CAMPO)
                .HasMaxLength(30);

            this.Property(t => t.VALORANTERIOR)
                .HasMaxLength(100);

            this.Property(t => t.VALORNOVO)
                .HasMaxLength(100);

            this.Property(t => t.IDUSUARIO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.USUARIO)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PROCEDIMENTO)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Auditoria");
            this.Property(t => t.TABELA).HasColumnName("TABELA");
            this.Property(t => t.IDREGISTRO).HasColumnName("IDREGISTRO");
            this.Property(t => t.OPERACAO).HasColumnName("OPERACAO");
            this.Property(t => t.CAMPO).HasColumnName("CAMPO");
            this.Property(t => t.VALORANTERIOR).HasColumnName("VALORANTERIOR");
            this.Property(t => t.VALORNOVO).HasColumnName("VALORNOVO");
            this.Property(t => t.IDUSUARIO).HasColumnName("IDUSUARIO");
            this.Property(t => t.USUARIO).HasColumnName("USUARIO");
            this.Property(t => t.PROCEDIMENTO).HasColumnName("PROCEDIMENTO");
            this.Property(t => t.DATAHORA).HasColumnName("DATAHORA");
        }
    }
}
