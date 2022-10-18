using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class PreFaturaRogeMap : EntityTypeConfiguration<PreFaturaRoge>
    {
        public PreFaturaRogeMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPreFaturaRoge);

            // Properties
            this.Property(t => t.Chave)
                .IsRequired()
                .HasMaxLength(440);

            this.Property(t => t.Operacao)
                .HasMaxLength(10);

            this.Property(t => t.Situacao)
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("PreFaturaRoge");
            this.Property(t => t.IdPreFaturaRoge).HasColumnName("IdPreFaturaRoge");
            this.Property(t => t.ChavePlanilha).HasColumnName("ChavePlanilha");
            this.Property(t => t.IdFilial).HasColumnName("IdFilial");
            this.Property(t => t.Chave).HasColumnName("Chave");
            this.Property(t => t.Frete).HasColumnName("Frete");
            this.Property(t => t.IdDocumentoNF).HasColumnName("IdDocumentoNF");
            this.Property(t => t.IdDocumentoCte).HasColumnName("IdDocumentoCte");
            this.Property(t => t.IdLinhaPlanilhaRoge).HasColumnName("IdLinhaPlanilhaRoge");
            this.Property(t => t.IdDocumentoAguardandoCtrc).HasColumnName("IdDocumentoAguardandoCtrc");
            this.Property(t => t.IdCliente).HasColumnName("IdCliente");
            this.Property(t => t.IdTitulo).HasColumnName("IdTitulo");
            this.Property(t => t.Operacao).HasColumnName("Operacao");
            this.Property(t => t.Situacao).HasColumnName("Situacao");
            this.Property(t => t.IdCteBkp).HasColumnName("IdCteBkp");
            this.Property(t => t.DataHora).HasColumnName("DataHora");
        }
    }
}
