using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class vwLiberacaoDePedidos_P1Map : EntityTypeConfiguration<vwLiberacaoDePedidos_P1>
    {
        public vwLiberacaoDePedidos_P1Map()
        {
            // Primary Key
            this.HasKey(t => new { t.IDDOCUMENTO, t.IDFILIAL, t.IDCLIENTE, t.FILIAL, t.CnpjCpf });

            // Properties
            this.Property(t => t.TIPODEDOCUMENTO)
                .HasMaxLength(20);

            this.Property(t => t.IDDOCUMENTO)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDFILIAL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.IDCLIENTE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RAZAOSOCIALNOME)
                .HasMaxLength(30);

            this.Property(t => t.FILIAL)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SITUACAO_DF)
                .HasMaxLength(30);

            this.Property(t => t.CnpjCpf)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.EntradaSaida)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("vwLiberacaoDePedidos_P1");
            this.Property(t => t.TIPODEDOCUMENTO).HasColumnName("TIPODEDOCUMENTO");
            this.Property(t => t.IDDOCUMENTO).HasColumnName("IDDOCUMENTO");
            this.Property(t => t.IDFILIAL).HasColumnName("IDFILIAL");
            this.Property(t => t.IDCLIENTE).HasColumnName("IDCLIENTE");
            this.Property(t => t.RAZAOSOCIALNOME).HasColumnName("RAZAOSOCIALNOME");
            this.Property(t => t.DATADEEMISSAO).HasColumnName("DATADEEMISSAO");
            this.Property(t => t.FILIAL).HasColumnName("FILIAL");
            this.Property(t => t.SITUACAO_DF).HasColumnName("SITUACAO_DF");
            this.Property(t => t.DATA_DF).HasColumnName("DATA_DF");
            this.Property(t => t.CnpjCpf).HasColumnName("CnpjCpf");
            this.Property(t => t.EntradaSaida).HasColumnName("EntradaSaida");
        }
    }
}
