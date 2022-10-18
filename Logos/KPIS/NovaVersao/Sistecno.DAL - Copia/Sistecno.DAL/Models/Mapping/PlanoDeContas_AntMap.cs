using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PlanoDeContas_AntMap : EntityTypeConfiguration<PlanoDeContas_Ant>
    {
        public PlanoDeContas_AntMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.Parent, t.Conta, t.Nome });

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(4);

            this.Property(t => t.Parent)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(4);

            this.Property(t => t.Conta)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.CodigoReduzido)
                .HasMaxLength(10);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("PlanoDeContas_Ant");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Parent).HasColumnName("Parent");
            this.Property(t => t.Conta).HasColumnName("Conta");
            this.Property(t => t.CodigoReduzido).HasColumnName("CodigoReduzido");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.IDContaContabil).HasColumnName("IDContaContabil");
            this.Property(t => t.IdEmpresa).HasColumnName("IdEmpresa");
            this.Property(t => t.IdParent).HasColumnName("IdParent");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
        }
    }
}
