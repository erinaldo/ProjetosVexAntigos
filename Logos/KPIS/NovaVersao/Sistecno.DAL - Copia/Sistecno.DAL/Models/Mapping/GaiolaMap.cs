using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class GaiolaMap : EntityTypeConfiguration<Gaiola>
    {
        public GaiolaMap()
        {
            // Primary Key
            this.HasKey(t => t.IdGaiola);

            // Properties
            this.Property(t => t.IdGaiola)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Gaiola1)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Filial)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Impresso)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PertenceAFilial)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.VolumeInicial)
                .HasMaxLength(100);

            this.Property(t => t.Situacao)
                .HasMaxLength(50);

            this.Property(t => t.NumeroColetor)
                .HasMaxLength(20);

            this.Property(t => t.EMEI)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Gaiola");
            this.Property(t => t.IdGaiola).HasColumnName("IdGaiola");
            this.Property(t => t.Gaiola1).HasColumnName("Gaiola");
            this.Property(t => t.Filial).HasColumnName("Filial");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.Impresso).HasColumnName("Impresso");
            this.Property(t => t.IdUsuarioRecebeu).HasColumnName("IdUsuarioRecebeu");
            this.Property(t => t.IdGaiolaLida).HasColumnName("IdGaiolaLida");
            this.Property(t => t.DataRecebimento).HasColumnName("DataRecebimento");
            this.Property(t => t.PertenceAFilial).HasColumnName("PertenceAFilial");
            this.Property(t => t.VolumeInicial).HasColumnName("VolumeInicial");
            this.Property(t => t.DataFechamento).HasColumnName("DataFechamento");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.NumeroColetor).HasColumnName("NumeroColetor");
            this.Property(t => t.EMEI).HasColumnName("EMEI");
            this.Property(t => t.QtdVolumesLidos).HasColumnName("QtdVolumesLidos");
        }
    }
}
