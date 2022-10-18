using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDILayoutTabelaMap : EntityTypeConfiguration<EDILayoutTabela>
    {
        public EDILayoutTabelaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEDILayoutTabela);

            // Properties
            this.Property(t => t.IDEDILayoutTabela)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tabela)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CampoUnico)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("EDILayoutTabela");
            this.Property(t => t.IDEDILayoutTabela).HasColumnName("IDEDILayoutTabela");
            this.Property(t => t.IDEDILayout).HasColumnName("IDEDILayout");
            this.Property(t => t.Tabela).HasColumnName("Tabela");
            this.Property(t => t.OrdemGravacao).HasColumnName("OrdemGravacao");
            this.Property(t => t.CampoUnico).HasColumnName("CampoUnico");

            // Relationships
            this.HasRequired(t => t.EDILayOut)
                .WithMany(t => t.EDILayoutTabelas)
                .HasForeignKey(d => d.IDEDILayout);

        }
    }
}
