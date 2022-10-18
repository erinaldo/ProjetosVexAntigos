using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class VeiculoMap : EntityTypeConfiguration<Veiculo>
    {
        public VeiculoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDVeiculo);

            // Properties
            this.Property(t => t.IDVeiculo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Placa)
                .HasMaxLength(10);

            this.Property(t => t.Renavam)
                .HasMaxLength(20);

            this.Property(t => t.Chassi)
                .HasMaxLength(50);

            this.Property(t => t.Cor)
                .HasMaxLength(20);

            this.Property(t => t.CategoriasDeCNHPermitidas)
                .HasMaxLength(30);

            this.Property(t => t.Antt)
                .HasMaxLength(20);

            this.Property(t => t.NumeroSerieEquipamento)
                .HasMaxLength(40);

            this.Property(t => t.TipoDeCarroceria)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Veiculo");
            this.Property(t => t.IDVeiculo).HasColumnName("IDVeiculo");
            this.Property(t => t.IDVeiculoModelo).HasColumnName("IDVeiculoModelo");
            this.Property(t => t.IDVeiculoTipo).HasColumnName("IDVeiculoTipo");
            this.Property(t => t.IDVeiculoRastreador).HasColumnName("IDVeiculoRastreador");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.IDProprietario).HasColumnName("IDProprietario");
            this.Property(t => t.IDMotorista).HasColumnName("IDMotorista");
            this.Property(t => t.IDCadastroTitular).HasColumnName("IDCadastroTitular");
            this.Property(t => t.Cadastro).HasColumnName("Cadastro");
            this.Property(t => t.Placa).HasColumnName("Placa");
            this.Property(t => t.Renavam).HasColumnName("Renavam");
            this.Property(t => t.Chassi).HasColumnName("Chassi");
            this.Property(t => t.Ano).HasColumnName("Ano");
            this.Property(t => t.Cor).HasColumnName("Cor");
            this.Property(t => t.CapacidadeDeCargaKG).HasColumnName("CapacidadeDeCargaKG");
            this.Property(t => t.CapacidadeDeCargaM3).HasColumnName("CapacidadeDeCargaM3");
            this.Property(t => t.QuatidadeDeEixos).HasColumnName("QuatidadeDeEixos");
            this.Property(t => t.CategoriasDeCNHPermitidas).HasColumnName("CategoriasDeCNHPermitidas");
            this.Property(t => t.Antt).HasColumnName("Antt");
            this.Property(t => t.NumeroSerieEquipamento).HasColumnName("NumeroSerieEquipamento");
            this.Property(t => t.AnttVencimento).HasColumnName("AnttVencimento");
            this.Property(t => t.DataDeLicenciamento).HasColumnName("DataDeLicenciamento");
            this.Property(t => t.AnoModelo).HasColumnName("AnoModelo");
            this.Property(t => t.TipoDeCarroceria).HasColumnName("TipoDeCarroceria");
            this.Property(t => t.ConfirmadoPara).HasColumnName("ConfirmadoPara");
            this.Property(t => t.ConfirmadoPor).HasColumnName("ConfirmadoPor");
            this.Property(t => t.IdLicenciamentoMes).HasColumnName("IdLicenciamentoMes");

            // Relationships
            this.HasOptional(t => t.Cadastro1)
                .WithMany(t => t.Veiculoes)
                .HasForeignKey(d => d.IDCadastroTitular);
            this.HasOptional(t => t.Cidade)
                .WithMany(t => t.Veiculoes)
                .HasForeignKey(d => d.IDCidade);
            this.HasOptional(t => t.Motorista)
                .WithMany(t => t.Veiculoes)
                .HasForeignKey(d => d.IDMotorista);
            this.HasOptional(t => t.Proprietario)
                .WithMany(t => t.Veiculoes)
                .HasForeignKey(d => d.IDProprietario);
            this.HasOptional(t => t.VeiculoModelo)
                .WithMany(t => t.Veiculoes)
                .HasForeignKey(d => d.IDVeiculoModelo);
            this.HasOptional(t => t.VeiculoRastreador)
                .WithMany(t => t.Veiculoes)
                .HasForeignKey(d => d.IDVeiculoRastreador);
            this.HasOptional(t => t.VeiculoTipo)
                .WithMany(t => t.Veiculoes)
                .HasForeignKey(d => d.IDVeiculoTipo);

        }
    }
}
