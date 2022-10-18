using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TESControleMap : EntityTypeConfiguration<TESControle>
    {
        public TESControleMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTESControle);

            // Properties
            this.Property(t => t.IDTESControle)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("TESControle");
            this.Property(t => t.IDTESControle).HasColumnName("IDTESControle");
            this.Property(t => t.IDTES).HasColumnName("IDTES");
            this.Property(t => t.IDModuloOpcao).HasColumnName("IDModuloOpcao");
            this.Property(t => t.PROGRAMA).HasColumnName("PROGRAMA");
            this.Property(t => t.Ativo).HasColumnName("Ativo");

            // Relationships
            this.HasOptional(t => t.ModuloOpcao)
                .WithMany(t => t.TESControles)
                .HasForeignKey(d => d.IDModuloOpcao);
            this.HasOptional(t => t.TE)
                .WithMany(t => t.TESControles)
                .HasForeignKey(d => d.IDTES);

        }
    }
}
