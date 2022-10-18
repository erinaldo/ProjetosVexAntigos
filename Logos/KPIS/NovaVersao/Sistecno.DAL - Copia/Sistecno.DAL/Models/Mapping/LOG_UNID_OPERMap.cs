using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LOG_UNID_OPERMap : EntityTypeConfiguration<LOG_UNID_OPER>
    {
        public LOG_UNID_OPERMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UOP_NU, t.UFE_SG, t.LOC_NU, t.BAI_NU, t.UOP_NO, t.UOP_ENDERECO, t.CEP, t.UOP_IN_CP });

            // Properties
            this.Property(t => t.UOP_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UFE_SG)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.LOC_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BAI_NU)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UOP_NO)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.UOP_ENDERECO)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CEP)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.UOP_IN_CP)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.UOP_NO_ABREV)
                .HasMaxLength(36);

            // Table & Column Mappings
            this.ToTable("LOG_UNID_OPER");
            this.Property(t => t.UOP_NU).HasColumnName("UOP_NU");
            this.Property(t => t.UFE_SG).HasColumnName("UFE_SG");
            this.Property(t => t.LOC_NU).HasColumnName("LOC_NU");
            this.Property(t => t.BAI_NU).HasColumnName("BAI_NU");
            this.Property(t => t.LOG_NU).HasColumnName("LOG_NU");
            this.Property(t => t.UOP_NO).HasColumnName("UOP_NO");
            this.Property(t => t.UOP_ENDERECO).HasColumnName("UOP_ENDERECO");
            this.Property(t => t.CEP).HasColumnName("CEP");
            this.Property(t => t.UOP_IN_CP).HasColumnName("UOP_IN_CP");
            this.Property(t => t.UOP_NO_ABREV).HasColumnName("UOP_NO_ABREV");
        }
    }
}
