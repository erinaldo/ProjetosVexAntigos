using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDILayoutRegTabelaMap : EntityTypeConfiguration<EDILayoutRegTabela>
    {
        public EDILayoutRegTabelaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEDILayoutRegTabela);

            // Properties
            this.Property(t => t.IDEDILayoutRegTabela)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tabela)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TabelaPrincipal)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CampoRelacionado)
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.CampoUnico)
                .HasMaxLength(50);

            this.Property(t => t.IncluirRegistro)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.AlterarRegistro)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.TabelaRelacionada)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EDILayoutRegTabela");
            this.Property(t => t.IDEDILayoutRegTabela).HasColumnName("IDEDILayoutRegTabela");
            this.Property(t => t.IDEDILayoutReg).HasColumnName("IDEDILayoutReg");
            this.Property(t => t.Tabela).HasColumnName("Tabela");
            this.Property(t => t.TabelaPrincipal).HasColumnName("TabelaPrincipal");
            this.Property(t => t.CampoRelacionado).HasColumnName("CampoRelacionado");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.CampoUnico).HasColumnName("CampoUnico");
            this.Property(t => t.IncluirRegistro).HasColumnName("IncluirRegistro");
            this.Property(t => t.AlterarRegistro).HasColumnName("AlterarRegistro");
            this.Property(t => t.TabelaRelacionada).HasColumnName("TabelaRelacionada");

            // Relationships
            this.HasRequired(t => t.EDILayOutReg)
                .WithMany(t => t.EDILayoutRegTabelas)
                .HasForeignKey(d => d.IDEDILayoutReg);

        }
    }
}
