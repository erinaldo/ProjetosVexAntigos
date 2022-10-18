using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RegiaoMap : EntityTypeConfiguration<Regiao>
    {
        public RegiaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRegiao);

            // Properties
            this.Property(t => t.IDRegiao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Ordem)
                .HasMaxLength(10);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.DiasDeSaida)
                .HasMaxLength(7);

            this.Property(t => t.Tipo)
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("Regiao");
            this.Property(t => t.IDRegiao).HasColumnName("IDRegiao");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.IDTransportadora).HasColumnName("IDTransportadora");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.PrazoDeEntrega).HasColumnName("PrazoDeEntrega");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.DiasDeSaida).HasColumnName("DiasDeSaida");
            this.Property(t => t.IdTabelaDeFrete).HasColumnName("IdTabelaDeFrete");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Ordenar).HasColumnName("Ordenar");

            // Relationships
            this.HasOptional(t => t.Cliente)
                .WithMany(t => t.Regiaos)
                .HasForeignKey(d => d.IDCliente);
            this.HasOptional(t => t.Filial)
                .WithMany(t => t.Regiaos)
                .HasForeignKey(d => d.IDFilial);
            this.HasOptional(t => t.TabelaDeFrete)
                .WithMany(t => t.Regiaos)
                .HasForeignKey(d => d.IdTabelaDeFrete);
            this.HasOptional(t => t.Transportadora)
                .WithMany(t => t.Regiaos)
                .HasForeignKey(d => d.IDTransportadora);

        }
    }
}
