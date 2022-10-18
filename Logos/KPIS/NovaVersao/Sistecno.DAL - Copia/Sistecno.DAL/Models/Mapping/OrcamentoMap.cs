using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class OrcamentoMap : EntityTypeConfiguration<Orcamento>
    {
        public OrcamentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdOrcamento);

            // Properties
            this.Property(t => t.Orcamento1)
                .HasMaxLength(50);

            this.Property(t => t.Solicitante)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.Fone)
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Orcamento");
            this.Property(t => t.IdOrcamento).HasColumnName("IdOrcamento");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.Orcamento1).HasColumnName("Orcamento");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Solicitante).HasColumnName("Solicitante");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Fone).HasColumnName("Fone");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
