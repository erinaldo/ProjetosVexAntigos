using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class FuncionarioMap : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioMap()
        {
            // Primary Key
            this.HasKey(t => t.IdFuncionario);

            // Properties
            this.Property(t => t.IdFuncionario)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Matricula)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("Funcionario");
            this.Property(t => t.IdFuncionario).HasColumnName("IdFuncionario");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.IdDepartamento).HasColumnName("IdDepartamento");
            this.Property(t => t.Matricula).HasColumnName("Matricula");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IDCentroDeCusto).HasColumnName("IDCentroDeCusto");
            this.Property(t => t.IDContaContabil).HasColumnName("IDContaContabil");

            // Relationships
            this.HasOptional(t => t.CentroDeCusto)
                .WithMany(t => t.Funcionarios)
                .HasForeignKey(d => d.IDCentroDeCusto);
            this.HasOptional(t => t.ContaContabil)
                .WithMany(t => t.Funcionarios)
                .HasForeignKey(d => d.IDContaContabil);
            this.HasRequired(t => t.Departamento)
                .WithMany(t => t.Funcionarios)
                .HasForeignKey(d => d.IdDepartamento);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Funcionarios)
                .HasForeignKey(d => d.IdFilial);

        }
    }
}
