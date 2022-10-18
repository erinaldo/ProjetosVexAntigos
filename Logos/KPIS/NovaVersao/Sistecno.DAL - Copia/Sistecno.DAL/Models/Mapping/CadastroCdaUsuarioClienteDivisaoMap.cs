using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CadastroCdaUsuarioClienteDivisaoMap : EntityTypeConfiguration<CadastroCdaUsuarioClienteDivisao>
    {
        public CadastroCdaUsuarioClienteDivisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdCadastroCdaUsuarioClienteDivisao);

            // Properties
            this.Property(t => t.IdCadastroCdaUsuarioClienteDivisao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Inventario)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("CadastroCdaUsuarioClienteDivisao");
            this.Property(t => t.IdCadastroCdaUsuarioClienteDivisao).HasColumnName("IdCadastroCdaUsuarioClienteDivisao");
            this.Property(t => t.IdCadastroCda).HasColumnName("IdCadastroCda");
            this.Property(t => t.IDUsuarioClienteDivisao).HasColumnName("IDUsuarioClienteDivisao");
            this.Property(t => t.Inventario).HasColumnName("Inventario");

            // Relationships
            this.HasRequired(t => t.UsuarioClienteDivisao)
                .WithMany(t => t.CadastroCdaUsuarioClienteDivisaos)
                .HasForeignKey(d => d.IDUsuarioClienteDivisao);

        }
    }
}
