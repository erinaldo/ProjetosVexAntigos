using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BancoContaBloquetoMap : EntityTypeConfiguration<BancoContaBloqueto>
    {
        public BancoContaBloquetoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBancoContaBloqueto);

            // Properties
            this.Property(t => t.IDBancoContaBloqueto)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Aceite)
                .HasMaxLength(6);

            this.Property(t => t.CodigoDoCedente)
                .HasMaxLength(30);

            this.Property(t => t.DigitoDoCedente)
                .HasMaxLength(10);

            this.Property(t => t.Convenio)
                .HasMaxLength(30);

            this.Property(t => t.NossoNumero)
                .HasMaxLength(30);

            this.Property(t => t.InstrucaoLivre01)
                .HasMaxLength(100);

            this.Property(t => t.InstrucaoLivre02)
                .HasMaxLength(100);

            this.Property(t => t.InstrucaoLivre03)
                .HasMaxLength(100);

            this.Property(t => t.InstrucaoLivre04)
                .HasMaxLength(100);

            this.Property(t => t.InstrucaoLivre05)
                .HasMaxLength(100);

            this.Property(t => t.TipoImpressao)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.TipoDeBloqueto)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("BancoContaBloqueto");
            this.Property(t => t.IDBancoContaBloqueto).HasColumnName("IDBancoContaBloqueto");
            this.Property(t => t.IDBancoConta).HasColumnName("IDBancoConta");
            this.Property(t => t.Sequencia).HasColumnName("Sequencia");
            this.Property(t => t.IDBancoCarteira).HasColumnName("IDBancoCarteira");
            this.Property(t => t.IDBancoEspecie).HasColumnName("IDBancoEspecie");
            this.Property(t => t.Aceite).HasColumnName("Aceite");
            this.Property(t => t.CodigoDoCedente).HasColumnName("CodigoDoCedente");
            this.Property(t => t.DigitoDoCedente).HasColumnName("DigitoDoCedente");
            this.Property(t => t.Convenio).HasColumnName("Convenio");
            this.Property(t => t.NossoNumero).HasColumnName("NossoNumero");
            this.Property(t => t.IDBancoInstrucao).HasColumnName("IDBancoInstrucao");
            this.Property(t => t.InstrucaoLivre01).HasColumnName("InstrucaoLivre01");
            this.Property(t => t.InstrucaoLivre02).HasColumnName("InstrucaoLivre02");
            this.Property(t => t.InstrucaoLivre03).HasColumnName("InstrucaoLivre03");
            this.Property(t => t.InstrucaoLivre04).HasColumnName("InstrucaoLivre04");
            this.Property(t => t.InstrucaoLivre05).HasColumnName("InstrucaoLivre05");
            this.Property(t => t.TipoImpressao).HasColumnName("TipoImpressao");
            this.Property(t => t.TipoDeBloqueto).HasColumnName("TipoDeBloqueto");
            this.Property(t => t.IdBancoInstrucaoDeProtesto).HasColumnName("IdBancoInstrucaoDeProtesto");
            this.Property(t => t.Disponibilidade).HasColumnName("Disponibilidade");

            // Relationships
            this.HasOptional(t => t.BancoCarteira)
                .WithMany(t => t.BancoContaBloquetoes)
                .HasForeignKey(d => d.IDBancoCarteira);
            this.HasRequired(t => t.BancoConta)
                .WithMany(t => t.BancoContaBloquetoes)
                .HasForeignKey(d => d.IDBancoConta);
            this.HasRequired(t => t.BancoContaBloqueto2)
                .WithOptional(t => t.BancoContaBloqueto1);
            this.HasRequired(t => t.BancoContaBloqueto3)
                .WithOptional(t => t.BancoContaBloqueto11);
            this.HasRequired(t => t.BancoContaBloqueto4)
                .WithOptional(t => t.BancoContaBloqueto12);
            this.HasOptional(t => t.BancoEspecie)
                .WithMany(t => t.BancoContaBloquetoes)
                .HasForeignKey(d => d.IDBancoEspecie);
            this.HasOptional(t => t.BancoInstrucao)
                .WithMany(t => t.BancoContaBloquetoes)
                .HasForeignKey(d => d.IDBancoInstrucao);
            this.HasOptional(t => t.BancoInstrucaoDeProtesto)
                .WithMany(t => t.BancoContaBloquetoes)
                .HasForeignKey(d => d.IdBancoInstrucaoDeProtesto);

        }
    }
}
