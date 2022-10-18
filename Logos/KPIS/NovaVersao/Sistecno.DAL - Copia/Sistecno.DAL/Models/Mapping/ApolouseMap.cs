using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ApolouseMap : EntityTypeConfiguration<Apolouse>
    {
        public ApolouseMap()
        {
            // Primary Key
            this.HasKey(t => t.IdApolice);

            // Properties
            this.Property(t => t.IdApolice)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Apolice");
            this.Property(t => t.IdApolice).HasColumnName("IdApolice");
            this.Property(t => t.IdEmpresa).HasColumnName("IdEmpresa");
            this.Property(t => t.IdCadastroSeguradora).HasColumnName("IdCadastroSeguradora");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Validade).HasColumnName("Validade");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasOptional(t => t.Cadastro)
                .WithMany(t => t.Apolice)
                .HasForeignKey(d => d.IdCliente);
            this.HasOptional(t => t.Empresa)
                .WithMany(t => t.Apolice)
                .HasForeignKey(d => d.IdEmpresa);
            this.HasOptional(t => t.Cadastro1)
                .WithMany(t => t.Apolice1)
                .HasForeignKey(d => d.IdCadastroSeguradora);

        }
    }
}
