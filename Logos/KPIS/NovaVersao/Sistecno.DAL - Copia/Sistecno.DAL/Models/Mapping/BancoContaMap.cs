using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BancoContaMap : EntityTypeConfiguration<BancoConta>
    {
        public BancoContaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBancoConta);

            // Properties
            this.Property(t => t.IDBancoConta)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BancoCarteira)
                .HasMaxLength(8);

            this.Property(t => t.Agencia)
                .HasMaxLength(5);

            this.Property(t => t.AgenciaDigito)
                .HasMaxLength(2);

            this.Property(t => t.Conta)
                .HasMaxLength(10);

            this.Property(t => t.ContaDigito)
                .HasMaxLength(2);

            this.Property(t => t.CodigoDaEmpresaNoBanco)
                .HasMaxLength(20);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Gerente)
                .HasMaxLength(60);

            this.Property(t => t.Telefone)
                .HasMaxLength(20);

            this.Property(t => t.Celular)
                .HasMaxLength(20);

            this.Property(t => t.ParticipaDoFluxoDeCaixa)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.EmiteCheque)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.AgenciaNome)
                .HasMaxLength(60);

            this.Property(t => t.ContaTipo)
                .HasMaxLength(8);

            this.Property(t => t.Titular)
                .HasMaxLength(60);

            this.Property(t => t.DDD)
                .HasMaxLength(4);

            this.Property(t => t.EMail)
                .HasMaxLength(80);

            // Table & Column Mappings
            this.ToTable("BancoConta");
            this.Property(t => t.IDBancoConta).HasColumnName("IDBancoConta");
            this.Property(t => t.IDBanco).HasColumnName("IDBanco");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.IdEmpresa).HasColumnName("IdEmpresa");
            this.Property(t => t.BancoCarteira).HasColumnName("BancoCarteira");
            this.Property(t => t.Agencia).HasColumnName("Agencia");
            this.Property(t => t.AgenciaDigito).HasColumnName("AgenciaDigito");
            this.Property(t => t.Conta).HasColumnName("Conta");
            this.Property(t => t.ContaDigito).HasColumnName("ContaDigito");
            this.Property(t => t.CodigoDaEmpresaNoBanco).HasColumnName("CodigoDaEmpresaNoBanco");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.LimiteDeCredito).HasColumnName("LimiteDeCredito");
            this.Property(t => t.LimiteDataDeVencimento).HasColumnName("LimiteDataDeVencimento");
            this.Property(t => t.Gerente).HasColumnName("Gerente");
            this.Property(t => t.Telefone).HasColumnName("Telefone");
            this.Property(t => t.Celular).HasColumnName("Celular");
            this.Property(t => t.ParticipaDoFluxoDeCaixa).HasColumnName("ParticipaDoFluxoDeCaixa");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.IDContaContabil).HasColumnName("IDContaContabil");
            this.Property(t => t.IDCentroDeCusto).HasColumnName("IDCentroDeCusto");
            this.Property(t => t.SaldoDinheiro).HasColumnName("SaldoDinheiro");
            this.Property(t => t.SaldoCheque).HasColumnName("SaldoCheque");
            this.Property(t => t.SaldoCartao).HasColumnName("SaldoCartao");
            this.Property(t => t.EmiteCheque).HasColumnName("EmiteCheque");
            this.Property(t => t.AgenciaNome).HasColumnName("AgenciaNome");
            this.Property(t => t.ContaTipo).HasColumnName("ContaTipo");
            this.Property(t => t.Titular).HasColumnName("Titular");
            this.Property(t => t.DDD).HasColumnName("DDD");
            this.Property(t => t.EMail).HasColumnName("EMail");
            this.Property(t => t.Saldo).HasColumnName("Saldo");
            this.Property(t => t.ProximoNumeroDeRemessa).HasColumnName("ProximoNumeroDeRemessa");
            this.Property(t => t.LimiteTed).HasColumnName("LimiteTed");
            this.Property(t => t.LimiteDoc).HasColumnName("LimiteDoc");
            this.Property(t => t.ConvenioPagFor).HasColumnName("ConvenioPagFor");
            this.Property(t => t.ProximoNumeroPagFor).HasColumnName("ProximoNumeroPagFor");

            // Relationships
            this.HasOptional(t => t.Banco)
                .WithMany(t => t.BancoContas)
                .HasForeignKey(d => d.IDBanco);
            this.HasOptional(t => t.CentroDeCusto)
                .WithMany(t => t.BancoContas)
                .HasForeignKey(d => d.IDCentroDeCusto);
            this.HasOptional(t => t.ContaContabil)
                .WithMany(t => t.BancoContas)
                .HasForeignKey(d => d.IDContaContabil);
            this.HasRequired(t => t.Empresa)
                .WithMany(t => t.BancoContas)
                .HasForeignKey(d => d.IdEmpresa);
            this.HasOptional(t => t.Filial)
                .WithMany(t => t.BancoContas)
                .HasForeignKey(d => d.IdFilial);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.BancoContas)
                .HasForeignKey(d => d.IDUsuario);

        }
    }
}
