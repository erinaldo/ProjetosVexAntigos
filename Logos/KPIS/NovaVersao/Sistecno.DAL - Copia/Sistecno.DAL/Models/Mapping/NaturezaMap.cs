using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class NaturezaMap : EntityTypeConfiguration<Natureza>
    {
        public NaturezaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDNatureza);

            // Properties
            this.Property(t => t.IDNatureza)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.IRRF)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ISS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.INSS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.CSLL)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.COFINS)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PIS)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Natureza");
            this.Property(t => t.IDNatureza).HasColumnName("IDNatureza");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.IRRF).HasColumnName("IRRF");
            this.Property(t => t.ISS).HasColumnName("ISS");
            this.Property(t => t.INSS).HasColumnName("INSS");
            this.Property(t => t.CSLL).HasColumnName("CSLL");
            this.Property(t => t.COFINS).HasColumnName("COFINS");
            this.Property(t => t.PIS).HasColumnName("PIS");
            this.Property(t => t.IRRFPercentual).HasColumnName("IRRFPercentual");
            this.Property(t => t.ISSPercentual).HasColumnName("ISSPercentual");
            this.Property(t => t.INSSPercentual).HasColumnName("INSSPercentual");
            this.Property(t => t.CLSSPercentual).HasColumnName("CLSSPercentual");
            this.Property(t => t.COFINSPercentual).HasColumnName("COFINSPercentual");
            this.Property(t => t.PISPercentual).HasColumnName("PISPercentual");
        }
    }
}
