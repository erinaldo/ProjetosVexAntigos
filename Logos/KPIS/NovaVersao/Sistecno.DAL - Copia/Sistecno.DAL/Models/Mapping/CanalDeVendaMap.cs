using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CanalDeVendaMap : EntityTypeConfiguration<CanalDeVenda>
    {
        public CanalDeVendaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCanalDeVenda);

            // Properties
            this.Property(t => t.IdCanalDeVenda)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CanalDeVenda");
            this.Property(t => t.IdCanalDeVenda).HasColumnName("IdCanalDeVenda");
            this.Property(t => t.IdSupervisor).HasColumnName("IdSupervisor");
            this.Property(t => t.IdRepresentante).HasColumnName("IdRepresentante");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.IdDivisao).HasColumnName("IdDivisao");
        }
    }
}
