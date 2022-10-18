using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ReposicaoRogeEanMap : EntityTypeConfiguration<ReposicaoRogeEan>
    {
        public ReposicaoRogeEanMap()
        {
            // Primary Key
            this.HasKey(t => t.CodigoDeBarras);

            // Properties
            this.Property(t => t.CodigoDeBarras)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .HasMaxLength(1);

            this.Property(t => t.Descricao)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ReposicaoRogeEan");
            this.Property(t => t.CodigoDeBarras).HasColumnName("CodigoDeBarras");
            this.Property(t => t.DataInclusao).HasColumnName("DataInclusao");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
        }
    }
}
