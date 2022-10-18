using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class DtChequeMap : EntityTypeConfiguration<DtCheque>
    {
        public DtChequeMap()
        {
            // Primary Key
            this.HasKey(t => t.IdDtCheque);

            // Properties
            this.Property(t => t.IdDtCheque)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DtCheque");
            this.Property(t => t.IdDtCheque).HasColumnName("IdDtCheque");
            this.Property(t => t.IdDt).HasColumnName("IdDt");
            this.Property(t => t.IdCheque).HasColumnName("IdCheque");

            // Relationships
            this.HasRequired(t => t.Cheque)
                .WithMany(t => t.DtCheques)
                .HasForeignKey(d => d.IdCheque);
            this.HasRequired(t => t.DT)
                .WithMany(t => t.DtCheques)
                .HasForeignKey(d => d.IdDt);

        }
    }
}
