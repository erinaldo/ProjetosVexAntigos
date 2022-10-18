using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class OperacaoMap : EntityTypeConfiguration<Operacao>
    {
        public OperacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdOperacao);

            // Properties
            this.Property(t => t.IdOperacao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Operacao1)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.CentroDeCusto)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Habilitado)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Operacao");
            this.Property(t => t.IdOperacao).HasColumnName("IdOperacao");
            this.Property(t => t.Operacao1).HasColumnName("Operacao");
            this.Property(t => t.CentroDeCusto).HasColumnName("CentroDeCusto");
            this.Property(t => t.Habilitado).HasColumnName("Habilitado");
        }
    }
}
