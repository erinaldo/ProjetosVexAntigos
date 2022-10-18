using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RPCIMap : EntityTypeConfiguration<RPCI>
    {
        public RPCIMap()
        {
            // Primary Key
            this.HasKey(t => t.IDRPCI);

            // Properties
            this.Property(t => t.IDRPCI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RPCIContrato)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.NumeroOriginal)
                .HasMaxLength(10);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Situacao)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("RPCI");
            this.Property(t => t.IDRPCI).HasColumnName("IDRPCI");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDCadastroTitular).HasColumnName("IDCadastroTitular");
            this.Property(t => t.RPCIContrato).HasColumnName("RPCIContrato");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.NumeroOriginal).HasColumnName("NumeroOriginal");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.DataDeEmissao).HasColumnName("DataDeEmissao");
            this.Property(t => t.BaseCalculoINSS).HasColumnName("BaseCalculoINSS");
            this.Property(t => t.AliquotaINSS).HasColumnName("AliquotaINSS");
            this.Property(t => t.BaseCalculoIRRF).HasColumnName("BaseCalculoIRRF");
            this.Property(t => t.AliquotaIRRF).HasColumnName("AliquotaIRRF");
            this.Property(t => t.BaseCalculoSestSenat).HasColumnName("BaseCalculoSestSenat");
            this.Property(t => t.AliquotaSestSenat).HasColumnName("AliquotaSestSenat");
            this.Property(t => t.CreditoValor).HasColumnName("CreditoValor");
            this.Property(t => t.CreditoAgenciamento).HasColumnName("CreditoAgenciamento");
            this.Property(t => t.CreditoPedagio).HasColumnName("CreditoPedagio");
            this.Property(t => t.CreditoCarga).HasColumnName("CreditoCarga");
            this.Property(t => t.CreditoDescarga).HasColumnName("CreditoDescarga");
            this.Property(t => t.CreditoDiaria).HasColumnName("CreditoDiaria");
            this.Property(t => t.CreditoColeta).HasColumnName("CreditoColeta");
            this.Property(t => t.CreditoEntrega).HasColumnName("CreditoEntrega");
            this.Property(t => t.CreditoAjudante).HasColumnName("CreditoAjudante");
            this.Property(t => t.CreditoAdicional).HasColumnName("CreditoAdicional");
            this.Property(t => t.CreditoOutros).HasColumnName("CreditoOutros");
            this.Property(t => t.DebitoINSS).HasColumnName("DebitoINSS");
            this.Property(t => t.DebitoSestSenat).HasColumnName("DebitoSestSenat");
            this.Property(t => t.DebitoIRRF).HasColumnName("DebitoIRRF");
            this.Property(t => t.DebitoSeguro).HasColumnName("DebitoSeguro");
            this.Property(t => t.DebitoOutros).HasColumnName("DebitoOutros");
            this.Property(t => t.DebitoAdiantamento).HasColumnName("DebitoAdiantamento");
            this.Property(t => t.SaldoAReceber).HasColumnName("SaldoAReceber");
            this.Property(t => t.Dependentes).HasColumnName("Dependentes");
            this.Property(t => t.DependentesDeducao).HasColumnName("DependentesDeducao");
            this.Property(t => t.ValorAcumuladoAnterior).HasColumnName("ValorAcumuladoAnterior");
            this.Property(t => t.IRRFAcumuladoAnterior).HasColumnName("IRRFAcumuladoAnterior");
            this.Property(t => t.INSSAcumuladoAnterior).HasColumnName("INSSAcumuladoAnterior");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.ValorPensaoAlimenticia).HasColumnName("ValorPensaoAlimenticia");
            this.Property(t => t.AliquotaISS).HasColumnName("AliquotaISS");
            this.Property(t => t.ValorISS).HasColumnName("ValorISS");
            this.Property(t => t.BaseCalculoIRRFAcumulado).HasColumnName("BaseCalculoIRRFAcumulado");

            // Relationships
            this.HasRequired(t => t.Cadastro)
                .WithMany(t => t.RPCIs)
                .HasForeignKey(d => d.IDCadastroTitular);
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.RPCIs)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.RPCIs)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
