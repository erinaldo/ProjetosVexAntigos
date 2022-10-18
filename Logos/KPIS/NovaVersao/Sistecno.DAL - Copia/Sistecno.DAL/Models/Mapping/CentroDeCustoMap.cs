using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CentroDeCustoMap : EntityTypeConfiguration<CentroDeCusto>
    {
        public CentroDeCustoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCentroDeCusto);

            // Properties
            this.Property(t => t.IDCentroDeCusto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CentroDeCusto1)
                .IsRequired()
                .HasMaxLength(12);

            this.Property(t => t.Nome)
                .HasMaxLength(50);

            this.Property(t => t.CodigoReduzido)
                .HasMaxLength(20);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            this.Property(t => t.TipoDeConta)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CentroDeCusto");
            this.Property(t => t.IDCentroDeCusto).HasColumnName("IDCentroDeCusto");
            this.Property(t => t.IDEmpresa).HasColumnName("IDEmpresa");
            this.Property(t => t.CentroDeCusto1).HasColumnName("CentroDeCusto");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.CodigoReduzido).HasColumnName("CodigoReduzido");
            this.Property(t => t.IDParente).HasColumnName("IDParente");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Percentual).HasColumnName("Percentual");
            this.Property(t => t.IDContaContabil).HasColumnName("IDContaContabil");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.TipoDeConta).HasColumnName("TipoDeConta");

            // Relationships
            this.HasOptional(t => t.ContaContabil)
                .WithMany(t => t.CentroDeCustoes)
                .HasForeignKey(d => d.IDContaContabil);
            this.HasRequired(t => t.Empresa)
                .WithMany(t => t.CentroDeCustoes)
                .HasForeignKey(d => d.IDEmpresa);

        }
    }
}
