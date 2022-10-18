using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class SobraMap : EntityTypeConfiguration<Sobra>
    {
        public SobraMap()
        {
            // Primary Key
            this.HasKey(t => t.IdSobra);

            // Properties
            this.Property(t => t.IdSobra)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NomeDoColaborador)
                .HasMaxLength(50);

            this.Property(t => t.NumeroNotaFiscal)
                .HasMaxLength(50);

            this.Property(t => t.PreNotaFiscal)
                .HasMaxLength(50);

            this.Property(t => t.TipoDeVolume)
                .HasMaxLength(50);

            this.Property(t => t.DescricaoDoVolume)
                .HasMaxLength(50);

            this.Property(t => t.NomeMotoristaEmbarque)
                .HasMaxLength(100);

            this.Property(t => t.PlacaCarretaEmbarque)
                .HasMaxLength(8);

            this.Property(t => t.RotaDoVeiculo)
                .HasMaxLength(100);

            this.Property(t => t.Finalizado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NumeroRoteiro)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sobra");
            this.Property(t => t.IdSobra).HasColumnName("IdSobra");
            this.Property(t => t.Data).HasColumnName("Data");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.NomeDoColaborador).HasColumnName("NomeDoColaborador");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.NumeroNotaFiscal).HasColumnName("NumeroNotaFiscal");
            this.Property(t => t.PreNotaFiscal).HasColumnName("PreNotaFiscal");
            this.Property(t => t.TipoDeVolume).HasColumnName("TipoDeVolume");
            this.Property(t => t.DescricaoDoVolume).HasColumnName("DescricaoDoVolume");
            this.Property(t => t.Quantidade).HasColumnName("Quantidade");
            this.Property(t => t.DataDeEmbarqueDoVolume).HasColumnName("DataDeEmbarqueDoVolume");
            this.Property(t => t.NomeMotoristaEmbarque).HasColumnName("NomeMotoristaEmbarque");
            this.Property(t => t.PlacaCarretaEmbarque).HasColumnName("PlacaCarretaEmbarque");
            this.Property(t => t.RotaDoVeiculo).HasColumnName("RotaDoVeiculo");
            this.Property(t => t.Finalizado).HasColumnName("Finalizado");
            this.Property(t => t.DataFinalizacao).HasColumnName("DataFinalizacao");
            this.Property(t => t.IdUsuarioBaizado).HasColumnName("IdUsuarioBaizado");
            this.Property(t => t.IdFilialDestino).HasColumnName("IdFilialDestino");
            this.Property(t => t.NumeroRoteiro).HasColumnName("NumeroRoteiro");

            // Relationships
            this.HasOptional(t => t.Cliente)
                .WithMany(t => t.Sobras)
                .HasForeignKey(d => d.IdCliente);
            this.HasOptional(t => t.Filial)
                .WithMany(t => t.Sobras)
                .HasForeignKey(d => d.IdFilial);

        }
    }
}
