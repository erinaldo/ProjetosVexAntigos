using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class BancoMap : EntityTypeConfiguration<Banco>
    {
        public BancoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDBanco);

            // Properties
            this.Property(t => t.IDBanco)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(60);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Banco");
            this.Property(t => t.IDBanco).HasColumnName("IDBanco");
            this.Property(t => t.Digito).HasColumnName("Digito");
            this.Property(t => t.Codigo).HasColumnName("Codigo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.LogoTipo).HasColumnName("LogoTipo");
            this.Property(t => t.IDLeiauteCheque).HasColumnName("IDLeiauteCheque");
            this.Property(t => t.IDLeiauteBoleto).HasColumnName("IDLeiauteBoleto");
            this.Property(t => t.IDPais).HasColumnName("IDPais");
            this.Property(t => t.DiasRetencao).HasColumnName("DiasRetencao");
            this.Property(t => t.TaxaCobrancaSimples).HasColumnName("TaxaCobrancaSimples");
        }
    }
}
