using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FornecedorMap : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorMap()
        {
            // Primary Key
            this.HasKey(t => t.IDFornecedor);

            // Properties
            this.Property(t => t.IDFornecedor)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Ativo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Fornecedor");
            this.Property(t => t.IDFornecedor).HasColumnName("IDFornecedor");
            this.Property(t => t.CodigoDoFornecedor).HasColumnName("CodigoDoFornecedor");
            this.Property(t => t.CodigoDoFornecedorFilial).HasColumnName("CodigoDoFornecedorFilial");
            this.Property(t => t.IDRamoDeAtividade).HasColumnName("IDRamoDeAtividade");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IDContaContabilCredito).HasColumnName("IDContaContabilCredito");
            this.Property(t => t.IDContaContabilDebito).HasColumnName("IDContaContabilDebito");
            this.Property(t => t.IDCentroDeCustoCredito).HasColumnName("IDCentroDeCustoCredito");
            this.Property(t => t.IDCentroDeCustoDebito).HasColumnName("IDCentroDeCustoDebito");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.IDCentroDeCusto).HasColumnName("IDCentroDeCusto");
            this.Property(t => t.IDContaContabil).HasColumnName("IDContaContabil");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithOptional(t => t.Fornecedor);
            this.HasOptional(t => t.CentroDeCusto)
                .WithMany(t => t.Fornecedors)
                .HasForeignKey(d => d.IDCentroDeCusto);
            this.HasOptional(t => t.CentroDeCusto1)
                .WithMany(t => t.Fornecedors1)
                .HasForeignKey(d => d.IDCentroDeCustoCredito);
            this.HasOptional(t => t.CentroDeCusto2)
                .WithMany(t => t.Fornecedors2)
                .HasForeignKey(d => d.IDCentroDeCustoDebito);
            this.HasOptional(t => t.ContaContabil)
                .WithMany(t => t.Fornecedors)
                .HasForeignKey(d => d.IDContaContabil);
            this.HasOptional(t => t.ContaContabil1)
                .WithMany(t => t.Fornecedors1)
                .HasForeignKey(d => d.IDContaContabilCredito);
            this.HasOptional(t => t.ContaContabil2)
                .WithMany(t => t.Fornecedors2)
                .HasForeignKey(d => d.IDContaContabilDebito);

        }
    }
}
