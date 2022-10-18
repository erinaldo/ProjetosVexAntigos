using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class AvisoMap : EntityTypeConfiguration<Aviso>
    {
        public AvisoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdAviso);

            // Properties
            this.Property(t => t.IdAviso)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Operacao)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Aviso");
            this.Property(t => t.IdAviso).HasColumnName("IdAviso");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.IdClienteDivisao).HasColumnName("IdClienteDivisao");
            this.Property(t => t.Operacao).HasColumnName("Operacao");
            this.Property(t => t.IdCanalDeVenda).HasColumnName("IdCanalDeVenda");
            this.Property(t => t.Sequencia).HasColumnName("Sequencia");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.Avisoes)
                .HasForeignKey(d => d.IdCliente);
            this.HasOptional(t => t.ClienteDivisao)
                .WithMany(t => t.Avisoes)
                .HasForeignKey(d => d.IdClienteDivisao);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Avisoes)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
