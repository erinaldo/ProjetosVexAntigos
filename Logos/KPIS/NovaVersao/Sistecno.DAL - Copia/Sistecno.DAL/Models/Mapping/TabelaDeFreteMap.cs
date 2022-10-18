using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TabelaDeFreteMap : EntityTypeConfiguration<TabelaDeFrete>
    {
        public TabelaDeFreteMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTabelaDeFrete);

            // Properties
            this.Property(t => t.IDTabelaDeFrete)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(200);

            this.Property(t => t.TipoDeTabela)
                .HasMaxLength(50);

            this.Property(t => t.Ativo)
                .HasMaxLength(3);

            this.Property(t => t.TipoDeCalculo)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("TabelaDeFrete");
            this.Property(t => t.IDTabelaDeFrete).HasColumnName("IDTabelaDeFrete");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.TipoDeTabela).HasColumnName("TipoDeTabela");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.TipoDeCalculo).HasColumnName("TipoDeCalculo");
        }
    }
}
