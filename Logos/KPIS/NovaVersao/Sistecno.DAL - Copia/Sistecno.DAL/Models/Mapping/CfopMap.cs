using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CfopMap : EntityTypeConfiguration<Cfop>
    {
        public CfopMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCfop);

            // Properties
            this.Property(t => t.IDCfop)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tipo)
                .IsRequired()
                .HasMaxLength(7);

            this.Property(t => t.Codigo)
                .IsRequired()
                .HasMaxLength(3);

            this.Property(t => t.Estadual)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Interestadual)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Internacional)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("Cfop");
            this.Property(t => t.IDCfop).HasColumnName("IDCfop");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Estadual).HasColumnName("Estadual");
            this.Property(t => t.Interestadual).HasColumnName("Interestadual");
            this.Property(t => t.Internacional).HasColumnName("Internacional");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Aplicacao).HasColumnName("Aplicacao");
            this.Property(t => t.Vigencia).HasColumnName("Vigencia");
        }
    }
}
