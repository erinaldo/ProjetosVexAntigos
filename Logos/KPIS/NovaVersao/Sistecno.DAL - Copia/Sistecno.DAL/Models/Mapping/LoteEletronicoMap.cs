using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class LoteEletronicoMap : EntityTypeConfiguration<LoteEletronico>
    {
        public LoteEletronicoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdLoteEletronico);

            // Properties
            this.Property(t => t.IdLoteEletronico)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(60);

            this.Property(t => t.Recibo)
                .HasMaxLength(50);

            this.Property(t => t.CStatus)
                .HasMaxLength(4);

            this.Property(t => t.Status)
                .HasMaxLength(200);

            this.Property(t => t.NomeDaMaquina)
                .HasMaxLength(50);

            this.Property(t => t.EnvLot)
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("LoteEletronico");
            this.Property(t => t.IdLoteEletronico).HasColumnName("IdLoteEletronico");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Recibo).HasColumnName("Recibo");
            this.Property(t => t.CStatus).HasColumnName("CStatus");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Xml).HasColumnName("Xml");
            this.Property(t => t.LoteGerado).HasColumnName("LoteGerado");
            this.Property(t => t.LoteEnviadoAoSefaz).HasColumnName("LoteEnviadoAoSefaz");
            this.Property(t => t.ConsultaDoRecibo).HasColumnName("ConsultaDoRecibo");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.NomeDaMaquina).HasColumnName("NomeDaMaquina");
            this.Property(t => t.EnvLot).HasColumnName("EnvLot");
            this.Property(t => t.EnvLotXML).HasColumnName("EnvLotXML");
        }
    }
}
