using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClienteDivisaoMap : EntityTypeConfiguration<ClienteDivisao>
    {
        public ClienteDivisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDClienteDivisao);

            // Properties
            this.Property(t => t.IDClienteDivisao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BaseExterna)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Sistema)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Ativo)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("ClienteDivisao");
            this.Property(t => t.IDClienteDivisao).HasColumnName("IDClienteDivisao");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.IDParente).HasColumnName("IDParente");
            this.Property(t => t.BaseExterna).HasColumnName("BaseExterna");
            this.Property(t => t.Sistema).HasColumnName("Sistema");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Sequencia).HasColumnName("Sequencia");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.ClienteDivisaos)
                .HasForeignKey(d => d.IDCliente);
            this.HasOptional(t => t.ClienteDivisao2)
                .WithMany(t => t.ClienteDivisao1)
                .HasForeignKey(d => d.IDParente);

        }
    }
}
