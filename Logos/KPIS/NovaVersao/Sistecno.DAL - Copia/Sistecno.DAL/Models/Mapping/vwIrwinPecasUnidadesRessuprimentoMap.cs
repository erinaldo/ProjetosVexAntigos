using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class vwIrwinPecasUnidadesRessuprimentoMap : EntityTypeConfiguration<vwIrwinPecasUnidadesRessuprimento>
    {
        public vwIrwinPecasUnidadesRessuprimentoMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMOVIMENTACAOITEM);

            // Properties
            this.Property(t => t.IDMOVIMENTACAOITEM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwIrwinPecasUnidadesRessuprimento");
            this.Property(t => t.DATADECADASTRO).HasColumnName("DATADECADASTRO");
            this.Property(t => t.IDGRUPODEPRODUTO).HasColumnName("IDGRUPODEPRODUTO");
            this.Property(t => t.IDMOVIMENTACAOITEM).HasColumnName("IDMOVIMENTACAOITEM");
            this.Property(t => t.QUANTIDADEUNIDADEESTOQUE).HasColumnName("QUANTIDADEUNIDADEESTOQUE");
            this.Property(t => t.FATOR).HasColumnName("FATOR");
        }
    }
}
