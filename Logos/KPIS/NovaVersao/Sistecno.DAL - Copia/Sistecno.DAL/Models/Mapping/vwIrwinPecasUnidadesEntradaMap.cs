using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class vwIrwinPecasUnidadesEntradaMap : EntityTypeConfiguration<vwIrwinPecasUnidadesEntrada>
    {
        public vwIrwinPecasUnidadesEntradaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMovimentacaoItem);

            // Properties
            this.Property(t => t.IDMovimentacaoItem)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwIrwinPecasUnidadesEntrada");
            this.Property(t => t.DataDeEmissao).HasColumnName("DataDeEmissao");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.IDGrupoDeProduto).HasColumnName("IDGrupoDeProduto");
            this.Property(t => t.IDMovimentacaoItem).HasColumnName("IDMovimentacaoItem");
            this.Property(t => t.QUANTIDADEUNIDADEESTOQUE).HasColumnName("QUANTIDADEUNIDADEESTOQUE");
            this.Property(t => t.FATOR).HasColumnName("FATOR");
        }
    }
}
