using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class UsuarioClienteDivisaoMap : EntityTypeConfiguration<UsuarioClienteDivisao>
    {
        public UsuarioClienteDivisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDUsuarioClienteDivisao);

            // Properties
            this.Property(t => t.IDUsuarioClienteDivisao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UsuarioClienteDivisao");
            this.Property(t => t.IDUsuarioClienteDivisao).HasColumnName("IDUsuarioClienteDivisao");
            this.Property(t => t.IDUsuarioCliente).HasColumnName("IDUsuarioCliente");
            this.Property(t => t.IDClienteDivisao).HasColumnName("IDClienteDivisao");

            // Relationships
            this.HasRequired(t => t.ClienteDivisao)
                .WithMany(t => t.UsuarioClienteDivisaos)
                .HasForeignKey(d => d.IDClienteDivisao);

        }
    }
}
