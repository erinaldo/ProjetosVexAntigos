using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class SetorCepMap : EntityTypeConfiguration<SetorCep>
    {
        public SetorCepMap()
        {
            // Primary Key
            this.HasKey(t => t.IDSetorCep);

            // Properties
            this.Property(t => t.IDSetorCep)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CepInicial)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.CepFinal)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.Origem)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("SetorCep");
            this.Property(t => t.IDSetorCep).HasColumnName("IDSetorCep");
            this.Property(t => t.IDSetor).HasColumnName("IDSetor");
            this.Property(t => t.CepInicial).HasColumnName("CepInicial");
            this.Property(t => t.CepFinal).HasColumnName("CepFinal");
            this.Property(t => t.Origem).HasColumnName("Origem");

            // Relationships
            this.HasRequired(t => t.Setor)
                .WithMany(t => t.SetorCeps)
                .HasForeignKey(d => d.IDSetor);

        }
    }
}
