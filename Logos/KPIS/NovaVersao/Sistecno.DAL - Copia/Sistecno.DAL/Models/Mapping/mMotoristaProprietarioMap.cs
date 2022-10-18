using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class mMotoristaProprietarioMap : EntityTypeConfiguration<mMotoristaProprietario>
    {
        public mMotoristaProprietarioMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CnpjCpf, t.ProprietarioMotoristaAmbos, t.Cadastro, t.Nome });

            // Properties
            this.Property(t => t.CnpjCpf)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProprietarioMotoristaAmbos)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.TipoDeEndereco)
                .HasMaxLength(26);

            this.Property(t => t.Endereco)
                .HasMaxLength(60);

            this.Property(t => t.Numero)
                .HasMaxLength(40);

            this.Property(t => t.Bairro)
                .HasMaxLength(60);

            this.Property(t => t.Cidade)
                .HasMaxLength(60);

            this.Property(t => t.Uf)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.Cep)
                .IsFixedLength()
                .HasMaxLength(9);

            this.Property(t => t.Telefone)
                .HasMaxLength(60);

            this.Property(t => t.Celular)
                .HasMaxLength(60);

            this.Property(t => t.Cnh)
                .HasMaxLength(60);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Rg)
                .HasMaxLength(30);

            this.Property(t => t.CnhCategoria)
                .HasMaxLength(5);

            this.Property(t => t.RgOrgaoEmissor)
                .HasMaxLength(10);

            this.Property(t => t.RgUf)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.PIS)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.Aposentado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Acidente)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Assalto)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.INSS)
                .HasMaxLength(20);

            this.Property(t => t.INSSRecolhe)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.SestSenatRecolhe)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.IRRFRecolhe)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ISSRecolhe)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ContaBanco)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.ContaAgencia)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.ContaNumero)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ContaTipo)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.AceitaCreditoEmConta)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Agregado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.FilialResponsavel)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.ContaTitular)
                .HasMaxLength(60);

            this.Property(t => t.EstadoCivil)
                .HasMaxLength(20);

            this.Property(t => t.FiliacaoPai)
                .HasMaxLength(50);

            this.Property(t => t.FiliacaoMae)
                .HasMaxLength(50);

            this.Property(t => t.Referencia)
                .HasMaxLength(40);

            this.Property(t => t.Liberado)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("mMotoristaProprietario");
            this.Property(t => t.CnpjCpf).HasColumnName("CnpjCpf");
            this.Property(t => t.ProprietarioMotoristaAmbos).HasColumnName("ProprietarioMotoristaAmbos");
            this.Property(t => t.Cadastro).HasColumnName("Cadastro");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.TipoDeEndereco).HasColumnName("TipoDeEndereco");
            this.Property(t => t.Endereco).HasColumnName("Endereco");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Bairro).HasColumnName("Bairro");
            this.Property(t => t.Cidade).HasColumnName("Cidade");
            this.Property(t => t.Uf).HasColumnName("Uf");
            this.Property(t => t.Cep).HasColumnName("Cep");
            this.Property(t => t.Telefone).HasColumnName("Telefone");
            this.Property(t => t.Celular).HasColumnName("Celular");
            this.Property(t => t.Cnh).HasColumnName("Cnh");
            this.Property(t => t.ValidadeDaCnh).HasColumnName("ValidadeDaCnh");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Rg).HasColumnName("Rg");
            this.Property(t => t.CnhEmissao).HasColumnName("CnhEmissao");
            this.Property(t => t.CnhCategoria).HasColumnName("CnhCategoria");
            this.Property(t => t.RgOrgaoEmissor).HasColumnName("RgOrgaoEmissor");
            this.Property(t => t.RgUf).HasColumnName("RgUf");
            this.Property(t => t.PIS).HasColumnName("PIS");
            this.Property(t => t.Dependentes).HasColumnName("Dependentes");
            this.Property(t => t.Aposentado).HasColumnName("Aposentado");
            this.Property(t => t.Acidente).HasColumnName("Acidente");
            this.Property(t => t.AcidenteQuantidade).HasColumnName("AcidenteQuantidade");
            this.Property(t => t.Assalto).HasColumnName("Assalto");
            this.Property(t => t.AssaltoQuantidade).HasColumnName("AssaltoQuantidade");
            this.Property(t => t.INSS).HasColumnName("INSS");
            this.Property(t => t.INSSRecolhe).HasColumnName("INSSRecolhe");
            this.Property(t => t.SestSenatRecolhe).HasColumnName("SestSenatRecolhe");
            this.Property(t => t.IRRFRecolhe).HasColumnName("IRRFRecolhe");
            this.Property(t => t.ISSRecolhe).HasColumnName("ISSRecolhe");
            this.Property(t => t.ISSAliquota).HasColumnName("ISSAliquota");
            this.Property(t => t.ContaBanco).HasColumnName("ContaBanco");
            this.Property(t => t.ContaAgencia).HasColumnName("ContaAgencia");
            this.Property(t => t.ContaNumero).HasColumnName("ContaNumero");
            this.Property(t => t.ContaTipo).HasColumnName("ContaTipo");
            this.Property(t => t.AceitaCreditoEmConta).HasColumnName("AceitaCreditoEmConta");
            this.Property(t => t.Agregado).HasColumnName("Agregado");
            this.Property(t => t.DataNascimento).HasColumnName("DataNascimento");
            this.Property(t => t.ValorAposentadoria).HasColumnName("ValorAposentadoria");
            this.Property(t => t.SestSenatAliquota).HasColumnName("SestSenatAliquota");
            this.Property(t => t.FilialResponsavel).HasColumnName("FilialResponsavel");
            this.Property(t => t.ContaTitular).HasColumnName("ContaTitular");
            this.Property(t => t.VencimentoDaLiberacao).HasColumnName("VencimentoDaLiberacao");
            this.Property(t => t.EstadoCivil).HasColumnName("EstadoCivil");
            this.Property(t => t.FiliacaoPai).HasColumnName("FiliacaoPai");
            this.Property(t => t.FiliacaoMae).HasColumnName("FiliacaoMae");
            this.Property(t => t.Referencia).HasColumnName("Referencia");
            this.Property(t => t.DataPrimeiraHabilitacao).HasColumnName("DataPrimeiraHabilitacao");
            this.Property(t => t.ValorLiberadoCarregamento).HasColumnName("ValorLiberadoCarregamento");
            this.Property(t => t.Liberado).HasColumnName("Liberado");
        }
    }
}
