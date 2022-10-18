using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ModuloOpcaoTabelaMap : EntityTypeConfiguration<ModuloOpcaoTabela>
    {
        public ModuloOpcaoTabelaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDModuloOpcaoTabela);

            // Properties
            this.Property(t => t.IDModuloOpcaoTabela)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tabela)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ModuloOpcaoTabela");
            this.Property(t => t.IDModuloOpcaoTabela).HasColumnName("IDModuloOpcaoTabela");
            this.Property(t => t.IDModuloOpcao).HasColumnName("IDModuloOpcao");
            this.Property(t => t.Tabela).HasColumnName("Tabela");

            // Relationships
            this.HasRequired(t => t.ModuloOpcao)
                .WithMany(t => t.ModuloOpcaoTabelas)
                .HasForeignKey(d => d.IDModuloOpcao);

        }
    }
}
