using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class CidadeMap : EntityTypeConfiguration<Cidade>
    {
        public CidadeMap()
        {
            // Primary Key
            this.HasKey(t => t.IDCidade);

            // Properties
            this.Property(t => t.IDCidade)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.Cep)
                .HasMaxLength(8);

            this.Property(t => t.Tipo)
                .HasMaxLength(10);

            this.Property(t => t.CodificarPor)
                .HasMaxLength(10);

            this.Property(t => t.Regiao)
                .HasMaxLength(15);

            this.Property(t => t.CodigoDoIBGE)
                .HasMaxLength(7);

            this.Property(t => t.CodigoDipam)
                .HasMaxLength(4);

            this.Property(t => t.NomeCidadeIbge)
                .HasMaxLength(80);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Cidade");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.IDEstado).HasColumnName("IDEstado");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Cep).HasColumnName("Cep");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.CodificarPor).HasColumnName("CodificarPor");
            this.Property(t => t.Regiao).HasColumnName("Regiao");
            this.Property(t => t.PrazoDeEntregaPadrao).HasColumnName("PrazoDeEntregaPadrao");
            this.Property(t => t.CodigoDoIBGE).HasColumnName("CodigoDoIBGE");
            this.Property(t => t.CodigoDipam).HasColumnName("CodigoDipam");
            this.Property(t => t.AliquotaDeIss).HasColumnName("AliquotaDeIss");
            this.Property(t => t.NomeCidadeIbge).HasColumnName("NomeCidadeIbge");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.Conferido).HasColumnName("Conferido");

            // Relationships
            this.HasRequired(t => t.Estado)
                .WithMany(t => t.Cidades)
                .HasForeignKey(d => d.IDEstado);

        }
    }
}
