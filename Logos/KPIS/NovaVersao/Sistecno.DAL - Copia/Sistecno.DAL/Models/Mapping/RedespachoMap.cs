using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RedespachoMap : EntityTypeConfiguration<Redespacho>
    {
        public RedespachoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRedespacho);

            // Properties
            this.Property(t => t.IDRedespacho)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Redespacho");
            this.Property(t => t.IDRedespacho).HasColumnName("IDRedespacho");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithOptional(t => t.Redespacho);

        }
    }
}
